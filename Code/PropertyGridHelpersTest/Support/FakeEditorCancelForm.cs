using PropertyGridHelpers.Support;
using System.ComponentModel;
using System.Windows.Forms;

namespace PropertyGridHelpersTest.Support
{
    /// <summary>
    /// Fake form implementing IValueEditorForm for testing ModalVisualizer.
    /// </summary>
    public class FakeEditorCancelForm : Form, IValueEditorForm
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
        /// Initializes a new instance of the <see cref="FakeEditorCancelForm"/> class.
        /// </summary>
        public FakeEditorCancelForm()
        {
            // avoid any window ever appearing
            ShowInTaskbar = false;
            Opacity = 0;
            StartPosition = FormStartPosition.Manual;
            Size = new System.Drawing.Size(0, 0);
            Load += (s, e) =>
            {
                EditedValue = "NewTestValue";
                DialogResult = DialogResult.Cancel;
                Close();
            };
        }
    }
}
