namespace PropertyGridHelpers.Controls
{
#if NET5_0_OR_GREATER
    /// <summary>
    /// Represents a wrapper for items in a ComboBox that allows for custom display text and value.
    /// </summary>
    /// <typeparam name="T">
    /// The type of the value associated with the ComboBox item.
    /// </typeparam>
    /// <param name="displayText">The display text.</param>
    /// <param name="value">The value.</param>
    public class ItemWrapper<T>(string displayText, T value)
#else
    /// <summary>
    /// Represents a wrapper for items in a ComboBox that allows for custom display text and value.
    /// </summary>
    /// <typeparam name="T">
    /// The type of the value associated with the ComboBox item.
    /// </typeparam>
    public class ItemWrapper<T>
#endif
    {
        /// <summary>
        /// Gets the display text.
        /// </summary>
        /// <value>
        /// The display text.
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
        /// Gets the value.
        /// </summary>
        /// <value>
        /// The value.
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
        /// <param name="displayText">The display text.</param>
        /// <param name="value">The value.</param>
        public ItemWrapper(string displayText, T value)
        {
            DisplayText = displayText;
            Value = value;
        }
#endif

        /// <summary>
        /// Converts to string.
        /// </summary>
        /// <returns>
        /// A <see cref="System.String" /> that represents this instance.
        /// </returns>
        public override string ToString() => DisplayText;
    }
}
