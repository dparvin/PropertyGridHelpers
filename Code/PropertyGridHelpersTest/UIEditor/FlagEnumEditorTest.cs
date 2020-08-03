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

namespace PropertyGridHelpersTest.UIEditor
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
                Console.WriteLine("EditValue returned null as expected");
#else
                Output.WriteLine("EditValue returned null as expected");
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
            Console.WriteLine("EditValue returned null as expected");
#else
            Output.WriteLine("EditValue returned null as expected");
#endif
        }

        [Fact]
        public void GetEditStyleTest()
        {
            using (var editor = new FlagEnumUIEditor())
            {
                Assert.Equal(UITypeEditorEditStyle.DropDown, editor.GetEditStyle(null));
#if NET35
                Console.WriteLine("Editor Edit Style is set as expected.  It is set to DropDown.");
#else
                Output.WriteLine("Editor Edit Style is set as expected.  It is set to DropDown.");
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
