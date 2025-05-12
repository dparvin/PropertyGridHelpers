using System;

namespace PropertyGridHelpers.Support
{
    /// <summary>
    /// Interface for a control that can be used to edit a value in a drop-down editor.
    /// </summary>
    public interface IDropDownEditorControl : IDisposable
    {
        /// <summary>
        /// Gets or sets the value to be edited.
        /// </summary>
        object Value
        {
            get; set;
        }

        /// <summary>
        /// Event raised when the user indicates they are done editing.
        /// </summary>
        event EventHandler ValueCommitted;
    }
}
