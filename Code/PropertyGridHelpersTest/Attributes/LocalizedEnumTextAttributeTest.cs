using PropertyGridHelpers.Attributes;
using Xunit;
using System.ComponentModel;
using PropertyGridHelpers.TypeDescriptors;
using PropertyGridHelpersTest.Enums;
using System.Reflection;

#if NET35
using Xunit.Extensions;
using System;
using System.Diagnostics;
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
#elif NET10_0
namespace PropertyGridHelpersTest.net100.Attributes
#endif
{
#if NET35
    /// <summary>
    /// Test for the Localized Enum Text Attribute
    /// </summary>
    public class LocalizedEnumTextAttributeTest
    {
#elif NET8_0_OR_GREATER
    /// <summary>
    /// Test for the Localized Enum Text Attribute
    /// </summary>
    /// <param name="output">system to use to output information to test runner</param>
    public class LocalizedEnumTextAttributeTest(ITestOutputHelper output)
    {
        private readonly ITestOutputHelper OutputHelper = output;
#else
    /// <summary>
    /// Test for the Localized Enum Text Attribute
    /// </summary>
    public class LocalizedEnumTextAttributeTest
    {
        private readonly ITestOutputHelper OutputHelper;
        /// <summary>
        /// Initializes a new instance of the <see cref="LocalizedEnumTextAttributeTest"/> class.
        /// </summary>
        /// <param name="output">system to use to output information to test runner</param>
        public LocalizedEnumTextAttributeTest(ITestOutputHelper output) => OutputHelper = output;
#endif

        #region Test Support objects ^^^^^^^^^^^^^^^^^^^^^^

        /// <summary>
        /// Provides missing values for testing purposes.
        /// </summary>
        public class TestClass
        {
            /// <summary>
            /// Represents a test enumeration with localized text attributes.
            /// </summary>
            public enum TestEnum
            {
                /// <summary>
                /// The one
                /// </summary>
                [LocalizedEnumText("SomeResourceKey")]
                [Description("This is One")]
                One,
                /// <summary>
                /// The two
                /// </summary>
                [Description("This is Two")]
                Two
            }

            /// <summary>
            /// Gets or sets the resource item.
            /// </summary>
            /// <value>
            /// The resource item.
            /// </value>
            public TestEnum ResourceItem { get; set; } = TestEnum.One;

            /// <summary>
            /// Gets or sets the item without attribute.
            /// </summary>
            /// <value>
            /// The item without attribute.
            /// </value>
            public TestEnum ItemWithoutAttribute { get; set; } = TestEnum.Two;

            /// <summary>
            /// The not enum field
            /// </summary>
            public string NotEnumField;

            /// <summary>
            /// The not an enum
            /// </summary>
            public const int NotAnEnum = 999;
        }

        #endregion

        #region Test routines ^^^^^^^^^^^^^^^^^^^^^^^^^^^^^

        /// <summary>
        /// Localized category attribute remembers resource key.
        /// </summary>
        [Fact]
        public void LocalizedEnumTextAttribute_Remembers_ResourceKey()
        {
            // Arrange
            const string Some_Resource_Key = "SOME_RESOURCE_KEY";

            // Act
            var attribute = new LocalizedEnumTextAttribute(Some_Resource_Key);

            // Assert
            Assert.NotNull(attribute);
#if NET35
            Assert.Equal(0, string.Compare(Some_Resource_Key, attribute.ResourceKey));
#else
            Assert.Equal(Some_Resource_Key, attribute.ResourceKey);
#endif
            Output($"The returned Enum Text resource key is: {attribute.ResourceKey}");
        }

        /// <summary>
        /// Gets the localized description attribute returns attribute if present.
        /// </summary>
        [Fact]
        public void GetLocalizedEnumTextAttribute_ReturnsAttribute_IfPresent()
        {
            // Arrange
            var instance = new TestClass();
            var propDesc = TypeDescriptor.GetProperties(instance)["ResourceItem"];
            var context = new CustomTypeDescriptorContext(propDesc, instance);

            // Act
            var attr = LocalizedEnumTextAttribute.Get(context);

            // Assert
            Output($"LocalizedEnumTextAttribute.ResourceKey: {attr?.ResourceKey}");
            Assert.NotNull(attr);
            Assert.NotEmpty(attr.ResourceKey);
        }

        /// <summary>
        /// Gets the localized description attribute returns null if not present.
        /// </summary>
        [Fact]
        public void GetLocalizedEnumTextAttribute_ReturnsNull_IfNotPresent()
        {
            // Arrange
            var instance = new TestClass();
            var propDesc = TypeDescriptor.GetProperties(instance)["OtherItem"];
            var context = new CustomTypeDescriptorContext(propDesc, null);

            // Act
            var attr = LocalizedEnumTextAttribute.Get(context);

            // Assert
            Assert.Null(attr);
            Output("Null was returned by the LocalizedEnumTextAttribute.Get call.");
        }

        /// <summary>
        /// Gets the localized category attribute returns null if no attribute.
        /// </summary>
        [Fact]
        public void GetLocalizedEnumTextAttribute_ReturnsNull_IfNoAttribute()
        {
            // Arrange
            var instance = new TestClass();
            var propDesc = TypeDescriptor.GetProperties(instance)["ItemWithoutAttribute"];
            var context = new CustomTypeDescriptorContext(propDesc, instance);

            // Act
            var attr = LocalizedEnumTextAttribute.Get(context);

            // Assert
            Assert.Null(attr);
            Output("Null was returned by the LocalizedEnumTextAttribute.Get call.");
        }

        /// <summary>
        /// Gets the localized enum text attribute returns null if null enum.
        /// </summary>
        [Fact]
        public void GetLocalizedEnumTextAttribute_ReturnsNull_IfNullEnum()
        {
            // Arrange
            TestEnum? test = null;

            // Act
            var attr = LocalizedEnumTextAttribute.Get(test);

            // Assert
            Assert.Null(attr);
            Output("Null was returned by the LocalizedEnumTextAttribute.Get call.");
        }

        /// <summary>
        /// Gets the returns null when field information is null.
        /// </summary>
        [Fact]
        public void Get_ReturnsNull_WhenFieldInfoIsNull()
        {
            var result = LocalizedEnumTextAttribute.Get((FieldInfo)null);
            Assert.Null(result);
        }

        /// <summary>
        /// Gets the returns null when enum parse fails.
        /// </summary>
        [Fact]
        public void Get_ReturnsNull_WhenEnumParseFails()
        {
            var fakeField = typeof(TestClass).GetField(nameof(TestClass.NotAnEnum)); // Field that is not part of an enum
            var result1 = LocalizedEnumTextAttribute.Get(fakeField);

            Assert.Null(result1);
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