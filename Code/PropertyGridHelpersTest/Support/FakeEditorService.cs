using System.Windows.Forms;
using System.Windows.Forms.Design;

namespace PropertyGridHelpersTest.Support
{
    /// <summary>
    /// Fake implementation of <see cref="IWindowsFormsEditorService"/> for testing purposes.
    /// </summary>
    /// <seealso cref="System.Windows.Forms.Design.IWindowsFormsEditorService" />
    public class FakeEditorService : IWindowsFormsEditorService
    {
        /// <summary>
        /// Gets the dropped control.
        /// </summary>
        /// <value>
        /// The dropped control.
        /// </value>
       //#if 
        public Control DroppedControl
        {
            get; private set;
        }

        /// <summary>
        /// Closes the drop down.
        /// </summary>
        public void CloseDropDown()
        {
            // Simulate closing the dropdown (you can track this if needed)
        }

        /// <summary>
        /// Drops down control.
        /// </summary>
        /// <param name="control">The control.</param>
        public void DropDownControl(Control control) => DroppedControl = control;

        /// <summary>
        /// Shows the dialog.
        /// </summary>
        /// <param name="dialog">The dialog.</param>
        /// <returns></returns>
        public DialogResult ShowDialog(Form dialog) => DialogResult.None;
    }
}
