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
    /// Test for the Localized Description Attribute
    /// </summary>
    public class LocalizedDescriptionAttributeTest
    {
        private static readonly Type ResourceSource = typeof(PropertyGridHelpersTest.Properties.Resources);

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
        }
    }
}
