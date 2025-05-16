using PropertyGridHelpers.Support;
using System;
using System.ComponentModel;
using System.Windows.Forms;

namespace PropertyGridHelpersTest.Controls
{
    /// <summary>
    /// Fake editor control for testing purposes.
    /// </summary>
    /// <seealso cref="Control" />
    /// <seealso cref="IDropDownEditorControl" />
    public class FakeEditorControl : Control, IDropDownEditorControl
    {
        /// <summary>
        /// Gets or sets the value to be edited.
        /// </summary>
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public object Value
        {
            get; set;
        }

        /// <summary>
        /// Event raised when the user indicates they are done editing.
        /// </summary>
        public event EventHandler ValueCommitted;

        /// <summary>
        /// Triggers the commit.
        /// </summary>
        public void TriggerCommit() => ValueCommitted?.Invoke(this, EventArgs.Empty);
    }
}
