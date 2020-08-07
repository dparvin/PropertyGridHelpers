using Xunit;
using System;
using PropertyGridHelpers.Controls;
#if NET35
#else
using Xunit.Abstractions;
#endif

#if NET35
namespace PropertyGridHelpersTest.net35.Controls
#elif NET452
namespace PropertyGridHelpersTest.net452.Controls
#elif NET48
namespace PropertyGridHelpersTest.net48.Controls
#endif
{
    /// <summary>
    /// Tests for the flag checked list box
    /// </summary>
    public class FlagCheckedListBoxTest
    {
#if NET35
#else
        readonly ITestOutputHelper Output;

        /// <summary>
        /// Initializes a new instance of the <see cref="FlagCheckedListBoxTest"/> class.
        /// </summary>
        /// <param name="output">The output.</param>
        public FlagCheckedListBoxTest(
            ITestOutputHelper output)

        {
            Output = output;
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
#if NET35
                Console.WriteLine("Caption set to 'Test' as expected");
#else
                Output.WriteLine("Caption set to 'Test' as expected");
#endif
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
#if NET35
                Console.WriteLine("Caption set to 'Test' as expected");
#else
                Output.WriteLine("Caption set to 'Test' as expected");
#endif
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
#if NET35
                Console.WriteLine("Value set to 1 as expected");
#else
                Output.WriteLine("Value set to 1 as expected");
#endif
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
#if NET35
                Console.WriteLine("Value set to 1 as expected");
#else
                Output.WriteLine("Value set to 1 as expected");
#endif
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
#if NET35
                Console.WriteLine("Value set to 1 as expected");
#else
                Output.WriteLine("Value set to 1 as expected");
#endif
            }
        }
    }
}
