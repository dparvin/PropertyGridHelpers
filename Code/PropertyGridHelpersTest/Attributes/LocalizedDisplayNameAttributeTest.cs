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
    public class LocalizedDisplayNameAttributeTest
    {
#elif NET8_0_OR_GREATER
    /// <summary>
    /// Test for the Localized Description Attribute
    /// </summary>
    /// <param name="output">system to use to output information to test runner</param>
    public class LocalizedDisplayNameAttributeTest(ITestOutputHelper output)
    {
        private readonly ITestOutputHelper OutputHelper = output;
#else
    /// <summary>
    /// Test for the Localized Description Attribute
    /// </summary>
    public class LocalizedDisplayNameAttributeTest
    {
        private readonly ITestOutputHelper OutputHelper;
        /// <summary>
        /// Initializes a new instance of the <see cref="LocalizedDisplayNameAttributeTest"/> class.
        /// </summary>
        /// <param name="output">system to use to output information to test runner</param>
        public LocalizedDisplayNameAttributeTest(ITestOutputHelper output) => OutputHelper = output;
#endif
        private static readonly Type ResourceSource = typeof(Properties.Resources);

        /// <summary>
        /// Localized display name attribute should return localized string.
        /// </summary>
        [Fact]
        public void LocalizedDisplayNameAttribute_ShouldReturnLocalizedString()
        {
            // Arrange
            const string displayNameKey = "DisplayName_TestDisplayName";
            const string displayNameValue = "Test Display Name";

            // Act
            var attribute = new LocalizedDisplayNameAttribute(displayNameKey, ResourceSource);

            // Assert
            Assert.NotNull(attribute);
#if NET35
            Assert.Equal(0, string.Compare(displayNameValue, attribute.DisplayName));
#else
            Assert.Equal(displayNameValue, attribute.DisplayName);
#endif
            Output($"The returned DisplayName is: {attribute.DisplayName}");
        }

        /// <summary>
        /// Localize category attribute invalid resource key should return key as fallback.
        /// </summary>
        [Fact]
        public void LocalizedDisplayNameAttribute_InvalidResourceKey_ShouldReturnKeyAsFallback()
        {
            // Arrange
            const string invalidKey = "Invalid_Key";

            // Act
            var attribute = new LocalizedDisplayNameAttribute(invalidKey, ResourceSource);

            // Assert
            Assert.NotNull(attribute);
#if NET35
            Assert.Equal(0, string.Compare(invalidKey, attribute.DisplayName)); // Fallback behavior
#else
            Assert.Equal(invalidKey, attribute.DisplayName); // Fallback behavior
#endif
            Output($"The returned DisplayName is: {attribute.DisplayName}");
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
