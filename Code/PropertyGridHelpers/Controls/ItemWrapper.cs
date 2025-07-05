namespace PropertyGridHelpers.Controls
{
#if NET5_0_OR_GREATER
    /// <summary>
    /// Represents a strongly-typed wrapper for a ComboBox item,
    /// associating custom display text with an underlying value.
    /// </summary>
    /// <typeparam name="T">
    /// The type of the value associated with this ComboBox item.
    /// </typeparam>
    /// <param name="displayText">The text shown to the user.</param>
    /// <param name="value">The backing value represented by this item.</param>
    public class ItemWrapper<T>(string displayText, T value)
#else
    /// <summary>
    /// Represents a strongly-typed wrapper for a ComboBox item,
    /// associating custom display text with an underlying value.
    /// </summary>
    /// <typeparam name="T">
    /// The type of the value associated with this ComboBox item.
    /// </typeparam>
    public class ItemWrapper<T>
#endif
    {
        /// <summary>
        /// Gets the text displayed in the ComboBox for this item.
        /// </summary>
        /// <value>
        /// The human-readable text shown to the user.
        /// </value>
        public string DisplayText
        {
            get;
#if NET5_0_OR_GREATER
        } = displayText;
#else
        }
#endif

        /// <summary>
        /// Gets the value associated with this item.
        /// </summary>
        /// <value>
        /// The strongly-typed backing value.
        /// </value>
        public T Value
        {
            get;
#if NET5_0_OR_GREATER
        } = value;
#else
        }
#endif

#if NET5_0_OR_GREATER
#else
        /// <summary>
        /// Initializes a new instance of the <see cref="ItemWrapper{T}"/> class.
        /// </summary>
        /// <param name="displayText">The text shown to the user.</param>
        /// <param name="value">The backing value represented by this item.</param>
        public ItemWrapper(string displayText, T value)
        {
            DisplayText = displayText;
            Value = value;
        }
#endif

        /// <summary>
        /// Returns the display text for this item.
        /// </summary>
        /// <returns>
        /// A string containing the <see cref="DisplayText"/>.
        /// </returns>
        public override string ToString() => DisplayText;
    }
}
