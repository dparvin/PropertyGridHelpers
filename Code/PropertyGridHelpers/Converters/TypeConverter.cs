using System;
using System.ComponentModel;
using System.Globalization;
using System.Reflection;
using System.Windows.Forms;

namespace PropertyGridHelpers.Converters
{
    /// <summary>
    /// A generic <see cref="ExpandableObjectConverter"/> that enables editing of
    /// complex types with nested properties directly in a <see cref="PropertyGrid"/>.
    /// </summary>
    /// <typeparam name="T">
    /// The type to expand in the property grid. This type should expose public
    /// properties to be displayed as editable sub-properties.
    /// </typeparam>
    /// <remarks>
    /// This converter supports:
    /// <list type="bullet">
    /// <item>Expanding nested objects in the property grid for inline editing.</item>
    /// <item>Conversion of <typeparamref name="T"/> to and from string via <c>ToString()</c>
    /// and optional parsing if <typeparamref name="T"/> implements <c>IParsable&lt;T&gt;</c>.</item>
    /// <item>Compatibility with .NET 7+ <c>IParsable&lt;T&gt;</c> interface when present.</item>
    /// </list>
    /// </remarks>
    /// <example>
    /// <code>
    /// public class Size
    /// {
    ///     public int Width { get; set; }
    ///     public int Height { get; set; }
    /// }
    /// 
    /// [TypeConverter(typeof(TypeConverter&lt;Size&gt;))]
    /// public Size MySize { get; set; }
    /// </code>
    /// This will allow <c>MySize</c> to expand in a property grid with separate
    /// editors for <c>Width</c> and <c>Height</c>.
    /// </example>
    public partial class TypeConverter<T> : ExpandableObjectConverter
    {
        /// <inheritdoc/>
        public override bool CanConvertTo(ITypeDescriptorContext context, Type destinationType) =>
            destinationType == typeof(T) || destinationType == typeof(string) || base.CanConvertTo(context, destinationType);

        /// <summary>
        /// Converts an object of type <typeparamref name="T"/> to the requested destination type.
        /// </summary>
        /// <param name="context">Context information from the property grid.</param>
        /// <param name="culture">The culture to use during conversion.</param>
        /// <param name="value">The current value to convert.</param>
        /// <param name="destinationType">The type to convert to (typically string).</param>
        /// <returns>
        /// A string representation of the value if <paramref name="destinationType"/> is <c>string</c>;
        /// otherwise defers to the base implementation.
        /// </returns>
        public override object ConvertTo(
            ITypeDescriptorContext context,
            CultureInfo culture,
            object value,
            Type destinationType) =>
                destinationType == typeof(string) &&
                value is T t
                ? t.ToString()
                : base.ConvertTo(context, culture, value, destinationType);

        /// <inheritdoc/>
        public override bool CanConvertFrom(ITypeDescriptorContext context, Type sourceType)
        {
            if (sourceType == typeof(string))
            {
                var tType = typeof(T);

#if NET7_0_OR_GREATER
                try
                {
                    var iParsableType = typeof(System.IParsable<>).MakeGenericType(tType);
                    return iParsableType.IsAssignableFrom(tType);
                }
                catch (ArgumentException)
                {
                    // If the type does not implement IParsable<T>, we will catch the exception
                    // and fall back to the custom IParsable<T> interface.
                }
#endif
                return typeof(IParsable<T>).IsAssignableFrom(tType);
            }

            return base.CanConvertFrom(context, sourceType);
        }

        /// <summary>
        /// Converts from a string to an instance of <typeparamref name="T" />, if supported.
        /// </summary>
        /// <param name="context">Context information from the property grid.</param>
        /// <param name="culture">Culture info to use for parsing.</param>
        /// <param name="value">The string to convert from.</param>
        /// <returns>
        /// An instance of <typeparamref name="T" /> if conversion is successful;
        /// otherwise defers to the base implementation.
        /// </returns>
        /// <remarks>
        /// Supports parsing if:
        /// <list type="bullet">
        /// <item><typeparamref name="T" /> implements .NET 7+ <c>IParsable&lt;T&gt;</c></item>
        /// <item>or the PropertyGridHelpers' custom <see cref="IParsable{T}" /> interface</item>
        /// </list>
        /// </remarks>
        public override object ConvertFrom(
            ITypeDescriptorContext context,
            CultureInfo culture,
            object value)
        {
            if (value is string s)
            {
                var tType = typeof(T);

#if NET7_0_OR_GREATER
                // Attempts to locate a static Parse method if the type supports System.IParsable<T>.
                MethodInfo parseMethod = null;

                try
                {
                    var iParsableType = typeof(System.IParsable<>).MakeGenericType(tType);
                    if (iParsableType.IsAssignableFrom(tType))
                    {
                        parseMethod = tType.GetMethod("Parse", [typeof(string), typeof(IFormatProvider)]);
                    }
                }
                catch (ArgumentException)
                {
                    // If the type does not implement IParsable<T>, we will catch the exception
                    // and fall back to the custom IParsable<T> interface.
                }

                if (parseMethod != null)
                {
                    try
                    {
                        // let this throw if parsing fails
                        return parseMethod.Invoke(null, [s, culture]);
                    }
                    catch (TargetInvocationException tie) when (tie.InnerException != null)
                    {
                        // Rethrow the real exception from inside Parse
                        throw tie.InnerException;
                    }
                }
#endif

                // Attempts to locate a static Parse method if the type supports IParsable<T>.
                if (typeof(IParsable<T>).IsAssignableFrom(tType))
                {
                    var instance = Activator.CreateInstance<T>();
                    return ((IParsable<T>)instance).Parse(s, culture);
                }
            }

            return base.ConvertFrom(context, culture, value);
        }
    }
}
