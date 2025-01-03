using PropertyGridHelpers.UIEditors;
using System.Drawing.Design;
using Xunit;
using System.Windows.Forms;
using System.ComponentModel;
using PropertyGridHelpers.Attributes;
using System;
using PropertyGridHelpers.Converters;
using PropertyGridHelpersTest.Properties;
using PropertyGridHelpers.Controls;
using PropertyGridHelpersTest.Support;
using PropertyGridHelpers.TypeDescriptors;


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
    /// Tests for the <see cref="FlagEnumUIEditor"/>
    /// </summary>
    public partial class FlagEnumUIEditorTest
    {
#if NET35
#else
        readonly ITestOutputHelper OutputHelper;
        public FlagEnumUIEditorTest(ITestOutputHelper output) => OutputHelper = output;
#endif

        /// <summary>
        /// Edits the value returns null with null entries test.
        /// </summary>
        [Fact]
        public void EditValueReturnsNullWithNullEntriesTest()
        {
#if NET6_0_OR_GREATER
            using var editor = new FlagEnumUIEditor();
#else
            using (var editor = new FlagEnumUIEditor())
#endif
                Assert.Null(editor.EditValue(null, null, TestEnums.FirstEntry));
            Output(Resources.EditValueNull);
        }

        /// <summary>
        /// Edits the value returns values test.
        /// </summary>
        [Fact]
        public void EditValueReturnsValuesTest()
        {
            var grid = new PropertyGrid
            {
                SelectedObject = new TestClass()
            };
            ((TestClass)grid.SelectedObject).EnumValue = TestEnums.AllEntries;

            grid.Dispose();
            Output(Resources.EditValueNull);
        }

        /// <summary>
        /// Gets the edit style test.
        /// </summary>
        [Fact]
        public void GetEditStyleTest()
        {
#if NET6_0_OR_GREATER
            using var editor = new FlagEnumUIEditor();
#else
            using (var editor = new FlagEnumUIEditor())
#endif
            {
                Assert.Equal(UITypeEditorEditStyle.DropDown, editor.GetEditStyle(null));
                Output(Resources.EditorStyle);
            }
        }

        /// <summary>
        /// Edits the value test.
        /// </summary>
        [Fact]
        public void EditValueTest()
        {
#if NET6_0_OR_GREATER
            using var editor = new FlagEnumUIEditor<EnumTextConverter<TestEnums>>();
#else
            using (var editor = new FlagEnumUIEditor<EnumTextConverter<TestEnums>>())
#endif
            {
                Assert.Equal(UITypeEditorEditStyle.DropDown, editor.GetEditStyle(null));
                Output(Resources.EditorStyle);
            }
        }

        /// <summary>
        /// Test Enums
        /// </summary>
        [Flags]
        public enum TestEnums
        {
            /// <summary>
            /// The first entry
            /// </summary>
            [EnumText("First Entry")]
            FirstEntry = 1,
            /// <summary>
            /// The second entry
            /// </summary>
            [EnumText("Second Entry")]
            SecondEntry = 2,
            /// <summary>
            /// All entries
            /// </summary>
            [EnumText("All Entries")]
            AllEntries = FirstEntry + SecondEntry,
        }

        /// <summary>
        /// class to use to help test the editor
        /// </summary>
        /// <seealso cref="Component" />
        private partial class TestClass : Component
        {
            /// <summary>
            /// Initializes a new instance of the <see cref="TestClass"/> class.
            /// </summary>
            public TestClass()
            {
                EnumValue = TestEnums.FirstEntry;
            }

            /// <summary>
            /// Gets or sets the enum value.
            /// </summary>
            /// <value>
            /// The enum value.
            /// </value>
            [Editor(typeof(FlagEnumUIEditor), typeof(UITypeEditor))]
            [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
            public TestEnums EnumValue { get; set; }
        }

        /// <summary>
        /// Edits the value test using helper classes.
        /// </summary>
        [Fact]
        public void EditValueTestUsingHelperClasses()
        {
            // Arrange
            var editorService = new TestWindowsFormsEditorService();
            var serviceProvider = new TestServiceProvider(editorService);
            var testInstance = new FlagEnumUIEditorTest.TestClass
            {
                EnumValue = FlagEnumUIEditorTest.TestEnums.FirstEntry
            };

            var propertyDescriptor = TypeDescriptor.GetProperties(testInstance)["EnumValue"];
            var context = new CustomTypeDescriptorContext(propertyDescriptor, testInstance);
            var editor = new FlagEnumUIEditor();
            var initialEnumValue = TestEnums.FirstEntry;

            // Act
            var result = editor.EditValue(context, serviceProvider, initialEnumValue);

            // Assert
            Assert.NotNull(result);
            Assert.IsType<TestEnums>(result);
            Assert.Equal(TestEnums.SecondEntry, result);

            // Verify that the dropDown was shown and interacted with
            Assert.NotNull(editorService.LastShownControl);
            Assert.IsType<FlagCheckedListBox>(editorService.LastShownControl);
            var flagListBox = (FlagCheckedListBox)editorService.LastShownControl;
            Assert.Equal(TestEnums.SecondEntry, flagListBox.EnumValue);
        }

        /// <summary>
        /// Outputs the specified message.
        /// </summary>
        /// <param name="message">The message.</param>
#if NET35
        private static void Output(string message)
#else
        private void Output(string message)
#endif
        {
#if NET35
            Console.WriteLine(message);
#else
            OutputHelper.WriteLine(message);
#endif
        }
    }
}
