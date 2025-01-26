﻿using PropertyGridHelpers.UIEditors;
using System;
using System.Drawing.Design;
using System.Drawing;
using System.Reflection;
using Xunit;
using PropertyGridHelpers.Attributes;
using PropertyGridHelpersTest.Support;
using System.ComponentModel;
using PropertyGridHelpers.TypeDescriptors;
using PropertyGridHelpersTest.Enums;

#if NET35
using Xunit.Extensions;
#else
using Xunit.Abstractions;
#endif

#if NET35
namespace PropertyGridHelpersTest.net35.UIEditor
#elif NET452
namespace PropertyGridHelpersTest.net452.UIEditor
#elif NET462
namespace PropertyGridHelpersTest.net462.UIEditor
#elif NET472
namespace PropertyGridHelpersTest.net472.UIEditor
#elif NET481
namespace PropertyGridHelpersTest.net481.UIEditor
#elif NET8_0
namespace PropertyGridHelpersTest.net80.UIEditor
#elif NET9_0
namespace PropertyGridHelpersTest.net90.UIEditor
#endif
{
#if NET8_0_OR_GREATER
    /// <summary>
    /// Class for testing the ImageTextUIEditor class
    /// </summary>
    /// <param name="output">The output.</param>
    public class ImageTextUIEditorTest(ITestOutputHelper output)
#else
    /// <summary>
    /// Class for testing the ImageTextUIEditor class
    /// </summary>
    public class ImageTextUIEditorTest
#endif
    {
#if NET35
#elif NET8_0_OR_GREATER
        private readonly ITestOutputHelper OutputHelper = output;
#else
        private readonly ITestOutputHelper OutputHelper;
#endif

#if NET35
        /// <summary>
        /// Enum Text Converter Test
        /// </summary>
        public ImageTextUIEditorTest()
        {
        }
#elif NET8_0_OR_GREATER
#else
        /// <summary>
        /// Enum Text Converter Test
        /// </summary>
        /// <param name="output"></param>
        public ImageTextUIEditorTest(ITestOutputHelper output)
            : base() =>
            OutputHelper = output;
#endif

        #region Constructors/Destructors Tests ^^^^^^^^^^^^

        /// <summary>
        /// Constructors the should initialize fields.
        /// </summary>
        [Fact]
        public void Constructor_ShouldInitializeFields()
        {
            var editor = new ImageTextUIEditor(typeof(DayOfWeek), "Custom.Resources");

#if NET35
            Assert.Equal(typeof(DayOfWeek), editor.GetType().GetProperty("EnumType", BindingFlags.NonPublic | BindingFlags.Instance).GetValue(editor, null));
            Assert.Equal("Custom.Resources", editor.GetType().GetProperty("ResourcePath", BindingFlags.NonPublic | BindingFlags.Instance).GetValue(editor, null));
#else
            Assert.Equal(typeof(DayOfWeek), editor.GetType().GetProperty("EnumType", BindingFlags.NonPublic | BindingFlags.Instance).GetValue(editor));
            Assert.Equal("Custom.Resources", editor.GetType().GetProperty("ResourcePath", BindingFlags.NonPublic | BindingFlags.Instance).GetValue(editor));
#endif
        }

        /// <summary>
        /// Defaults the constructor should initialize properly.
        /// </summary>
        [Fact]
        public void DefaultConstructor_ShouldInitializeProperly()
        {
            // Arrange & Act
            var editor = new TestImageTextUIEditor<TestEnum>();

            // Assert
            Assert.NotNull(editor); // Ensure the object is not null
            Assert.Equal(typeof(TestEnum), editor.TestEnumType); // Check that the generic type is correctly set
#if NET35
            Assert.Equal(0, string.Compare("Images", editor.TestResourcePath)); // Ensure the ResourcePath is initialized to null
#else
            Assert.Equal("Images", editor.TestResourcePath); // Ensure the ResourcePath is initialized to null
#endif
        }

        /// <summary>
        /// Generics the constructor should pass enum type to base.
        /// </summary>
        [Fact]
        public void GenericConstructor_ShouldPassEnumTypeToBase()
        {
            var editor = new TestImageTextUIEditor<DayOfWeek>("Test.Resources");

            Assert.Equal(typeof(DayOfWeek), editor.TestEnumType);
        }

        /// <summary>
        /// Gets the paint value supported should return true.
        /// </summary>
        [Fact]
        public void GetPaintValueSupported_ShouldReturnTrue()
        {
            var editor = new ImageTextUIEditor(typeof(DayOfWeek));
            var result = editor.GetPaintValueSupported(null);
            Assert.True(result);
        }

        /// <summary>
        /// Disposes the should set disposed flag.
        /// </summary>
        [Fact]
        public void Dispose_ShouldSetDisposedFlag()
        {
            var editor = new ImageTextUIEditor(typeof(DayOfWeek));
            editor.Dispose();

            var disposedValue = editor.GetType().GetField("disposedValue", BindingFlags.NonPublic | BindingFlags.Instance).GetValue(editor);
            Assert.True((bool)disposedValue);
        }

        #endregion

        #region PaintValue Tests ^^^^^^^^^^^^^^^^^^^^^^^^^^

        /// <summary>
        /// Paints the value should draw image.
        /// </summary>
        [Fact]
        public void PaintValue_ShouldDrawImage()
        {
            // Arrange
            var bitmap = new Bitmap(100, 100);
            var graphics = Graphics.FromImage(bitmap);
            var editor = new ImageTextUIEditor(typeof(DayOfWeek), "Test.Resources");
            var paintEventArgs = new PaintValueEventArgs(null, DayOfWeek.Monday, graphics, new Rectangle(0, 0, 50, 50));

            // Act
            editor.PaintValue(paintEventArgs);

            // Assert
            Assert.True(IsBitmapEmpty(bitmap), "The bitmap should not be empty after drawing.");
        }

        /// <summary>
        /// Paints the value should draw image integration test.
        /// </summary>
        /// <param name="testEnum">The test enum.</param>
        [Theory]
        [InlineData(TestEnum.ItemWithImage)]
        [InlineData(TestEnum.ItemWithBitmapImage)]
        [InlineData(TestEnum.confetti)]
        public void PaintValue_ShouldDrawImage_IntegrationTest(TestEnum testEnum)
        {
            // Arrange
            var bitmap = new Bitmap(100, 100); // Use a bitmap as the drawing surface
            var graphics = Graphics.FromImage(bitmap);
            var bounds = new Rectangle(0, 0, 100, 100);

            var editor = new ImageTextUIEditor(typeof(TestEnum), "Images");

            // Create a PaintValueEventArgs with a real Graphics object
            var paintEventArgs = new PaintValueEventArgs(
                null,
                testEnum, // Example enum value with the attribute
                graphics,
                bounds);

            // Act
            editor.PaintValue(paintEventArgs);

            // Assert
            Assert.False(IsBitmapEmpty(bitmap), "The bitmap should not be empty after drawing an embedded image.");
        }

        /// <summary>
        /// Paints the value should draw image without resource path.
        /// </summary>
        [Fact]
        public void PaintValue_ShouldDrawImage_WithoutResourcePath()
        {
            // Arrange
            var bitmap = new Bitmap(100, 100); // Use a bitmap as the drawing surface
            var graphics = Graphics.FromImage(bitmap);
            var bounds = new Rectangle(0, 0, 100, 100);

            var editor = new ImageTextUIEditor(typeof(FullPathTestItems), "");

            // Create a PaintValueEventArgs with a real Graphics object
            var paintEventArgs = new PaintValueEventArgs(
                null,
                FullPathTestItems.Item2, // Example enum value with the attribute
                graphics,
                bounds);

            // Act
            editor.PaintValue(paintEventArgs);

            // Assert
            Assert.False(IsBitmapEmpty(bitmap), "The bitmap should not be empty after drawing an embedded image.");
        }

        /// <summary>
        /// Paints the value should draw image without resource path.
        /// </summary>
        [Fact]
        public void PaintValue_ShouldDrawImage_WithoutResourcePath_ThrowsAnException()
        {
            // Arrange
            var bitmap = new Bitmap(100, 100); // Use a bitmap as the drawing surface
            var graphics = Graphics.FromImage(bitmap);
            var bounds = new Rectangle(0, 0, 100, 100);

            var editor = new ImageTextUIEditor(typeof(FullPathTestItems), "");

            // Create a PaintValueEventArgs with a real Graphics object
            var paintEventArgs = new PaintValueEventArgs(
                null,
                FullPathTestItems.Item1, // Example enum value with the attribute
                graphics,
                bounds);

            // Act & Assert
            var exception = Assert.Throws<InvalidOperationException>(() => editor.PaintValue(paintEventArgs));

            // Verify the exception message if needed
            Assert.Contains("Resource", exception.Message);
        }

        /// <summary>
        /// Paints the value embedded image should draw image.
        /// </summary>
        /// <param name="testEnum">The test enum.</param>
        /// <param name="resourcePath">The resource path.</param>
        [Theory]
        [InlineData(TestEnum.ItemWithEmbeddedImage, "Resources")]
        [InlineData(TestEnum.ItemWithEmbeddedImageResource, "")]
        public void PaintValue_EmbeddedImage_ShouldDrawImage(TestEnum testEnum, string resourcePath)
        {
            // Arrange
            var editor = new ImageTextUIEditor(typeof(TestEnum), resourcePath);
            var bitmap = new Bitmap(100, 100);
            var graphics = Graphics.FromImage(bitmap);
            var bounds = new Rectangle(0, 0, 100, 100);

            var paintEventArgs = new PaintValueEventArgs(
                null,
                testEnum, // Enum value with embedded image attribute
                graphics,
                bounds);

            // Act
            editor.PaintValue(paintEventArgs);

            // Assert
            Assert.False(IsBitmapEmpty(bitmap), "The bitmap should not be empty after drawing an embedded image.");
        }

        /// <summary>
        /// Paints the value file image should draw image.
        /// </summary>
        [Fact]
        public void PaintValue_FileImage_ShouldDrawImage()
        {
            // Arrange
            var editor = new ImageTextUIEditor(typeof(TestEnum), "Images");
            var bitmap = new Bitmap(100, 100);
            var graphics = Graphics.FromImage(bitmap);
            var bounds = new Rectangle(0, 0, 100, 100);

            var paintEventArgs = new PaintValueEventArgs(
                null,
                TestEnum.ItemWithFileImage, // Enum value with file image attribute
                graphics,
                bounds);

            // Mock the file system or ensure the file exists for this test

            // Act
            editor.PaintValue(paintEventArgs);

            // Assert
            Assert.False(IsBitmapEmpty(bitmap), "The bitmap should not be empty after drawing an image from a file.");
        }

        /// <summary>
        /// Paints the value should throw exception with invalid parameter.
        /// </summary>
        [Fact]
        public void PaintValue_ShouldThrowExceptionWithInvalidParameter()
        {
            // Arrange
            var editor = new ImageTextUIEditor(typeof(TestEnum), "Images");

            // Act and Assert
            var ex = Assert.Throws<ArgumentNullException>(() => editor.PaintValue(null));

            Assert.Contains("Value cannot be null.", ex.Message);
            Output($"Exception thrown as expected: {ex.Message}");
        }

        /// <summary>
        /// Paints the value should throw invalid operation exception when resource is invalid.
        /// </summary>
        [Fact]
        public void PaintValue_ShouldThrowInvalidOperationException_WhenResourceIsInvalid()
        {
            // Arrange
            var editor = new ImageTextUIEditor(typeof(TestEnum));
            var bitmap = new Bitmap(100, 100);
            var graphics = Graphics.FromImage(bitmap);
            var mockValue = TestEnum.ItemWithInvalidResourceType; // Use any enum value.
            var e = new PaintValueEventArgs(null, mockValue, graphics, new Rectangle(0, 0, 100, 100));

            // Act & Assert
            var exception = Assert.Throws<InvalidOperationException>(() => editor.PaintValue(e));

            Output(exception.Message);
            const string expectedMessage = "Resource 'PropertyGridHelpersTest.Images.resources.Stars' is not a valid image or byte array.";
#if NET35
            Assert.Equal(0, string.Compare(expectedMessage, exception.Message));
#else
            Assert.Equal(expectedMessage, exception.Message);
#endif
        }

        #endregion

        #region TargetSizes Tests ^^^^^^^^^^^^^^^^^^^^^^^^^

        /// <summary>
        /// Targets the sizes calculates correctly.
        /// </summary>
        /// <param name="imageWidth">Width of the image.</param>
        /// <param name="imageHeight">Height of the image.</param>
        /// <param name="expectedWidth">The expected width.</param>
        /// <param name="expectedHeight">The expected height.</param>
        /// <param name="expectedOffsetX">The expected offset x.</param>
        /// <param name="expectedOffsetY">The expected offset y.</param>
        [Theory]
        [InlineData(100, 120, 41, 50, 4, 0)]
        [InlineData(120, 100, 50, 41, 0, 4)]
        public void TargetSizes_CalculatesCorrectly(
            int imageWidth,
            int imageHeight,
            int expectedWidth,
            int expectedHeight,
            int expectedOffsetX,
            int expectedOffsetY)
        {
            // Arrange

            var bitmap = new Bitmap(imageWidth, imageHeight);
            var rect = new Rectangle(0, 0, 50, 50);
            // Act
            var ts = ImageTextUIEditor.GetTargetSizes(bitmap, rect);

            // Assert
            Assert.Equal(expectedWidth, ts.TargetWidth);
            Assert.Equal(expectedHeight, ts.TargetHeight);
            Assert.Equal(expectedOffsetX, ts.OffsetX);
            Assert.Equal(expectedOffsetY, ts.OffsetY);
        }

        #endregion

        #region GetResourcePath Tests ^^^^^^^^^^^^^^^^^^^^^

        /// <summary>
        /// Gets the resource path should return resource path.
        /// </summary>
        /// <param name="PropertyName">Name of the property.</param>
        /// <param name="ExpectedPath">The expected path.</param>
        [Theory]
        [InlineData(nameof(TestItemWithImage), "TestResourcePath.Resources")]
        [InlineData(nameof(TestItemWithBitmapImage), "TestItemWithBitmapImage.Resources")]
        public void GetResourcePath_ShouldReturnResourcePath(
            string PropertyName,
            string ExpectedPath)
        {
            // Arrange
            var PropertyDescriptor = TypeDescriptor.GetProperties(this)[PropertyName];
            var context = new CustomTypeDescriptorContext(PropertyDescriptor, this);

            // Act
            var resourcePath = ImageTextUIEditor.GetResourcePath(context, TestItemWithImage.GetType());
            context.OnComponentChanged();

            // Assert
            Output(resourcePath);
#if NET35
            Assert.Equal(0, string.Compare(ExpectedPath, resourcePath));
#else
            Assert.Equal(ExpectedPath, resourcePath);
#endif
            Assert.Null(context.Container);
            Assert.Null(context.GetService(typeof(IContainer)));
            Assert.True(context.OnComponentChanging());
        }

        #endregion

        #region GetImageFromRsource Tests ^^^^^^^^^^^^^^^^^

        /// <summary>
        /// Gets the image from resource should return image.
        /// </summary>
        [Fact]
        public void GetImageFromResource_ShouldReturnImage()
        {
            // Arrange
            // Act
            var image = ImageTextUIEditor.GetImageFromResource(TestEnum.ItemWithImage, typeof(TestEnum), "Images", "", new Rectangle(0, 0, 100, 100));

            // Assert
            Assert.NotNull(image);
            Assert.NotEqual(0, image.Width);
            Assert.NotEqual(0, image.Height);
        }

        /// <summary>
        /// Gets the image from resource should return image.
        /// </summary>
        [Fact]
        public void GetImageFromResource_ShouldReturnNull_WhenThereIsNoImage()
        {
            // Arrange
            // Act
            var image = ImageTextUIEditor.GetImageFromResource(TestEnum.ItemWithImage, typeof(TestEnum), "Images", "jpg", new Rectangle(0, 0, 100, 100));

            // Assert
            Assert.Null(image);
        }

        /// <summary>
        /// Gets the image from resource should return image.
        /// </summary>
        [Fact]
        public void GetImageFromResource_ShouldReturnNull_WhenThereIsNoValuePassed()
        {
            // Arrange
            // Act
            var ex = Assert.Throws<ArgumentNullException>(() => ImageTextUIEditor.GetImageFromResource(null, typeof(TestEnum), "Properties.Resources", "jpg", new Rectangle(0, 0, 100, 100)));

            // Assert
            Assert.Contains("Value cannot be null.", ex.Message);
            Output(ex.Message);
        }

        /// <summary>
        /// Gets the image from resource should return image.
        /// </summary>
        [Fact]
        public void GetImageFromResource_ShouldReturnNull_WhenThereIsNoTypeIsNull()
        {
            // Arrange
            // Act
            var ex = Assert.Throws<ArgumentNullException>(() => ImageTextUIEditor.GetImageFromResource(TestEnum.ItemWithImage, null, "Properties.Resources", "jpg", new Rectangle(0, 0, 100, 100)));

            // Assert
            Assert.Contains("Value cannot be null.", ex.Message);
            Output(ex.Message);
        }

        #endregion

        #region GetImageFromImbeddedResource Tests ^^^^^^^^

        /// <summary>
        /// Gets the image from embedded resource should return image.
        /// </summary>
        [Fact]
        public void GetImageFromEmbeddedResource_ShouldReturnImage()
        {
            // Arrange
            // Act
            var image = ImageTextUIEditor.GetImageFromEmbeddedResource(TestEnum.ItemWithEmbeddedImage, "confetti-stars", "PropertyGridHelpersTest.Resources", "jpg");
            // Assert
            Assert.NotNull(image);
            Assert.NotEqual(0, image.Width);
            Assert.NotEqual(0, image.Height);
        }

        /// <summary>
        /// Gets the image from embedded resource should return null when value is null.
        /// </summary>
        [Fact]
        public void GetImageFromEmbeddedResource_ShouldReturnException_WhenValueIsNull()
        {
            // Arrange
            // Act
            var ex = Assert.Throws<ArgumentNullException>(() => ImageTextUIEditor.GetImageFromEmbeddedResource(null, "confetti-stars", "Properties.Resources", "jpg"));

            // Assert
            Assert.Contains("Value cannot be null.", ex.Message);
            Output(ex.Message);
        }

        /// <summary>
        /// Gets the image from embedded resource should return exception when resource item empty.
        /// </summary>
        [Fact]
        public void GetImageFromEmbeddedResource_ShouldReturnException_WhenResourceItemEmpty()
        {
            // Arrange
            // Act
            var ex = Assert.Throws<ArgumentException>(() => ImageTextUIEditor.GetImageFromEmbeddedResource(TestEnum.ItemWithEmbeddedImage, string.Empty, "Properties.Resources", "jpg"));

            // Assert
            Assert.Contains("'ResourceItem' cannot be null or empty.", ex.Message);
            Output(ex.Message);
        }

        /// <summary>
        /// Gets the image from embedded resource should return exception when resource path empty.
        /// </summary>
        [Fact]
        public void GetImageFromEmbeddedResource_ShouldReturnException_WhenResourcePathEmpty()
        {
            // Arrange
            // Act
            var ex = Assert.Throws<ArgumentException>(() => ImageTextUIEditor.GetImageFromEmbeddedResource(TestEnum.ItemWithEmbeddedImage, "confetti-stars", string.Empty, "jpg"));

            // Assert
            Assert.Contains("'ResourcePath' cannot be null or empty.", ex.Message);
            Output(ex.Message);
        }

        #endregion

        #region GetImageFromResourceFile Tests ^^^^^^^^^^^^

        /// <summary>
        /// Gets the image from resource file should return image.
        /// </summary>
        [Fact]
        public void GetImageFromResourceFile_ShouldReturnImage()
        {
            // Arrange
            // Act
            var image = ImageTextUIEditor.GetImageFromResourceFile(TestEnum.confetti, "confetti", "PropertyGridHelpersTest.Images", "", false);

            // Assert
            Assert.NotNull(image);
            Assert.NotEqual(0, image.Width);
            Assert.NotEqual(0, image.Height);
        }

        /// <summary>
        /// Gets the image from resource file in design mode should return image.
        /// </summary>
        [Fact]
        public void GetImageFromResourceFile_InDesignMode_ShouldReturnImage()
        {
            // Arrange
            var tc = new Images();

            // Act
            var image = ImageTextUIEditor.GetImageFromResourceFile(tc, "confetti", "Images", "", true);

            // Assert
            Assert.NotNull(image);
            Assert.NotEqual(0, image.Width);
            Assert.NotEqual(0, image.Height);
        }

        /// <summary>
        /// Gets the image from resource file in design mode should return image.
        /// </summary>
        [Fact]
        public void GetImageFromResourceFileWithExtension_InDesignMode_ShouldReturnImage()
        {
            // Arrange
            var tc = new Images();

            // Act
            var image = ImageTextUIEditor.GetImageFromResourceFile(tc, "confetti", "Images", "jpg", true);

            // Assert
            Assert.NotNull(image);
            Assert.NotEqual(0, image.Width);
            Assert.NotEqual(0, image.Height);
        }

        /// <summary>
        /// Gets the image from resource file resource file not found should throw invalid operation exception.
        /// </summary>
        [Theory]
        [InlineData("")]
        [InlineData("gif")]
        public void GetImageFromResourceFile_ResourceFileNotFound_ShouldThrowInvalidOperationException(
            string fileExtension)
        {
            // Arrange
            var testValue = new Images(); // Any dummy object for this test
            var resourcePath = "Images";
            var resourceItem = "InvalidItem";
            var isInDesignMode = false;

            // Act & Assert
            var exception = Assert.Throws<InvalidOperationException>(() =>
                    ImageTextUIEditor.GetImageFromResourceFile(testValue, resourceItem, resourcePath, fileExtension, isInDesignMode));

            // Additional Assertions
            Output(exception.Message);
            Assert.Contains("Error retrieving resource", exception.Message); // Check for expected part of the error message
        }

        /// <summary>
        /// Gets the image from resource file should return image.
        /// </summary>
        [Fact]
        public void GetImageFromResourceFile_ShouldReturnException_WhenValueIsNull()
        {
            // Arrange
            // Act
            var ex = Assert.Throws<ArgumentNullException>(() => ImageTextUIEditor.GetImageFromResourceFile(null, "confetti-stars", "Properties.Resources", "jpg", false));
            // Assert
            Assert.Contains("Value cannot be null.", ex.Message);
            Output(ex.Message);
        }

        /// <summary>
        /// Gets the image from resource file should return exception when resource item is empty.
        /// </summary>
        [Fact]
        public void GetImageFromResourceFile_ShouldReturnException_WhenResourceItemIsEmpty()
        {
            // Arrange
            // Act
            var ex = Assert.Throws<ArgumentException>(() => ImageTextUIEditor.GetImageFromResourceFile(TestEnum.ItemWithFileImage, string.Empty, "Properties.Resources", "jpg", false));
            // Assert
            // Assert
            Assert.Contains("'ResourceItem' cannot be null or empty.", ex.Message);
            Output(ex.Message);
        }

        /// <summary>
        /// Gets the image from resource file should return exception when resource path is empty.
        /// </summary>
        [Fact]
        public void GetImageFromResourceFile_ShouldReturnException_WhenResourcePathIsEmpty()
        {
            // Arrange
            // Act
            var ex = Assert.Throws<ArgumentException>(() => ImageTextUIEditor.GetImageFromResourceFile(TestEnum.ItemWithFileImage, "confetti-stars", String.Empty, "jpg", false));
            // Assert
            // Assert
            Assert.Contains("'ResourcePath' cannot be null or empty.", ex.Message);
            Output(ex.Message);
        }

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
            var fileExtension = ImageTextUIEditor.GetFileExtension(context);
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
            var fileExtension = ImageTextUIEditor.GetFileExtension(context);
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
            var fileExtension = ImageTextUIEditor.GetFileExtension(context);
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
            var ex = Assert.Throws<InvalidOperationException>(() => ImageTextUIEditor.GetFileExtension(context));
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
            var ex = Assert.Throws<InvalidOperationException>(() => ImageTextUIEditor.GetFileExtension(context));
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
        public void GetFileExtension_EnumWithCustomEnumText_ShouldReturnEnumText(
            ImageFileExtension testExtension,
            string expectedValue)
        {
            // Arrange
            ImageFileExtension = testExtension;

            var PropertyDescriptor = TypeDescriptor.GetProperties(this)[nameof(TestItemWithEnum)];
            var context = new CustomTypeDescriptorContext(PropertyDescriptor, this);

            // Act
            var fileExtension = ImageTextUIEditor.GetFileExtension(context);

            // Assert
#if NET35
            Assert.Equal(0, string.Compare(expectedValue, fileExtension));
#else
            Assert.Equal(expectedValue, fileExtension);
#endif
            Output(fileExtension);
        }

        #endregion

        #region Test objects ^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^

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
        /// Gets or sets the file extension.
        /// </summary>
        /// <value>
        /// The file extension.
        /// </value>
        public ImageFileExtension ImageFileExtension { get; set; } = ImageFileExtension.jpg;

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

        /// <summary>
        /// Enum to test the different types of image processing
        /// </summary>
        [ResourcePath("Images")]
        public enum TestEnum
        {
            /// <summary>
            /// The item with image
            /// </summary>
            [EnumImage("confetti-stars", PropertyGridHelpers.Enums.ImageLocation.Resource)]
            ItemWithImage,
            /// <summary>
            /// The item with bitmap image
            /// </summary>
            [EnumImage("confetti-stars-bitmap", PropertyGridHelpers.Enums.ImageLocation.Resource)]
            ItemWithBitmapImage,
            /// <summary>
            /// The confetti
            /// </summary>
            [EnumImage(PropertyGridHelpers.Enums.ImageLocation.Resource)]
            confetti,
            /// <summary>
            /// The item without image
            /// </summary>
            ItemWithoutImage,
            /// <summary>
            /// The item with embedded image
            /// </summary>
            [EnumImage("confetti-stars.jpg")]
            ItemWithEmbeddedImage,
            /// <summary>
            /// The item with embedded image
            /// </summary>
            [EnumImage("Resources.confetti-stars.jpg")]
            ItemWithEmbeddedImageResource,
            /// <summary>
            /// The item with file image
            /// </summary>
            [EnumImage("confetti-stars.jpg", PropertyGridHelpers.Enums.ImageLocation.File)]
            ItemWithFileImage,
            /// <summary>
            /// The item with invalid resource type
            /// </summary>
            [EnumImage("Stars", PropertyGridHelpers.Enums.ImageLocation.Resource)]
            ItemWithInvalidResourceType,
        }

        /// <summary>
        /// Enum to test full paths to the images in the project
        /// </summary>
        public enum FullPathTestItems
        {
            /// <summary>
            /// The item1
            /// </summary>
            [EnumImage(".Properties.Resources.confetti-stars", PropertyGridHelpers.Enums.ImageLocation.Resource)]
            Item1,
            /// <summary>
            /// The item2
            /// </summary>
            [EnumImage("Resources.confetti-stars.jpg")]
            Item2,
        }

        #endregion

        #region Test Support Methods ^^^^^^^^^^^^^^^^^^^^^^

        /// <summary>
        /// Helper method to check if a bitmap is empty
        /// </summary>
        /// <param name="bitmap">The bitmap.</param>
        /// <returns>
        ///   <c>true</c> if the bitmap is empty for the specified bitmap; otherwise, <c>false</c>.
        /// </returns>
        private static bool IsBitmapEmpty(Bitmap bitmap)
        {
            for (var x = 0; x < bitmap.Width; x++)
                for (var y = 0; y < bitmap.Height; y++)
                    if (bitmap.GetPixel(x, y).A != 0) // Check for any non-transparent pixel
                        return false;
            return true;
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

        #endregion
    }
}
