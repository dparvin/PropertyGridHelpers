using Xunit;
using PropertyGridHelpers.Attributes;
using System;
using PropertyGridHelpers.Converters;
#if NET35
#else
using Xunit.Abstractions;
#endif

namespace PropertyGridHelpersTest.Converters
{
    public class EnumTextConverterTest
    {
#if NET35
#else
        readonly ITestOutputHelper Output;
        public EnumTextConverterTest(ITestOutputHelper output)

        {
            Output = output;
        }
#endif

        /// <summary>
        /// Converts from returns null with null entries test.
        /// </summary>
        [Fact]
        public void ConvertFromReturnsNullWithNullEntriesTest()
        {
            using (var converter = new EnumTextConverter<TestEnum>())
            {
                Assert.Null(converter.ConvertFrom(null));
#if NET35
                Console.WriteLine("ConvertFrom returned null as expected");
#else
                Output.WriteLine("ConvertFrom returned null as expected");
#endif
            }
        }

        /// <summary>
        /// Converts from returns values test.
        /// </summary>
        [Fact]
        public void ConvertFromReturnsStringValuesTest()
        {
            using (var converter = new EnumTextConverter<TestEnum>())
            {
                Assert.Equal(TestEnum.FirstEntry, converter.ConvertFrom("First Entry"));
#if NET35
                Console.WriteLine("ConvertFrom returned FirstEntry as expected");
#else
                Output.WriteLine("ConvertFrom returned FirstEntry as expected");
#endif
            }
        }

        /// <summary>
        /// Converts from returns values test.
        /// </summary>
        [Fact]
        public void CanConvertFromStringTest()
        {
            using (var converter = new EnumTextConverter<TestEnum>())
            {
                Assert.True(converter.CanConvertFrom(null, typeof(string)));
#if NET35
                Console.WriteLine("CanConvertFrom returned true as expected");
#else
                Output.WriteLine("CanConvertFrom returned true as expected");
#endif
            }
        }

        /// <summary>
        /// Converts from returns values test.
        /// </summary>
        [Fact]
        public void CanConvertFromIntTest()
        {
            using (var converter = new EnumTextConverter<TestEnum>())
            {
                Assert.True(converter.CanConvertFrom(null, typeof(int)));
#if NET35
                Console.WriteLine("CanConvertFrom returned true as expected");
#else
                Output.WriteLine("CanConvertFrom returned true as expected");
#endif
            }
        }

        /// <summary>
        /// Converts from returns values test.
        /// </summary>
        [Fact]
        public void ConvertFromReturnsIntValuesTest()
        {
            using (var converter = new EnumTextConverter<TestEnum>())
            {
                Assert.Equal(TestEnum.FirstEntry, converter.ConvertFrom(1));
#if NET35
                Console.WriteLine("ConvertFrom returned FirstEntry as expected");
#else
                Output.WriteLine("ConvertFrom returned FirstEntry as expected");
#endif
            }
        }

        /// <summary>
        /// Converts from returns null with null entries test.
        /// </summary>
        [Fact]
        public void ConvertToReturnsNullWithNullEntriesTest()
        {
            using (var converter = new EnumTextConverter<TestEnum>())
            {
                Assert.Null(converter.ConvertTo(null, null));
#if NET35
                Console.WriteLine("ConvertTo returned null as expected");
#else
                Output.WriteLine("ConvertTo returned null as expected");
#endif
            }
        }

        /// <summary>
        /// Converts from returns values test.
        /// </summary>
        [Fact]
        public void ConvertToStringReturnsValuesTest()
        {
            using (var converter = new EnumTextConverter<TestEnum>())
            {
                Assert.Equal("First Entry", converter.ConvertTo(TestEnum.FirstEntry, typeof(string)));
#if NET35
                Console.WriteLine("ConvertTo returned null as expected");
#else
                Output.WriteLine("ConvertTo returned 'First Entry' as expected");
#endif
            }
        }

        /// <summary>
        /// Converts from returns values test.
        /// </summary>
        [Fact]
        public void ConvertToIntReturnsValuesTest()
        {
            using (var converter = new EnumTextConverter<TestEnum>())
            {
                Assert.Equal(1, converter.ConvertTo(TestEnum.FirstEntry, typeof(int)));
#if NET35
                Console.WriteLine("ConvertTo returned null as expected");
#else
                Output.WriteLine("ConvertTo returned 1 as expected");
#endif
            }
        }

        /// <summary>
        /// Enum to use in the tests to test flag
        /// </summary>
        [Flags]
        enum TestEnum
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
    }
}
