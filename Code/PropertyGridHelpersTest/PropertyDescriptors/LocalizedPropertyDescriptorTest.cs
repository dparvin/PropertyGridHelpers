using System;
using System.Reflection;
using Moq;
using Xunit;
using System.ComponentModel;
using PropertyGridHelpers.PropertyDescriptors;
using PropertyGridHelpers.Attributes;

#if NET35
#else
using Xunit.Abstractions;
#endif

#if NET35
namespace PropertyGridHelpersTest.net35.PropertyDescriptors
#elif NET452
namespace PropertyGridHelpersTest.net452.PropertyDescriptors
#elif NET462
namespace PropertyGridHelpersTest.net462.PropertyDescriptors
#elif NET472
namespace PropertyGridHelpersTest.net472.PropertyDescriptors
#elif NET481
namespace PropertyGridHelpersTest.net481.PropertyDescriptors
#elif NET8_0
namespace PropertyGridHelpersTest.net80.PropertyDescriptors
#elif NET9_0
namespace PropertyGridHelpersTest.net90.PropertyDescriptors
#elif NET10_0
namespace PropertyGridHelpersTest.net100.PropertyDescriptors
#endif
{
    /// <summary>
    /// Localized Property Descriptor Test
    /// </summary>
    public class LocalizedPropertyDescriptorTest
    {
#if NET35
        /// <summary>
        /// Initializes a new instance of the <see cref="LocalizedPropertyDescriptorTest"/> class.
        /// </summary>
        public LocalizedPropertyDescriptorTest()
        {
            mockBaseProperty = new Mock<PropertyDescriptor>(MockBehavior.Strict, "TestProperty", new Attribute[0]);

            // Set up the necessary members
            _ = mockBaseProperty.Setup(p => p.Name).Returns("TestProperty");
            _ = mockBaseProperty.Setup(p => p.CanResetValue(It.IsAny<object>())).Returns(true);
            _ = mockBaseProperty.Setup(p => p.ComponentType).Returns(typeof(object));
            _ = mockBaseProperty.Setup(p => p.IsReadOnly).Returns(false);
            _ = mockBaseProperty.Setup(p => p.PropertyType).Returns(typeof(string));

            // Add this setup to handle the Attributes property
            _ = mockBaseProperty.Setup(p => p.Attributes).Returns(new AttributeCollection(new Attribute[0]));

            descriptor = new LocalizedPropertyDescriptor(mockBaseProperty.Object);
        }
#else
        private readonly ITestOutputHelper OutputHelper;

        /// <summary>
        /// Localized Property Descriptor Test
        /// </summary>
        /// <param name="output">xunit output implementation</param>
        public LocalizedPropertyDescriptorTest(ITestOutputHelper output)
        {
            OutputHelper = output;
#if NET8_0_OR_GREATER
            mockBaseProperty = new Mock<PropertyDescriptor>(MockBehavior.Strict, "TestProperty", Array.Empty<Attribute>());
#else
            mockBaseProperty = new Mock<PropertyDescriptor>(MockBehavior.Strict, "TestProperty", new Attribute[0]);
#endif

            // Set up the necessary members
            _ = mockBaseProperty.Setup(p => p.Name).Returns("TestProperty");
            _ = mockBaseProperty.Setup(p => p.CanResetValue(It.IsAny<object>())).Returns(true);
            _ = mockBaseProperty.Setup(p => p.ComponentType).Returns(typeof(object));
            _ = mockBaseProperty.Setup(p => p.IsReadOnly).Returns(false);
            _ = mockBaseProperty.Setup(p => p.PropertyType).Returns(typeof(string));

            // Add this setup to handle the Attributes property
#if NET8_0_OR_GREATER
            _ = mockBaseProperty.Setup(p => p.Attributes).Returns(new AttributeCollection([]));
#else
            _ = mockBaseProperty.Setup(p => p.Attributes).Returns(new AttributeCollection(new Attribute[0]));
#endif

            descriptor = new LocalizedPropertyDescriptor(mockBaseProperty.Object);
        }
#endif

        #region Test Methods ^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^

        private readonly Mock<PropertyDescriptor> mockBaseProperty;
        private readonly LocalizedPropertyDescriptor descriptor;

        /// <summary>
        /// Categories the returns localized value when attribute exists.
        /// </summary>
        [Fact]
        public void Category_ReturnsLocalizedValue_WhenAttributeExists()
        {
            _ = mockBaseProperty.Setup(p => p.Category).Returns("DefaultCategory");
            _ = mockBaseProperty.Setup(p => p.Attributes[typeof(LocalizedCategoryAttribute)])
                .Returns(new LocalizedCategoryAttribute("ResourceKey"));

            var category = descriptor.Category;

#if NET8_0_OR_GREATER
            Assert.Equal("DefaultCategory", category);
#else
            Assert.Equal(0, string.Compare("DefaultCategory", category));
#endif
        }

        /// <summary>
        /// Descriptions the returns localized value when attribute exists.
        /// </summary>
        [Fact]
        public void Description_ReturnsLocalizedValue_WhenAttributeExists()
        {
            _ = mockBaseProperty.Setup(p => p.Description).Returns("DefaultDescription");
            _ = mockBaseProperty.Setup(p => p.Attributes[typeof(LocalizedDescriptionAttribute)])
                .Returns(new LocalizedDescriptionAttribute("ResourceKey"));

            var description = descriptor.Description;

            Output($"Description: {description}");
#if NET8_0_OR_GREATER
            Assert.Equal("DefaultDescription", description);
#else
            Assert.Equal(0, string.Compare("DefaultDescription", description));
#endif
        }

        /// <summary>
        /// Displays the name returns localized value when attribute exists.
        /// </summary>
        [Fact]
        public void DisplayName_ReturnsLocalizedValue_WhenAttributeExists()
        {
            _ = mockBaseProperty.Setup(p => p.DisplayName).Returns("DefaultDisplayName");
            _ = mockBaseProperty.Setup(p => p.Attributes[typeof(LocalizedDisplayNameAttribute)])
                .Returns(new LocalizedDisplayNameAttribute("ResourceKey"));

            var displayName = descriptor.DisplayName;

            Output($"DisplayName: {displayName}");
#if NET8_0_OR_GREATER
            Assert.Equal("DefaultDisplayName", displayName);
#else
            Assert.Equal(0, string.Compare("DefaultDisplayName", displayName));
#endif
        }

        /// <summary>
        /// Determines whether this instance [can reset value delegates to base property].
        /// </summary>
        [Fact]
        public void CanResetValue_DelegatesToBaseProperty()
        {
            _ = mockBaseProperty.Setup(p => p.CanResetValue(It.IsAny<object>())).Returns(true);

            var result = descriptor.CanResetValue(new object());

            Output($"CanResetValue: {result}");
            Assert.True(result);
        }

        /// <summary>
        /// Gets the value delegates to base property.
        /// </summary>
        [Fact]
        public void GetValue_DelegatesToBaseProperty()
        {
            var expectedValue = new object();
            _ = mockBaseProperty.Setup(p => p.GetValue(It.IsAny<object>())).Returns(expectedValue);

            var result = (object)descriptor.GetValue(new object());

            Output($"GetValue: {result}");
            Assert.Equal(expectedValue, result);
        }

        /// <summary>
        /// Determines whether is read only delegates to base property.
        /// </summary>
        [Fact]
        public void IsReadOnly_DelegatesToBaseProperty()
        {
            _ = mockBaseProperty.Setup(p => p.IsReadOnly).Returns(true);

            var result = descriptor.IsReadOnly;

            Output($"IsReadOnly: {result}");
            Assert.True(result);
        }

        /// <summary>
        /// Resets the value delegates to base property.
        /// </summary>
        [Fact]
        public void ResetValue_DelegatesToBaseProperty()
        {
            var component = new object();

            // Ensure the mock allows ResetValue to be called
            _ = mockBaseProperty.Setup(p => p.ResetValue(component));

            descriptor.ResetValue(component);

            mockBaseProperty.Verify(p => p.ResetValue(component), Times.Once);
        }

        /// <summary>
        /// Sets the value delegates to base property.
        /// </summary>
        [Fact]
        public void SetValue_DelegatesToBaseProperty()
        {
            var component = new object();
            var value = new object();

            // Ensure the mock allows SetValue to be called
            _ = mockBaseProperty.Setup(p => p.SetValue(component, value));

            descriptor.SetValue(component, value);

            mockBaseProperty.Verify(p => p.SetValue(component, value), Times.Once);
        }

        /// <summary>
        /// Should the serialize value delegates to base property.
        /// </summary>
        [Fact]
        public void ShouldSerializeValue_DelegatesToBaseProperty()
        {
            _ = mockBaseProperty.Setup(p => p.ShouldSerializeValue(It.IsAny<object>())).Returns(true);

            var result = descriptor.ShouldSerializeValue(new object());

            Output($"ShouldSerializeValue: {result}");
            Assert.True(result);
        }

        /// <summary>
        /// Gets the localized string returns localized string when resource is available.
        /// </summary>
        [Fact]
        public void GetLocalizedString_ReturnsLocalizedString_WhenResourceIsAvailable()
        {
            // Arrange
            var defaultValue = "DefaultValue";
            var localizedValue = "LocalizedValue";
            // Get the PropertyDescriptor from the test class
            var propertyDescriptor = TypeDescriptor.GetProperties(typeof(TestClass))["LocalizedProperty"];

            // Create the descriptor under test
            var descriptor = new LocalizedPropertyDescriptor(propertyDescriptor);

            // Use reflection to access the private GetLocalizedString<T> method
            var methodGetLocalizedString = typeof(LocalizedPropertyDescriptor)
                .GetMethod("GetLocalizedString", BindingFlags.NonPublic | BindingFlags.Instance)
                ?.MakeGenericMethod(typeof(LocalizedTextAttribute));

            // Act
#if NET8_0_OR_GREATER
            var result = methodGetLocalizedString?.Invoke(descriptor, [defaultValue]) as string;
#else
            var result = methodGetLocalizedString?.Invoke(descriptor, new object[] { defaultValue }) as string;
#endif

            // Assert
            Output($"Localized Value: {result}");
#if NET8_0_OR_GREATER
            Assert.Equal(localizedValue, result);
#else
            Assert.Equal(0, string.Compare(localizedValue, result));
#endif
        }

        /// <summary>
        /// Gets the localized string returns default value when key does not exist.
        /// </summary>
        [Fact]
        public void GetLocalizedString_ReturnsDefaultValue_WhenKeyDoesNotExist()
        {
            // Arrange
            var defaultValue = "DefaultValue";
            var propertyDescriptor = TypeDescriptor.GetProperties(typeof(TestClass))["NonLocalizedProperty"];
            var descriptor = new LocalizedPropertyDescriptor(propertyDescriptor);

            // Use reflection to access the private GetLocalizedString<T> method
            var methodGetLocalizedString = typeof(LocalizedPropertyDescriptor)
                .GetMethod("GetLocalizedString", BindingFlags.NonPublic | BindingFlags.Instance)
                ?.MakeGenericMethod(typeof(LocalizedTextAttribute));

            // Act
#if NET8_0_OR_GREATER
            var result = methodGetLocalizedString?.Invoke(descriptor, [defaultValue]) as string;
#else
            var result = methodGetLocalizedString?.Invoke(descriptor, new object[] { defaultValue }) as string;
#endif

            // Assert
            Output($"Localized Value: {result}");
#if NET8_0_OR_GREATER
            Assert.Equal(defaultValue, result);
#else
            Assert.Equal(0, string.Compare(defaultValue, result));
#endif
        }

        /// <summary>
        /// Gets the localized string returns default value when resource file is missing.
        /// </summary>
        [Fact]
        public void GetLocalizedString_ReturnsDefaultValue_WhenResourceFileIsMissing()
        {
            // Arrange
            var defaultValue = "DefaultValue";
            var propertyDescriptor = TypeDescriptor.GetProperties(typeof(TestClass2))["LocalizedProperty"];

            // Use reflection to inject the fake resource path (if applicable)
            var descriptor = new LocalizedPropertyDescriptor(propertyDescriptor);

            // Use reflection to access the private GetLocalizedString<T> method
            var methodGetLocalizedString = typeof(LocalizedPropertyDescriptor)
                .GetMethod("GetLocalizedString", BindingFlags.NonPublic | BindingFlags.Instance)
                ?.MakeGenericMethod(typeof(LocalizedTextAttribute));

            // Act
#if NET8_0_OR_GREATER
            var result = methodGetLocalizedString?.Invoke(descriptor, [defaultValue]) as string;
#else
            var result = methodGetLocalizedString?.Invoke(descriptor, new object[] { defaultValue }) as string;
#endif

            // Assert
            Output($"Localized Value: {result}");
#if NET8_0_OR_GREATER
            Assert.Equal(defaultValue, result);
#else
            Assert.Equal(0, string.Compare(defaultValue, result));
#endif
        }

        /// <summary>
        /// Gets the resource type from property returns custom assembly when resource assembly is set.
        /// </summary>
        [Fact]
        public void GetResourceTypeFromProperty_ReturnsCustomAssembly_WhenResourceAssemblyIsSet()
        {
            // Arrange
            string resourcePath = null;
            var propertyDescriptor = TypeDescriptor.GetProperties(typeof(TestClassWithResourceAssembly))["LocalizedProperty"];
            var descriptor = new LocalizedPropertyDescriptor(propertyDescriptor);

            // Act
            var result = descriptor.GetResourceTypeFromProperty(ref resourcePath);

            // Assert
            Assert.NotNull(result);
#if NET8_0_OR_GREATER
            Assert.Equal("PropertyGridHelpersTest", result.GetName().Name);
#else
            Assert.Equal(0, string.Compare("PropertyGridHelpersTest", result.GetName().Name));
#endif
        }

        /// <summary>
        /// Gets the resource type from property returns component assembly when resource assembly is empty.
        /// </summary>
        [Fact]
        public void GetResourceTypeFromProperty_ReturnsComponentAssembly_WhenResourceAssemblyIsEmpty()
        {
            // Arrange
            string resourcePath = null;
            var propertyDescriptor = TypeDescriptor.GetProperties(typeof(TestClassWithoutResourceAssembly))["LocalizedProperty"];
            var descriptor = new LocalizedPropertyDescriptor(propertyDescriptor);

            // Act
            var result = descriptor.GetResourceTypeFromProperty(ref resourcePath);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(typeof(TestClassWithoutResourceAssembly).Assembly, result);
        }

        /// <summary>
        /// Localizeds the property descriptor properties return expected values.
        /// </summary>
        [Fact]
        public void LocalizedPropertyDescriptor_Properties_ReturnExpectedValues()
        {
            // Arrange
            var propertyDescriptor = TypeDescriptor.GetProperties(typeof(TestClass))["LocalizedProperty"];
            var descriptor = new LocalizedPropertyDescriptor(propertyDescriptor);

            // Act
            var componentType = descriptor.ComponentType;
            var propertyType = descriptor.PropertyType;

            // Assert
            Assert.NotNull(componentType);
            Assert.Equal(typeof(TestClass), componentType);

            Assert.NotNull(propertyType);
            Assert.Equal(typeof(string), propertyType);
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
        #region test support classes ^^^^^^^^^^^^^^^^^^^^^^

        /// <summary>
        /// Test Class
        /// </summary>
        [ResourcePath("Properties.Resources")]
        private class TestClass
        {
            /// <summary>
            /// Gets or sets the localized property.
            /// </summary>
            /// <value>
            /// The localized property.
            /// </value>
            [LocalizedCategory("TestKey")]
            [LocalizedDescription("TestKey")]
            [LocalizedDisplayName("TestKey")]
            public string LocalizedProperty
            {
                get; set;
            }

            [LocalizedCategory("NoTestKey")]
            [LocalizedDescription("NoTestKey")]
            [LocalizedDisplayName("NoTestKey")]
            public string NonLocalizedProperty
            {
                get; set;
            }
        }

        /// <summary>
        /// Test Class
        /// </summary>
        [ResourcePath("NonExistent.Resources")]
        private class TestClass2
        {
            /// <summary>
            /// Gets or sets the localized property.
            /// </summary>
            /// <value>
            /// The localized property.
            /// </value>
            [LocalizedCategory("TestKey")]
            [LocalizedDescription("TestKey")]
            [LocalizedDisplayName("TestKey")]
            public string LocalizedProperty
            {
                get; set;
            }
        }

        [ResourcePath("Properties.Resources", resourceAssembly: "PropertyGridHelpersTest")]
        private class TestClassWithResourceAssembly
        {
            [LocalizedCategory("TestKey")]
            public string LocalizedProperty
            {
                get; set;
            }
        }

        [ResourcePath("Properties.Resources")]
        private class TestClassWithoutResourceAssembly
        {
            [LocalizedCategory("TestKey")]
            public string LocalizedProperty
            {
                get; set;
            }
        }
        #endregion
    }
}
