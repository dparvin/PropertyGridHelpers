using Xunit;
using System;
using PropertyGridHelpers.Converters;
#if NET35
#else
using Xunit.Abstractions;
#endif

#if NET35
namespace PropertyGridHelpersTest.net35.Converters
#elif NET452
namespace PropertyGridHelpersTest.net452.Converters
#elif NET48
namespace PropertyGridHelpersTest.net48.Converters
#endif
{
    public class TypeConverterTest
    {
#if NET35
#else
        readonly ITestOutputHelper Output;
        public TypeConverterTest(ITestOutputHelper output)

        {
            Output = output;
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
#if NET35
                Console.WriteLine("ConvertTo returned null as expected");
#else
                Output.WriteLine("ConvertTo returned null as expected");
#endif
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
#if NET35
                Console.WriteLine("ConvertTo returned '1' as expected");
#else
                Output.WriteLine("ConvertTo returned '1' as expected");
#endif
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
#if NET35
                Console.WriteLine("CanConvertTo returned True as expected");
#else
                Output.WriteLine("CanConvertTo returned True as expected");
#endif
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
#if NET35
                Console.WriteLine("CanConvertFrom returned False as expected");
#else
                Output.WriteLine("CanConvertFrom returned False as expected");
#endif
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
#if NET35
                Console.WriteLine("CanConvertTo returned False as expected");
#else
                Output.WriteLine("CanConvertTo returned False as expected");
#endif
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
#if NET35
                Console.WriteLine("CanConvertFrom returned False as expected");
#else
                Output.WriteLine("CanConvertFrom returned False as expected");
#endif
            }
        }
    }
}
