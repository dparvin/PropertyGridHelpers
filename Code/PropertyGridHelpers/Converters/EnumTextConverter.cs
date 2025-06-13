using PropertyGridHelpers.Attributes;
using System;
using System.ComponentModel;
using System.Globalization;
using System.Linq;

namespace PropertyGridHelpers.Converters
{
    /// <summary>
    /// Enum Text Converter
    /// </summary>
    /// <remarks>
    /// This converter is used to display specialized text in the PropertyGrid
    /// where the text is tied to the elements of an Enum.  Use the
    /// <see cref="EnumTextAttribute" /> to attach the text to the Enum
    /// elements.
    /// </remarks>
    /// <seealso cref="EnumConverter" />
    /// <seealso cref="IDisposable" />
    public partial class EnumTextConverter : EnumConverter, IDisposable
    {
        private bool disposedValue;

        /// <summary>
        /// Initializes a new instance of the <see cref="EnumTextConverter" /> class.
        /// </summary>
        /// <param name="type">The type.</param>
        public EnumTextConverter(Type type) : base(type) { }

        /// <summary>
        /// Determines whether this instance can convert to the specified context.
        /// </summary>
        /// <param name="context">The context.</param>
        /// <param name="destinationType">Type of the destination.</param>
        /// <returns>
        ///   <c>true</c> if this instance can convert to the specified context; otherwise, <c>false</c>.
        /// </returns>
        /// <exception cref="ArgumentNullException">
        /// context
        /// or
        /// destinationType
        /// </exception>
        public override bool CanConvertTo(
            ITypeDescriptorContext context,
            Type destinationType) =>
            destinationType is null
                ? throw new ArgumentNullException(nameof(destinationType))
                : destinationType == typeof(string) || destinationType == typeof(int);

        /// <summary>
        /// Converts to an int or string from an enum.
        /// </summary>
        /// <param name="context">The context.</param>
        /// <param name="culture">The culture.</param>
        /// <param name="value">The value.</param>
        /// <param name="destinationType">Type of the destination.</param>
        /// <returns></returns>
        /// <exception cref="ArgumentException">value is expected to be of type {_enumType}. - value</exception>
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

        /// <summary>
        /// Determines whether this instance can convert from the specified context.
        /// </summary>
        /// <param name="context">The context.</param>
        /// <param name="sourceType">Type of the source.</param>
        /// <returns>
        ///   <c>true</c> if this instance can convert from the specified context; otherwise, <c>false</c>.
        /// </returns>
        public override bool CanConvertFrom(ITypeDescriptorContext context, Type sourceType) =>
            sourceType == typeof(string) || sourceType == typeof(int);

        /// <summary>
        /// Converts from String or int to Enum.
        /// </summary>
        /// <param name="context">The context.</param>
        /// <param name="culture">The culture.</param>
        /// <param name="value">The value.</param>
        /// <returns></returns>
        /// <exception cref="ArgumentException">The value is expected to be a string or an int. - value</exception>
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
        /// Gets the standard values.
        /// </summary>
        /// <param name="context">The context.</param>
        /// <returns></returns>
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

        /// <summary>
        /// Releases unmanaged and - optionally - managed resources.
        /// </summary>
        /// <param name="disposing"><c>true</c> to release both managed and unmanaged resources; <c>false</c> to release only unmanaged resources.</param>
        protected virtual void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                if (disposing)
                {
                    // TODO: dispose managed state (managed objects)
                }

                // TODO: free unmanaged resources (unmanaged objects) and override finalizer
                // TODO: set large fields to null
                disposedValue = true;
            }
        }

        // // TODO: override finalizer only if 'Dispose(bool disposing)' has code to free unmanaged resources
        // ~EnumTextConverter()
        // {
        //     // Do not change this code. Put cleanup code in 'Dispose(bool disposing)' method
        //     Dispose(disposing: false);
        // }

        /// <summary>
        /// Releases unmanaged and - optionally - managed resources.
        /// </summary>
        public void Dispose()
        {
            // Do not change this code. Put cleanup code in 'Dispose(bool disposing)' method
            Dispose(disposing: true);
            GC.SuppressFinalize(this);
        }
    }
}
