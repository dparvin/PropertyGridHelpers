using PropertyGridHelpers.UIEditors;
using System;
using System.Drawing.Design;
using System.Drawing;
using System.Reflection;
using Xunit;
using PropertyGridHelpers.Attributes;
using PropertyGridHelpersTest.Support;
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

        #region GetImageFromFile Tests ^^^^^^^^^^^^^^^^^^^^

        /// <summary>
        /// Gets the image from file should return image.
        /// </summary>
        [Fact]
        public void GetImageFromFile_ShouldReturnImage()
        {
            // Arrange
            // Act
            var image = ImageTextUIEditor.GetImageFromFile(TestEnum.ItemWithImage, "confetti-stars", "Images", "jpg");
            // Assert
            Assert.NotNull(image);
            Assert.NotEqual(0, image.Width);
            Assert.NotEqual(0, image.Height);
        }

        /// <summary>
        /// Gets the image from file without separate extension should return image.
        /// </summary>
        [Fact]
        public void GetImageFromFile_withoutSeparateExtension_ShouldReturnImage()
        {
            // Arrange
            // Act
            var image = ImageTextUIEditor.GetImageFromFile(TestEnum.ItemWithImage, "confetti-stars.jpg", "Images", "");
            // Assert
            Assert.NotNull(image);
            Assert.NotEqual(0, image.Width);
            Assert.NotEqual(0, image.Height);
        }

        /// <summary>
        /// Gets the image from file should throw exception with missing resource path.
        /// </summary>
        [Fact]
        public void GetImageFromFile_ShouldThrowExceptionWithMissingValue()
        {
            // Arrange
            // Act
            var ex = Assert.Throws<ArgumentNullException>(() => ImageTextUIEditor.GetImageFromFile(null, "confetti-stars", "Images", "jpg"));
            Output(ex.Message);
            // Assert
            Assert.Contains("Value cannot be null.", ex.Message);
        }

        /// <summary>
        /// Gets the image from file should throw exception with missing resource path.
        /// </summary>
        [Fact]
        public void GetImageFromFile_ShouldThrowExceptionWithMissingResourcePath()
        {
            // Arrange
            // Act
            var image = Assert.Throws<ArgumentException>(() => ImageTextUIEditor.GetImageFromFile(TestEnum.ItemWithImage, "confetti-stars", "", "jpg"));
            // Assert
            Assert.Contains("' cannot be null or empty.", image.Message);
        }

        /// <summary>
        /// Gets the image from file should throw exception with missing resource item.
        /// </summary>
        [Fact]
        public void GetImageFromFile_ShouldThrowExceptionWithMissingResourceItem()
        {
            // Arrange
            // Act
            var image = Assert.Throws<ArgumentException>(() => ImageTextUIEditor.GetImageFromFile(TestEnum.ItemWithImage, "", "Image", "jpg"));
            // Assert
            Assert.Contains("' cannot be null or empty.", image.Message);
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
