using PropertyGridHelpers.Support;
using System;
using System.ComponentModel;
using System.Drawing.Design;
using System.Windows.Forms;

namespace PropertyGridHelpers.UIEditors
{
    /// <summary>
    /// Provides a reusable, generic modal visualizer for editing a property value within a <see cref="PropertyGrid"/>.
    /// This visualizer displays a modal dialog using a form of type <typeparamref name="TForm"/>, allowing
    /// rich editing experiences beyond the standard in-place or dropdown editors.
    /// </summary>
    /// <typeparam name="TForm">
    /// A Windows Forms <see cref="Form"/> class that implements <see cref="IValueEditorForm"/> to handle
    /// the editing of the property value.
    /// </typeparam>
    /// <remarks>
    /// To use this editor, define a custom form implementing the <see cref="IValueEditorForm"/> interface.
    /// The <see cref="IValueEditorForm.EditedValue"/> property is used to push the initial value
    /// into the form and retrieve the edited value when the dialog is closed with <see cref="DialogResult.OK"/>.
    /// </remarks>
    /// <example>
    /// <code language="csharp">
    /// public class CurrencyEditForm : Form, IValueEditorForm
    /// {
    ///     public object EditedValue { get; set; }
    ///     // implement form logic here
    /// }
    ///
    /// [Editor(typeof(ModalVisualizer&lt;CurrencyEditForm&gt;), typeof(UITypeEditor))]
    /// public decimal Amount { get; set; }
    /// </code>
    /// </example>
    /// <seealso cref="UITypeEditor"/>
    /// <seealso cref="IValueEditorForm"/>
    public class ModalVisualizer<TForm> : UITypeEditor
        where TForm : Form, IValueEditorForm, new()
    {
        /// <inheritdoc/>
        public override UITypeEditorEditStyle GetEditStyle(ITypeDescriptorContext context)
            => UITypeEditorEditStyle.Modal;

        /// <summary>
        /// Displays the modal editor form to allow the user to edit the property value.
        /// </summary>
        /// <param name="context">An optional type descriptor context for additional information.</param>
        /// <param name="provider">A service provider for editor services.</param>
        /// <param name="value">The current value of the property to edit.</param>
        /// <returns>
        /// The edited value returned from the modal form if the user confirms the edit; otherwise,
        /// returns the original <paramref name="value"/>.
        /// </returns>
        public override object EditValue(ITypeDescriptorContext context, IServiceProvider provider, object value)
        {
#if NET5_0_OR_GREATER
            using var form = new TForm();
#else
            using (var form = new TForm())
#endif
            {
                form.EditedValue = value; // push initial value
                if (form.ShowEditor() == DialogResult.OK)
                    return form.EditedValue; // get updated value

                return value; // no change
            }
        }
    }
}
