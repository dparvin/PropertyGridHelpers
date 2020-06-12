namespace PropertyGridHelpers.Controls
{
    /// <summary>
    /// Represents an item in the CheckListBox
    /// </summary>
    public class FlagCheckedListBoxItem
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="FlagCheckedListBoxItem"/> class.
        /// </summary>
        /// <param name="v">The value.</param>
        /// <param name="c">The caption.</param>
        public FlagCheckedListBoxItem(int v, string c)
        {
            value = v;
            caption = c;
        }

        /// <summary>
        /// Returns a <see cref="string" /> that represents this instance.
        /// </summary>
        /// <returns>
        /// A <see cref="string" /> that represents this instance.
        /// </returns>
        public override string ToString()
        {
            return caption;
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
                return ((value & (value - 1)) == 0);
            }
        }

        /// <summary>
        /// Returns true if this value is a member of the composite bit value
        /// </summary>
        /// <param name="composite">The composite.</param>
        /// <returns></returns>
        public bool IsMemberFlag(FlagCheckedListBoxItem composite)
        {
            return (IsFlag && ((value & composite.value) == value));
        }

        public int value;
        public string caption;
    }

}
