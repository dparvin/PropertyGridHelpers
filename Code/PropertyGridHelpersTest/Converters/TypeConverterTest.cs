using Xunit;
using PropertyGridHelpers.Converters;
#if NET35
using System;
#else
using Xunit.Abstractions;
#endif

#if NET35
namespace PropertyGridHelpersTest.net35.Converters
#elif NET452
namespace PropertyGridHelpersTest.net452.Converters
#elif NET462
namespace PropertyGridHelpersTest.net462.Converters
#elif NET472
namespace PropertyGridHelpersTest.net472.Converters
#elif NET48
namespace PropertyGridHelpersTest.net48.Converters
#elif NET6_0
namespace PropertyGridHelpersTest.net60.Converters
#endif
{
    /// <summary>
    /// Tests for the type converter
    /// </summary>
    public class TypeConverterTest
    {
#if NET35
#else
        readonly ITestOutputHelper OutputHelper;
        /// <summary>
        /// 
        /// </summary>
        /// <param name="output"></param>
        public TypeConverterTest(ITestOutputHelper output)

        {
            OutputHelper = output;
        }
#endif

        /// <summary>
        /// Converts to returns null with null entries test.
        /// </summary>
        [Fact]
        public void ConvertToReturnsEmptyStringWithNullEntriesTest()
        {
            using (var converter = new TypeConverter<int>())
            {
                Assert.Equal(string.Empty, converter.ConvertTo(null, typeof(string)));
                Output("ConvertTo returned null as expected");
            }
        }

        /// <summary>
        /// Converts to returns string with int entries test.
        /// </summary>
        [Fact]
        public void ConvertToReturnsStringWithIntEntriesTest()
        {
            using (var converter = new TypeConverter<int>())
            {
                Assert.Equal("1", converter.ConvertTo(1, typeof(string)));
                Output("ConvertTo returned '1' as expected");
            }
        }

        /// <summary>
        /// Determines whether this instance can convert to string test.
        /// </summary>
        [Fact]
        public void CanConvertToStringTest()
        {
            using (var converter = new TypeConverter<int>())
            {
                Assert.True(converter.CanConvertTo(typeof(string)));
                Output("CanConvertTo returned True as expected");
            }
        }

        /// <summary>
        /// Determines whether this instance can convert to string test.
        /// </summary>
        [Fact]
        public void CanConvertFromStringTest()
        {
            using (var converter = new TypeConverter<int>())
            {
                Assert.False(converter.CanConvertFrom(typeof(string)));
                Output("CanConvertFrom returned False as expected");
            }
        }

        /// <summary>
        /// Determines whether this instance can convert to string test.
        /// </summary>
        [Fact]
        public void CanConvertToShortTest()
        {
            using (var converter = new TypeConverter<int>())
            {
                Assert.False(converter.CanConvertTo(typeof(short)));
                Output("CanConvertTo returned False as expected");
            }
        }

        /// <summary>
        /// Determines whether this instance can convert to string test.
        /// </summary>
        [Fact]
        public void CanConvertFromShortTest()
        {
            using (var converter = new TypeConverter<int>())
            {
                Assert.False(converter.CanConvertFrom(typeof(short)));
                Output("CanConvertFrom returned False as expected");
            }
        }

        /// <summary>
        /// Outputs the specified message.
        /// </summary>
        /// <param name="message">The message.</param>
#if NET35
        private static void Output(string message)
#else
        private void Output(string message)
#endif
        {
#if NET35
            Console.WriteLine(message);
#else
            OutputHelper.WriteLine(message);
#endif
        }
    }
}
