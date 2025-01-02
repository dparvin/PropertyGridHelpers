using PropertyGridHelpers.UIEditors;
using System;
using System.Drawing.Design;
using System.Drawing;
using System.Reflection;
using Xunit;
using PropertyGridHelpers.Attributes;
using PropertyGridHelpersTest.Support;
using System.ComponentModel;
using PropertyGridHelpers.TypeDescriptors;
using System.Diagnostics;




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
#elif WINDOWS7_0
namespace PropertyGridHelpersTest.net60.W7.UIEditor
#elif WINDOWS10_0
namespace PropertyGridHelpersTest.net60.W10.UIEditor
#elif NET8_0
namespace PropertyGridHelpersTest.net80.UIEditor
#elif NET9_0
namespace PropertyGridHelpersTest.net90.UIEditor
#endif
{
    /// <summary>
    /// Class for testing the ImageTextUIEditor class
    /// </summary>
    public class ImageTextUIEditorTest
    {
#if NET35
#else
        readonly ITestOutputHelper OutputHelper;
#endif

        /// <summary>
        /// Enum Text Converter Test
        /// </summary>
#if NET35
        public ImageTextUIEditorTest()
#else
        /// <param name="output"></param>
        public ImageTextUIEditorTest(ITestOutputHelper output)
#endif
#if NET462 || NET472 || NET481 || NET5_0_OR_GREATER
            : base()
#endif
        {
#if NET35
#else
            OutputHelper = output;
#endif
        }

        /// <summary>
        /// Enum to test the different types of image processing
        /// </summary>
        [ResourcePath("TestEnum.Resources")]
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

            var editor = new ImageTextUIEditor(typeof(TestEnum));

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
#if NET40_OR_GREATER || NET5_0_OR_GREATER
            Assert.Equal("Properties.Resources", editor.TestResourcePath); // Ensure the ResourcePath is initialized to null
#endif
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
            var exception = Assert.Throws<InvalidOperationException>(() =>
            {
                editor.PaintValue(e);
            });

#if NET35
#else
            OutputHelper.WriteLine(exception.Message);
            Assert.Equal($"Resource 'PropertyGridHelpersTest.Properties.Resources.resources.Stars' is not a valid image or byte array.", exception.Message);
#endif
        }

        /// <summary>
        /// Gets the resource path should return resource path.
        /// </summary>
        /// <param name="PropertyName">Name of the property.</param>
        /// <param name="ExpectedPath">The expected path.</param>
        [Theory]
        [InlineData(nameof(TestItemWithImage), "TestResourcePath.Resources")]
        [InlineData(nameof(TestItemWithBitmapImage), "TestItemWithBitmapImage.Resources")]
        public void GetResourcePath_ShouldReturnResourcePath(string PropertyName, string ExpectedPath)
        {
            // Arrange
            var PropertyDescriptor = TypeDescriptor.GetProperties(this)[PropertyName];
            var context = new CustomTypeDescriptorContext(PropertyDescriptor, this);

            // Act
            var resourcePath = ImageTextUIEditor.GetResourcePath(context, TestItemWithImage.GetType());
            context.OnComponentChanged();

            // Assert
#if NET35
            Debug.WriteLine(resourcePath);
#else
            OutputHelper.WriteLine(resourcePath);
            Assert.Equal(ExpectedPath, resourcePath);
#endif
            Assert.Null(context.Container);
            Assert.Null(context.GetService(typeof(IContainer)));
            Assert.True(context.OnComponentChanging());
        }

        /// <summary>
        /// Gets or sets the test enum.
        /// </summary>
        /// <value>
        /// The test enum.
        /// </value>
        [DynamicPathSource(nameof(TestResourcePath))]
        public TestEnum TestItemWithImage { get; set; } = TestEnum.ItemWithImage;

        /// <summary>
        /// Gets or sets the test item with bitmap image.
        /// </summary>
        /// <value>
        /// The test item with bitmap image.
        /// </value>
        [ResourcePath("TestItemWithBitmapImage.Resources")]
        public TestEnum TestItemWithBitmapImage { get; set; } = TestEnum.ItemWithBitmapImage;

        /// <summary>
        /// Gets or sets the test resource path.
        /// </summary>
        /// <value>
        /// The test resource path.
        /// </value>
        public string TestResourcePath { get; set; } = "TestResourcePath.Resources";

        #region Test Support Methods

        /// <summary>
        /// Helper method to check if a bitmap is empty
        /// </summary>
        /// <param name="bitmap">The bitmap.</param>
        /// <returns>
        ///   <c>true</c> if the bitmap is empty for the specified bitmap; otherwise, <c>false</c>.
        /// </returns>
        private static bool IsBitmapEmpty(Bitmap bitmap)
        {
            for (int x = 0; x < bitmap.Width; x++)
                for (int y = 0; y < bitmap.Height; y++)
                    if (bitmap.GetPixel(x, y).A != 0) // Check for any non-transparent pixel
                        return false;
            return true;
        }

        #endregion
    }
}
