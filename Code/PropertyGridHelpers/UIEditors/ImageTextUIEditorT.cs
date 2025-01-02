using System;
using System.Drawing.Design;

namespace PropertyGridHelpers.UIEditors
{
    /// <summary>
    /// UITypeEditor for flag Enums
    /// </summary>
    /// <typeparam name="T">EnumConverter to use to make the text in the drop-down list</typeparam>
    /// <seealso cref="UITypeEditor" />
    /// <seealso cref="IDisposable" />
#if NET35
    public partial class ImageTextUIEditor<T> : ImageTextUIEditor where T : struct
    {
        /// <summary>
        /// Initializes the <see cref="ImageTextUIEditor{T}"/> class.
        /// </summary>
        /// <exception cref="ArgumentException">T must be an enumerated type</exception>
        static ImageTextUIEditor()
        {
            if (!typeof(T).IsEnum)
                throw new ArgumentException("T must be an enumerated type");
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
#else
    public partial class ImageTextUIEditor<T> : ImageTextUIEditor where T : struct, Enum
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
