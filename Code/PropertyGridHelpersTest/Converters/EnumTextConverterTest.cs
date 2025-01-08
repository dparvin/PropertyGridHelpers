﻿using PropertyGridHelpers.Attributes;
using PropertyGridHelpers.Converters;
using PropertyGridHelpers.TypeDescriptors;
using System;
using System.ComponentModel;
using System.Linq;
using Xunit;

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
#if NET8_0_OR_GREATER
    /// <summary>
    ///
    /// </summary>
    /// <param name="OutputHelper">Test Output helper</param>
    public class EnumTextConverterTest(ITestOutputHelper OutputHelper)
#else
    /// <summary>
    ///
    /// </summary>
    public class EnumTextConverterTest
#endif
    {
#if NET35
#elif NET8_0_OR_GREATER
        private readonly ITestOutputHelper OutputHelper = OutputHelper;
#else
        private readonly ITestOutputHelper OutputHelper;
#endif

#if NET35
        /// <summary>
        /// Enum Text Converter Test
        /// </summary>
        public EnumTextConverterTest()
        {
        }
#elif NET8_0_OR_GREATER
#else
        /// <summary>
        /// Enum Text Converter Test
        /// </summary>
        /// <param name="output"></param>
        public EnumTextConverterTest(ITestOutputHelper output)
            : base() =>
            OutputHelper = output;
#endif

        /// <summary>
        /// Tests CanConvertTo method with various types
        /// </summary>
        [Fact]
        public void CanConvertToTest()
        {
            var converter = new EnumTextConverter<TestEnums>();

            // Test valid conversions
            Assert.True(converter.CanConvertTo(null, typeof(string)), "Should be able to convert to string");
            Assert.True(converter.CanConvertTo(null, typeof(int)), "Should be able to convert to int");

            // Test invalid conversions
            Assert.False(converter.CanConvertTo(null, typeof(double)), "Should not be able to convert to double");
            Assert.False(converter.CanConvertTo(null, typeof(object)), "Should not be able to convert to object unless explicitly allowed");

            // Test null destinationType
            var exception = Assert.Throws<ArgumentNullException>(() => converter.CanConvertTo(null, null));
            Assert.Equal("destinationType", exception.ParamName);
        }

        /// <summary>
        /// Converts from returns null with null entries test.
        /// </summary>
        [Fact]
        public void ConvertFromReturnsNullWithNullEntriesTest()
        {
#if NET5_0_OR_GREATER
            using var converter = new EnumTextConverter<TestEnums>();
#else
            using (var converter = new EnumTextConverter<TestEnums>())
            {
#endif
                Assert.Null(converter.ConvertFrom(null));
                Output("ConvertFrom returned null as expected");
#if NET5_0_OR_GREATER
#else
            }
#endif
        }

        /// <summary>
        /// Converts from returns values test.
        /// </summary>
        [Fact]
        public void ConvertFromReturnsStringValuesTest()
        {
#if NET5_0_OR_GREATER
            using var converter = new EnumTextConverter<TestEnums>();
#else
            using (var converter = new EnumTextConverter<TestEnums>())
            {
#endif
                Assert.Equal(TestEnums.FirstEntry, converter.ConvertFrom("First Entry"));
                Output("ConvertFrom returned FirstEntry as expected");
#if NET5_0_OR_GREATER
#else
            }
#endif
        }

        /// <summary>
        /// Converts from returns values test.
        /// </summary>
        [Fact]
        public void CanConvertFromStringTest()
        {
#if NET5_0_OR_GREATER
            using var converter = new EnumTextConverter<TestEnums>();
#else
            using (var converter = new EnumTextConverter<TestEnums>())
            {
#endif
                Assert.True(converter.CanConvertFrom(null, typeof(string)));
                Output("CanConvertFrom returned true as expected");
#if NET5_0_OR_GREATER
#else
            }
#endif
        }

        /// <summary>
        /// Converts from returns values test.
        /// </summary>
        [Fact]
        public void CanConvertFromIntTest()
        {
#if NET5_0_OR_GREATER
            using var converter = new EnumTextConverter<TestEnums>();
#else
            using (var converter = new EnumTextConverter<TestEnums>())
            {
#endif
                Assert.True(converter.CanConvertFrom(null, typeof(int)));
                Output("CanConvertFrom returned true as expected");
#if NET5_0_OR_GREATER
#else
            }
#endif
        }

        /// <summary>
        /// Converts from returns values test.
        /// </summary>
        [Fact]
        public void ConvertFromReturnsIntValuesTest()
        {
#if NET5_0_OR_GREATER
            using var converter = new EnumTextConverter<TestEnums>();
#else
            using (var converter = new EnumTextConverter<TestEnums>())
            {
#endif
                Assert.Equal(TestEnums.FirstEntry, converter.ConvertFrom(1));
                Output("ConvertFrom returned FirstEntry as expected");
#if NET5_0_OR_GREATER
#else
            }
#endif
        }

        /// <summary>
        /// Converts from returns null with null entries test.
        /// </summary>
        [Fact]
        public void ConvertToReturnsNullWithNullEntriesTest()
        {
#if NET5_0_OR_GREATER
            using var converter = new EnumTextConverter<TestEnums>();
#else
            using (var converter = new EnumTextConverter<TestEnums>())
            {
#endif
                Assert.Null(converter.ConvertTo(null, null));
                Output("ConvertTo returned null as expected");
#if NET5_0_OR_GREATER
#else
            }
#endif
        }

        /// <summary>
        /// Converts from returns values test.
        /// </summary>
        [Fact]
        public void ConvertToStringReturnsValuesTest()
        {
#if NET5_0_OR_GREATER
            using var converter = new EnumTextConverter<TestEnums>();
#else
            using (var converter = new EnumTextConverter<TestEnums>())
            {
#endif
                Assert.Equal("First Entry", converter.ConvertTo(TestEnums.FirstEntry, typeof(string)));
                Output("ConvertTo returned 'First Entry' as expected");
#if NET5_0_OR_GREATER
#else
            }
#endif
        }

        /// <summary>
        /// Converts from returns values test.
        /// </summary>
        [Fact]
        public void ConvertToIntReturnsValuesTest()
        {
#if NET5_0_OR_GREATER
            using var converter = new EnumTextConverter<TestEnums>();
#else
            using (var converter = new EnumTextConverter<TestEnums>())
            {
#endif
                Assert.Equal(1, converter.ConvertTo(TestEnums.FirstEntry, typeof(int)));
                Output("ConvertTo returned 1 as expected");
#if NET5_0_OR_GREATER
#else
            }
#endif
        }

        /// <summary>
        /// Tests the ConvertTo method for full coverage.
        /// </summary>
        [Fact]
        public void ConvertToTest()
        {
            var converter = new EnumTextConverter<TestEnums>();

            // Test null value
            Assert.Null(converter.ConvertTo(null, null, null, typeof(string)));

            // Test invalid type for value
            var ex = Assert.Throws<ArgumentException>(() =>
                converter.ConvertTo(null, null, 123.45, typeof(string)));
            Assert.Contains("TestEnums", ex.Message);

            // Test conversion to string with EnumTextAttribute
            Assert.Equal("First Entry", converter.ConvertTo(null, null, TestEnums.FirstEntry, typeof(string)));

            // Test conversion to string without EnumTextAttribute
            Assert.Equal("First Entry", converter.ConvertTo(null, null, (TestEnums)1, typeof(string)));

            // Test conversion to int
            Assert.Equal(1, converter.ConvertTo(null, null, TestEnums.FirstEntry, typeof(int)));

            // Test unsupported destinationType
            Assert.Null(converter.ConvertTo(null, null, TestEnums.FirstEntry, typeof(double)));
        }

        /// <summary>
        /// Tests the ConvertFrom method for full coverage.
        /// </summary>
        [Fact]
        public void ConvertFromTest()
        {
            var converter = new EnumTextConverter<TestEnums>();

            // Test null value
            Assert.Null(converter.ConvertFrom(null, null, null));

            // Test invalid type for value
            var ex = Assert.Throws<ArgumentException>(() =>
                converter.ConvertFrom(null, null, 123.45));
            Assert.Contains("string or an int", ex.Message);

            // Test conversion from string with EnumTextAttribute
            Assert.Equal(TestEnums.FirstEntry, converter.ConvertFrom(null, null, "First Entry"));

            // Test conversion from string without EnumTextAttribute
            Assert.Equal(TestEnums.None, (TestEnums)converter.ConvertFrom(null, null, "NonExistent"));

            // Test conversion from int
            Assert.Equal(TestEnums.FirstEntry, converter.ConvertFrom(null, null, 1));

            // Test conversion from invalid string (missing attribute and enum name mismatch)
            Assert.Equal(TestEnums.None, converter.ConvertFrom(null, null, "InvalidValue"));
        }

        /// <summary>
        /// Converts to string returns enum name when no attribute test.
        /// </summary>
        [Fact]
        public void ConvertToStringReturnsEnumNameWhenNoAttributeTest()
        {
#if NET5_0_OR_GREATER
            using var converter = new EnumTextConverter<TestEnums>();
#else
            using (var converter = new EnumTextConverter<TestEnums>())
            {
#endif
                var result = converter.ConvertTo(TestEnums.NoAttribute, typeof(string));
                Assert.Equal("NoAttribute", result);
                Output("ConvertTo returned the enum name as expected when no attribute is present");
#if NET5_0_OR_GREATER
#else
            }
#endif
        }

        /// <summary>
        /// Converts to string returns enum name when no attribute test.
        /// </summary>
        [Fact]
        public void ConvertFromStringReturnsEnumNameWhenNoAttributeTest()
        {
#if NET5_0_OR_GREATER
            using var converter = new EnumTextConverter<TestEnums>();
#else
            using (var converter = new EnumTextConverter<TestEnums>())
            {
#endif
                var result = converter.ConvertFrom("NoAttribute");
                Assert.Equal(TestEnums.NoAttribute, result);
                Output("ConvertTo returned the enum name as expected when no attribute is present");
#if NET5_0_OR_GREATER
#else
            }
#endif
        }

        /// <summary>
        /// Converts to string returns enum name when no attribute test.
        /// </summary>
        [Fact]
        public void ConvertFromIntReturnsEnumNameWhenNoAttributeTest()
        {
#if NET5_0_OR_GREATER
            using var converter = new EnumTextConverter<TestEnums>();
#else
            using (var converter = new EnumTextConverter<TestEnums>())
            {
#endif
                var result = converter.ConvertFrom(8);
                Assert.Equal(TestEnums.NoAttribute, result);
                Output("ConvertTo returned the enum name as expected when no attribute is present");
#if NET5_0_OR_GREATER
#else
            }
#endif
        }

        /// <summary>
        /// Converts from invalid string test.
        /// </summary>
        [Fact]
        public void ConvertFromInvalidStringTest()
        {
#if NET5_0_OR_GREATER
            using var converter = new EnumTextConverter<TestEnums>();
#else
            using (var converter = new EnumTextConverter<TestEnums>())
            {
#endif
                var exception = Assert.Throws<ArgumentException>(() => converter.ConvertFrom(10.2));
                Assert.Contains("expected to be a string or an int", exception.Message);
#if NET5_0_OR_GREATER
#else
            }
#endif
        }

        /// <summary>
        /// Converts to unsupported type test.
        /// </summary>
        [Fact]
        public void ConvertToUnsupportedTypeTest()
        {
#if NET5_0_OR_GREATER
            using var converter = new EnumTextConverter<TestEnums>();
#else
            using (var converter = new EnumTextConverter<TestEnums>())
            {
#endif
                Assert.Null(converter.ConvertTo(null, typeof(object)));
#if NET5_0_OR_GREATER
#else
            }
#endif
        }

        /// <summary>
        /// Tests GetStandardValues method to ensure it returns all enum values for the given context.
        /// </summary>
        [Fact]
        public void GetStandardValuesTest_WithCustomTypeDescriptorContext()
        {
            // Arrange
            var converter = new EnumTextConverter<TestEnums>();

            // Create a PropertyDescriptor for the enum type
            var PropertyDescriptor = TypeDescriptor.GetProperties(this)[nameof(TestProperty)];

            // Create the CustomTypeDescriptorContext instance
            var context = new CustomTypeDescriptorContext(PropertyDescriptor, null);

            // Act
            var standardValues = converter.GetStandardValues(context);

            // Assert
            Assert.NotNull(standardValues);
            Assert.Contains(TestEnums.FirstEntry, standardValues.Cast<TestEnums>());
            Assert.Contains(TestEnums.SecondEntry, standardValues.Cast<TestEnums>());
            Assert.Contains(TestEnums.AllEntries, standardValues.Cast<TestEnums>());
#if NET9_0_OR_GREATER
            Assert.Equal(Enum.GetValues<TestEnums>().Length, standardValues.Count);
#else
            Assert.Equal(Enum.GetValues(typeof(TestEnums)).Length, standardValues.Count);
#endif
        }

        /// <summary>
        /// Gets the standard values test not enum type property.
        /// </summary>
        [Fact]
        public void GetStandardValuesTest_NotEnumTypeProperty()
        {
            // Arrange
            var converter = new EnumTextConverter<TestEnums>();

            // Create a PropertyDescriptor for the enum type
            var PropertyDescriptor = TypeDescriptor.GetProperties(this)[nameof(StringTestProperty)];

            // Create the CustomTypeDescriptorContext instance
            var context = new CustomTypeDescriptorContext(PropertyDescriptor, null);

            // Act
            var standardValues = converter.GetStandardValues(context);

            // Assert
            Assert.NotNull(standardValues);
        }

        /// <summary>
        /// Gets the standard values test not enum type property.
        /// </summary>
        [Fact]
        public void GetStandardValuesTest_PropertyDescriptorInvalid()
        {
            // Arrange
            var converter = new EnumTextConverter<TestEnums>();

            // Create the CustomTypeDescriptorContext instance
            var context = new CustomTypeDescriptorContext(null, null);

            // Act
            var standardValues = converter.GetStandardValues(context);

            // Assert
            Assert.NotNull(standardValues);
        }

        /// <summary>
        /// Gets the standard values test not enum type property.
        /// </summary>
        [Fact]
        public void GetStandardValuesTest_ContextInvalid()
        {
            // Arrange
            var converter = new EnumTextConverter<TestEnums>();

            // Act
            var standardValues = converter.GetStandardValues(null);

            // Assert
            Assert.NotNull(standardValues);
        }
        /// <summary>
        /// Gets or sets the test property.
        /// </summary>
        /// <value>
        /// The test property.
        /// </value>
        public TestEnums TestProperty
        {
            get; set;
        }

        /// <summary>
        /// Gets or sets the string test property.
        /// </summary>
        /// <value>
        /// The string test property.
        /// </value>
        public string StringTestProperty
        {
            get; set;
        }

        /// <summary>
        /// Enum to use in the tests to test flag
        /// </summary>
        [Flags]
        public enum TestEnums
        {
            /// <summary>
            /// The none
            /// </summary>
            None = 0,
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
            /// <summary>
            /// The no attribute
            /// </summary>
            NoAttribute = 8, // Enum value without an EnumTextAttribute
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
