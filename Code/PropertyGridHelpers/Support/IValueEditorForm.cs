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
    }
}
