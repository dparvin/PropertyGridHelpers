// Ignore Spelling: Parsable

using Xunit;
using PropertyGridHelpers.Converters;
using System;
using System.Globalization;

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
#elif NET48
namespace PropertyGridHelpersTest.net480.Converters
#elif NET481
namespace PropertyGridHelpersTest.net481.Converters
#elif NET8_0
namespace PropertyGridHelpersTest.net80.Converters
#elif NET9_0
namespace PropertyGridHelpersTest.net90.Converters
#elif NET10_0
namespace PropertyGridHelpersTest.net100.Converters
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

        #region Test classes ^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^

        /// <summary>
        /// Test class for TypeConverter.
        /// </summary>
        /// <seealso cref="PropertyGridHelpers.Converters.IParsable{T}"/>
        public class CustomParsable : PropertyGridHelpers.Converters.IParsable<CustomParsable>
        {
            /// <summary>
            /// Gets or sets the value.
            /// </summary>
            /// <value>
            /// The value.
            /// </value>
            public string Value
            {
                get; set;
            }

            /// <summary>
            /// Converts the specified string representation into an instance of type <typeref name="CustomParsable"/>.
            /// </summary>
            /// <param name="s">The input string to parse.</param>
            /// <param name="provider">
            /// An optional format provider, which can be used to apply culture-specific or format-specific parsing
            /// rules.
            /// </param>
            /// <returns>
            /// An instance of <typeref name="CustomParsable"/> that represents the parsed string.
            /// </returns>
            public CustomParsable Parse(string s, IFormatProvider provider) =>
#if NET5_0_OR_GREATER
                new()
                {
                    Value = s
                };

#else
                new CustomParsable() { Value = s };
#endif

            /// <summary>
            /// Converts to string.
            /// </summary>
            /// <returns>
            /// A <see cref="string"/> that represents this instance.
            /// </returns>
            public override string ToString() => Value;
        }

        /// <summary>
        /// Test class for TypeConverter.
        /// </summary>
        public class PlainObject
        {
        }

        /// <summary>
        /// Test class for TypeConverter that implements the custom IParsable interface.
        /// </summary>
        /// <seealso cref="PropertyGridHelpers.Converters.IParsable{T}"/>
        public class FailingParsable : PropertyGridHelpers.Converters.IParsable<FailingParsable>
        {
            /// <summary>
            /// Converts the specified string representation into an instance of type <typeref name="FailingParsable"/>.
            /// </summary>
            /// <param name="s">The input string to parse.</param>
            /// <param name="provider">
            /// An optional format provider, which can be used to apply culture-specific or format-specific parsing
            /// rules.
            /// </param>
            /// <returns>
            /// An instance of <typeref name="FailingParsable"/> that represents the parsed string.
            /// </returns>
            /// <exception cref="System.InvalidOperationException">fail</exception>
            public FailingParsable Parse(string s, IFormatProvider provider) => throw new InvalidOperationException(
                "fail");
        }

        /// <summary>
        /// Test class for TypeConverter that does not have a default constructor.
        /// </summary>
        /// <seealso cref="PropertyGridHelpers.Converters.IParsable{T}"/>
        public class NoDefaultCtor : PropertyGridHelpers.Converters.IParsable<NoDefaultCtor>
        {
            /// <summary>
            /// Initializes a new instance of the <see cref="NoDefaultCtor"/> class.
            /// </summary>
            /// <param name="x">The x.</param>
            public NoDefaultCtor(string x)
            {
            }

            /// <summary>
            /// Converts the specified string representation into an instance of type <typeref name="NoDefaultCtor"/>.
            /// </summary>
            /// <param name="s">The input string to parse.</param>
            /// <param name="provider">
            /// An optional format provider, which can be used to apply culture-specific or format-specific parsing
            /// rules.
            /// </param>
            /// <returns>
            /// An instance of <typeref name="NoDefaultCtor"/> that represents the parsed string.
            /// </returns>
            public NoDefaultCtor Parse(string s, IFormatProvider provider) =>
#if NET5_0_OR_GREATER
                new("test");
#else
                new NoDefaultCtor("test");
#endif
        }

        #endregion

        #region Unit Tests ^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^

        /// <summary>
        /// Converts to returns null with null entries test.
        /// </summary>
        [Fact]
        public void ConvertToReturnsEmptyStringWithNullEntriesTest()
        {
            var converter = new TypeConverter<int>();
            Assert.Equal(string.Empty, converter.ConvertTo(null, typeof(string)));
            Output("ConvertTo returned null as expected");
        }

        /// <summary>
        /// Converts to returns string with int entries test.
        /// </summary>
        [Fact]
        public void ConvertToReturnsStringWithIntEntriesTest()
        {
            var converter = new TypeConverter<int>();
            Assert.Equal("1", converter.ConvertTo(1, typeof(string)));
            Output("ConvertTo returned '1' as expected");
        }

        /// <summary>
        /// Converts to returns string with different types test.
        /// </summary>
        [Fact]
        public void ConvertToReturnsStringWithDifferentTypesTest()
        {
            var converter = new TypeConverter<int>();
            Assert.Equal("1.1", converter.ConvertTo(1.1, typeof(string)));
            Output("ConvertTo returned '1.1' as expected");
        }

        /// <summary>
        /// Convert to returns byte test.
        /// </summary>
        [Fact]
        public void ConvertToReturnsByteTest()
        {
            var converter = new TypeConverter<int>();
            _ = Assert.Throws<NotSupportedException>(() => converter.ConvertTo(2, typeof(byte)));
            Output("ConvertTo throws an exception as expected");
        }

        /// <summary>
        /// Determines whether this instance can convert to string test.
        /// </summary>
        [Fact]
        public void CanConvertToStringTest()
        {
            var converter = new TypeConverter<int>();
            Assert.True(converter.CanConvertTo(typeof(string)));
            Output("CanConvertTo returned True as expected");
        }

        /// <summary>
        /// Determines whether this instance can convert to string test.
        /// </summary>
        [Fact]
        public void CanConvertFromStringTest()
        {
            var converter = new TypeConverter<TypeConverterTest>();
            Assert.False(converter.CanConvertFrom(typeof(string)));
            Output("CanConvertFrom returned False as expected");
        }

