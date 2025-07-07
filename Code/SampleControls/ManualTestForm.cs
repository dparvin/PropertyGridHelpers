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
        /// The text box
        /// </summary>
        private readonly TextBox _textBox;
    }
}
