using PropertyGridHelpers.Attributes;
using PropertyGridHelpers.Enums;
using System;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Design;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Resources;

namespace PropertyGridHelpers.UIEditors
{
    /// <summary>
    /// Provides a <see cref="UITypeEditor"/> for editing enumeration values that have associated images.
    /// </summary>
    /// <remarks>
    /// This editor is designed to display an enumeration in a UI with each value optionally associated with an image.
    /// The editor can be customized to include additional functionality or presentation enhancements.
    /// </remarks>
    /// <example>
    /// To use this editor, apply the <see cref="ImageTextUIEditor"/> to an enum property:
    /// <code>
    /// [Editor(typeof(ImageTextUIEditor), typeof(UITypeEditor))]
    /// public TestEnum EnumWithImages { get; set; }
    /// </code>
    /// Ensure that the enum has descriptions or resources set up using the <see cref="EnumImageAttribute"/> to provide the images.
    /// </example>
    /// <seealso cref="UITypeEditor" />
    public partial class ImageTextUIEditor : UITypeEditor, IDisposable
    {
        #region Fields ^^^^^^^^^^^^^^^^

        /// <summary>
        /// The object is disposed
        /// </summary>
        private bool disposedValue;

        /// <summary>
        /// The enum type
        /// </summary>
        protected Type EnumType
        {
            get;
        }

        /// <summary>
        /// The path to the resources where the images are stored
        /// </summary>
        protected string ResourcePath
        {
            get; private set;
        }

        /// <summary>
        /// Gets the file extension.
        /// </summary>
        /// <value>
        /// The file extension.
        /// </value>
        protected string FileExtension
        {
            get; private set;
        }

        #endregion

        #region Constructors ^^^^^^^^^^

