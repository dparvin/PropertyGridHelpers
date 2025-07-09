using System.ComponentModel;
using System.Globalization;
using System.Windows.Forms;

namespace PropertyGridHelpers.Support
{
    /// <summary>
    /// Defines the contract for a modal editor form used within a property grid.
    /// </summary>
    /// <remarks>
    /// Implementations of this interface provide a Windows Forms dialog that supports
    /// assigning an initial value and returning the edited value after the user
    /// completes the edit operation.
    /// </remarks>
    public interface IValueEditorForm
    {
        /// <summary>
        /// Gets or sets the value being edited in the form.
        /// </summary>
        object EditedValue
        {
            get; set;
        }

        /// <summary>
        /// Gets or sets the type descriptor context in which the control is being used.
        /// </summary>
        ITypeDescriptorContext Context
        {
            get; set;
        }

        /// <summary>
        /// Gets or sets the culture to use for localization or formatting.
        /// </summary>
        CultureInfo Culture
        {
            get; set;
        }

        /// <summary>
        /// Displays the editor form as a modal dialog, allowing the user to edit the value
        /// and return the result of the operation.
        /// </summary>
        /// <remarks>
        /// This method is intended to be called by the property editor to show the editing interface.
        /// In production implementations, it should display the form using <c>ShowDialog()</c>, while
        /// in unit tests, it can be overridden to simulate user interaction without displaying a window.
        /// </remarks>
        /// <returns>
        /// A <see cref="DialogResult"/> indicating how the user closed the editor, such as
        /// <see cref="DialogResult.OK"/> if the edit was confirmed.
        /// </returns>
        DialogResult ShowEditor();
    }
}
