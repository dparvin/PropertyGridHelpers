using PropertyGridHelpers.Controls;
using System;
using System.ComponentModel;
using System.Drawing.Design;
using System.Windows.Forms;
using System.Windows.Forms.Design;

namespace PropertyGridHelpers.UIEditors
{
    /// <summary>
    /// UITypeEditor for flag enums
    /// </summary>
    /// <seealso cref="UITypeEditor" />
    public class FlagEnumUIEditor : UITypeEditor
    {
        /// <summary>
        /// The flag enum checkbox
        /// </summary>
        private FlagCheckedListBox flagEnumCB;

        /// <summary>
        /// Initializes a new instance of the <see cref="FlagEnumUIEditor"/> class.
        /// </summary>
        public FlagEnumUIEditor()
        {
            flagEnumCB = new FlagCheckedListBox();
            flagEnumCB.BorderStyle = BorderStyle.None;
        }

        /// <summary>
        /// Edits the value.
        /// </summary>
        /// <param name="context">The context.</param>
        /// <param name="provider">The provider.</param>
        /// <param name="value">The value.</param>
        /// <returns></returns>
        public override object EditValue(
            ITypeDescriptorContext context,
            IServiceProvider provider,
            object value)
        {
            if (context != null
                && context.Instance != null
                && provider != null)
            {

                IWindowsFormsEditorService edSvc = (IWindowsFormsEditorService)provider.GetService(typeof(IWindowsFormsEditorService));

                if (edSvc != null)
                {

                    Enum e = (Enum)Convert.ChangeType(value, context.PropertyDescriptor.PropertyType);
                    flagEnumCB.EnumValue = e;
                    edSvc.DropDownControl(flagEnumCB);
                    return flagEnumCB.EnumValue;

                }
            }
            return null;
        }

        /// <summary>
        /// Gets the edit style.
        /// </summary>
        /// <param name="context">The context.</param>
        /// <returns></returns>
        public override UITypeEditorEditStyle GetEditStyle(
            ITypeDescriptorContext context)
        {
            return UITypeEditorEditStyle.DropDown;
        }
    }
}
