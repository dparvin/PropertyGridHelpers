using System;

namespace PropertyGridHelpers.Attributes
{
#if NET8_0_OR_GREATER
    /// <summary>
    /// Apply text to an Enum for use in a property page
    /// </summary>
    /// <seealso cref="Attribute" />
    [AttributeUsage(AttributeTargets.All, AllowMultiple = false, Inherited = false)]
    public sealed class EnumTextAttribute(string text) : Attribute
    {
#else
    /// <summary>
    /// Apply text to an Enum for use in a property page
    /// </summary>
    /// <seealso cref="Attribute" />
    [AttributeUsage(AttributeTargets.All, AllowMultiple = false, Inherited = false)]
    public sealed class EnumTextAttribute : Attribute
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="EnumTextAttribute"/> class.
        /// </summary>
        /// <param name="text">The text.</param>
        public EnumTextAttribute(string text) => EnumText = text;
#endif

        /// <summary>
        /// Gets the Enum text.
        /// </summary>
        /// <value>
        /// The Enum text.
        /// </value>
        public string EnumText
        {
            get;
#if NET8_0_OR_GREATER
        } = text;
#else
        }
#endif

        /// <summary>
        /// Exists the specified value.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <returns></returns>
        public static bool Exists(Enum value) => Get(value) != null;

        /// <summary>
        /// Gets the enum image attribute.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <returns></returns>
        public static EnumTextAttribute Get(Enum value)
        {
            if (value == null)
                return null;
            var field = value.GetType().GetField(Enum.GetName(value.GetType(), value));
            return (EnumTextAttribute)GetCustomAttribute(field, typeof(EnumTextAttribute));
        }
    }
}
