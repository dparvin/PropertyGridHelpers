using PropertyGridHelpers.Support;
using PropertyGridHelpers.UIEditors;
using System;
using System.ComponentModel;
using System.Drawing.Design;
using System.Windows.Forms;

namespace PropertyGridHelpers.Controls
{
    /// <summary>
    /// A specialized <see cref="ComboBox"/> designed for use in a <see cref="PropertyGrid"/> 
    /// drop-down editor, supporting auto-completion and value commitment handling.
    /// </summary>
    /// <remarks>
    /// This control is intended for use in property editors where a user-friendly 
    /// auto-complete experience is desired, such as file paths, URLs, or custom lists.
    /// The control implements <see cref="IDropDownEditorControl"/>, allowing it to be hosted 
    /// by a <see cref="UITypeEditor"/> like <see cref="UIEditors.DropDownVisualizer{TControl}"/>.
    /// </remarks>
    /// <seealso cref="ComboBox"/>
    /// <seealso cref="IDropDownEditorControl"/>
    public class AutoCompleteComboBox
        : ComboBox, IDropDownEditorControl
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="AutoCompleteComboBox"/> class.
        /// </summary>
        /// <remarks>
        /// Default settings:
        /// <list type="bullet">
        /// <item><description><see cref="ComboBox.DropDownStyle"/> = <see cref="ComboBoxStyle.DropDown"/></description></item>
        /// <item><description><see cref="ComboBox.AutoCompleteMode"/> = <see cref="AutoCompleteMode.SuggestAppend"/></description></item>
        /// <item><description><see cref="ComboBox.AutoCompleteSource"/> = <see cref="AutoCompleteSource.CustomSource"/></description></item>
        /// </list>
        /// </remarks>
        public AutoCompleteComboBox()
        {
            DropDownStyle = ComboBoxStyle.DropDown;
            AutoCompleteMode = AutoCompleteMode.SuggestAppend;
            AutoCompleteSource = AutoCompleteSource.CustomSource;
        }

        /// <summary>
        /// Gets or sets the value displayed and edited in the combo box.
        /// </summary>
        /// <value>
        /// The current text of the combo box.
        /// </value>
        /// <remarks>
        /// This property is used by <see cref="DropDownVisualizer{TControl}"/> to transfer
        /// data between the editor and the hosting environment.
        /// </remarks>
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public object Value
        {
            get => Text;
            set => Text = (string)value;
        }

        /// <summary>
        /// Occurs when the user finishes editing and the value should be committed.
        /// </summary>
        /// <remarks>
        /// This event is used to close the drop-down when editing is complete.
        /// It is raised when either:
        /// <list type="bullet">
        /// <item><description>The selected value changes</description></item>
        /// <item><description>The control loses focus</description></item>
        /// </list>
        /// </remarks>
        public event EventHandler ValueCommitted = delegate { };

        /// <summary>
        /// Raises the <see cref="ListControl.SelectedValueChanged"/> event and notifies that editing is complete.
        /// </summary>
        /// <param name="e">Event data.</param>
        protected override void OnSelectedValueChanged(EventArgs e)
        {
            base.OnSelectedValueChanged(e);
            ValueCommitted.Invoke(this, EventArgs.Empty);
        }

        /// <summary>
        /// Raises the <see cref="Control.Leave"/> event and notifies that editing is complete.
        /// </summary>
        /// <param name="e">Event data.</param>
        protected override void OnLeave(EventArgs e)
        {
            base.OnLeave(e);
            ValueCommitted.Invoke(this, EventArgs.Empty);
        }
    }
}
