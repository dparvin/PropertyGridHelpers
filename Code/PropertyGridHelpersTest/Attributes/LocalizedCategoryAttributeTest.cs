﻿using PropertyGridHelpers.Attributes;
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
    /// Test for the Localized Category Attribute
    /// </summary>
    public class LocalizedCategoryAttributeTest
    {
#elif NET8_0_OR_GREATER
    /// <summary>
    /// Test for the Localized Category Attribute
    /// </summary>
    /// <param name="output">system to use to output information to test runner</param>
    public class LocalizedCategoryAttributeTest(ITestOutputHelper output)
    {
        private readonly ITestOutputHelper OutputHelper = output;
#else
    /// <summary>
    /// Test for the Localized Category Attribute
    /// </summary>
    public class LocalizedCategoryAttributeTest
    {
        private readonly ITestOutputHelper OutputHelper;
        /// <summary>
        /// Initializes a new instance of the <see cref="LocalizedCategoryAttributeTest"/> class.
        /// </summary>
        /// <param name="output">system to use to output information to test runner</param>
        public LocalizedCategoryAttributeTest(ITestOutputHelper output) => OutputHelper = output;
#endif

        private const string ResourceKey = "Category_TestCategory";
        private const string ResourceValue = "Test Category";
        private static readonly Type ResourceSource = typeof(Properties.Resources);

        /// <summary>
        /// Localized category attribute should return localized string.
        /// </summary>
        [Fact]
        public void LocalizedCategoryAttribute_ShouldReturnLocalizedString()
        {
            // Act
            var attribute = new LocalizedCategoryAttribute(ResourceKey, ResourceSource);

            // Assert
            Assert.NotNull(attribute);
#if NET35
            Assert.Equal(0, string.Compare(ResourceValue, attribute.Category));
#else
            Assert.Equal(ResourceValue, attribute.Category);
#endif
            Output($"The returned Category is: {attribute.Category}");
        }

        /// <summary>
        /// Localize category attribute invalid resource key should return key as fallback.
        /// </summary>
        [Fact]
        public void LocalizedCategoryAttribute_InvalidResourceKey_ShouldReturnKeyAsFallback()
        {
            // Arrange
            const string invalidKey = "Invalid_Key";

            // Act
            var attribute = new LocalizedCategoryAttribute(invalidKey, ResourceSource);

            // Assert
            Assert.NotNull(attribute);
#if NET35
            Assert.Equal(0, string.Compare(invalidKey, attribute.Category)); // Fallback behavior
#else
            Assert.Equal(invalidKey, attribute.Category); // Fallback behavior
#endif
            Output($"The returned Category is: {attribute.Category}");
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
