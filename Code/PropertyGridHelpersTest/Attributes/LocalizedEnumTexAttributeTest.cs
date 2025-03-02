using PropertyGridHelpers.Attributes;
using Xunit;
using System;
using System.Threading;

#if NET35
using Xunit.Extensions;
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

        private const string ResourceKey = "Test_Item";
        private static readonly Type ResourceSource = typeof(Properties.Resources);

        /// <summary>
        /// Localized Enum Text attribute should return localized string.
        /// </summary>
        [Theory]
        [InlineData("Test Item", "en-US")]
        [InlineData("Élément de test", "fr-CA")]
        [InlineData("Élément de test", "fr")]
        [InlineData("Elemento de prueba", "es")]
        public void LocalizedEnumTextAttribute_ShouldReturnLocalizedString(string ResourceValue, string selectedCulture)
        {
            Thread.CurrentThread.CurrentCulture = new System.Globalization.CultureInfo(selectedCulture);
            // Act
            var attribute = new LocalizedEnumTextAttribute(ResourceKey, ResourceSource);

            // Assert
            Assert.NotNull(attribute);
#if NET35
            Assert.Equal(0, string.Compare(ResourceValue, attribute.EnumText));
#else
            Assert.Equal(ResourceValue, attribute.EnumText);
#endif
            Output($"The returned EnumText is: {attribute.EnumText}");
        }

        /// <summary>
        /// Localize Enum Text attribute invalid resource key should return key as fallback.
        /// </summary>
        [Fact]
        public void LocalizedEnumTextAttribute_InvalidResourceKey_ShouldReturnKeyAsFallback()
        {
            // Arrange
            const string invalidKey = "Invalid_Key";

            // Act
            var attribute = new LocalizedEnumTextAttribute(invalidKey, ResourceSource);

            // Assert
            Assert.NotNull(attribute);
#if NET35
            Assert.Equal(0, string.Compare(invalidKey, attribute.EnumText)); // Fallback behavior
#else
            Assert.Equal(invalidKey, attribute.EnumText); // Fallback behavior
#endif
            Output($"The returned EnumText is: {attribute.EnumText}");
        }

        /// <summary>
        /// Localized category attribute remembers resource key.
        /// </summary>
        [Fact]
        public void LocalizedEnumTextAttribute_Remembers_ResourceKey()
        {
            // Arrange
            const string Some_Resource_Key = "SOME_RESOURCE_KEY";

            // Act
            var attribute = new LocalizedEnumTextAttribute(Some_Resource_Key, ResourceSource);

            // Assert
            Assert.NotNull(attribute);
#if NET35
            Assert.Equal(0, string.Compare(Some_Resource_Key, attribute.ResourceKey));
#else
            Assert.Equal(Some_Resource_Key, attribute.ResourceKey); 
#endif
            Output($"The returned Category resource key is: {attribute.ResourceKey}");
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