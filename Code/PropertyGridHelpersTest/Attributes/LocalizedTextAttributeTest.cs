using PropertyGridHelpers.Attributes;
using PropertyGridHelpers.TypeDescriptors;
using System;
using System.ComponentModel;
using Xunit;
#if NET35
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
    /// Localized Text Attribute Test
    /// </summary>
    /// <param name="output">xunit output implementation</param>
    public class LocalizedTextAttributeTest(ITestOutputHelper output)
    {
#else
    /// <summary>
    /// Localized Text Attribute Test
    /// </summary>
    public class LocalizedTextAttributeTest
    {
#endif
#if NET35
#else
#if NET8_0_OR_GREATER
        private readonly ITestOutputHelper OutputHelper = output;
#else
        private readonly ITestOutputHelper OutputHelper;

        /// <summary>
        /// Localized Text Attribute Test
        /// </summary>
        /// <param name="output">xunit output implementation</param>
        public LocalizedTextAttributeTest(ITestOutputHelper output) => OutputHelper = output;
#endif
#endif

        #region Test Support objects ^^^^^^^^^^^^^^^^^^^^^^

#if NET8_0_OR_GREATER
        /// <summary>
        /// Test Localized Text Attribute
        /// </summary>
        /// <seealso cref="LocalizedTextAttribute" />
        /// <remarks>
        /// Initializes a new instance of the <see cref="TestLocalizedTextAttribute"/> class.
        /// </remarks>
        /// <param name="resourceKey">The resource key.</param>
        private class TestLocalizedTextAttribute(string resourceKey) : LocalizedTextAttribute(resourceKey)
        {
        }
#else
        /// <summary>
        /// Test Localized Text Attribute
        /// </summary>
        /// <seealso cref="LocalizedTextAttribute" />
        /// <remarks>
        /// Initializes a new instance of the <see cref="TestLocalizedTextAttribute"/> class.
        /// </remarks>
        private class TestLocalizedTextAttribute : LocalizedTextAttribute
        {
            /// <summary>
            /// Initializes a new instance of the <see cref="TestLocalizedTextAttribute"/> class.
            /// </summary>
            /// <param name="resourceKey">The resource key.</param>
            public TestLocalizedTextAttribute(string resourceKey) : base(resourceKey) { }
        }
#endif

        /// <summary>
        /// Test Class
        /// </summary>
        [ResourcePath("Properties.Resources")]
        private class TestClass
        {
            /// <summary>
            /// Gets or sets the test property.
            /// </summary>
            /// <value>
            /// The test property.
            /// </value>
            [TestLocalizedText("TestKey")]
            public string TestProperty
            {
                get; set;
            }
        }

        /// <summary>
        /// Test Class No Resource
        /// </summary>
        /// <remarks>
        /// Test class with NO ResourcePathAttribute
        /// </remarks>
        private class TestClassNoResource
        {
            /// <summary>
            /// Gets or sets the test property.
            /// </summary>
            /// <value>
            /// The test property.
            /// </value>
            public string TestProperty
            {
                get; set;
            }
        }

        /// <summary>
        /// Test Class Missing Resource
        /// </summary>
        /// <remarks>
        /// Test class with a ResourcePathAttribute whose value does not correspond to a valid resource type.
        /// </remarks>
        [ResourcePath("NonexistentResource")]
        private class TestClassMissingResource
        {
            /// <summary>
            /// Gets or sets the test property.
            /// </summary>
            /// <value>
            /// The test property.
            /// </value>
            public string TestProperty
            {
                get; set;
            }
        }

        #endregion

        #region Test routines ^^^^^^^^^^^^^^^^^^^^^^^^^^^^^

        /// <summary>
        /// Constructors the sets resource key.
        /// </summary>
        [Fact]
        public void Constructor_SetsResourceKey()
        {
            // Arrange & Act
            var attribute = new TestLocalizedTextAttribute("TestKey");

            // Assert
#if NET35
            Assert.Equal(0, string.Compare("TestKey", attribute.ResourceKey, StringComparison.OrdinalIgnoreCase));
#else
            Assert.Equal("TestKey", attribute.ResourceKey);
#endif
        }

        /// <summary>
        /// Gets the localized text returns localized string.
        /// </summary>
        [Fact]
        public void GetLocalizedText_ReturnsLocalizedString()
        {
            // Arrange
            var attribute = new TestLocalizedTextAttribute("TestKey");

            // Act
            var result = attribute.GetLocalizedText(typeof(TestClass));

            // Assert
            Output($"result = {result}");
#if NET35
            Assert.Equal(0, string.Compare("LocalizedValue", result, StringComparison.OrdinalIgnoreCase));
#else
            Assert.Equal("LocalizedValue", result);
#endif
        }

        /// <summary>
        /// Gets the localized text throws exception when resource path attribute missing.
        /// </summary>
        [Fact]
        public void GetLocalizedText_ThrowsException_WhenResourcePathAttributeMissing()
        {
            // Arrange: Create an attribute instance.
            var attribute = new TestLocalizedTextAttribute("TestKey");

            // Act & Assert: Expect an exception because TestClassNoResource doesn't have ResourcePathAttribute.
            var ex = Assert.Throws<InvalidOperationException>(() =>
                attribute.GetLocalizedText(typeof(TestClassNoResource)));
            Assert.Contains("ResourcePathAttribute not found", ex.Message);
        }

        /// <summary>
        /// Gets the localized text throws exception when resource type not found.
        /// </summary>
        [Fact]
        public void GetLocalizedText_ThrowsException_WhenResourceTypeNotFound()
        {
            // Arrange: Use a test class whose ResourcePath is invalid.
            var attribute = new TestLocalizedTextAttribute("TestKey");

            // Act & Assert: Expect an exception because the computed resource type won't be found.
            var ex = Assert.Throws<InvalidOperationException>(() =>
                attribute.GetLocalizedText(typeof(TestClassMissingResource)));
            Assert.Contains("Resource type", ex.Message);
            Assert.Contains("not found", ex.Message);
        }

        /// <summary>
        /// Gets the localized description attribute returns attribute if present.
        /// </summary>
        [Fact]
        public void GetLocalizedTextAttribute_ReturnsAttribute_IfPresent()
        {
            // Arrange
            var context = CustomTypeDescriptorContext.Create(typeof(TestClass), "TestProperty");

            // Act
            var attr = LocalizedTextAttribute.Get(context);

            // Assert
            Output($"LocalizedTextAttribute.ResourceKey: {attr?.ResourceKey}");
            Assert.NotNull(attr);
            Assert.NotEmpty(attr.ResourceKey);
        }

        /// <summary>
        /// Gets the localized description attribute returns null if not present.
        /// </summary>
        [Fact]
        public void GetLocalizedTextAttribute_ReturnsNull_IfNotPresent()
        {
            // Arrange
            var context = CustomTypeDescriptorContext.Create(typeof(TestClass), "OtherItem");

            // Act
            var attr = LocalizedTextAttribute.Get(context);

            // Assert
            Assert.Null(attr);
            Output("Null was returned by the LocalizedTextAttribute.Get call.");
        }

        /// <summary>
        /// Gets the localized category attribute returns null if no attribute.
        /// </summary>
        [Fact]
        public void GetLocalizedTextAttribute_ReturnsNull_IfNoAttribute()
        {
            // Arrange
            var context = CustomTypeDescriptorContext.Create(typeof(TestClass), "ItemWithoutAttribute");

            // Act
            var attr = LocalizedTextAttribute.Get(context);

            // Assert
            Assert.Null(attr);
            Output("Null was returned by the LocalizedTextAttribute.Get call.");
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
        private void Output(string message) =>
            OutputHelper.WriteLine(message);
#endif
    }
}
