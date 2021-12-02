using PropertyGridHelpers.Attributes;
using PropertyGridHelpers.Converters;
using System;
using Xunit;
using System.IO;
#if NET35
#else
#if NET462 || NET472 || NET48 || NET5_0_OR_GREATER
using VerifyXunit;
using VerifyTests;
#endif
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
#elif NET5_0
namespace PropertyGridHelpersTest.net50.Converters
#elif NET6_0
namespace PropertyGridHelpersTest.net60.Converters
#endif
{
    /// <summary>
    ///
    /// </summary>
    public class EnumTextConverterTest
#if NET462 || NET472 || NET48 || NET5_0_OR_GREATER
        : VerifyBase
#endif
    {
#if NET35
#else
        readonly ITestOutputHelper OutputHelper;
#endif
#if NET462 || NET472 || NET48 || NET5_0_OR_GREATER
        VerifySettings verifySettings = null;
#endif

        /// <summary>
        /// Enum Text Converter Test
        /// </summary>
#if NET35
        public EnumTextConverterTest()
#else
        /// <param name="output"></param>
        public EnumTextConverterTest(ITestOutputHelper output)
#endif
#if NET462 || NET472 || NET48 || NET5_0_OR_GREATER
            : base()
#endif
        {
#if NET35
#else
            OutputHelper = output;
#endif
        }

        /// <summary>
        /// Converts from returns null with null entries test.
        /// </summary>
        [Fact]
        public void ConvertFromReturnsNullWithNullEntriesTest()
        {
            using (var converter = new EnumTextConverter<TestEnums>())
            {
                Assert.Null(converter.ConvertFrom(null));
                Output("ConvertFrom returned null as expected");
                VerifyObject(converter);
            }
        }

        /// <summary>
        /// Converts from returns values test.
        /// </summary>
        [Fact]
        public void ConvertFromReturnsStringValuesTest()
        {
            using (var converter = new EnumTextConverter<TestEnums>())
            {
                Assert.Equal(TestEnums.FirstEntry, converter.ConvertFrom("First Entry"));
                Output("ConvertFrom returned FirstEntry as expected");
            }
        }

        /// <summary>
        /// Converts from returns values test.
        /// </summary>
        [Fact]
        public void CanConvertFromStringTest()
        {
            using (var converter = new EnumTextConverter<TestEnums>())
            {
                Assert.True(converter.CanConvertFrom(null, typeof(string)));
                Output("CanConvertFrom returned true as expected");
            }
        }

        /// <summary>
        /// Converts from returns values test.
        /// </summary>
        [Fact]
        public void CanConvertFromIntTest()
        {
            using (var converter = new EnumTextConverter<TestEnums>())
            {
                Assert.True(converter.CanConvertFrom(null, typeof(int)));
                Output("CanConvertFrom returned true as expected");
            }
        }

        /// <summary>
        /// Converts from returns values test.
        /// </summary>
        [Fact]
        public void ConvertFromReturnsIntValuesTest()
        {
            using (var converter = new EnumTextConverter<TestEnums>())
            {
                Assert.Equal(TestEnums.FirstEntry, converter.ConvertFrom(1));
                Output("ConvertFrom returned FirstEntry as expected");
            }
        }

        /// <summary>
        /// Converts from returns null with null entries test.
        /// </summary>
        [Fact]
        public void ConvertToReturnsNullWithNullEntriesTest()
        {
            using (var converter = new EnumTextConverter<TestEnums>())
            {
                Assert.Null(converter.ConvertTo(null, null));
                Output("ConvertTo returned null as expected");
            }
        }

        /// <summary>
        /// Converts from returns values test.
        /// </summary>
        [Fact]
        public void ConvertToStringReturnsValuesTest()
        {
            using (var converter = new EnumTextConverter<TestEnums>())
            {
                Assert.Equal("First Entry", converter.ConvertTo(TestEnums.FirstEntry, typeof(string)));
                Output("ConvertTo returned 'First Entry' as expected");
            }
        }

        /// <summary>
        /// Converts from returns values test.
        /// </summary>
        [Fact]
        public void ConvertToIntReturnsValuesTest()
        {
            using (var converter = new EnumTextConverter<TestEnums>())
            {
                Assert.Equal(1, converter.ConvertTo(TestEnums.FirstEntry, typeof(int)));
                Output("ConvertTo returned 1 as expected");
            }
        }

        /// <summary>
        /// Enum to use in the tests to test flag
        /// </summary>
        [Flags]
        public enum TestEnums
        {
            /// <summary>
            /// The first entry
            /// </summary>
            [EnumText("First Entry")]
            FirstEntry = 1,
            /// <summary>
            /// The second entry
            /// </summary>
            [EnumText("Second Entry")]
            SecondEntry = 2,
            /// <summary>
            /// All entries
            /// </summary>
            [EnumText("All Entries")]
            AllEntries = FirstEntry + SecondEntry,
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

        private void VerifyObject(Object obj)
        {
#if NET462 || NET472 || NET48 || NET5_0_OR_GREATER
            verifySettings = new VerifySettings();
            verifySettings.UseDirectory(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "..\\..\\Verify\\PR\\"));
            Verify(obj, verifySettings);
#endif
        }
    }
}
