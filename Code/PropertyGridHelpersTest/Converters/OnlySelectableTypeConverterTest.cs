using Xunit;
using System.ComponentModel;
using PropertyGridHelpers.TypeDescriptors;
using System.Globalization;
using PropertyGridHelpers.Converters;
using System.Linq;
using System;
using PropertyGridHelpersTest.Converters;
using PropertyGridHelpers.Attributes;

#if NET35
using System.Collections.Generic;
using System.Diagnostics;
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
#elif NET8_0
namespace PropertyGridHelpersTest.net80.Converters
#elif NET9_0
namespace PropertyGridHelpersTest.net90.Converters
#endif
{
#if NET8_0_OR_GREATER
    /// <summary>
    /// Only Selectable Type Converter Test
    /// </summary>
    /// <param name="output">The output from the unit test.</param>
    public class OnlySelectableTypeConverterTest(ITestOutputHelper output)
#else
    /// <summary>
    /// Only Selectable Type Converter Test
    /// </summary>
    public class OnlySelectableTypeConverterTest
#endif
    {
#if NET35
#elif NET8_0_OR_GREATER
        private readonly ITestOutputHelper OutputHelper = output;
#elif NET40_OR_GREATER
        private readonly ITestOutputHelper OutputHelper;

        /// <summary>
        /// Initializes a new instance of the <see cref="OnlySelectableTypeConverterTest"/> class.
        /// </summary>
        /// <param name="output">The output from the unit test.</param>
        public OnlySelectableTypeConverterTest(
            ITestOutputHelper output) => OutputHelper = output;
#endif

        #region Test classes ^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^

        /// <summary>
        /// Test class with a property that has a OnlySelectableTypeConverter.
        /// </summary>
        public class TestClassWithResources
        {
            /// <summary>
            /// Gets or sets the resource property.
            /// </summary>
            /// <value>
            /// The resource property.
            /// </value>
            public string ResourceProperty
            {
                get; set;
            }
        }

        /// <summary>
        /// Test class with a property that has an <see cref="AllowBlankAttribute"/>.
        /// </summary>
        private class TestClassWithAllowBlank
        {
            /// <summary>
            /// Gets or sets the resource property.
            /// </summary>
            /// <value>
            /// The resource property.
            /// </value>
            [AllowBlank]
            public string ResourceProperty
            {
                get; set;
            }
        }

        /// <summary>
        /// Converts from blank value disallowed blank returns first standard value.
        /// </summary>
        [Fact]
        public void ConvertFrom_BlankValue_DisallowedBlank_ReturnsFirstStandardValue()
        {
            // Arrange
            var converter = new OnlySelectableTypeConverter();
            var instance = new TestClassWithResources();
            var propDesc = TypeDescriptor.GetProperties(instance)["ResourceProperty"];
            var context = new CustomTypeDescriptorContext(propDesc, instance);

            // Act
            var result = converter.ConvertFrom(context, CultureInfo.InvariantCulture, "");

            // Assert
            Output($"Result = '{result}'");
            Assert.NotNull(result); // should return the first resource name if present
        }

        /// <summary>
        /// Converts from blank value disallowed blank and no standard values throws.
        /// </summary>
        [Fact]
        public void ConvertFrom_BlankValue_DisallowedBlankAndNoStandardValues_Throws()
        {
            // Arrange
            var converter = new OnlySelectableTestTypeConverter();

            var instance = new TestClassWithResources(); // anonymous object with no resources
            var propDesc = TypeDescriptor.GetProperties(instance).Cast<PropertyDescriptor>().FirstOrDefault();
            var context = new CustomTypeDescriptorContext(propDesc, instance);

            // Act & Assert
            var ex = Assert.Throws<ArgumentException>(() =>
                converter.ConvertFrom(context, CultureInfo.InvariantCulture, "")
            );

            Assert.Contains("Value cannot be blank", ex.Message);
        }

        /// <summary>
        /// Converts from non blank value calls base and returns input.
        /// </summary>
        [Fact]
        public void ConvertFrom_NonBlankValue_CallsBaseAndReturnsInput()
        {
            // Arrange
            var converter = new OnlySelectableTypeConverter();
            var instance = new TestClassWithResources();
            var propDesc = TypeDescriptor.GetProperties(instance)["ResourceProperty"];
            var context = new CustomTypeDescriptorContext(propDesc, instance);

            // Act
            var result = converter.ConvertFrom(context, CultureInfo.InvariantCulture, "CustomInput");

            // Assert
            Assert.Equal("CustomInput", result);
        }

        /// <summary>
        /// Converts from non string value calls base convert from.
        /// </summary>
        [Fact]
        public void ConvertFrom_NonStringValue_CallsBaseConvertFrom()
        {
            // Arrange
            var converter = new OnlySelectableTypeConverter();
            var instance = new TestClassWithResources();
            var propDesc = TypeDescriptor.GetProperties(instance)["ResourceProperty"];
            var context = new CustomTypeDescriptorContext(propDesc, instance);

            // Act & Assert
            var ex = Assert.Throws<NotSupportedException>(() =>
                converter.ConvertFrom(context, CultureInfo.InvariantCulture, 123) // int triggers is-string == false
            );

            // Check the message returned in the excepton
            Assert.Contains("OnlySelectableTypeConverter cannot convert", ex.Message);
        }

        /// <summary>
        /// Converts from blank value allowed blank does not throw.
        /// </summary>
        [Fact]
        public void ConvertFrom_BlankValue_AllowedBlank_DoesNotThrow()
        {
            // Arrange
            var converter = new OnlySelectableTypeConverter();
            var instance = new TestClassWithAllowBlank();
            var propDesc = TypeDescriptor.GetProperties(instance)["ResourceProperty"];
            var context = new CustomTypeDescriptorContext(propDesc, instance);

            // Act
            var result = converter.ConvertFrom(context, CultureInfo.InvariantCulture, "");

            // Assert
            Assert.Equal("", result); // base.ConvertFrom should return the empty string
        }

        /// <summary>
        /// Gets the standard values returns expected resources.
        /// </summary>
        [Fact]
        public void GetStandardValues_ReturnsExpectedResources()
        {
            // Arrange
            var converter = new OnlySelectableTypeConverter();
            var instance = new TestClassWithResources();
            var propDesc = TypeDescriptor.GetProperties(instance)["ResourceProperty"];
            var context = new CustomTypeDescriptorContext(propDesc, instance);

            // Act
            var values = converter.GetStandardValues(context);

            // Assert
            var list = values.Cast<string>().ToList();
#if NET35
            Output($"Resources: {string.Join(", ", list.ToArray())}");
            ContainsIgnoreCaseSubstring(list, "Images");
#else
            Output($"Resources: {string.Join(", ", list)}");
#if NET5_0_OR_GREATER
            Assert.Contains(list, s => s.Contains("Images", StringComparison.OrdinalIgnoreCase));
#else
            Assert.Contains(list, s => s.Contains("Images"));
#endif
#endif
        }

        /// <summary>
        /// Gets the standard values context is null returns empty collection.
        /// </summary>
        [Fact]
        public void GetStandardValues_ContextIsNull_ReturnsEmptyCollection()
        {
            // Arrange
            var converter = new OnlySelectableTypeConverter();

            // Act
            var values = converter.GetStandardValues(null);

            // Assert
            Assert.Empty(values.Cast<string>());
        }

        /// <summary>
        /// Gets the standard values returns empty string with null instance.
        /// </summary>
        [Fact]
        public void GetStandardValues_ReturnsEmptyStringWithNullInstance()
        {
            // Arrange
            var converter = new OnlySelectableTypeConverter();
            TestClassWithResources instance = null;
            PropertyDescriptor propDesc = null;
            var context = new CustomTypeDescriptorContext(propDesc, instance);

            // Act
            var values = converter.GetStandardValues(context);

            // Assert
            var list = values.Cast<string>().ToList();
#if NET5_0_OR_GREATER
            Output($"values: {(list.Count == 0 ? "Empty Array" : string.Join(", ", list))}");
#else
            Output($"values: {(list.Count == 0 ? "Empty Array" : string.Join(", ", list.ToArray()))}");
#endif
            Assert.Empty(values.Cast<string>());
        }

        /// <summary>
        /// Gets the standard values supported and exclusive return true.
        /// </summary>
        [Fact]
        public void GetStandardValuesSupported_And_Exclusive_ReturnTrue()
        {
            var converter = new OnlySelectableTypeConverter();
            var resultSupported = converter.GetStandardValuesSupported(null);
            var resultExclusive = converter.GetStandardValuesExclusive(null);

            Assert.True(resultSupported);
            Assert.True(resultExclusive);
        }

        #endregion

#if NET35
        /// <summary>
        /// Determines whether contains ignore case substring the specified collection.
        /// </summary>
        /// <param name="collection">The collection.</param>
        /// <param name="substring">The substring.</param>
        public static void ContainsIgnoreCaseSubstring(IEnumerable<string> collection, string substring)
        {
            var found = false;
            foreach (var item in collection)
            {
                if (item != null && item.IndexOf(substring, StringComparison.OrdinalIgnoreCase) >= 0)
                {
                    found = true;
                    break;
                }
            }

            Assert.True(found, $"Expected a string containing '{substring}' (case-insensitive) in the collection.");
        }
#endif

        /// <summary>
        /// Outputs the specified message.
        /// </summary>
        /// <param name="message">The message.</param>
#if NET35
        private void Output(string message) => Debug.WriteLine(message);
#else
        private void Output(string message) => OutputHelper.WriteLine(message);
#endif
    }
}
