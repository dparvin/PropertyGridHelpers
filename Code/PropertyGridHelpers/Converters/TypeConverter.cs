using System;
using System.ComponentModel;
using System.Globalization;

namespace PropertyGridHelpers.Converters
{
    /// <summary>
    /// Convert a type to another type.  Used for expandable objects.
    /// </summary>
    /// <typeparam name="T">type to convert to or from</typeparam>
    /// <seealso cref="ExpandableObjectConverter" />
    public class TypeConverter<T> : ExpandableObjectConverter, IDisposable
    {
        private bool disposedValue;

        /// <summary>
        /// Determines whether this instance can convert to the specified context.
        /// </summary>
        /// <param name="context">The context.</param>
        /// <param name="destinationType">Type of the destination.</param>
        /// <returns></returns>
        public override bool CanConvertTo(ITypeDescriptorContext context, Type destinationType) =>
            destinationType == typeof(T) || destinationType == typeof(string) || base.CanConvertTo(context, destinationType);

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
            Type destinationType)
        {
            if (destinationType == typeof(string) &&
                 value is T t)
            {
                var so = t;

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
        public override bool CanConvertFrom(ITypeDescriptorContext context, Type sourceType) =>
            sourceType != typeof(string) && base.CanConvertFrom(context, sourceType);

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
        // ~TypeConverter()
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
