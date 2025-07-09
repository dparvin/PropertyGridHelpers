using PropertyGridHelpers.Support;
using System;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Windows.Forms;

namespace SampleControls
{
    /// <summary>
    /// A simple calculator control that can be used in a property grid as a drop-down editor.
    /// </summary>
    /// <seealso cref="UserControl" />
    /// <seealso cref="IDropDownEditorControl" />
    public partial class CalculatorControl : UserControl, IDropDownEditorControl
    {
        /// <summary>
        /// Occurs when value is committed.
        /// </summary>
        public event EventHandler ValueCommitted = delegate { };

        /// <summary>
        /// Initializes a new instance of the <see cref="CalculatorControl"/> class.
        /// </summary>
        public CalculatorControl() =>
            InitializeComponent();

        /// <summary>
        /// Handles the KeyDown event of the CalculatorControl control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="KeyEventArgs"/> instance containing the event data.</param>
        private void CalculatorControl_KeyDown(
            object sender,
            KeyEventArgs e)
        {
            string key = null;
            var isShifted = (e.Modifiers & Keys.Shift) == Keys.Shift;
            if (e.KeyCode == Keys.Enter)
                key = "=";
            else if (e.KeyCode == Keys.Add ||
                     (e.KeyCode == Keys.Oemplus && isShifted))
                key = "+";
            else if (e.KeyCode == Keys.Subtract ||
                     e.KeyCode == Keys.OemMinus)
                key = "-";
            else if (e.KeyCode == Keys.Multiply ||
                     (e.KeyCode == Keys.D8 && isShifted))
                key = "*";
            else if (e.KeyCode == Keys.Divide ||
                     (e.KeyCode == Keys.Oem2 && !isShifted))
                key = "/";
            else if (e.KeyCode == Keys.Decimal || e.KeyCode == Keys.OemPeriod)
                key = ".";
            else if (e.KeyCode == Keys.OemOpenBrackets ||
                     (e.KeyCode == Keys.D9 && isShifted))
                key = "(";
            else if (e.KeyCode == Keys.OemCloseBrackets ||
                     (e.KeyCode == Keys.D0 && isShifted))
                key = ")";
            else if (e.KeyCode == Keys.Back)
                key = "<";
            else if (e.KeyCode == Keys.Escape)
                key = "C";
            else if (e.KeyCode >= Keys.D0 && e.KeyCode <= Keys.D9)
                key = ((char)e.KeyCode).ToString();
            else if (e.KeyCode >= Keys.NumPad0 && e.KeyCode <= Keys.NumPad9)
                key = Convert.ToString(e.KeyCode - Keys.NumPad0, 0);

            if (key != null)
            {
                foreach (Control control in _buttonsPanel.Controls)
                {
                    if (control is Button btn && string.Equals((string)btn.Tag, key, StringComparison.Ordinal))
                    {
                        btn.PerformClick();
                        break;
                    }
                }

                e.Handled = true;
            }
        }

        /// <summary>
        /// Raises the <see cref="Control.PreviewKeyDown" /> event.
        /// </summary>
        /// <param name="e">The <see cref="PreviewKeyDownEventArgs"/> instance containing the event data.</param>
        protected override void OnPreviewKeyDown(PreviewKeyDownEventArgs e)
        {
            base.OnPreviewKeyDown(e);

            if (e.KeyCode == Keys.Enter ||
                e.KeyCode == Keys.Escape ||
                e.KeyCode == Keys.Back ||
                e.KeyCode == Keys.OemOpenBrackets ||
                e.KeyCode == Keys.OemCloseBrackets)
            {
                e.IsInputKey = true;
            }
        }

        /// <summary>
        /// Handles the PreviewKeyDown event of the CalculatorControl control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="PreviewKeyDownEventArgs"/> instance containing the event data.</param>
        private void CalculatorControl_PreviewKeyDown(object sender, PreviewKeyDownEventArgs e) =>
            e.IsInputKey = true;

        /// <summary>
        /// Called when button click.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        private void OnButtonClick(object sender, EventArgs e)
        {
            var errorOccurred = false;
#if NET8_0_OR_GREATER
            if (sender is not Button btn) return;
#else
            if (!(sender is Button btn)) return;
#endif

            var input = (string)btn.Tag;

            if (string.Equals(input, "=", StringComparison.Ordinal))
            {
                try
                {
                    var result = new DataTable().Compute(_expression, null);
                    Value = Convert.ToDecimal(result, CultureInfo.CurrentCulture);
                    _display.Text = ((decimal)Value).ToString(CultureInfo.CurrentCulture);
                    ValueCommitted?.Invoke(this, EventArgs.Empty);
                }
                catch (Exception ex)
                {
                    errorOccurred = true;
                    _display.BackColor = Color.MistyRose;
                    _display.ForeColor = Color.DarkRed;
                    _tooltip.SetToolTip(_display, ex.Message);
                    // Don't clear expression so user can fix it
                }
            }
            else if (string.Equals(input, "<", StringComparison.Ordinal))
            {
                // Remove the last character from the expression
                if (_expression.Length > 0)
                {
#if NET5_0_OR_GREATER
                    // Use range operator to remove the last character
                    _expression = _expression[..^1];
#else
                    _expression = _expression.Substring(0, _expression.Length - 1);
#endif
                    _display.Text = _expression;
                }
            }
            else if (string.Equals(input, "C", StringComparison.Ordinal))
            {
                _expression = string.Empty;
                _display.Text = _expression;
            }
            else
            {
                _expression += input;
                _display.Text = _expression;
            }
            // If no error happened, clear the error state 
            if (!errorOccurred)
                ClearErrorState();
        }

        /// <summary>
        /// Clears the state of the error.
        /// </summary>
        private void ClearErrorState()
        {
            _display.BackColor = SystemColors.Window;
            _display.ForeColor = SystemColors.ControlText;
            _tooltip.SetToolTip(_display, string.Empty);
        }

        /// <summary>
        /// Processes the dialog key.
        /// </summary>
        /// <param name="keyData">The key data.</param>
        /// <returns></returns>
        protected override bool ProcessDialogKey(Keys keyData)
        {
            if (keyData == Keys.Enter)
            {
                OnButtonClick(_buttonsPanel.Controls
                    .OfType<Button>()
                    .First(b => string.Equals((string)b.Tag, "=", StringComparison.Ordinal)), EventArgs.Empty);
                return true;
            }

            return base.ProcessDialogKey(keyData);
        }

        /// <summary>
        /// Gets or sets the value.
        /// </summary>
        /// <value>
        /// The value.
        /// </value>
        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public object Value
        {
            get => _value;
            set
            {
                _value = (decimal)value;
                _expression = _value.ToString(CultureInfo.CurrentCulture);
                _display.Text = _expression;
            }
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
    }
}
