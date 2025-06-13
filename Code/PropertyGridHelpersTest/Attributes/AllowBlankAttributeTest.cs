using PropertyGridHelpers.Attributes;
using Xunit;
using System.ComponentModel;
using PropertyGridHelpers.TypeDescriptors;

#if NET35
using System.Diagnostics;
using Xunit.Extensions;
#else
using Xunit.Abstractions;
#endif

#if NET35
namespace PropertyGridHelpersTest.net35.Attributes
#elif NET452
namespace PropertyGridHelpersTest.net452.Attributes
#elif NET462
namespace PropertyGridHelpersTest.net462.Attributes
#elif NET472
namespace PropertyGridHelpersTest.net472.Attributes
#elif NET481
namespace PropertyGridHelpersTest.net481.Attributes
#elif NET8_0
namespace PropertyGridHelpersTest.net80.Attributes
#elif NET9_0
namespace PropertyGridHelpersTest.net90.Attributes
#endif
{
#if NET8_0_OR_GREATER
    /// <summary>
    /// Tests for the Allow Blank Attribute
    /// </summary>
    /// <param name="output">The output from the unit test.</param>
    public class AllowBlankAttributeTest(ITestOutputHelper output)
#else
    /// <summary>
    /// Tests for the Allow Blank Attribute
    /// </summary>
    public class AllowBlankAttributeTest
#endif
    {
#if NET35
#elif NET8_0_OR_GREATER
        private readonly ITestOutputHelper OutputHelper = output;
#elif NET40_OR_GREATER
        private readonly ITestOutputHelper OutputHelper;

        /// <summary>
        /// Initializes a new instance of the <see cref="AllowBlankAttributeTest"/> class.
        /// </summary>
        /// <param name="output">The output from the unit test.</param>
        public AllowBlankAttributeTest(
            ITestOutputHelper output) => OutputHelper = output;
#endif

        #region Test classes ^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^

        /// <summary>
        /// Test class with attribute.
        /// </summary>
        private class TestClassWithAttribute
        {
            /// <summary>
            /// Gets or sets the property with allow blank.
            /// </summary>
            /// <value>
            /// The property with allow blank.
            /// </value>
            [AllowBlank(allow: true, includeItem: true, resourceItem: "BlankItem")]
            public string PropertyWithAllowBlank
            {
                get; set;
            }

            /// <summary>
            /// Gets or sets the property without allow.
            /// </summary>
            /// <value>
            /// The property without allow.
            /// </value>
            [AllowBlank(allow: false)]
            public string PropertyWithoutAllow
            {
                get; set;
            }

            /// <summary>
            /// Gets or sets the property without attribute.
            /// </summary>
            /// <value>
            /// The property without attribute.
            /// </value>
            public string PropertyWithoutAttribute
            {
                get; set;
            }
        }

        /// <summary>
        /// Test class with resource path.
        /// </summary>
        [ResourcePath("FakeResourcePath")]
        private class TestClassWithResourcePath
        {
            /// <summary>
            /// Gets or sets the property with resource.
            /// </summary>
            /// <value>
            /// The property with resource.
            /// </value>
            [AllowBlank(allow: true, includeItem: true, resourceItem: "FakeResource")]
            public string PropertyWithResource
            {
                get; set;
            }
        }

        /// <summary>
        /// Test class with real resource path.
        /// </summary>
        [ResourcePath("Properties.Resources")]
        private class TestClassWithRealResourcePath
        {
            /// <summary>
            /// Gets or sets the property with resource.
            /// </summary>
            /// <value>
            /// The property with resource.
            /// </value>
            [AllowBlank(allow: true, includeItem: true, resourceItem: "Blank_Entry")]
            public string PropertyWithResource
            {
                get; set;
            } = "PropertyWithResource";

            /// <summary>
            /// Gets or sets the property with incorrect resource.
            /// </summary>
            /// <value>
            /// The property with incorrect resource.
            /// </value>
            [AllowBlank(allow: true, includeItem: true, resourceItem: "SomethingElse")]
            public string PropertyWithIncorrectResource
            {
                get; set;
            } = "PropertyWithIncorrectResource";

            [AllowBlank(allow: true, includeItem: true)]
            public string PropertyWithNoResource
            {
                get; set;
            } = "PropertyWithNoResource";

            /// <summary>
            /// Gets or sets the property dont include.
            /// </summary>
            /// <value>
            /// The property dont include.
            /// </value>
            [AllowBlank(allow: true, includeItem: false)]
            public string PropertyDoNotInclude
            {
                get; set;
            }
        }

        #endregion

        #region Test routines ^^^^^^^^^^^^^^^^^^^^^^^^^^^^^

        /// <summary>
        /// Constructors the defaults work correctly.
        /// </summary>
        [Fact]
        public void Constructor_Defaults_WorkCorrectly()
        {
            // Arrange
            // Act
            var attr = new AllowBlankAttribute();

            // Assert
            Assert.True(attr.Allow);
            Assert.False(attr.IncludeItem);
            Assert.Empty(attr.ResourceItem);
        }

        /// <summary>
        /// Constructors the with parameters sets properties.
        /// </summary>
        [Fact]
        public void Constructor_WithParameters_SetsProperties()
        {
            // Arrange
            // Act
            var attr = new AllowBlankAttribute(allow: false, includeItem: true, resourceItem: "Test");

            // Assert
            Assert.False(attr.Allow);
            Assert.True(attr.IncludeItem);
#if NET5_0_OR_GREATER
            Assert.Equal("Test", attr.ResourceItem);
#else
            Assert.Equal(0, string.Compare("Test", attr.ResourceItem, System.StringComparison.Ordinal));
#endif
        }

        /// <summary>
        /// Converts to string_returnsexpectedformat.
        /// </summary>
        [Fact]
        public void ToString_ReturnsExpectedFormat()
        {
            // Arrange
            var attr = new AllowBlankAttribute(allow: false, includeItem: true, resourceItem: "Label");

            // Act
            var result = attr.ToString();

            // Assert
#if NET5_0_OR_GREATER
            Assert.Equal("AllowBlank: False, IncludeItem: True, ResourceItem: 'Label'", result);
#else
            Assert.Equal(0, string.Compare("AllowBlank: False, IncludeItem: True, ResourceItem: 'Label'", result, System.StringComparison.Ordinal));
#endif
        }

        /// <summary>
        /// Gets the allow blank attribute returns attribute if present.
        /// </summary>
        [Fact]
        public void GetAllowBlankAttribute_ReturnsAttribute_IfPresent()
        {
            // Arrange
            var propDesc = TypeDescriptor.GetProperties(typeof(TestClassWithAttribute))["PropertyWithAllowBlank"];
            var context = new CustomTypeDescriptorContext(propDesc, this);

            // Act
            var attr = AllowBlankAttribute.Get(context);

            // Assert
            Assert.NotNull(attr);
            Assert.True(attr.Allow);
            Assert.True(attr.IncludeItem);
#if NET5_0_OR_GREATER
            Assert.Equal("BlankItem", attr.ResourceItem);
#else
            Assert.Equal(0, string.Compare("BlankItem", attr.ResourceItem, System.StringComparison.OrdinalIgnoreCase));
#endif
        }

        /// <summary>
        /// Gets the allow blank attribute returns null when no attribute.
        /// </summary>
        [Fact]
        public void GetAllowBlankAttribute_ReturnsNull_WhenNoAttribute()
        {
            // Arrange
            var instance = new TestClassWithAttribute();
            var propDesc = TypeDescriptor.GetProperties(instance)["PropertyWithoutAttribute"];
            var context = new CustomTypeDescriptorContext(propDesc, instance);

            // Act
            var attr = AllowBlankAttribute.Get(context);

            // Assert
            Assert.Null(attr);
        }

        /// <summary>
        /// Gets the allow blank attribute context is null returns null.
        /// </summary>
        [Fact]
        public void GetAllowBlankAttribute_ContextIsNull_ReturnsNull()
        {
            // Arrange
            ITypeDescriptorContext context = null;

            // Act
            var result = AllowBlankAttribute.Get(context);

            // Assert
            Assert.Null(result);
        }

        /// <summary>
        /// Determines whether [is blank allowed returns true when allow true].
        /// </summary>
        [Fact]
        public void IsBlankAllowed_ReturnsTrue_WhenAllowTrue()
        {
            // Arrange
            var propDesc = TypeDescriptor.GetProperties(typeof(TestClassWithAttribute))["PropertyWithAllowBlank"];
            var context = new CustomTypeDescriptorContext(propDesc, this);

            // Act
            var allowed = AllowBlankAttribute.IsBlankAllowed(context);

            // Assert
            Assert.True(allowed);
        }

        /// <summary>
        /// Determines whether [is blank allowed returns false when allow false].
        /// </summary>
        [Fact]
        public void IsBlankAllowed_ReturnsFalse_WhenAllowFalse()
        {
            // Arrange
            var propDesc = TypeDescriptor.GetProperties(typeof(TestClassWithAttribute))["PropertyWithoutAllow"];
            var context = new CustomTypeDescriptorContext(propDesc, this);

            // Act
            var allowed = AllowBlankAttribute.IsBlankAllowed(context);

            // Assert
            Assert.False(allowed);
        }

        /// <summary>
        /// Determines whether [is blank allowed returns false when no attribute].
        /// </summary>
        [Fact]
        public void IsBlankAllowed_ReturnsFalse_WhenNoAttribute()
        {
            // Arrange
            var propDesc = TypeDescriptor.GetProperties(typeof(TestClassWithAttribute))["PropertyWithoutAttribute"];
            var context = new CustomTypeDescriptorContext(propDesc, this);

            // Act
            var allowed = AllowBlankAttribute.IsBlankAllowed(context);

            // Assert
            Assert.False(allowed);
        }

        /// <summary>
        /// Gets the blank label returns empty when no context.
        /// </summary>
        [Fact]
        public void GetBlankLabel_ReturnsEmpty_WhenNoContext()
        {
            // Arrange
            // Act
            var label = AllowBlankAttribute.GetBlankLabel(null);

            // Assert
            Assert.Empty(label);
        }

        /// <summary>
        /// Gets the blank label returns empty when no attribute.
        /// </summary>
        [Fact]
        public void GetBlankLabel_ReturnsEmpty_WhenNoAttribute()
        {
            // Arrange
            var propDesc = TypeDescriptor.GetProperties(typeof(TestClassWithAttribute))["PropertyWithoutAttribute"];
            var context = new CustomTypeDescriptorContext(propDesc, this);

            // Act
            var label = AllowBlankAttribute.GetBlankLabel(context);

            // Assert
            Assert.Empty(label);
        }

        /// <summary>
        /// Gets the blank label returns none when include item true but no resource found.
        /// </summary>
        [Fact]
        public void GetBlankLabel_ReturnsNone_WhenIncludeItemTrue_ButNoResourceFound()
        {
            // Arrange
            var propDesc = TypeDescriptor.GetProperties(typeof(TestClassWithAttribute))["PropertyWithAllowBlank"];
            var context = new CustomTypeDescriptorContext(propDesc, this);

            // Act
            var label = AllowBlankAttribute.GetBlankLabel(context);

            Output($"Label: {label}");

            // Assert
#if NET5_0_OR_GREATER
            Assert.Equal("(none)", label);
#else
            Assert.Equal(0, string.Compare("(none)", label, System.StringComparison.OrdinalIgnoreCase));
#endif
        }

        /// <summary>
        /// Gets the blank label returns none when resource path is missing.
        /// </summary>
        [Fact]
        public void GetBlankLabel_ReturnsNone_WhenResourcePathIsMissing()
        {
            // Arrange
            var propDesc = TypeDescriptor.GetProperties(typeof(TestClassWithAttribute))["PropertyWithAllowBlank"];
            var context = new CustomTypeDescriptorContext(propDesc, this);

            // Act
            var label = AllowBlankAttribute.GetBlankLabel(context);

            Output($"Label: {label}");

            // Assert
#if NET5_0_OR_GREATER
            Assert.Equal("(none)", label);
#else
            Assert.Equal(0, string.Compare("(none)", label, System.StringComparison.OrdinalIgnoreCase));
#endif
        }

        /// <summary>
        /// Gets the blank label resource path attribute exists with invalid information returns none.
        /// </summary>
        /// <param name="propertyName">Name of the property.</param>
        /// <param name="expectedValue">The expected value.</param>
        [Theory]
        [InlineData("PropertyDoNotInclude", "")]
        [InlineData("PropertyWithIncorrectResource", "(none)")]
        [InlineData("PropertyWithNoResource", "(none)")]
        public void GetBlankLabel_ResourcePathAttributeExists_WithInvalidInformation_ReturnsExpected(
            string propertyName,
            string expectedValue)
        {
            // Arrange
            // Add an AllowBlankAttribute manually to the PropertyDescriptor
            var instance = new TestClassWithRealResourcePath();
            var propertyDescriptor = TypeDescriptor.GetProperties(instance)[propertyName];
            var context = new CustomTypeDescriptorContext(propertyDescriptor, instance);

            // Act
            var result = AllowBlankAttribute.GetBlankLabel(context);

            // Assert
            Output($"Results = '{result}'");
#if NET5_0_OR_GREATER
            Assert.Equal(expectedValue, result);
#else
            Assert.Equal(0, string.Compare(expectedValue, result, System.StringComparison.OrdinalIgnoreCase));
#endif
        }

        /// <summary>
        /// Gets the blank label resource path attribute exists with information returns localized value.
        /// </summary>
        [Fact]
        public void GetBlankLabel_ResourcePathAttributeExists_WithInformation_ReturnsLocalizedValue()
        {
            // Arrange
            // Add an AllowBlankAttribute manually to the PropertyDescriptor
            var instance = new TestClassWithRealResourcePath();
            var propertyDescriptor = TypeDescriptor.GetProperties(instance)["PropertyWithResource"];
            var context = new CustomTypeDescriptorContext(propertyDescriptor, instance);

            // Act
            var result = AllowBlankAttribute.GetBlankLabel(context);

            // Assert
            Output($"result = {result}");
#if NET5_0_OR_GREATER
            Assert.Equal("-- None --", result);
#else
            Assert.Equal(0, string.Compare("-- None --", result));
#endif
        }

        /// <summary>
        /// Gets the blank label invalid resource type triggers catch and returns fallback.
        /// </summary>
        [Fact]
        public void GetBlankLabel_InvalidResourceType_TriggersCatchAndReturnsFallback()
        {
            // Arrange
            var instance = new TestClassWithResourcePath();

            var propDesc = TypeDescriptor.GetProperties(instance)["PropertyWithResource"];
            var context = new CustomTypeDescriptorContext(propDesc, instance);

            // Act
            var result = AllowBlankAttribute.GetBlankLabel(context);

            // Assert
            Output($"Results = '{result}'");
#if NET5_0_OR_GREATER
            Assert.Equal("(none)", result);
#else
            Assert.Equal(0, string.Compare("(none)", result, System.StringComparison.OrdinalIgnoreCase));
#endif
        }

        #endregion

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
