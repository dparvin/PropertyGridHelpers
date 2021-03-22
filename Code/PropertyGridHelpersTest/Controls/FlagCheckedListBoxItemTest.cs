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
#elif NET462
namespace PropertyGridHelpersTest.net462.Controls
#elif NET48
namespace PropertyGridHelpersTest.net48.Controls
#elif NET5_0
namespace PropertyGridHelpersTest.net50.Controls
#endif
{
    /// <summary>
    /// Tests for the flag checked list box
    /// </summary>
    public class FlagCheckedListBoxItemTest
    {
#if NET35
#else
        readonly ITestOutputHelper OutputHelper;

        /// <summary>
        /// Initializes a new instance of the <see cref="FlagCheckedListBoxItemTest"/> class.
        /// </summary>
        /// <param name="output">The output.</param>
        public FlagCheckedListBoxItemTest(
            ITestOutputHelper output)

        {
            OutputHelper = output;
        }
#endif

        /// <summary>
        /// Caption is set correctly test.
        /// </summary>
        [Fact]
        public void CaptionSetCorrectlyTest()
        {
            var item = new FlagCheckedListBoxItem(1, "Test");
            Assert.Equal("Test", item.Caption);
            Output("Caption set to 'Test' as expected");
        }

        /// <summary>
        /// Value is set correctly test.
        /// </summary>
        [Fact]
        public void ValueSetCorrectlyTest()
        {
            var item = new FlagCheckedListBoxItem(1, "Test");
            Assert.Equal(1, item.Value);
            Output("Value set to 1 as expected");
        }

        /// <summary>
        /// Null the parameters handled correctly test.
        /// </summary>
        [Fact]
        public void NullParametersHandledCorrectlyTest()
        {
            var item = new FlagCheckedListBoxItem(0, null);
            Assert.Null(item.Caption);
            Output("Caption set to null as expected");
        }

        /// <summary>
        /// Determines whether IsFlag is true test.
        /// </summary>
        [Fact]
        public void IsFlagIsTrueTest()
        {
            var item = new FlagCheckedListBoxItem(4, null);
            Assert.True(item.IsFlag);
            Output("IsFlag returns true as expected");
        }

        /// <summary>
        /// Determines whether IsFlag is false test.
        /// </summary>
        [Fact]
        public void IsFlagIsFalseTest()
        {
            var item = new FlagCheckedListBoxItem(5, null);
            Assert.False(item.IsFlag);
            Output("IsFlag returns false as expected");
        }

        /// <summary>
        /// Determines whether IsMemberFlag is true test.
        /// </summary>
        [Fact]
        public void IsMemberFlagIsTrueTest()
        {
            var item = new FlagCheckedListBoxItem(4, null);
            Assert.True(item.IsMemberFlag(new FlagCheckedListBoxItem(5, null)));
            Output("IsMemberFlag returns true as expected");
        }

        /// <summary>
        /// Determines whether IsMemberFlag is false test.
        /// </summary>
        [Fact]
        public void IsMemberFlagIsFalseTest()
        {
            var item = new FlagCheckedListBoxItem(5, null);
            Assert.False(item.IsMemberFlag(new FlagCheckedListBoxItem(4, null)));
            Output("IsMemberFlag returns false as expected");
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
