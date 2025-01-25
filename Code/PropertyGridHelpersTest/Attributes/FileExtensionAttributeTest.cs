using PropertyGridHelpers.Attributes;
using PropertyGridHelpers.TypeDescriptors;
using System.ComponentModel;
using Xunit;
using System;
using PropertyGridHelpersTest.Support;

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
    /// <summary>
    /// File Extension Attribute Test
    /// </summary>
#if NET8_0_OR_GREATER
    public class FileExtensionAttributeTest(ITestOutputHelper output)
    {
#else
    public class FileExtensionAttributeTest
    {
#endif
#if NET35
#elif NET8_0_OR_GREATER
        private readonly ITestOutputHelper OutputHelper = output;
#else
        private readonly ITestOutputHelper OutputHelper;

        /// <summary>
        /// Initializes a new instance of the <see cref="EnumImageAttributeTest"/> class.
        /// </summary>
        /// <param name="output">The output.</param>
        public FileExtensionAttributeTest(
            ITestOutputHelper output) => OutputHelper = output;
#endif

        private class TestClass
        {
            [FileExtension("TestProperty")]
            public string MyProperty
            {
                get; set;
            }
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
            Output("The Property name matched as expected");
        }

        /// <summary>
        /// Exists - the property exists.
        /// </summary>
        [Fact]
        public void Exists_PropertyExists()
        {
            var tc = new TestClass();
            var PropertyDescriptor = TypeDescriptor.GetProperties(tc)["MyProperty"];
            var context = new CustomTypeDescriptorContext(PropertyDescriptor, this);

            Assert.True(FileExtensionAttribute.Exists(context));
            Output("The property was found and had the attribute on it.");
        }

        /// <summary>
        /// Exists - the property exists.
        /// </summary>
        [Fact]
        public void Exists_PropertyDoesNotExists()
        {
            Assert.False(FileExtensionAttribute.Exists(null));
            Output("The property was not found as expected.");
        }

        /// <summary>
        /// Get should throw when property does not exist.
        /// </summary>
        [Fact]
        public void Get_ShouldThrow_WhenPropertyDoesNotExist()
        {
            //Arrange
            const string MissingPropertyName = "MyProperty2";
            var tc = new TestClass();
            var PropertyDescriptor = new FakePropertyDescriptor(MissingPropertyName, typeof(TestClass), typeof(string));
            var context = new CustomTypeDescriptorContext(PropertyDescriptor, this);

            // Act and Assert
            var ex = Assert.Throws<InvalidOperationException>(() => FileExtensionAttribute.Get(context));

            // Verify the exception message contains the expected details
#if NET35
            Assert.True(ex.Message.StartsWith($"Property '{MissingPropertyName}' not found on type"));
#else
            Assert.StartsWith($"Property '{MissingPropertyName}' not found on type", ex.Message);
#endif
            Output($"Exception thrown as expected: {ex.Message}");
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
