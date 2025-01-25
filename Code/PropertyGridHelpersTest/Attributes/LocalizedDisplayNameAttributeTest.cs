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
    public class LocalizedDisplayNameAttributeTest
    {
        private static readonly Type ResourceSource = typeof(PropertyGridHelpersTest.Properties.Resources);

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
        }
    }
}