#if NET7_0_OR_GREATER
        /// <summary>
        /// Verifies that the TypeConverter for <c>int</c> reports it can convert from string, leveraging <see
        /// cref="System.IParsable{T}"/>.
        /// </summary>
        [Fact]
        public void CanConvertIntFromStringTest()
        {
            var converter = new TypeConverter<int>();
            Assert.True(converter.CanConvertFrom(typeof(string)));
            Output("CanConvertFrom returned True as expected");
        }
#endif

        /// <summary>
        /// Determines whether this instance can convert to string test.
        /// </summary>
        [Fact]
        public void CanConvertToShortTest()
        {
            var converter = new TypeConverter<int>();
            Assert.False(converter.CanConvertTo(typeof(short)));
            Output("CanConvertTo returned False as expected");
        }

        /// <summary>
        /// Determines whether this instance can convert to string test.
        /// </summary>
        [Fact]
        public void CanConvertFromShortTest()
        {
            var converter = new TypeConverter<int>();
            Assert.False(converter.CanConvertFrom(typeof(short)));
            Output("CanConvertFrom returned False as expected");
        }

#if NET7_0_OR_GREATER
        /// <summary>
        /// Verifies that the TypeConverter for <c>int</c> can convert from string
        /// </summary>
        [Fact]
        public void ConvertFrom_SystemIParsable_Int_Success()
        {
            var converter = new TypeConverter<int>();
            var result = converter.ConvertFrom(null, CultureInfo.InvariantCulture, "42");
            Assert.Equal(42, result);
        }

        /// <summary>
        /// Verifies that the TypeConverter for <c>int</c> throws a FormatException
        /// </summary>
        [Fact]
        public void ConvertFrom_SystemIParsable_Int_ParseFailure()
        {
            var converter = new TypeConverter<int>();
            _ = Assert.Throws<FormatException>(
                () => converter.ConvertFrom(null, CultureInfo.InvariantCulture, "NotANumber"));
        }
#endif

        /// <summary>
        /// Converts from custom iParsable success.
        /// </summary>
        [Fact]
        public void ConvertFrom_CustomIParsable_Success()
        {
            var converter = new TypeConverter<CustomParsable>();
            var result = converter.ConvertFrom(null, CultureInfo.InvariantCulture, "hello");
            _ = Assert.IsType<CustomParsable>(result);
#if NET35
            Assert.Equal(0, string.Compare("hello", ((CustomParsable)result).Value));
#else

            Assert.Equal("hello", ((CustomParsable)result).Value);
#endif
        }

        /// <summary>
        /// Converts from custom i parsable failure.
        /// </summary>
        [Fact]
        public void ConvertFrom_CustomIParsable_Failure()
        {
            var converter = new TypeConverter<FailingParsable>();
            _ = Assert.Throws<InvalidOperationException>(
                () => converter.ConvertFrom(null, CultureInfo.InvariantCulture, "anything"));
        }

        /// <summary>
        /// Converts from fallback to base throws.
        /// </summary>
        [Fact]
        public void ConvertFrom_FallbackToBase_Throws()
        {
            var converter = new TypeConverter<PlainObject>();
            _ = Assert.Throws<NotSupportedException>(
                () => converter.ConvertFrom(null, CultureInfo.InvariantCulture, "some string"));
        }

        /// <summary>
        /// Converts from activator create instance fails.
        /// </summary>
        [Fact]
        public void ConvertFrom_ActivatorCreateInstance_Fails()
        {
            var converter = new TypeConverter<NoDefaultCtor>();
            _ = Assert.Throws<MissingMethodException>(
                () => converter.ConvertFrom(null, CultureInfo.InvariantCulture, "test"));
        }

        /// <summary>
        /// Converts from null string returns base.
        /// </summary>
        [Fact]
        public void ConvertFrom_NullString_ReturnsBase()
        {
            var converter = new TypeConverter<int>();
            _ = Assert.Throws<NotSupportedException>(
                () => converter.ConvertFrom(null, CultureInfo.InvariantCulture, null));
        }

        /// <summary>
        /// Converts from non string input falls back.
        /// </summary>
        [Fact]
        public void ConvertFrom_NonStringInput_FallsBack()
        {
            var converter = new TypeConverter<int>();
            _ = Assert.Throws<NotSupportedException>(
                () => converter.ConvertFrom(null, CultureInfo.InvariantCulture, 123.45));
        }
        #endregion

        /// <summary>
        /// Outputs the specified message.
        /// </summary>
        /// <param name="message">The message.</param>
#if NET35
        private static void Output(string message) =>
            Console.WriteLine(message);
#else
        private void Output(string message) => OutputHelper.WriteLine(message);
#endif
    }
}
