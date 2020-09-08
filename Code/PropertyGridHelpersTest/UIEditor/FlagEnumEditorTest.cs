using PropertyGridHelpers.UIEditors;
using System.Drawing.Design;
using Xunit;
using System.Windows.Forms;
using System.ComponentModel;
using PropertyGridHelpers.Attributes;
using System;
#if NET35
#else
using Xunit.Abstractions;
#endif

#if NET35
namespace PropertyGridHelpersTest.net35.UIEditor
#elif NET452
namespace PropertyGridHelpersTest.net452.UIEditor
#elif NET48
namespace PropertyGridHelpersTest.net48.UIEditor
#endif
{
    public class FlagEnumEditorTest
    {
#if NET35
#else
        readonly ITestOutputHelper Output;
        public FlagEnumEditorTest(ITestOutputHelper output)

        {
            Output = output;
        }
#endif

        [Fact]
        public void EditValueReturnsNullWithNullEntriesTest()
        {
            using (var editor = new FlagEnumUIEditor())
            {
                Assert.Null(editor.EditValue(null, null, TestEnum.FirstEntry));
#if NET35
                Console.WriteLine(Properties.Resources.EditValueNull);
#else
                Output.WriteLine(Properties.Resources.EditValueNull);
#endif
            }
        }

        [Fact]
        public void EditValueReturnsValuesTest()
        {
            var grid = new PropertyGrid
            {
                SelectedObject = new TestClass()
            };
            ((TestClass)grid.SelectedObject).EnumValue = TestEnum.AllEntries;

            grid?.Dispose();
#if NET35
            Console.WriteLine(Properties.Resources.EditValueNull);
#else
            Output.WriteLine(Properties.Resources.EditValueNull);
#endif
        }

        [Fact]
        public void GetEditStyleTest()
        {
            using (var editor = new FlagEnumUIEditor())
            {
                Assert.Equal(UITypeEditorEditStyle.DropDown, editor.GetEditStyle(null));
#if NET35
                Console.WriteLine(Properties.Resources.EditorStyle);
#else
                Output.WriteLine(Properties.Resources.EditorStyle);
#endif
            }
        }

        [Flags]
        enum TestEnum
        {
            [EnumText("First Entry")]
            FirstEntry = 1,
            [EnumText("Second Entry")]
            SecondEntry = 2,
            [EnumText("All Entries")]
            AllEntries = FirstEntry + SecondEntry,
        }

        private class TestClass : Component
        {
            public TestClass()
            {
                EnumValue = TestEnum.FirstEntry;
            }

            [Editor(typeof(FlagEnumUIEditor), typeof(UITypeEditor))]
            public TestEnum EnumValue { get; set; }
        }
    }
}
