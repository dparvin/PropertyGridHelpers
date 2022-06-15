using PropertyGridHelpers.Attributes;
using System;
using System.ComponentModel;
using System.Globalization;
using System.Reflection;

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
    public class EnumTextConverter : EnumConverter, IDisposable
    {
        /// <summary>
        /// The enum type
        /// </summary>
        private readonly Type _enumType;
        private bool disposedValue;

        /// <summary>
        /// Initializes a new instance of the <see cref="EnumTextConverter" /> class.
        /// </summary>
        /// <param name="type">The type.</param>
        public EnumTextConverter(Type type)
            : base(type)
        {
            _enumType = type;
        }

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
            Type destinationType)
        {
            if (context is null)
            {
                throw new ArgumentNullException(nameof(context));
            }

            if (destinationType is null)
            {
                throw new ArgumentNullException(nameof(destinationType));
            }

            return destinationType == typeof(string) || destinationType == typeof(int);
        }

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
            if (value.GetType() != _enumType)
                throw new ArgumentException($"value is expected to be of type {_enumType}.", nameof(value));
            if (destinationType == typeof(string))
            {
                FieldInfo fi = _enumType.GetField(Enum.GetName(_enumType, value));
                EnumTextAttribute dna =
                        (EnumTextAttribute)Attribute.GetCustomAttribute(
                        fi, typeof(EnumTextAttribute));

                if (dna == null)
                    return value.ToString();
                else
                    return dna.EnumText;
            }
            else if (destinationType == typeof(int))
            {
                return (int)value;
            }
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
        public override bool CanConvertFrom(
            ITypeDescriptorContext context,
            Type sourceType)
        {
            return sourceType == typeof(string) || sourceType == typeof(int);
        }

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
            if (!(value.GetType() == typeof(string) ||
                  value.GetType() == typeof(int)))
                throw new ArgumentException("The value is expected to be a string or an int.", nameof(value));
            if (value.GetType() == typeof(string))
            {
                foreach (FieldInfo fi in _enumType.GetFields())
                {
                    EnumTextAttribute dna =
                            (EnumTextAttribute)Attribute.GetCustomAttribute(
                                fi, typeof(EnumTextAttribute));

                    if ((dna != null) && ((string)value == dna.EnumText))
                        return Enum.Parse(_enumType, fi.Name);
                }
            }
            else if (value.GetType() == typeof(int))
            {
                var s = Enum.GetName(_enumType, (int)value);
                var e = Enum.Parse(_enumType, s);
                return e;
            }

            return Enum.Parse(_enumType, (string)value);
        }

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

    /// <summary>
    /// Generic version of Enum Text Converter
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <seealso cref="System.ComponentModel.EnumConverter" />
    /// <seealso cref="System.IDisposable" />
    /// <remarks>
    /// This converter is used to display specialized text in the PropertyGrid
    /// where the text is tied to the elements of an Enum.  Use the
    /// <see cref="EnumTextAttribute" /> to attach the text to the Enum
    /// elements.
    /// It uses the specified enum type set as a generic type for the class.
    /// </remarks>
    /// <seealso cref="EnumConverter" />
    public class EnumTextConverter<T> : EnumTextConverter where T : Enum
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="EnumTextConverter" /> class.
        /// </summary>
        public EnumTextConverter()
            : base(typeof(T))
        {
        }
    }
}
