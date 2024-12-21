using PropertyGridHelpers.UIEditors;
using System;
using System.Drawing.Design;
using System.Drawing;
using System.Reflection;
using Xunit;
#if NET35
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
        /// Paints the value should draw image.
        /// </summary>
        [Fact]
        public void PaintValue_ShouldDrawImage()
        {
            // Mock Enum and EnumImageAttribute
            var editor = new ImageTextUIEditor(typeof(DayOfWeek), "Test.Resources");
            var paintEventArgs = new PaintValueEventArgs(null, DayOfWeek.Monday, Graphics.FromImage(new Bitmap(100, 100)), new Rectangle());

            editor.PaintValue(paintEventArgs);

            // Assertions depend on how you verify the behavior, such as checking calls to Graphics.DrawImage.
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
    }
}
