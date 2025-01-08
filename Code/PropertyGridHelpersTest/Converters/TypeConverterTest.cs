using Xunit;
using PropertyGridHelpers.Converters;
using System;

#if NET35
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
#elif NET481
namespace PropertyGridHelpersTest.net481.Converters
#elif WINDOWS7_0
namespace PropertyGridHelpersTest.net60.W7.Converters
#elif WINDOWS10_0
namespace PropertyGridHelpersTest.net60.W10.Converters
#elif NET8_0
namespace PropertyGridHelpersTest.net80.Converters
#elif NET9_0
namespace PropertyGridHelpersTest.net90.Converters
#endif
{
    /// <summary>
    /// Tests for the type converter
    /// </summary>
#if NET8_0_OR_GREATER
    public class TypeConverterTest(ITestOutputHelper output)
#else
    public class TypeConverterTest
#endif
    {
#if NET35
        /// <summary>
        /// Initializes a new instance of the <see cref="TypeConverterTest"/> class.
        /// </summary>
        public TypeConverterTest()
        {
        }
#elif NET8_0_OR_GREATER
        private readonly ITestOutputHelper OutputHelper = output;
#else
        private readonly ITestOutputHelper OutputHelper;
        /// <summary>
        /// Initializes a new instance of the <see cref="TypeConverterTest"/> class.
        /// </summary>
        /// <param name="output">Test Output object</param>
        public TypeConverterTest(ITestOutputHelper output) => OutputHelper = output;
#endif

        /// <summary>
        /// Converts to returns null with null entries test.
        /// </summary>
        [Fact]
        public void ConvertToReturnsEmptyStringWithNullEntriesTest()
        {
#if NET6_0_OR_GREATER
            using var converter = new TypeConverter<int>();
#else
            using (var converter = new TypeConverter<int>())
#endif
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
#if NET6_0_OR_GREATER
            using var converter = new TypeConverter<int>();
#else
            using (var converter = new TypeConverter<int>())
#endif
            {
                Assert.Equal("1", converter.ConvertTo(1, typeof(string)));
                Output("ConvertTo returned '1' as expected");
            }
        }

        /// <summary>
        /// Converts to returns string with different types test.
        /// </summary>
        [Fact]
        public void ConvertToReturnsStringWithDifferentTypesTest()
        {
#if NET6_0_OR_GREATER
            using var converter = new TypeConverter<int>();
#else
            using (var converter = new TypeConverter<int>())
#endif
            {
                Assert.Equal("1.1", converter.ConvertTo(1.1, typeof(string)));
                Output("ConvertTo returned '1.1' as expected");
            }
        }

        /// <summary>
        /// Convert to returns byte test.
        /// </summary>
        [Fact]
        public void ConvertToReturnsByteTest()
        {
#if NET6_0_OR_GREATER
            using var converter = new TypeConverter<int>();
#else
            using (var converter = new TypeConverter<int>())
#endif
            {
                Assert.Throws<NotSupportedException>(() => converter.ConvertTo(2, typeof(byte)));
                Output("ConvertTo throws an exception as expected");
            }
        }

        /// <summary>
        /// Determines whether this instance can convert to string test.
        /// </summary>
        [Fact]
        public void CanConvertToStringTest()
        {
#if NET6_0_OR_GREATER
            using var converter = new TypeConverter<int>();
#else
            using (var converter = new TypeConverter<int>())
#endif
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
#if NET6_0_OR_GREATER
            using var converter = new TypeConverter<int>();
#else
            using (var converter = new TypeConverter<int>())
#endif
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
#if NET6_0_OR_GREATER
            using var converter = new TypeConverter<int>();
#else
            using (var converter = new TypeConverter<int>())
#endif
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
#if NET6_0_OR_GREATER
            using var converter = new TypeConverter<int>();
#else
            using (var converter = new TypeConverter<int>())
#endif
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
        private static void Output(string message) =>
            Console.WriteLine(message);
#else
        private void Output(string message) =>
            OutputHelper.WriteLine(message);
#endif
    }
}
