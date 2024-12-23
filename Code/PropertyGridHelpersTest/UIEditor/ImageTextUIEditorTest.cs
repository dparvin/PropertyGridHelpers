using PropertyGridHelpers.UIEditors;
using System;
using System.Drawing.Design;
using System.Drawing;
using System.Reflection;
using Xunit;
using PropertyGridHelpers.Attributes;

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
        }
        /// <summary>
        /// Constructors the should initialize fields.
        /// </summary>
        [Fact]
        public void Constructor_ShouldInitializeFields()
        {
            var editor = new ImageTextUIEditor(typeof(DayOfWeek), "Custom.Resources");

            Assert.Equal(typeof(DayOfWeek), editor.GetType().GetField("_enumType", BindingFlags.NonPublic | BindingFlags.Instance).GetValue(editor));
            Assert.Equal("Custom.Resources", editor.GetType().GetField("_resourcePath", BindingFlags.NonPublic | BindingFlags.Instance).GetValue(editor));
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
            var editor = new ImageTextUIEditor<DayOfWeek>("Test.Resources");

            Assert.Equal(typeof(DayOfWeek), editor.GetType().BaseType.GetField("_enumType", BindingFlags.NonPublic | BindingFlags.Instance).GetValue(editor));
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
        /// Helper method to check if a bitmap is empty
        /// </summary>
        /// <param name="bitmap">The bitmap.</param>
        /// <returns>
        ///   <c>true</c> if the bitmap is empty for the specified bitmap; otherwise, <c>false</c>.
        /// </returns>
        private bool IsBitmapEmpty(Bitmap bitmap)
        {
            for (int x = 0; x < bitmap.Width; x++)
                for (int y = 0; y < bitmap.Height; y++)
                    if (bitmap.GetPixel(x, y).A != 0) // Check for any non-transparent pixel
                        return false;
            return true;
        }
    }
}
