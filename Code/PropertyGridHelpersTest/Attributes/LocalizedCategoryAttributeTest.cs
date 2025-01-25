using PropertyGridHelpers.Attributes;
using System.Linq;
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
    /// <summary>
    /// Test for the Localized Category Attribute
    /// </summary>
    public class LocalizedCategoryAttributeTest
    {
        private const string ResourceKey = "Category_TestCategory";
        private const string ResourceValue = "Test Category";
        private static readonly Type ResourceSource = typeof(PropertyGridHelpersTest.Properties.Resources);

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
        }
    }
}
