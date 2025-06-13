namespace PropertyGridHelpers.Controls
{
    /// <summary>
    /// Represents an item in the CheckListBox
    /// </summary>
    public class FlagCheckedListBoxItem : ItemWrapper<int>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="FlagCheckedListBoxItem" /> class.
        /// </summary>
        /// <param name="v">The value.</param>
        /// <param name="c">The caption.</param>
        public FlagCheckedListBoxItem(int v, string c) : base(c, v)
        {
        }

        /// <summary>
        /// Returns true if the value corresponds to a single bit being set
        /// </summary>
        /// <value>
        ///   <c>true</c> if this instance is flag; otherwise, <c>false</c>.
        /// </value>
        public bool IsFlag
        {
            get
            {
                return (Value & (Value - 1)) == 0;
            }
        }

        /// <summary>
        /// Returns true if this value is a member of the composite bit value
        /// </summary>
        /// <param name="composite">The composite.</param>
        /// <returns>
        ///   <c>true</c> if  flag is member flag the specified composite; otherwise, <c>false</c>.
        /// </returns>
        public bool IsMemberFlag(FlagCheckedListBoxItem composite) =>
            composite != null && IsFlag && ((Value & composite.Value) == Value);
    }
}
