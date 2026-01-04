// Ignore Spelling: Nullable

using PropertyGridHelpers.Attributes;
using PropertyGridHelpers.Enums;
using PropertyGridHelpers.TypeDescriptors;
using PropertyGridHelpersTest.Enums;
using System;
using System.ComponentModel;
using System.IO;
using Xunit;

#if NET35
using Xunit.Extensions;
#else
using Xunit.Abstractions;
using Xunit.Sdk;
#endif

#if NET35
namespace PropertyGridHelpersTest.net35.Support
#elif NET452
namespace PropertyGridHelpersTest.net452.Support
#elif NET462
namespace PropertyGridHelpersTest.net462.Support
#elif NET472
namespace PropertyGridHelpersTest.net472.Support
#elif NET481
namespace PropertyGridHelpersTest.net481.Support
#elif NET8_0
namespace PropertyGridHelpersTest.net80.Support
#elif NET9_0
namespace PropertyGridHelpersTest.net90.Support
#elif NET10_0
namespace PropertyGridHelpersTest.net100.Support
#endif
{
#if NET5_0_OR_GREATER
    /// <summary>
    /// Class for testing the ImageTextUIEditor class
    /// </summary>
    /// <param name="output">The output.</param>
    public class SupportTest(ITestOutputHelper output)
#else
    /// <summary>
    /// Tests for the <see cref="PropertyGridHelpers.Support.Support"/> class.
    /// </summary>
    public class SupportTest
#endif
    {
#if NET35
#elif NET5_0_OR_GREATER
        private readonly ITestOutputHelper OutputHelper = output;
#else
        private readonly ITestOutputHelper OutputHelper;
#endif

#if NET35
        /// <summary>
        /// Enum Text Converter Test
        /// </summary>
        public SupportTest()
        {
        }
#elif NET5_0_OR_GREATER
#else
        /// <summary>
        /// Enum Text Converter Test
        /// </summary>
        /// <param name="output"></param>
        public SupportTest(ITestOutputHelper output)
            : base() =>
            OutputHelper = output;
#endif

        #region GetEnumField Tests ^^^^^^^^^^^^^^^^^^^^^^^^

        /// <summary>
        /// Gets the enum field returns correct field information for valid enum value.
        /// </summary>
        [Fact]
        public void GetEnumField_ReturnsCorrectFieldInfo_ForValidEnumValue()
        {
            var field = PropertyGridHelpers.Support.Support.GetEnumField(TestEnum.confetti);

            Assert.NotNull(field);
#if NET35
            Assert.Equal(0, string.Compare(nameof(TestEnum.confetti), field.Name));
#else
            Assert.Equal(nameof(TestEnum.confetti), field.Name);
#endif
            Assert.Equal(typeof(TestEnum), field.DeclaringType);
        }

        /// <summary>
        /// Gets the enum field returns null for undefined enum value.
        /// </summary>
        [Fact]
        public void GetEnumField_ReturnsNull_ForUndefinedEnumValue()
        {
            var value = (TestEnum)999;

            var results = PropertyGridHelpers.Support.Support.GetEnumField(value);

            Assert.Null(results);
        }

        /// <summary>
        /// Gets the enum field throws when value is null.
        /// </summary>
        [Fact]
        public void GetEnumField_Throws_WhenValueIsNull() =>
            _ = Assert.Throws<ArgumentNullException>(() => PropertyGridHelpers.Support.Support.GetEnumField(null));

        #endregion

        #region GetFileExtension Tests ^^^^^^^^^^^^^^^^^^^^

        /// <summary>
        /// Gets the file extension should return file extension.
        /// </summary>
        [Fact]
        public void GetFileExtension_ShouldReturnFileExtension()
        {
            // Arrange
            var PropertyDescriptor = TypeDescriptor.GetProperties(this)[nameof(TestItemWithImage)];
            var context = new CustomTypeDescriptorContext(PropertyDescriptor, this);

            // Act
            var fileExtension = PropertyGridHelpers.Support.Support.GetFileExtension(context);
            // Assert
#if NET35
            Assert.Equal(0, string.Compare("jpg - jpeg file", fileExtension));
#else
            Assert.Equal("jpg - jpeg file", fileExtension);
#endif
        }

        /// <summary>
        /// Gets the file extension int property should return file extension.
        /// </summary>
        [Fact]
        public void GetFileExtension_IntProperty_ShouldReturnFileExtension()
        {
            // Arrange
            var PropertyDescriptor = TypeDescriptor.GetProperties(this)[nameof(TestItemWithInt)];
            var context = new CustomTypeDescriptorContext(PropertyDescriptor, this);

            // Act
            var fileExtension = PropertyGridHelpers.Support.Support.GetFileExtension(context);
            // Assert
#if NET35
            Assert.Equal(0, string.Compare(string.Empty, fileExtension));
#else
            Assert.Equal(string.Empty, fileExtension);
#endif
        }

        /// <summary>
        /// Gets the file extension should return file extension.
        /// </summary>
        [Fact]
        public void GetFileExtension_StringPropertyShouldReturnFileExtension()
        {
            // Arrange
            var PropertyDescriptor = TypeDescriptor.GetProperties(this)[nameof(TestItemWithBitmapImage)];
            var context = new CustomTypeDescriptorContext(PropertyDescriptor, this);

            // Act
            var fileExtension = PropertyGridHelpers.Support.Support.GetFileExtension(context);
            // Assert
#if NET35
            Assert.Equal(0, string.Compare("jpg", fileExtension));
#else
            Assert.Equal("jpg", fileExtension);
#endif
        }

        /// <summary>
        /// Gets the file extension should return file extension.
        /// </summary>
        [Fact]
        public void GetFileExtension_ExceptionForPropertyReferencingInvalidProperty()
        {
            // Arrange
            var PropertyDescriptor = TypeDescriptor.GetProperties(this)[nameof(TestItemWithResource)];
            var context = new CustomTypeDescriptorContext(PropertyDescriptor, this);

            // Act
            var ex = Assert.Throws<InvalidOperationException>(() => PropertyGridHelpers.Support.Support.GetFileExtension(context));
            // Assert

            Assert.Contains("Property 'invalidPropertName' not found on type 'PropertyGridHelpersTest.", ex.Message);
            Output(ex.Message);
        }

        /// <summary>
        /// Gets the file extension should return file extension.
        /// </summary>
        [Fact]
        public void GetFileExtension_ExceptionForPropertyReferencingPrivateProperty()
        {
            // Arrange
            var PropertyDescriptor = TypeDescriptor.GetProperties(this)[nameof(TestItemWithPrivateProperty)];
            var context = new CustomTypeDescriptorContext(PropertyDescriptor, this);

            // Act
            var ex = Assert.Throws<InvalidOperationException>(() => PropertyGridHelpers.Support.Support.GetFileExtension(context));
            // Assert
            Assert.Contains("Property 'PrivateImageFileExtension' on type 'PropertyGridHelpersTest.", ex.Message);
            Output(ex.Message);
        }

        /// <summary>
        /// Gets the file extension enum with custom enum text should return enum text.
        /// </summary>
        /// <param name="testExtension">The test extension.</param>
        /// <param name="expectedValue">The expected value.</param>
        [Theory]
        [InlineData(ImageFileExtension.jpg, "jpg - jpeg file")]
        [InlineData(ImageFileExtension.png, "png")]
        [InlineData(ImageFileExtension.None, "")]
        public void GetFileExtension_EnumWithCustomEnumText_ShouldReturnEnumText(
            ImageFileExtension testExtension,
            string expectedValue)
        {
            // Arrange
            ImageFileExtension = testExtension;

            var PropertyDescriptor = TypeDescriptor.GetProperties(this)[nameof(TestItemWithEnum)];
            var context = new CustomTypeDescriptorContext(PropertyDescriptor, this);

            // Act
            var fileExtension = PropertyGridHelpers.Support.Support.GetFileExtension(context);

            // Assert
#if NET35
            Assert.Equal(0, string.Compare(expectedValue, fileExtension));
#else
            Assert.Equal(expectedValue, fileExtension);
#endif
            Output(fileExtension);
        }

        /// <summary>
        /// Gets the file extension should return empty string when enum property is not enum instance.
        /// </summary>
        [Fact]
        public void GetFileExtension_ShouldReturnEmptyString_WhenEnumPropertyIsNotEnumInstance()
        {
            // Arrange
            ImageFileExtension = ImageFileExtension.png;
            var PropertyDescriptor = TypeDescriptor.GetProperties(this)[nameof(TestItemWithEnum)];
            var context = new CustomTypeDescriptorContext(PropertyDescriptor, this);

            // Act
            var result = PropertyGridHelpers.Support.Support.GetFileExtension(context);

            // Assert
#if NET35
            Assert.Equal(0, string.Compare(ImageFileExtension.ToString(), result));
#else
            Assert.Equal(ImageFileExtension.ToString(), result);
#endif
        }

        /// <summary>
        /// Gets the file extension should return empty string when enum property is null.
        /// </summary>
        [Fact]
        public void GetFileExtension_ShouldReturnEmptyString_WhenEnumPropertyIsNull()
        {
            // Arrange
            NullableImageFileExtension = null;
            var PropertyDescriptor = TypeDescriptor.GetProperties(this)[nameof(NullableTestItemWithEnum)];
            var context = new CustomTypeDescriptorContext(PropertyDescriptor, this);

            // Act
            var result = PropertyGridHelpers.Support.Support.GetFileExtension(context);

            // Assert
#if NET35
            Assert.Equal(0, string.Compare(String.Empty, result));
#else
            Assert.Equal(String.Empty, result);
#endif
        }

        #endregion

        #region GetFirstCustomAttribute ^^^^^^^^^^^^^^^^^^^

        /// <summary>
        /// Gets the first custom attribute throws argument null exception when member is null.
        /// </summary>
        [Fact]
        public void GetFirstCustomAttribute_ThrowsArgumentNullException_WhenMemberIsNull() =>
            Assert.Throws<ArgumentNullException>(() => PropertyGridHelpers.Support.Support.GetFirstCustomAttribute<AutoCompleteSetupAttribute>(null));

        /// <summary>
        /// Gets the first custom attribute returns attribute when present.
        /// </summary>
        [Fact]
        public void GetFirstCustomAttribute_ReturnsAttribute_WhenPresent()
        {
            var member = typeof(TestAttributeTarget).GetProperty(nameof(TestAttributeTarget.WithAttribute));

            var result = PropertyGridHelpers.Support.Support.GetFirstCustomAttribute<AutoCompleteSetupAttribute>(member);

            Assert.NotNull(result);
            Assert.Equal(PropertyGridHelpers.Attributes.AutoCompleteSetupAttribute.SourceMode.Values, result.Mode);
#if NET5_0_OR_GREATER
            Assert.Equal(["A", "B"], result.Values);
#else
            Assert.Equal(new[] { "A", "B" }, result.Values);
#endif
        }

        /// <summary>
        /// Gets the first custom attribute returns null when attribute not present.
        /// </summary>
        [Fact]
        public void GetFirstCustomAttribute_ReturnsNull_WhenAttributeNotPresent()
        {
            var member = typeof(TestAttributeTarget).GetProperty(nameof(TestAttributeTarget.WithoutAttribute));

            var result = PropertyGridHelpers.Support.Support.GetFirstCustomAttribute<AutoCompleteSetupAttribute>(member);

            Assert.Null(result);
        }

        #endregion

        #region GetPropertyInfo Tests ^^^^^^^^^^^^^^^^^^^^^

        /// <summary>
        /// Gets the property information throws argument null when context is null.
        /// </summary>
        [Fact]
        public void GetPropertyInfo_ThrowsArgumentNull_WhenContextIsNull() =>
            Assert.Throws<ArgumentNullException>(() => PropertyGridHelpers.Support.Support.GetPropertyInfo(null));

        /// <summary>
        /// Gets the property information throws argument exception when property descriptor is null.
        /// </summary>
        [Fact]
        public void GetPropertyInfo_ThrowsArgumentException_WhenPropertyDescriptorIsNull()
        {
            var context = new CustomTypeDescriptorContext(null, new TestClass());

            var ex = Assert.Throws<ArgumentException>(() => PropertyGridHelpers.Support.Support.GetPropertyInfo(context));
            Assert.Contains("PropertyDescriptor", ex.Message);
        }

        /// <summary>
        /// Gets the property information throws invalid operation when property not found.
        /// </summary>
        [Fact]
        public void GetPropertyInfo_ThrowsInvalidOperation_WhenPropertyNotFound()
        {
            var fakeDescriptor = TypeDescriptor.CreateProperty(typeof(TestClass), "MissingProperty", typeof(string));
            var context = new CustomTypeDescriptorContext(fakeDescriptor, new TestClass());

            var ex = Assert.Throws<InvalidOperationException>(() => PropertyGridHelpers.Support.Support.GetPropertyInfo(context));
            Assert.Contains("Property 'MissingProperty' not found", ex.Message);
        }

        /// <summary>
        /// Gets the property information returns property information when property exists.
        /// </summary>
        [Fact]
        public void GetPropertyInfo_ReturnsPropertyInfo_WhenPropertyExists()
        {
            var context = CustomTypeDescriptorContext.Create(typeof(TestClass), nameof(TestClass.TestEnum));

            var propInfo = PropertyGridHelpers.Support.Support.GetPropertyInfo(context);

            Assert.NotNull(propInfo);
#if NET35
            Assert.Equal(0, string.Compare(nameof(TestClass.TestEnum), propInfo.Name));
#else
            Assert.Equal(nameof(TestClass.TestEnum), propInfo.Name);
#endif
        }

        #endregion

        #region GetResourcesNames Tests ^^^^^^^^^^^^^^^^^^^

        /// <summary>
        /// Gets the resources names when enum type is null throws argument null exception.
        /// </summary>
        [Fact]
        public void GetResourcesNames_WhenEnumTypeIsNull_ThrowsArgumentNullException()
        {
            var ex = Assert.Throws<ArgumentNullException>(() => PropertyGridHelpers.Support.Support.GetResourcesNames(null));
            // Assert
            Assert.Contains("Value cannot be null.", ex.Message);
            Output(ex.Message);
        }

        /// <summary>
        /// Gets the resources names when enum type is not enum throws argument exception.
        /// </summary>
        [Fact]
        public void GetResourcesNames_WhenEnumTypeIsNotEnum_ThrowsArgumentException()
        {
            var ex = Assert.Throws<ArgumentException>(() => PropertyGridHelpers.Support.Support.GetResourcesNames(typeof(SupportTest)));
            // Assert
            Assert.Contains("The provided type must be an enum.", ex.Message);
            Output(ex.Message);
        }

        /// <summary>
        /// Gets the resources names when enum type is enum returns resource names.
        /// </summary>
        [Fact]
        public void GetResourcesNames_WhenEnumTypeIsEnum_ReturnsResourceNames()
        {
            var resourceNames = PropertyGridHelpers.Support.Support.GetResourcesNames(typeof(ImageFileExtension));
            Assert.NotNull(resourceNames);
            Output("Resource Names:");
            foreach (var resourceName in resourceNames)
            {
                Output($"  {resourceName}");
            }
        }

        /// <summary>
        /// Checks the resource type when assembly is null throws argument null exception.
        /// </summary>
        [Fact]
        public void CheckResourceType_WhenAssemblyIsNull_ThrowsArgumentNullException()
        {
            var ex = Assert.Throws<ArgumentNullException>(() => PropertyGridHelpers.Support.Support.CheckResourceType(null));
            // Assert
            Assert.Contains("Value cannot be null.", ex.Message);
            Output(ex.Message);
        }

        /// <summary>
        /// Checks the resource type when assembly is not null does not throw exception.
        /// </summary>
        [Fact]
        public void CheckResourceType_WhenAssemblyIsNotNull_DoesNotThrowException()
        {
            var stringWriter = new StringWriter();
            Console.SetOut(stringWriter);

            PropertyGridHelpers.Support.Support.CheckResourceType(typeof(SupportTest).Assembly);

            var output = stringWriter.ToString();
            Console.SetOut(new StreamWriter(Console.OpenStandardOutput()) { AutoFlush = true });
            Output(output);

            // Assert
            Assert.Contains("Checking resources in assembly:", output);
        }

        #endregion

        #region GetResourcePath Tests ^^^^^^^^^^^^^^^^^^^^^

        /// <summary>
        /// Gets the resource path should return resource path.
        /// </summary>
        /// <param name="PropertyName">Name of the property.</param>
        /// <param name="ExpectedPath">The expected path.</param>
        /// <param name="TestComment">The test comment.</param>
        [Theory]
        [InlineData(nameof(ImageFileExtension), "Properties.Resources", "Test with an Enum without a Resource Path attribute")]
        [InlineData(nameof(TestItemNonString), "Images", "Test where the Dynamic Resource Path option is pointing to a non string property")]
        [InlineData(nameof(TestItemWithBitmapImage), "TestItemWithBitmapImage.Resources", "Test with a fixed Resource Path")]
        [InlineData(nameof(TestItemWithImage), "TestResourcePath.Resources", "Test a DynamicResourcePath option")]
        [InlineData(nameof(TestItemWithResource), "Images", "Test with the Resource Path on the Enum")]
        [InlineData(nameof(TestPropertyPointingToMissingProperty), "Properties.Resources", "Test that has a dynamic path source that is pointing to an invalid property reference")]
        public void GetResourcePath_ShouldReturnResourcePath(
            string PropertyName,
            string ExpectedPath,
            string TestComment)
        {
            // Arrange
            var PropertyDescriptor = TypeDescriptor.GetProperties(this)[PropertyName];
            var context = new CustomTypeDescriptorContext(PropertyDescriptor, this);

            // Get the type of the property
            var propertyType = PropertyDescriptor.PropertyType;

            // Act
            var resourcePath = PropertyGridHelpers.Support.Support.GetResourcePath(context, propertyType);
            context.OnComponentChanged();

            // Assert
            Output(TestComment);
            Output(string.Empty);
            Output($"The resource path returned is '{resourcePath}'");
#if NET35
            Assert.Equal(0, string.Compare(ExpectedPath, resourcePath));
#else
            Assert.Equal(ExpectedPath, resourcePath);
#endif
            Assert.Null(context.Container);
            Assert.Null(context.GetService(typeof(IContainer)));
            Assert.True(context.OnComponentChanging());
        }

        /// <summary>
        /// Gets the resource path null property descriptor should return resource path.
        /// </summary>
        [Fact]
        public void GetResourcePath_NullPropertyDescriptor_ShouldReturnResourcePath()
        {
            // Arrange
            PropertyDescriptor PropertyDescriptor = null;
            var context = new CustomTypeDescriptorContext(PropertyDescriptor, this);

            // Act
            var resourcePath = PropertyGridHelpers.Support.Support.GetResourcePath(context, TestItemWithImage.GetType());
            context.OnComponentChanged();

            // Assert
            Output(resourcePath);
#if NET35
            Assert.Equal(0, string.Compare("Images", resourcePath));
#else
            Assert.Equal("Images", resourcePath);
#endif
            Assert.Null(context.Container);
            Assert.Null(context.GetService(typeof(IContainer)));
            Assert.True(context.OnComponentChanging());
        }

        /// <summary>
        /// Gets the resource path null context should return resource path.
        /// </summary>
        [Fact]
        public void GetResourcePath_NullContext_ShouldReturnResourcePath()
        {
            // Arrange
            CustomTypeDescriptorContext context = null;

            // Act
            var resourcePath = PropertyGridHelpers.Support.Support.GetResourcePath(context, TestItemWithImage.GetType());

            // Assert
            Output(resourcePath);
#if NET35
            Assert.Equal(0, string.Compare("Images", resourcePath));
#else
            Assert.Equal("Images", resourcePath);
#endif
        }

        /// <summary>
        /// Gets the resource path null property should return resource path.
        /// </summary>
        [Fact]
        public void GetResourcePath_NullProperty_ShouldReturnResourcePath()
        {
            // Arrange
            NullableTestItemWithEnum = null;
            var PropertyDescriptor = TypeDescriptor.GetProperties(this)[nameof(NullableTestItemWithEnum)];
            var context = new CustomTypeDescriptorContext(PropertyDescriptor, this);

            // Act
            var resourcePath = PropertyGridHelpers.Support.Support.GetResourcePath(context, PropertyDescriptor.PropertyType);

            // Assert
            Output(resourcePath);
#if NET35
            Assert.Equal(0, string.Compare("Images", resourcePath));
#else
            Assert.Equal("Images", resourcePath);
#endif
        }

        /// <summary>
        /// Gets the resource path not covered property should return default resource path.
        /// </summary>
        [Fact]
        public void GetResourcePath_NotCoveredProperty_ShouldReturnDefaultResourcePath()
        {
            // Arrange
            var PropertyDescriptor = TypeDescriptor.GetProperties(this)[nameof(TestResourcePathInt)];
            var context = new CustomTypeDescriptorContext(PropertyDescriptor, this);

            // Act
            var resourcePath = PropertyGridHelpers.Support.Support.GetResourcePath(context, PropertyDescriptor.PropertyType);

            // Assert
            Output(resourcePath);
#if NET35
            Assert.Equal(0, string.Compare("Properties.Resources", resourcePath));
#else
            Assert.Equal("Properties.Resources", resourcePath);
#endif
        }

        /// <summary>
        /// Gets the resource path returns null when dynamic path source points to missing property.
        /// </summary>
        [Fact]
        public void GetResourcePath_ReturnsNull_WhenDynamicPathSourcePointsToMissingProperty()
        {
            var instance = new TestClass();
            var propDesc = TypeDescriptor.GetProperties(instance)[nameof(TestClass.TestPropertyPointingToMissingProperty)];
            var context = new CustomTypeDescriptorContext(propDesc, instance);

            var result = PropertyGridHelpers.Support.Support.GetResourcePath(context, typeof(string), ResourceUsage.Strings);

#if NET35
            Assert.Equal(0, string.Compare("Properties.Resources", result)); // Falls back to default
#else
            Assert.Equal("Properties.Resources", result); // Falls back to default
#endif
        }

        #endregion

        #region GetResourceString Tests ^^^^^^^^^^^^^^^^^^^

        /// <summary>
        /// Gets the resource string throws argument null exception when resource source is null.
        /// </summary>
        [Fact]
        public void GetResourceString_ThrowsArgumentNullException_WhenResourceSourceIsNull()
        {
            // Arrange
            var key = "TestKey";

            // Act & Assert
            var ex = Assert.Throws<ArgumentNullException>(() =>
                PropertyGridHelpers.Support.Support.GetResourceString(key, null, null));

#if NET8_0_OR_GREATER
            Assert.Equal("resourceSource", ex.ParamName);
#else
            Assert.Equal("resourceSource", ex.ParamName, StringComparer.OrdinalIgnoreCase);
#endif
        }

        /// <summary>
        /// Gets the resource string throws argument null exception when resource key is null.
        /// </summary>
        [Fact]
        public void GetResourceString_ThrowsArgumentNullException_WhenResourceKeyIsNull()
        {
            // Arrange

            // Act & Assert
            var ex = Assert.Throws<ArgumentNullException>(() =>
                PropertyGridHelpers.Support.Support.GetResourceString("", null, null));

#if NET8_0_OR_GREATER
            Assert.Equal("resourceKey", ex.ParamName);
#else
            Assert.Equal("resourceKey", ex.ParamName, StringComparer.OrdinalIgnoreCase);
#endif
        }

        /// <summary>
        /// Gets the resource string returns key when resource key does not exist.
        /// </summary>
        [Fact]
        public void GetResourceString_ReturnsKey_WhenResourceKeyDoesNotExist()
        {
            // Arrange
            var missingKey = "ThisKeyDoesNotExist";
            var resourceSource = typeof(Properties.Resources); // replace with your known .resx class

            // Act
            var result = PropertyGridHelpers.Support.Support.GetResourceString(missingKey, null, resourceSource);

            // Assert
#if NET8_0_OR_GREATER
            Assert.Equal(missingKey, result);
#else
            Assert.Equal(missingKey, result, StringComparer.OrdinalIgnoreCase);
#endif
        }

        /// <summary>
        /// Gets the resource string returns key when missing manifest resource exception is thrown.
        /// </summary>
        [Fact]
        public void GetResourceString_ReturnsKey_WhenMissingManifestResourceExceptionIsThrown()
        {
            // Arrange
            var key = "AnyKey";
            var fakeResourceSource = typeof(PropertyGridHelpers.Support.Support); // does not have associated .resx
            PropertyGridHelpers.Support.Support.SetLanguage("es");

            // Act
            var result = PropertyGridHelpers.Support.Support.GetResourceString(key, null, fakeResourceSource);

            // Assert
#if NET8_0_OR_GREATER
            Assert.Equal(key, result);
#else
            Assert.Equal(key, result, StringComparer.OrdinalIgnoreCase);
#endif
        }

        #endregion

        #region Test objects ^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^

        /// <summary>
        /// Test class for testing purposes.
        /// </summary>
        public class TestClass
        {
            /// <summary>
            /// Gets or sets the test enum.
            /// </summary>
            /// <value>
            /// The test enum.
            /// </value>
            public TestEnum TestEnum { get; set; } = TestEnum.ItemWithImage;

            /// <summary>
            /// Gets or sets the test property pointing to missing property.
            /// </summary>
            /// <value>
            /// The test property pointing to missing property.
            /// </value>
            [DynamicPathSource("MissingProperty", ResourceUsage.Strings)]
            public string TestPropertyPointingToMissingProperty
            {
                get; set;
            }
        }

        /// <summary>
        /// Test attribute target class for testing
        /// </summary>
        private class TestAttributeTarget
        {
            /// <summary>
            /// Gets or sets the with attribute.
            /// </summary>
            /// <value>
            /// The with attribute.
            /// </value>
            [AutoCompleteSetup("A", "B")]
            public string WithAttribute
            {
                get; set;
            }

            /// <summary>
            /// Gets or sets the without attribute.
            /// </summary>
            /// <value>
            /// The without attribute.
            /// </value>
            public string WithoutAttribute
            {
                get; set;
            }
        }

        /// <summary>
        /// Gets or sets the test enum.
        /// </summary>
        /// <value>
        /// The test enum.
        /// </value>
        [DynamicPathSource(nameof(TestResourcePath))]
        [FileExtension(nameof(ImageFileExtension))]
        public TestEnum TestItemWithImage { get; set; } = TestEnum.ItemWithImage;

        /// <summary>
        /// Gets or sets the test enum.
        /// </summary>
        /// <value>
        /// The test enum.
        /// </value>
        [DynamicPathSource(nameof(TestResourcePathInt))]
        [FileExtension(nameof(ImageFileExtension))]
        public TestEnum TestItemNonString { get; set; } = TestEnum.ItemWithImage;

        /// <summary>
        /// Gets or sets the test property pointing to missing property.
        /// </summary>
        /// <value>
        /// The test property pointing to missing property.
        /// </value>
        [DynamicPathSource("MissingProperty", ResourceUsage.Strings)]
        public string TestPropertyPointingToMissingProperty
        {
            get; set;
        }

        /// <summary>
        /// Gets or sets the test item with bitmap image.
        /// </summary>
        /// <value>
        /// The test item with bitmap image.
        /// </value>
        [ResourcePath("TestItemWithBitmapImage.Resources")]
        [FileExtension(nameof(ImageFileExtensionString))]
        public TestEnum TestItemWithBitmapImage { get; set; } = TestEnum.ItemWithBitmapImage;

        /// <summary>
        /// Gets or sets the test item with resource.
        /// </summary>
        /// <value>
        /// The test item with resource.
        /// </value>
        [FileExtension("invalidPropertName")]
        public TestEnum TestItemWithResource { get; set; } = TestEnum.confetti;

        /// <summary>
        /// Gets or sets the test item with resource.
        /// </summary>
        /// <value>
        /// The test item with resource.
        /// </value>
        [FileExtension(nameof(PrivateImageFileExtension))]
        public TestEnum TestItemWithPrivateProperty { get; set; } = TestEnum.confetti;

        /// <summary>
        /// Gets or sets the test item with enum.
        /// </summary>
        /// <value>
        /// The test item with enum.
        /// </value>
        [FileExtension(nameof(ImageFileExtension))]
        public TestEnum TestItemWithEnum { get; set; } = TestEnum.ItemWithImage;

        /// <summary>
        /// Gets or sets the test item with enum.
        /// </summary>
        /// <value>
        /// The test item with enum.
        /// </value>
        [FileExtension(nameof(NullableImageFileExtension))]
        public TestEnum? NullableTestItemWithEnum { get; set; } = TestEnum.ItemWithImage;

        /// <summary>
        /// Gets or sets the test item with int.
        /// </summary>
        /// <value>
        /// The test item with int.
        /// </value>
        [FileExtension(nameof(ImageFileExtensionInt))]
        public int TestItemWithInt { get; set; } = (int)TestEnum.ItemWithImage;

        /// <summary>
        /// Gets or sets the test resource path.
        /// </summary>
        /// <value>
        /// The test resource path.
        /// </value>
        public string TestResourcePath { get; set; } = "TestResourcePath.Resources";

        /// <summary>
        /// Gets or sets the test resource path as an int to test that this type of property is skipped.
        /// </summary>
        /// <value>
        /// The test resource path.
        /// </value>
        public int TestResourcePathInt { get; set; } = 0;

        /// <summary>
        /// Gets or sets the file extension.
        /// </summary>
        /// <value>
        /// The file extension.
        /// </value>
        public ImageFileExtension ImageFileExtension { get; set; } = ImageFileExtension.jpg;

        /// <summary>
        /// Gets or sets the nullable image file extension.
        /// </summary>
        /// <value>
        /// The nullable image file extension.
        /// </value>
        public ImageFileExtension? NullableImageFileExtension { get; set; } = ImageFileExtension.jpg;

        /// <summary>
        /// Gets or sets the image file extension string.
        /// </summary>
        /// <value>
        /// The image file extension string.
        /// </value>
        public string ImageFileExtensionString { get; set; } = "jpg";

        /// <summary>
        /// Gets or sets the image file extension int.
        /// </summary>
        /// <value>
        /// The image file extension string.
        /// </value>
        public int ImageFileExtensionInt { get; set; } = 5;

        /// <summary>
        /// Gets or sets the image file extension string.
        /// </summary>
        /// <value>
        /// The image file extension string.
        /// </value>
        private string PrivateImageFileExtension { get; set; } = "jpg";

        #endregion

        #region Test Support Methods ^^^^^^^^^^^^^^^^^^^^^^

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

        #endregion
    }
}
