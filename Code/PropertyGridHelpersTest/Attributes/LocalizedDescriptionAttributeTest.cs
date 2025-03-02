using PropertyGridHelpers.Attributes;
using Xunit;
using System;

#if NET35
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
    /// Test for the Localized Description Attribute
    /// </summary>
    public class LocalizedDescriptionAttributeTest
    {
#elif NET8_0_OR_GREATER
    /// <summary>
    /// Test for the Localized Description Attribute
    /// </summary>
    /// <param name="output">system to use to output information to test runner</param>
    public class LocalizedDescriptionAttributeTest(ITestOutputHelper output)
    {
        private readonly ITestOutputHelper OutputHelper = output;
#else
    /// <summary>
    /// Test for the Localized Description Attribute
    /// </summary>
    public class LocalizedDescriptionAttributeTest
    {
        private readonly ITestOutputHelper OutputHelper;
        /// <summary>
        /// Initializes a new instance of the <see cref="LocalizedDescriptionAttributeTest"/> class.
        /// </summary>
        /// <param name="output">system to use to output information to test runner</param>
        public LocalizedDescriptionAttributeTest(ITestOutputHelper output) => OutputHelper = output;
#endif

        private static readonly Type ResourceSource = typeof(Properties.Resources);

        /// <summary>
        /// Localized description attribute should return localized string.
        /// </summary>
        [Fact]
        public void LocalizedDescriptionAttribute_ShouldReturnLocalizedString()
        {
            // Arrange
            const string descriptionKey = "Description_TestDescription";
            const string descriptionValue = "Test Description";

            // Act
            var attribute = new LocalizedDescriptionAttribute(descriptionKey, ResourceSource);

            // Assert
            Assert.NotNull(attribute);
#if NET35
            Assert.Equal(0, string.Compare(descriptionValue, attribute.Description));
#else
            Assert.Equal(descriptionValue, attribute.Description);
#endif
            Output($"The returned Description is: {attribute.Description}");
        }

        /// <summary>
        /// Localize description attribute invalid resource key should return key as fallback.
        /// </summary>
        [Fact]
        public void LocalizedDescriptionAttribute_InvalidResourceKey_ShouldReturnKeyAsFallback()
        {
            // Arrange
            const string invalidKey = "Invalid_Key";

            // Act
            var attribute = new LocalizedDescriptionAttribute(invalidKey, ResourceSource);

            // Assert
            Assert.NotNull(attribute);
#if NET35
            Assert.Equal(0, string.Compare(invalidKey, attribute.Description)); // Fallback behavior
#else
            Assert.Equal(invalidKey, attribute.Description); // Fallback behavior
#endif
            Output($"The returned Description is: {attribute.Description}");
        }

        /// <summary>
        /// Localized description attribute remembers resource key.
        /// </summary>
        [Fact]
        public void LocalizedDescriptionAttribute_Remembers_ResourceKey()
        {
            // Arrange
            const string Some_Resource_Key = "SOME_RESOURCE_KEY";

            // Act
            var attribute = new LocalizedDescriptionAttribute(Some_Resource_Key, ResourceSource);

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
