using PropertyGridHelpers.Converters;
using System.ComponentModel;

#if NET8_0_OR_GREATER || NET462_OR_GREATER
using System;
#endif

namespace PropertyGridHelpersTest.Converters
{
    /// <summary>
    /// Test class for <see cref="OnlySelectableTypeConverter" />.
    /// </summary>
    /// <seealso cref="OnlySelectableTypeConverter" />
    public class OnlySelectableTestTypeConverter : OnlySelectableTypeConverter
    {
        /// <summary>
        /// Gets a list of resource paths.
        /// </summary>
        /// <param name="context">The context.</param>
        /// <returns></returns>
        public override StandardValuesCollection GetStandardValues(ITypeDescriptorContext context) =>
#if NET8_0_OR_GREATER 
            new(Array.Empty<string>());
#elif NET462_OR_GREATER
            new StandardValuesCollection(Array.Empty<string>());
#else
            new StandardValuesCollection(new string[0]);
#endif

    }
}
