using PropertyGridHelpers.Attributes;
using PropertyGridHelpers.Converters;
using System;
using System.ComponentModel;
using System.Drawing.Design;

namespace PropertyGridHelpers.UIEditors
{
    /// <summary>
    /// A generic <see cref="UITypeEditor" /> that displays an enumeration with both images and localized text in a drop-down list.
    /// </summary>
    /// <typeparam name="TEnum">The enumeration type to be edited. This must be a value type constrained to an <see cref="Enum" /> (except on .NET Framework 3.5).</typeparam>
    /// <remarks>
    /// This class extends <see cref="ImageTextUIEditor" /> and uses the specified generic enum type <typeparamref name="TEnum" /> 
    /// to simplify application and enforce type safety. It supports localized display names 
    /// via <see cref="LocalizedEnumTextAttribute" /> and image rendering via <see cref="EnumImageAttribute" />.
    /// <para>
    /// This editor is useful for properties where both textual meaning and visual context help the user choose the correct value.
    /// </para>
    /// </remarks>
    /// <example>
    /// To use this editor for an enum named <c>ButtonStyle</c>, define your enum with attributes:
    /// <code>
    /// public enum ButtonStyle
    /// {
    /// [EnumText("Flat"), EnumImage("FlatIcon")]
    /// Flat,
    /// [EnumText("Raised"), EnumImage("RaisedIcon")]
    /// Raised,
    /// [EnumText("3D"), EnumImage("ThreeDIcon")]
    /// ThreeD
    /// }
    /// </code>
    /// Then apply the editor to your property:
    /// <code>
    /// [Editor(typeof(ImageTextUIEditor&lt;ButtonStyle&gt;), typeof(UITypeEditor))]
    /// public ButtonStyle Style { get; set; }
    /// </code></example>
    /// <seealso cref="ImageTextUIEditor" />
    /// <seealso cref="EnumImageAttribute" />
    /// <seealso cref="LocalizedEnumTextAttribute" />
    /// <seealso cref="EnumTextConverter" />
#if NET35
    public partial class ImageTextUIEditor<TEnum> :
        ImageTextUIEditor
        where TEnum : struct
    {
        /// <summary>
        /// Initializes the <see cref="ImageTextUIEditor{TEnum}"/> class.
        /// </summary>
        /// <exception cref="ArgumentException">T must be an enumerated type</exception>
        static ImageTextUIEditor()
        {
            if (!typeof(TEnum).IsEnum)
                throw new ArgumentException("TEnum must be an enumerated type");
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ImageTextUIEditor" /> class.
        /// </summary>
        public ImageTextUIEditor() : base(typeof(TEnum)) { }

        /// <summary>
        /// Initializes a new instance of the <see cref="ImageTextUIEditor" /> class.
        /// </summary>
        /// <param name="ResourcePath">The path to the resources where the images are stored</param>
        public ImageTextUIEditor(string ResourcePath) : base(typeof(TEnum), ResourcePath) {}
    }
#else
    public partial class ImageTextUIEditor<TEnum>
        : ImageTextUIEditor
        where TEnum : struct, Enum
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ImageTextUIEditor" /> class.
        /// </summary>
        public ImageTextUIEditor() : base(typeof(TEnum)) { }

        /// <summary>
        /// Initializes a new instance of the <see cref="ImageTextUIEditor" /> class.
        /// </summary>
        /// <param name="ResourcePath">The path to the resources where the images are stored</param>
        public ImageTextUIEditor(string ResourcePath) : base(typeof(TEnum), ResourcePath) { }
    }
#endif
}
