using PropertyGridHelpers.Attributes;
using System;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Design;
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
        private readonly Type _enumType;
        /// <summary>
        /// The path to the resources where the images are stored
        /// </summary>
        private readonly string _resourcePath;

        /// <summary>
        /// Initializes a new instance of the <see cref="ImageTextUIEditor"/> class.
        /// </summary>
        /// <param name="type">Type of enum that is used in the process</param>
        public ImageTextUIEditor(Type type)
        {
            _enumType = type;
            _resourcePath = "Properties.Resources";
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ImageTextUIEditor"/> class.
        /// </summary>
        /// <param name="type">Type of enum that is used in the process</param>
        /// <param name="ResourcePath">The path to the resources where the images are stored</param>
        public ImageTextUIEditor(Type type, string ResourcePath)
        {
            _enumType = type;
            _resourcePath = ResourcePath;
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
            FieldInfo fi = _enumType.GetField(Enum.GetName(_enumType, e.Value));
            EnumImageAttribute dna =
                    (EnumImageAttribute)Attribute.GetCustomAttribute(
                    fi, typeof(EnumImageAttribute));

            if (dna != null)
            {
                string m = e.Value.GetType().Module.Name;
#if NET5_0_OR_GREATER
                m = m[0..^4];
#else
                m = m.Substring(0, m.Length - 4);
#endif
                var rm = new ResourceManager(
                    m + (string.IsNullOrEmpty(_resourcePath) ? "" : "." + _resourcePath), e.Value.GetType().Assembly);

                // Draw the image
                Bitmap newImage = (Bitmap)rm.GetObject(dna.EnumImage);
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
    /// <seealso cref="UITypeEditor" />
    public class ImageTextUIEditor<T> : ImageTextUIEditor where T : Enum, new()
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ImageTextUIEditor" /> class.
        /// </summary>
        public ImageTextUIEditor() : base(typeof(T))
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ImageTextUIEditor" /> class.
        /// </summary>
        /// <param name="ResourcePath">The path to the resources where the images are stored</param>
        public ImageTextUIEditor(string ResourcePath) : base(typeof(T), ResourcePath)
        {
        }
    }
}
