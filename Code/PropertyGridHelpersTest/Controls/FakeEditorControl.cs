using PropertyGridHelpers.Support;
using System;
using System.ComponentModel;
using System.Globalization;
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
        /// Gets or sets the type descriptor context in which the control is being used.
        /// </summary>
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public ITypeDescriptorContext Context
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets the culture to use for localization or formatting.
        /// </summary>
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public CultureInfo Culture
        {
            get;
            set;
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
