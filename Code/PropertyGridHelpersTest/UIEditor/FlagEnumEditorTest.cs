﻿using PropertyGridHelpers.UIEditors;
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
    /// <summary>
    /// Tests for the <see cref="FlagEnumUIEditor"/>
    /// </summary>
    public class FlagEnumEditorTest
    {
#if NET35
#else
        readonly ITestOutputHelper OutputHelper;
        public FlagEnumEditorTest(ITestOutputHelper output)

        {
            OutputHelper = output;
        }
#endif

        /// <summary>
        /// Edits the value returns null with null entries test.
        /// </summary>
        [Fact]
        public void EditValueReturnsNullWithNullEntriesTest()
        {
            using (var editor = new FlagEnumUIEditor())
            {
                Assert.Null(editor.EditValue(null, null, TestEnums.FirstEntry));
                Output(Properties.Resources.EditValueNull);
            }
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

            grid?.Dispose();
            Output(Properties.Resources.EditValueNull);
        }

        /// <summary>
        /// Gets the edit style test.
        /// </summary>
        [Fact]
        public void GetEditStyleTest()
        {
            using (var editor = new FlagEnumUIEditor())
            {
                Assert.Equal(UITypeEditorEditStyle.DropDown, editor.GetEditStyle(null));
                Output(Properties.Resources.EditorStyle);
            }
        }

        /// <summary>
        ///
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

        private class TestClass : Component
        {
            public TestClass()
            {
                EnumValue = TestEnums.FirstEntry;
            }

            [Editor(typeof(FlagEnumUIEditor), typeof(UITypeEditor))]
            public TestEnums EnumValue { get; set; }
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
