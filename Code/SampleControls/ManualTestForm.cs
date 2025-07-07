using PropertyGridHelpers.Support;
using System;
using System.ComponentModel;
using System.Windows.Forms;

namespace SampleControls
{
    /// <summary>
    /// A simple form for manual testing of the IValueEditorForm interface.
    /// </summary>
    /// <seealso cref="Form" />
    /// <seealso cref="IValueEditorForm" />
    public partial class ManualTestForm : Form, IValueEditorForm
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
        /// Initializes a new instance of the <see cref="ManualTestForm"/> class.
        /// </summary>
        public ManualTestForm()
        {
            InitializeComponent();

            var textBox = new TextBox
            {
                Multiline = true,
                Dock = DockStyle.Fill
            };

            Controls.Add(textBox);

            var okButton = new Button
            {
                Text = "OK",
                Dock = DockStyle.Bottom
            };
            okButton.Click += (s, e) =>
            {
                EditedValue = _textBox.Text;
                DialogResult = DialogResult.OK;
                Close();
            };
            Controls.Add(okButton);

            _textBox = textBox;
        }

        /// <summary>
        /// Raises the <see cref="Form.Load"/> event.
        /// </summary>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);

            if (EditedValue is string initial)
                _textBox.Text = initial;
        }

        /// <summary>
        /// Displays the editor form as a modal dialog, allowing the user to edit the value
        /// and return the result of the operation.
        /// </summary>
        /// <returns>
        /// A <see cref="DialogResult" /> indicating how the user closed the editor, such as
        /// <see cref="DialogResult.OK" /> if the edit was confirmed.
        /// </returns>
        /// <remarks>
        /// This method is intended to be called by the property editor to show the editing interface.
        /// In production implementations, it should display the form using <c>ShowDialog()</c>, while
        /// in unit tests, it can be overridden to simulate user interaction without displaying a window.
        /// </remarks>
        public DialogResult ShowEditor() => this.ShowDialog();

        /// <summary>
        /// The text box
        /// </summary>
        private readonly TextBox _textBox;
    }
}