        /// <summary>
        /// Initializes a new instance of the <see cref="ImageTextUIEditor"/> class.
        /// </summary>
        /// <param name="type">Type of enum that is used in the process</param>
        public ImageTextUIEditor(Type type)
        {
            EnumType = type;
            ResourcePath = GetResourcePath(null, type);
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ImageTextUIEditor"/> class.
        /// </summary>
        /// <param name="type">Type of enum that is used in the process</param>
        /// <param name="ResourcePath">The path to the resources where the images are stored</param>
        public ImageTextUIEditor(Type type, string ResourcePath)
        {
            EnumType = type;
            this.ResourcePath = ResourcePath;
        }

        #endregion

        #region PaintValue Routines ^^^

        /// <summary>
        /// return that the editor will paint the items in the drop-down
        /// </summary>
        /// <param name="context">The Type Descriptor Context</param>
        public override bool GetPaintValueSupported(ITypeDescriptorContext context)
        {
            ResourcePath = GetResourcePath(context, EnumType);
            FileExtension = GetFileExtension(context);
            return true;
        }

        /// <summary>
        /// Paint the value in the drop-down list
        /// </summary>
        /// <param name="e">Paint Value Event Arguments</param>
        public override void PaintValue(PaintValueEventArgs e)
        {
#if NET5_0_OR_GREATER
            ArgumentNullException.ThrowIfNull(e);
#else
            if (e is null) throw new ArgumentNullException(nameof(e));
#endif
            var newImage = GetImageFromResource(e.Value, EnumType, ResourcePath, FileExtension, e.Bounds);
            if (newImage != null)
                e.Graphics.DrawImage(newImage, e.Bounds);
        }

        #endregion

        #region Static Methods ^^^^^^^^

        /// <summary>
        /// Gets the image from resource.
        /// </summary>
        /// <param name="Value">The value.</param>
        /// <param name="enumType">Type of the enum.</param>
        /// <param name="ResourcePath">The resource path.</param>
        /// <param name="fileExtension"></param>
        /// <returns></returns>
        /// <exception cref="System.InvalidOperationException">Resource file '{ResourceName}.resources' not found in assembly '{m}'. \n" +
        /// $"Available resources: {string.Join(", ", resourceNames)}
        /// or
        /// Resource {enumImage} is not a valid image or byte array.</exception>
        /// <exception cref="InvalidOperationException">Resource file not found in the assembly.
        /// or
        /// Resource is not a valid image or byte array.</exception>
        /// <param name="bounds">The bounds of the generated image.</param>
        public static Bitmap GetImageFromResource(
            object Value,
            Type enumType,
            string ResourcePath,
            string fileExtension,
            Rectangle bounds)
        {
#if NET5_0_OR_GREATER
            ArgumentNullException.ThrowIfNull(Value);
            ArgumentNullException.ThrowIfNull(enumType);
#else
            if (Value is null) throw new ArgumentNullException(nameof(Value));
            if (enumType is null) throw new ArgumentNullException(nameof(enumType));
#endif
            // get the EnumImageAttribute for the field
            var dna = EnumImageAttribute.Get((Enum)Value);

            if (dna != null)
            {
                var m = GetModuleName(Value);
                var ei = EnumImageAttribute.GetEnumImage((Enum)Value);
                Bitmap originalImage = null;
                string ResourceName;
                switch (dna.ImageLocation)
                {
                    case ImageLocation.Embedded:
                        ResourceName = $"{m}{(string.IsNullOrEmpty(ResourcePath) ? "" : $".{ResourcePath}")}";
                        originalImage = GetImageFromEmbeddedResource(Value, ei, ResourceName, fileExtension);
                        break;
                    case ImageLocation.Resource:
                        // Create a resource manager to access the resources
                        ResourceName = $"{m}{(string.IsNullOrEmpty(ResourcePath) ? "" : $".{ResourcePath}")}";
                        originalImage = GetImageFromResourceFile(Value, ei, ResourceName, fileExtension);
                        break;
                    case ImageLocation.File:
                        originalImage = GetImageFromFile(Value, ei, ResourcePath, fileExtension);
                        break;
                }

                if (originalImage == null) return null;

                // Create a new bitmap for the scaled image
                var scaledImage = new Bitmap(bounds.Width, bounds.Height);

                using (var g = Graphics.FromImage(scaledImage))
                {
                    g.Clear(Color.Transparent); // Optional: set background to transparent
                    g.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.HighQualityBicubic;

                    var ts = GetTargetSizes(originalImage, bounds);

                    // Draw the scaled image centered in the bounds
                    g.DrawImage(originalImage, ts.OffsetX, ts.OffsetY, ts.TargetWidth, ts.TargetHeight);
                }

                return scaledImage;
            }

            return null;
        }

        /// <summary>
        /// Gets the image from embedded resource.
        /// </summary>
        /// <param name="Value">The value.</param>
        /// <param name="ResourceItem">The resource entry to retrieve.</param>
        /// <param name="ResourcePath">Name of the resource.</param>
        /// <param name="fileExtension">The file extension.</param>
        /// <returns></returns>
        /// <exception cref="System.ArgumentNullException">Value</exception>
        /// <exception cref="System.ArgumentException">'{nameof(enumImage)}' cannot be null or empty. - enumImage
        /// or
        /// '{nameof(ResourceName)}' cannot be null or empty. - ResourceName</exception>
        public static Bitmap GetImageFromEmbeddedResource(
            object Value,
            string ResourceItem,
            string ResourcePath,
            string fileExtension)
        {
#if NET5_0_OR_GREATER
            ArgumentNullException.ThrowIfNull(Value);
#else
            if (Value is null) throw new ArgumentNullException(nameof(Value));
#endif
            if (string.IsNullOrEmpty(ResourceItem)) throw new ArgumentException($"'{nameof(ResourceItem)}' cannot be null or empty.", nameof(ResourceItem));
            if (string.IsNullOrEmpty(ResourcePath)) throw new ArgumentException($"'{nameof(ResourcePath)}' cannot be null or empty.", nameof(ResourcePath));
            ResourcePath = $"{ResourcePath}.{ResourceItem}";
            if (!string.IsNullOrEmpty(fileExtension))
                ResourcePath = $"{ResourcePath}.{fileExtension}";

            Bitmap newImage = null;

            using (var stream = Value.GetType().Assembly.GetManifestResourceStream(ResourcePath))
#if NET5_0_OR_GREATER
                if (stream is not null)
#else
                if (!(stream is null))
#endif
                    newImage = (Bitmap)Image.FromStream(stream);

            return newImage;
        }

        /// <summary>
        /// Gets the image from resource file.
        /// </summary>
        /// <param name="Value">The value.</param>
        /// <param name="ResourceItem">The resource entry to retrieve.</param>
        /// <param name="ResourcePath">Name of the resource.</param>
        /// <param name="fileExtension">The file extension.</param>
        /// <returns></returns>
        /// <exception cref="ArgumentNullException">Value</exception>
        /// <exception cref="ArgumentException">
        /// '{nameof(ResourceItem)}' cannot be null or empty. - ResourceItem
        /// or
        /// '{nameof(ResourcePath)}' cannot be null or empty. - ResourcePath
        /// </exception>
        /// <exception cref="InvalidOperationException">
        /// Resource file '{ResourcePath}.resources' not found in assembly '{m}'. \n" +
        ///                     $"Available resources: {string.Join(", ", resourceNames)}
        /// or
        /// Resource '{ResourcePath}.resources.{ResourceItem}' is not a valid image or byte array.
        /// </exception>
        /// <exception cref="InvalidOperationException">Resource file '{ResourceName}.resources' not found in assembly '{m}'. \n" +
        /// $"Available resources: {string.Join(", ", resourceNames)}
        /// or
        /// Resource {ResourceItem} is not a valid image or byte array.</exception>
        private static Bitmap GetImageFromResourceFile(
            object Value,
            string ResourceItem,
            string ResourcePath,
            string fileExtension)
        {
#if NET5_0_OR_GREATER
            ArgumentNullException.ThrowIfNull(Value);
#else
            if (Value is null) throw new ArgumentNullException(nameof(Value));
#endif
            if (string.IsNullOrEmpty(ResourceItem)) throw new ArgumentException($"'{nameof(ResourceItem)}' cannot be null or empty.", nameof(ResourceItem));
            if (string.IsNullOrEmpty(ResourcePath)) throw new ArgumentException($"'{nameof(ResourcePath)}' cannot be null or empty.", nameof(ResourcePath));

            // Check if the resource file exists in the assembly
            var resourceNames = Value.GetType().Assembly.GetManifestResourceNames();
            if (!resourceNames.Any(r => r.EndsWith($"{ResourcePath}.resources", StringComparison.CurrentCulture)))
            {
                var m = GetModuleName(Value);
                throw new InvalidOperationException(
                    $"Resource file '{ResourcePath}.resources' not found in assembly '{m}'. \n" +
                    $"Available resources: {string.Join(", ", resourceNames)}");
            }

            object resource;

            // Get the resource object
            if (LicenseManager.UsageMode == LicenseUsageMode.Designtime)
            {
                // If in design mode, use the ResourceManager to get the resource
                var rm = new ComponentResourceManager(Value.GetType());
                resource = rm.GetObject(ResourceItem + (string.IsNullOrEmpty(fileExtension) ? "" : "." + fileExtension), CultureInfo.CurrentCulture);
            }
            else
            {
                // If in runtime, use the ResourceManager to get the resource
                var rm = new ResourceManager(ResourcePath, Value.GetType().Assembly);
                resource = rm.GetObject(ResourceItem + (string.IsNullOrEmpty(fileExtension) ? "" : "." + fileExtension), CultureInfo.CurrentCulture);
            }

            Bitmap newImage = null;
            if (resource != null)
            {
                if (resource is Bitmap bitmap)
                    // If the resource is a Bitmap, use it directly
                    newImage = bitmap;
                else if (resource is byte[] byteArray)
                    // If the resource is a byte array, convert it to an image
                    using (var ms = new MemoryStream(byteArray))
                        newImage = new Bitmap(ms);
                else
                    throw new InvalidOperationException($"Resource '{ResourcePath}.resources.{ResourceItem}' is not a valid image or byte array.");
            }

            return newImage;
        }

        /// <summary>
        /// Gets the image from file.
        /// </summary>
        /// <param name="Value">The value.</param>
        /// <param name="ResourceItem">The resource entry to retrieve.</param>
        /// <param name="ResourcePath">The resource path.</param>
        /// <param name="fileExtension">The file extension.</param>
        /// <returns></returns>
        /// <exception cref="System.ArgumentNullException">Value</exception>
        /// <exception cref="System.ArgumentException">
        /// '{nameof(ResourceItem)}' cannot be null or empty. - ResourceItem
        /// or
        /// '{nameof(ResourcePath)}' cannot be null or empty. - ResourcePath
        /// </exception>
        /// <exception cref="ArgumentNullException">Value</exception>
        /// <exception cref="ArgumentException">'{nameof(ResourceItem)}' cannot be null or empty. - ResourceItem
        /// or
        /// '{nameof(ResourcePath)}' cannot be null or empty. - ResourcePath</exception>
        private static Bitmap GetImageFromFile(
            object Value,
            string ResourceItem,
            string ResourcePath,
            string fileExtension)
        {
#if NET5_0_OR_GREATER
            ArgumentNullException.ThrowIfNull(Value);
#else
            if (Value is null) throw new ArgumentNullException(nameof(Value));
#endif
            if (string.IsNullOrEmpty(ResourceItem)) throw new ArgumentException($"'{nameof(ResourceItem)}' cannot be null or empty.", nameof(ResourceItem));
            if (string.IsNullOrEmpty(ResourcePath)) throw new ArgumentException($"'{nameof(ResourcePath)}' cannot be null or empty.", nameof(ResourcePath));
            string assemblyPath;
            // Get the directory of the assembly containing _enumType
#pragma warning disable SYSLIB0012 // The class is obsolete
            var uri = new UriBuilder(Value.GetType().Assembly.CodeBase);
#pragma warning restore SYSLIB0012 // The class is obsolete
            assemblyPath = Path.GetDirectoryName(Uri.UnescapeDataString(uri.Path));
            // Construct the full file path
            if (!string.IsNullOrEmpty(ResourcePath))
                assemblyPath = Path.Combine(assemblyPath, ResourcePath);
            if (!string.IsNullOrEmpty(fileExtension))
                ResourceItem = $"{ResourceItem}.{fileExtension}";
            var ResourceName = Path.Combine(assemblyPath, ResourceItem);

            // Load the image from the file path
            var newImage = new Bitmap(ResourceName);

            return newImage;
        }

        /// <summary>
        /// Gets the name of the module.
        /// </summary>
        /// <param name="Value">The value.</param>
        /// <returns></returns>
        public static string GetModuleName(object Value)
        {
            // Get the name of the DLL or EXE where the reference object is declared
            var m = Value.GetType().Module.Name;
            // Remove the file extension from the name
#if NET5_0_OR_GREATER
            m = m[0..^4];
#else
            m = m.Substring(0, m.Length - 4);
#endif
            return m;
        }

        /// <summary>
        /// Gets the resource path.
        /// </summary>
        /// <param name="context">The context.</param>
        /// <param name="type">The type.</param>
        /// <returns></returns>
        public static string GetResourcePath(ITypeDescriptorContext context, Type type)
        {
            if (context?.Instance != null && context?.PropertyDescriptor != null)
            {
                // Check if the property has the DynamicPathSourceAttribute
                var propertyInfo = context.Instance.GetType().GetProperty(context.PropertyDescriptor.Name);
                if (Attribute.GetCustomAttribute(propertyInfo, typeof(DynamicPathSourceAttribute)) is DynamicPathSourceAttribute dynamicPathAttr)
                {
                    // Find the referenced property
                    var pathProperty = context?.Instance.GetType().GetProperty(dynamicPathAttr.PathPropertyName);
                    if (pathProperty != null && pathProperty.PropertyType == typeof(string))
                    {
                        // Return the value of the referenced property
                        return pathProperty.GetValue(context?.Instance, null) as string;
                    }
                }
            }

            if (context?.PropertyDescriptor != null)
            {
                // Retrieve the ResourcePath from the PropertyDescriptor's attributes
                if (context.PropertyDescriptor.Attributes[typeof(ResourcePathAttribute)] is ResourcePathAttribute attribute)
                    return attribute.ResourcePath;
            }

            var property = type.GetProperties()
                .FirstOrDefault(prop => prop.GetCustomAttributes(typeof(ResourcePathAttribute), false).Length > 0);

            if (property != null)
            {
                var attribute = property.GetCustomAttributes(typeof(ResourcePathAttribute), false)
                                        .FirstOrDefault() as ResourcePathAttribute;

                return attribute?.ResourcePath;
            }
            else
            {
                return "Properties.Resources";
            }
        }

        /// <summary>
        /// Gets the file extension.
        /// </summary>
        /// <param name="context">The context.</param>
        /// <returns></returns>
        public static string GetFileExtension(ITypeDescriptorContext context)
        {
            if (context != null)
            {
                var FileExtensionAttr = FileExtensionAttribute.Get(context);
                if (FileExtensionAttr != null)
                {
                    // Find the referenced property
                    var fileExtensionProperty = GetRequiredProperty(context.Instance, FileExtensionAttr.PropertyName);
                    if (fileExtensionProperty != null && fileExtensionProperty.PropertyType == typeof(string))
                    {
                        // Return the value of the referenced property
                        return fileExtensionProperty.GetValue(context.Instance, null) as string;
                    }
                    else if (fileExtensionProperty != null && fileExtensionProperty.PropertyType.IsEnum)
                    {
                        // Check if the enum value has an EnumTextAttribute
#if NET6_0_OR_GREATER
                        if (fileExtensionProperty.GetValue(context.Instance, null) is not Enum extension)
                            return string.Empty;
#else
                        if (!(fileExtensionProperty.GetValue(context.Instance, null) is Enum extension))
                            return string.Empty;
#endif
                        var enumField = extension.GetType().GetField(extension.ToString());
                        if (enumField != null)
                        {
                            var enumTextAttr = enumField.GetCustomAttributes(typeof(EnumTextAttribute), false) as EnumTextAttribute[];
                            if (enumTextAttr.Length > 0)
                                return enumTextAttr[0].EnumText; // Return custom text
                        }
                        // Return the value of the referenced property
                        return (string.IsNullOrEmpty(extension.ToString()) ||
                                string.Equals(extension.ToString(), "None", StringComparison.OrdinalIgnoreCase))
                            ? string.Empty
                            : extension.ToString();
                    }

                }
            }

            return string.Empty;
        }

        /// <summary>
        /// Gets the required public property.
        /// </summary>
        /// <param name="instance">The instance.</param>
        /// <param name="propertyName">Name of the property.</param>
        /// <returns>
        /// The <see cref="PropertyInfo"/> of the property if it exists and is public.
        /// </returns>
        /// <exception cref="InvalidOperationException">
        /// Thrown if the property is not found or is not public.
        /// </exception>
        private static PropertyInfo GetRequiredProperty(object instance, string propertyName)
        {
            var property = instance.GetType().GetProperty(propertyName, BindingFlags.Instance |
                                                                        BindingFlags.Public |
                                                                        BindingFlags.NonPublic);
            if (property == null)
            {
                throw new InvalidOperationException(
                    $"Property '{propertyName}' not found on type '{instance.GetType()}'.");
            }

#if NET35
            if (property.GetGetMethod() == null)
#else
            if (!property.GetMethod.IsPublic)
#endif
            {
                throw new InvalidOperationException(
                    $"Property '{propertyName}' on type '{instance.GetType()}' must be public.");
            }

            return property;
        }

        /// <summary>
        /// Gets the target sizes.
        /// </summary>
        /// <param name="originalImage">The original image.</param>
        /// <param name="bounds">The bounds.</param>
        /// <returns></returns>
        public static TargetSizes GetTargetSizes(
            Bitmap originalImage,
            Rectangle bounds)
        {
            var ts = new TargetSizes();
            var aspectRatio = (double)originalImage.Width / originalImage.Height;

            if (bounds.Width / (double)bounds.Height > aspectRatio)
            {
                ts.TargetHeight = bounds.Height;
                ts.TargetWidth = (int)(ts.TargetHeight * aspectRatio);
            }
            else
            {
                ts.TargetWidth = bounds.Width;
                ts.TargetHeight = (int)(ts.TargetWidth / aspectRatio);
            }

            ts.OffsetX = (bounds.Width - ts.TargetWidth) / 2;
            ts.OffsetY = (bounds.Height - ts.TargetHeight) / 2;
            return ts;
        }

        /// <summary>
        /// Target sizes for the image 
        /// </summary>
        public struct TargetSizes
        {
            /// <summary>
            /// The target width
            /// </summary>
            public int TargetWidth;
            /// <summary>
            /// The target height
            /// </summary>
            public int TargetHeight;
            /// <summary>
            /// The offset x
            /// </summary>
            public int OffsetX;
            /// <summary>
            /// The offset y
            /// </summary>
            public int OffsetY;
        }

        #endregion

        #region Disposal routines ^^^^^

        /// <summary>
        /// Releases unmanaged and - optionally - managed resources.
        /// </summary>
        /// <param name="disposing"><c>true</c> to release both managed and unmanaged resources; <c>false</c> to release only unmanaged resources.</param>
        protected virtual void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                if (disposing)
                {
                }

                disposedValue = true;
            }
        }

        // // TODO: override finalizer only if 'Dispose(bool disposing)' has code to free unmanaged resources
        // ~FlagEnumUIEditor()
        // {
        //     // Do not change this code. Put cleanup code in 'Dispose(bool disposing)' method
        //     Dispose(disposing: false);
        // }

        /// <summary>
        /// Releases unmanaged and - optionally - managed resources.
        /// </summary>
        public void Dispose()
        {
            // Do not change this code. Put cleanup code in 'Dispose(bool disposing)' method
            Dispose(disposing: true);
            GC.SuppressFinalize(this);
        }

        #endregion
    }
}