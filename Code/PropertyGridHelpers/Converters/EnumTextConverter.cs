using PropertyGridHelpers.Attributes;
using System;
using System.ComponentModel;
using System.Globalization;
using System.Linq;
using static System.ComponentModel.TypeConverter;

namespace PropertyGridHelpers.Converters
{
    /// <summary>
    /// Provides a type converter for enums that supports displaying custom user-friendly
    /// text for enum fields using the <see cref="EnumTextAttribute"/> or
    /// <see cref="LocalizedEnumTextAttribute"/>.
    /// </summary>
    /// <remarks>
    /// This converter allows enum values to be shown in a property grid or dropdown list
    /// with more readable labels instead of their default names. For localization or
    /// specialized labeling, decorate the enum fields with <see cref="EnumTextAttribute"/>
    /// or <see cref="LocalizedEnumTextAttribute"/>.
    /// </remarks>
    /// <example>
    /// <code>
    /// public enum Status
    /// {
    ///     [EnumText("Pending Approval")]
    ///     Pending,
    ///     [EnumText("Approved")]
    ///     Approved,
    ///     [EnumText("Rejected")]
    ///     Rejected
    /// }
    /// 
    /// [TypeConverter(typeof(EnumTextConverter))]
    /// public Status Status { get; set; }
    /// </code>
    /// </example>
    /// <seealso cref="EnumConverter"/>
    public partial class EnumTextConverter : EnumConverter
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="EnumTextConverter"/> class
        /// for the given enum type.
        /// </summary>
        /// <param name="type">The target enum type to convert.</param>
        /// <exception cref="ArgumentNullException">Thrown if <paramref name="type"/> is null.</exception>
        public EnumTextConverter(Type type) : base(type) { }

        /// <inheritdoc/>
        /// <remarks>
        /// Supports conversion to <see cref="string"/> or <see cref="int"/> representations.
        /// </remarks>
        public override bool CanConvertTo(
            ITypeDescriptorContext context,
            Type destinationType) =>
            destinationType is null
                ? throw new ArgumentNullException(nameof(destinationType))
                : destinationType == typeof(string) || destinationType == typeof(int);

        /// <summary>
        /// Converts the specified enum value to a <see cref="string"/> or <see cref="int"/>
        /// based on the requested destination type.
        /// </summary>
        /// <param name="context">An optional format context.</param>
        /// <param name="culture">Culture information for the conversion.</param>
        /// <param name="value">The value to convert, expected to be an enum of the specified type.</param>
        /// <param name="destinationType">The target type for conversion (string or int).</param>
        /// <returns>The converted value, or <c>null</c> if the input is <c>null</c>.</returns>
        /// <exception cref="ArgumentException">
        /// Thrown if <paramref name="value"/> is not of the expected enum type, or
        /// if the destination type is unsupported.
        /// </exception>
        public override object ConvertTo(
            ITypeDescriptorContext context,
            CultureInfo culture,
            object value,
            Type destinationType)
        {
            if (value == null) return null;
            if (value.GetType() != EnumType)
                throw new ArgumentException($"value is expected to be of type {EnumType}.", nameof(value));
            if (destinationType == typeof(string))
            {
                string results;
                Attribute dna = LocalizedEnumTextAttribute.Get((Enum)value);
                if (dna == null)
                {
                    dna = EnumTextAttribute.Get((Enum)value);
                    results = dna == null
                        ? value.ToString()
                        : ((EnumTextAttribute)dna).EnumText;
                }
                else
                    results = ((LocalizedEnumTextAttribute)dna).GetLocalizedText(EnumType);

                return results;
            }
            else if (destinationType == typeof(int))
                return (int)value;
            return null;
        }

        /// <inheritdoc/>
        /// <remarks>
        /// Supports conversion from <see cref="string"/> or <see cref="int"/> to the target enum.
        /// </remarks>
        public override bool CanConvertFrom(ITypeDescriptorContext context, Type sourceType) =>
            sourceType == typeof(string) || sourceType == typeof(int);

        /// <summary>
        /// Converts from a string or integer to the corresponding enum value.
        /// </summary>
        /// <param name="context">An optional format context.</param>
        /// <param name="culture">Culture information for the conversion.</param>
        /// <param name="value">The string or integer to convert.</param>
        /// <returns>The parsed enum value.</returns>
        /// <exception cref="ArgumentException">
        /// Thrown if <paramref name="value"/> is neither a string nor an int, or cannot be parsed.
        /// </exception>
        public override object ConvertFrom(
            ITypeDescriptorContext context,
            CultureInfo culture,
            object value)
        {
            if (value == null) return null;
            if (value.GetType() == typeof(string))
            {
                foreach (var fi in EnumType.GetFields())
                {
                    var dna = (EnumTextAttribute)Attribute.GetCustomAttribute(fi, typeof(EnumTextAttribute));

                    if ((dna != null) && (string.Equals((string)value, dna.EnumText, StringComparison.Ordinal)))
                        return Enum.Parse(EnumType, fi.Name);
                }

                if (Enum.GetNames(EnumType).Contains(value))
                    return Enum.Parse(EnumType, (string)value);
                else
                {
                    var enums = Enum.GetValues(EnumType);
                    return enums.GetValue(0);
                }
            }
            else if (value.GetType() == typeof(int))
            {
                var s = Enum.GetName(EnumType, (int)value);
                var e = Enum.Parse(EnumType, s);
                return e;
            }

            throw new ArgumentException("The value is expected to be a string or an int.", nameof(value));
        }

        #region GetStandardValues ^^^^^^^^^^^^^^^^^^^^^^^^^

        /// <summary>
        /// Returns a collection of standard enum values for the associated type.
        /// </summary>
        /// <param name="context">A type descriptor context providing information about the component.</param>
        /// <returns>
        /// A <see cref="StandardValuesCollection"/> containing the enum values, or the base
        /// implementation if no enum type is detected.
        /// </returns>
        public override StandardValuesCollection GetStandardValues(ITypeDescriptorContext context)
        {
            // Return all enum values for the property
            if (context?.PropertyDescriptor != null)
            {
                var enumType = context.PropertyDescriptor.PropertyType;
                if (enumType.IsEnum)
                {
                    return new StandardValuesCollection(Enum.GetValues(enumType));
                }
            }

            return base.GetStandardValues(context);
        }

        #endregion
    }
}
