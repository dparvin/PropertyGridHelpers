using System.ComponentModel;
using System.Globalization;

namespace CultureInfoEditor.Converters
{
    /// <summary>
    /// Convert a type to another type.  Used for expandable objects.
    /// </summary>
    /// <typeparam name="T">type to convert to or from</typeparam>
    /// <seealso cref="ExpandableObjectConverter" />
    public class TypeConverter<T> : ExpandableObjectConverter
    {
        /// <summary>
        /// Determines whether this instance can convert to the specified context.
        /// </summary>
        /// <param name="context">The context.</param>
        /// <param name="destinationType">Type of the destination.</param>
        /// <returns></returns>
        public override bool CanConvertTo(
            ITypeDescriptorContext context,
            System.Type destinationType)
        {
            if (destinationType == typeof(T))
                return true;

            return base.CanConvertTo(context, destinationType);
        }

        /// <summary>
        /// Converts to.
        /// </summary>
        /// <param name="context">The context.</param>
        /// <param name="culture">The culture.</param>
        /// <param name="value">The value.</param>
        /// <param name="destinationType">Type of the destination.</param>
        /// <returns></returns>
        public override object ConvertTo(
            ITypeDescriptorContext context,
            CultureInfo culture,
            object value,
            System.Type destinationType)
        {
            if (destinationType == typeof(string) &&
                 value is T t)
            {
                T so = t;

                return so.ToString();
            }
            return base.ConvertTo(context, culture, value, destinationType);
        }

        /// <summary>
        /// Determines whether this instance can convert from the specified context.
        /// </summary>
        /// <param name="context">The context.</param>
        /// <param name="sourceType">Type of the source.</param>
        /// <returns></returns>
        public override bool CanConvertFrom(
            ITypeDescriptorContext context,
            System.Type sourceType)
        {
            if (sourceType == typeof(string))
                return false;

            return base.CanConvertFrom(context, sourceType);
        }
    }
}
