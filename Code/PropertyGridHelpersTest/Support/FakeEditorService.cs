using PropertyGridHelpersTest.Controls;
using System.Windows.Forms;
using System.Windows.Forms.Design;

namespace PropertyGridHelpersTest.Support
{
    /// <summary>
    /// Fake implementation of <see cref="IWindowsFormsEditorService"/> for testing purposes.
    /// </summary>
    /// <seealso cref="IWindowsFormsEditorService" />
    public class FakeEditorService
        : IWindowsFormsEditorService
    {
        /// <summary>
        /// Gets a value indicating whether drop down closed.
        /// </summary>
        /// <value>
        ///   <c>true</c> if drop down closed; otherwise, <c>false</c>.
        /// </value>
        public bool DropDownClosed
        {
            get; private set;
        }

        /// <summary>
        /// Closes the drop down.
        /// </summary>
        public void CloseDropDown() => DropDownClosed = true;

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
        /// Drops down control.
        /// </summary>
        /// <param name="control">The control.</param>
        public void DropDownControl(Control control)
        {
            // Simulate user interaction and closing
            if (control is FakeEditorControl fakeEditor)
            {
                fakeEditor.Value = "EmptyResourceFile"; // Simulate user changing value
                fakeEditor.TriggerCommit();             // Simulate the user committing the value
            }
        }

        /// <summary>
        /// Shows the dialog.
        /// </summary>
        /// <param name="dialog">The dialog.</param>
        /// <returns></returns>
        public DialogResult ShowDialog(Form dialog) => DialogResult.OK;
    }
}
