using PropertyGridHelpers.Controls;
using System.ComponentModel;
using System.Drawing.Design;
using System.Windows.Forms;

namespace PropertyGridHelpers.UIEditors
{
    /// <summary>
    /// A reusable <see cref="UITypeEditor"/> for editing [Flags]-decorated enums 
    /// in a <see cref="PropertyGrid"/> using a drop-down checklist.
    /// </summary>
    /// <remarks>
    /// This editor hosts a <see cref="FlagCheckedListBox"/> inside a drop-down 
    /// and is intended for use with enumerations marked with the 
    /// <see cref="System.FlagsAttribute"/>. The drop-down allows users to 
    /// select multiple values by checking corresponding options.
    ///
    /// The control handles bitwise logic internally, updating the composite
    /// enum value based on the user's selection.
    ///
    /// Example usage:
    /// <code>
    /// [Editor(typeof(FlagEnumUIEditor), typeof(UITypeEditor))]
    /// public MyFlagsEnum Options { get; set; }
    /// </code>
    ///
    /// To customize the display text for enum values, consider using
    /// <see cref="FlagEnumUIEditor{T}"/> with an <see cref="EnumConverter"/>.
    /// </remarks>
    /// <seealso cref="DropDownVisualizer{TControl}"/>
    /// <seealso cref="FlagCheckedListBox"/>
    /// <seealso cref="System.FlagsAttribute"/>
    /// <seealso cref="UITypeEditor"/>
    public partial class FlagEnumUIEditor : DropDownVisualizer<FlagCheckedListBox>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="FlagEnumUIEditor"/> class,
        /// configuring the drop-down checklist style.
        /// </summary>
        public FlagEnumUIEditor() : base() =>
            DropDownControl.BorderStyle = BorderStyle.None;
    }
}
