using Xunit;
using System;
using PropertyGridHelpers.Controls;
#if NET35
using Xunit.Extensions;
#else
using Xunit.Abstractions;
#endif

#if NET35
namespace PropertyGridHelpersTest.net35.Controls
#elif NET452
namespace PropertyGridHelpersTest.net452.Controls
#elif NET462
namespace PropertyGridHelpersTest.net462.Controls
#elif NET472
namespace PropertyGridHelpersTest.net472.Controls
#elif NET48
namespace PropertyGridHelpersTest.net48.Controls
#elif NET6_0
namespace PropertyGridHelpersTest.net60.Controls
#endif
{
    /// <summary>
    /// Tests for the flag checked list box
    /// </summary>
    public class FlagCheckedListBoxTest
    {
#if NET35
#else
        readonly ITestOutputHelper OutputHelper;

        /// <summary>
        /// Initializes a new instance of the <see cref="FlagCheckedListBoxTest"/> class.
        /// </summary>
        /// <param name="output">The output.</param>
        public FlagCheckedListBoxTest(
            ITestOutputHelper output)

        {
            OutputHelper = output;
        }
#endif

        /// <summary>
        /// Add Item Caption is set correctly test.
        /// </summary>
        [Fact]
        public void AddItemCaptionSetCorrectlyTest()
        {
            using (var list = new FlagCheckedListBox())
            {
                list.Add(new FlagCheckedListBoxItem(1, "Test"));
                Assert.Equal("Test", ((FlagCheckedListBoxItem)list.Items[0]).Caption);
                Output("Caption set to 'Test' as expected");
            }
        }

        /// <summary>
        /// Add Item Caption is set correctly test.
        /// </summary>
        [Fact]
        public void AddValuesCaptionSetCorrectlyTest()
        {
            using (var list = new FlagCheckedListBox())
            {
                list.Add(1, "Test");
                Assert.Equal("Test", ((FlagCheckedListBoxItem)list.Items[0]).Caption);
                Output("Caption set to 'Test' as expected");
            }
        }

        /// <summary>
        /// Value is set correctly test.
        /// </summary>
        [Fact]
        public void AddItemValueSetCorrectlyTest()
        {
            using (var list = new FlagCheckedListBox())
            {
                list.Add(new FlagCheckedListBoxItem(1, "Test"));
                Assert.Equal(1, ((FlagCheckedListBoxItem)list.Items[0]).Value);
                Output("Value set to 1 as expected");
            }
        }

        /// <summary>
        /// Value is set correctly test.
        /// </summary>
        [Fact]
        public void AddValuesValueSetCorrectlyTest()
        {
            using (var list = new FlagCheckedListBox())
            {
                list.Add(1, "Test");
                Assert.Equal(1, ((FlagCheckedListBoxItem)list.Items[0]).Value);
                Output("Value set to 1 as expected");
            }
        }

        /// <summary>
        /// Value is set correctly test.
        /// </summary>
        [Fact]
        public void ItemCheckedTest()
        {
            using (var list = new FlagCheckedListBox())
            {
                list.Add(1, "Test");
                list.SetItemCheckState(0, System.Windows.Forms.CheckState.Checked);
                Assert.Equal(1, list.GetCurrentValue());
                Output("Value set to 1 as expected");
            }
        }

        /// <summary>
        /// Value is set correctly test.
        /// </summary>
        [Fact]
        public void SetEnumValueTest()
        {
            using (var list = new FlagCheckedListBox())
            {
                list.EnumValue = Converters.EnumTextConverterTest.TestEnums.FirstEntry;
                Assert.Equal(1, list.GetCurrentValue());
                Output("Value set to FirstEntry as expected");
            }
        }

        /// <summary>
        /// Value is set correctly test.
        /// </summary>
        [Theory]
        [InlineData(0)]
        [InlineData(1)]
        [InlineData(2)]
        [InlineData(3)]
        public void GetEnumValueTest(int testValue)
        {
            Converters.EnumTextConverterTest.TestEnums item = (Converters.EnumTextConverterTest.TestEnums)testValue;
            using (var list = new FlagCheckedListBox())
            {
                list.EnumValue = item;
                Assert.Equal(item, list.EnumValue);
                Output("Value set to FirstEntry as expected");
            }
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
