namespace PropertyGridHelpers.Controls
{
    /// <summary>
    /// Represents an item displayed in a checklist box for editing bitwise
    /// enum values (flags). This item stores both the integer flag value
    /// and a user-friendly caption.
    /// </summary>
    public class FlagCheckedListBoxItem : ItemWrapper<int>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="FlagCheckedListBoxItem"/> class
        /// with the specified flag value and display caption.
        /// </summary>
        /// <param name="value">The integer value representing the flag.</param>
        /// <param name="caption">The caption displayed to the user.</param>
        public FlagCheckedListBoxItem(int value, string caption) : base(caption, value)
        {
        }

        /// <summary>
        /// Gets a value indicating whether this item represents a
        /// single flag (a single bit set).
        /// </summary>
        /// <value>
        /// <c>true</c> if this value has exactly one bit set; otherwise, <c>false</c>.
        /// </value>
        public bool IsFlag =>
            (Value & (Value - 1)) == 0;

        /// <summary>
        /// Determines whether this flag item is a member of the specified composite flag value.
        /// </summary>
        /// <param name="composite">
        /// A <see cref="FlagCheckedListBoxItem"/> representing the composite flags to test against.
        /// </param>
        /// <returns>
        /// <c>true</c> if this flag is included in the composite; otherwise, <c>false</c>.
        /// </returns>
        public bool IsMemberFlag(FlagCheckedListBoxItem composite) =>
            composite != null && IsFlag && ((Value & composite.Value) == Value);
    }
}
