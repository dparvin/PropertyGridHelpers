using PropertyGridHelpers.Controls;
using System;
using System.ComponentModel;
using System.Drawing.Design;
using System.Windows.Forms;

namespace PropertyGridHelpers.UIEditors
{
    /// <summary>
    /// A generic <see cref="UITypeEditor"/> for editing flag-style enums in a <see cref="PropertyGrid"/>,
    /// with support for customized display text using a specified <see cref="EnumConverter"/>.
    /// </summary>
    /// <typeparam name="T">
    /// The <see cref="EnumConverter"/> type used to convert enum values to user-friendly display text.
    /// Must have a parameterless constructor.
    /// </typeparam>
    /// <remarks>
    /// This editor uses a <see cref="FlagCheckedListBox"/> hosted in a dropdown and allows
    /// multiple values to be selected for enums decorated with the <see cref="FlagsAttribute"/>.
    ///
    /// The generic parameter <typeparamref name="T"/> allows you to specify a custom converter,
    /// such as <see cref="Converters.EnumTextConverter{TEnum}"/>, to localize or stylize enum values.
    ///
    /// Example:
    /// <code>
    /// [Editor(typeof(FlagEnumUIEditor&lt;EnumTextConverter&lt;MyEnum&gt;&gt;), typeof(UITypeEditor))]
    /// [TypeConverter(typeof(EnumTextConverter&lt;MyEnum&gt;))]
    /// public MyEnum Options { get; set; }
    /// </code>
    /// </remarks>
    /// <seealso cref="DropDownVisualizer{FlagCheckedListBox}"/>
    /// <seealso cref="FlagEnumUIEditor"/>
    /// <seealso cref="FlagCheckedListBox"/>
    /// <seealso cref="Converters.EnumTextConverter{T}"/>
    public partial class FlagEnumUIEditor<T>
        : DropDownVisualizer<FlagCheckedListBox>
        where T : EnumConverter, new()
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="FlagEnumUIEditor" /> class.
        /// </summary>
        public FlagEnumUIEditor() : base() =>
            DropDownControl.Converter = new T();
    }
}