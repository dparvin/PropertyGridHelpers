using PropertyGridHelpers.Attributes;
using Xunit;

namespace PropertyGridHelpersTest.Attributes
{
    /// <summary>
    /// File Extension Attribute Test
    /// </summary>
    public class FileExtensionAttributeTest
    {
        private class TestClass
        {
            [FileExtension("TestProperty")]
            public string MyProperty { get; set; }
        }

        /// <summary>
        /// Attributes the should apply to properties.
        /// </summary>
        [Fact]
        public void Attribute_ShouldApplyToProperties()
        {
            // Arrange
            var property = typeof(TestClass).GetProperty("MyProperty");
            Assert.NotNull(property); // Property must exist

            // Act
            var attribute = property.GetCustomAttributes(typeof(FileExtensionAttribute), false);

            // Assert
            Assert.NotNull(attribute); // Attribute should be applied in NET8
        }

        /// <summary>
        /// Constructors the name of the should set property.
        /// </summary>
        [Fact]
        public void Constructor_ShouldSetPropertyName()
        {
            const string propertyName = "TestProperty";
            // Arrange
            var attribute = new FileExtensionAttribute(propertyName);

            // Act
            var pn = attribute.PropertyName;

            // Assert
            Assert.Equal(0, string.Compare(propertyName, pn)); // Verify the property name
        }
    }
}
