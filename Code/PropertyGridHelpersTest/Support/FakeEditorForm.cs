using PropertyGridHelpers.Support;
using System.ComponentModel;
using System.Windows.Forms;

namespace PropertyGridHelpersTest.Support
{
    /// <summary>
    /// Fake form implementing IValueEditorForm for testing ModalVisualizer.
    /// </summary>
    public class FakeEditorForm : Form, IValueEditorForm
    {
        /// <summary>
        /// Gets or sets the value being edited in the form.
        /// </summary>
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public object EditedValue
        {
            get; set;
        }

        /// <summary>
        /// Displays the editor form as a modal dialog, allowing the user to edit the value
        /// and return the result of the operation.
        /// </summary>
        /// <returns>
        /// A <see cref="T:System.Windows.Forms.DialogResult" /> indicating how the user closed the editor, such as
        /// <see cref="F:System.Windows.Forms.DialogResult.OK" /> if the edit was confirmed.
        /// </returns>
        /// <remarks>
        /// This method is intended to be called by the property editor to show the editing interface.
        /// In production implementations, it should display the form using <c>ShowDialog()</c>, while
        /// in unit tests, it can be overridden to simulate user interaction without displaying a window.
        /// </remarks>
        public DialogResult ShowEditor()
        {
            // This method is not used in this test, but must be implemented
            // to satisfy the IValueEditorForm interface.
            EditedValue = "NewTestValue";
            return DialogResult.OK;
        }
    }
}
