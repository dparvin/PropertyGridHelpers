using System;

namespace PropertyGridHelpers.Attributes
{
    /// <summary>
    /// Apply an image to an Enum item for use in a property page
    /// </summary>
    /// <seealso cref="Attribute" />
    [AttributeUsage(AttributeTargets.All, AllowMultiple = false, Inherited = false)]
    public sealed class EnumImageAttribute : Attribute
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="EnumImageAttribute"/> class.
        /// </summary>
        public EnumImageAttribute()
        {
            EnumImage = null;
        }
        /// <summary>
        /// Initializes a new instance of the <see cref="EnumImageAttribute"/> class.
        /// </summary>
        /// <param name="text">The text.</param>
        public EnumImageAttribute(string text)
        {
            EnumImage = text;
        }

        /// <summary>
        /// Gets the name of the image associated with the Enum item.
        /// </summary>
        /// <value>
        /// The Enum text.
        /// </value>
        public string EnumImage { get; }
    }
}
