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
    /// UITypeEditor for an Enum with associated images
    /// </summary>
    /// <seealso cref="UITypeEditor" />
    public class ImageTextUIEditor : UITypeEditor, IDisposable
    {
        /// <summary>
        /// The object is disposed
        /// </summary>
        private bool disposedValue;
        /// <summary>
        /// The enum type
        /// </summary>
        protected Type EnumType { get; }
        /// <summary>
        /// The path to the resources where the images are stored
        /// </summary>
        protected string ResourcePath { get; }

        /// <summary>
        /// Initializes a new instance of the <see cref="ImageTextUIEditor"/> class.
        /// </summary>
        /// <param name="type">Type of enum that is used in the process</param>
        public ImageTextUIEditor(Type type)
        {
            EnumType = type;
            ResourcePath = "Properties.Resources";
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

        /// <summary>
        /// return that the editor will paint the items in the drop-down
        /// </summary>
        /// <param name="context">The Type Descriptor Context</param>
        public override bool GetPaintValueSupported(ITypeDescriptorContext context) => true;

        /// <summary>
        /// Paint the value in the drop-down list
        /// </summary>
        /// <param name="e">Paint Value Event Arguments</param>
        public override void PaintValue(PaintValueEventArgs e)
        {
            // get the field info for the enum value
            FieldInfo fi = EnumType.GetField(Enum.GetName(EnumType, e.Value));
            // get the EnumImageAttribute for the field
            var dna =
                    (EnumImageAttribute)Attribute.GetCustomAttribute(
                    fi, typeof(EnumImageAttribute));

            if (dna != null)
            {
                // Get the name of the DLL or EXE where the reference object is declared
                string m = e.Value.GetType().Module.Name;
                string ei = dna.EnumImage;
                if (string.IsNullOrEmpty(ei))
                {
                    ei = Enum.GetName(EnumType, e.Value);
                }
                // Remove the file extension from the name
#if NET5_0_OR_GREATER
                m = m[0..^4];
#else
                m = m.Substring(0, m.Length - 4);
#endif
                Bitmap newImage = new Bitmap(100, 100);
                string ResourceName;
                switch (dna.ImageLocation)
                {
                    case ImageLocation.Embedded:
                        ResourceName = $"{m}{(string.IsNullOrEmpty(ResourcePath) ? "" : $".{ResourcePath}")}.{ei}";
                        using (var stream = e.Value.GetType().Assembly.GetManifestResourceStream(ResourceName))
                        {
                            newImage = (Bitmap)Image.FromStream(stream);
                        }
                        break;
                    case ImageLocation.Resource:
                        // Create a resource manager to access the resources
                        ResourceName = $"{m}{(string.IsNullOrEmpty(ResourcePath) ? "" : $".{ResourcePath}")}";
                        var rm = new ResourceManager(ResourceName, e.Value.GetType().Assembly);

                        // Check if the resource file exists in the assembly
                        var resourceNames = e.Value.GetType().Assembly.GetManifestResourceNames();
                        if (!resourceNames.Any(r => r.EndsWith($"{ResourceName}.resources", StringComparison.CurrentCulture)))
                        {
                            throw new InvalidOperationException(
                                $"Resource file '{ResourceName}.resources' not found in assembly '{m}'. \n" +
                                $"Available resources: {string.Join(", ", resourceNames)}");
                        }

                        // Get the resource object
                        var resource = rm.GetObject(ei, CultureInfo.CurrentCulture);

                        if (resource is Bitmap bitmap)
                        {
                            // If the resource is a Bitmap, use it directly
                            newImage = bitmap;
                        }
                        else if (resource is byte[] byteArray)
                        {
                            // If the resource is a byte array, convert it to an image
                            using (var ms = new MemoryStream(byteArray))
                            {
                                newImage = new Bitmap(ms);
                            }
                        }
                        else
                            throw new InvalidOperationException($"Resource {ei} is not a valid image or byte array.");
                        break;
                    case ImageLocation.File:
                        string assemblyPath;
                        // Get the directory of the assembly containing _enumType
#pragma warning disable SYSLIB0012 // The class is obsolete
                        var uri = new UriBuilder(Assembly.GetExecutingAssembly().CodeBase);
#pragma warning restore SYSLIB0012 // The class is obsolete
                        assemblyPath = Path.GetDirectoryName(Uri.UnescapeDataString(uri.Path));
                        // Construct the full file path
                        if (!string.IsNullOrEmpty(ResourcePath))
                        {
                            assemblyPath = Path.Combine(assemblyPath, ResourcePath);
                        }

                        ResourceName = Path.Combine(assemblyPath, ei);

                        // Load the image from the file path
                        newImage = new Bitmap(ResourceName);
                        break;
                }
                Rectangle dr = e.Bounds;
                newImage.MakeTransparent();
                e.Graphics.DrawImage(newImage, dr);
            }
        }

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
    }

    /// <summary>
    /// UITypeEditor for flag Enums
    /// </summary>
    /// <typeparam name="T">EnumConverter to use to make the text in the drop-down list</typeparam>
    /// <seealso cref="UITypeEditor" />
    /// <seealso cref="IDisposable" />
#if NET35
    public class ImageTextUIEditor<T> : ImageTextUIEditor where T : struct
    {
        /// <summary>
        /// Initializes the <see cref="ImageTextUIEditor{T}"/> class.
        /// </summary>
        /// <exception cref="ArgumentException">T must be an enumerated type</exception>
        static ImageTextUIEditor()
        {
            if (!typeof(T).IsEnum)
            {
                throw new ArgumentException("T must be an enumerated type");
            }
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ImageTextUIEditor" /> class.
        /// </summary>
        public ImageTextUIEditor() : base(typeof(T)) { }

        /// <summary>
        /// Initializes a new instance of the <see cref="ImageTextUIEditor" /> class.
        /// </summary>
        /// <param name="ResourcePath">The path to the resources where the images are stored</param>
        public ImageTextUIEditor(string ResourcePath) : base(typeof(T), ResourcePath) { }
    }
#elif NET40_OR_GREATER || NET5_0_OR_GREATER
    public class ImageTextUIEditor<T> : ImageTextUIEditor where T : struct, Enum
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ImageTextUIEditor" /> class.
        /// </summary>
        public ImageTextUIEditor() : base(typeof(T)) { }

        /// <summary>
        /// Initializes a new instance of the <see cref="ImageTextUIEditor" /> class.
        /// </summary>
        /// <param name="ResourcePath">The path to the resources where the images are stored</param>
        public ImageTextUIEditor(string ResourcePath) : base(typeof(T), ResourcePath) { }
    }
#endif
}
