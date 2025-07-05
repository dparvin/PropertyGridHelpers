using System;

namespace PropertyGridHelpers.Support
{
    /// <summary>
    /// Defines the contract for a control used within a drop-down editor in a property grid.
    /// </summary>
    /// <remarks>
    /// Implementations of this interface are intended to provide interactive editors that
    /// can be hosted inside a drop-down area of a <see cref="System.Windows.Forms.PropertyGrid"/>.
    /// The editor control must support assigning and retrieving a value and notify when
    /// editing is committed.
    /// </remarks>
    public interface IDropDownEditorControl : IDisposable
    {
        /// <summary>
        /// Gets or sets the current value being edited by the drop-down control.
        /// </summary>
        /// <value>
        /// The value to edit or the value resulting from the edit operation.
        /// </value>
        object Value
        {
            get; set;
        }

        /// <summary>
        /// Occurs when the user has completed editing and committed the value.
        /// </summary>
        /// <remarks>
        /// Implementations should raise this event to notify the hosting editor that
        /// the value has changed and the drop-down should be closed.
        /// </remarks>
        event EventHandler ValueCommitted;
    }
}
