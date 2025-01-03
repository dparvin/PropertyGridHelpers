using PropertyGridHelpers.Controls;
using Xunit;
#if NET35
using System;
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
#elif NET481
namespace PropertyGridHelpersTest.net481.Controls
#elif WINDOWS7_0
namespace PropertyGridHelpersTest.net60.W7.Controls
#elif WINDOWS10_0
namespace PropertyGridHelpersTest.net60.W10.Controls
#elif NET8_0
namespace PropertyGridHelpersTest.net80.Controls
#elif NET9_0
namespace PropertyGridHelpersTest.net90.Controls
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
        /// Tests IsMemberFlag returns false when IsFlag is false.
        /// </summary>
        [Fact]
        public void IsMemberFlagReturnsFalseWhenIsFlagIsFalse()
        {
            var item = new FlagCheckedListBoxItem(5, null); // Not a flag
            var composite = new FlagCheckedListBoxItem(7, null); // Composite value
            Assert.False(item.IsMemberFlag(composite));
            Output("IsMemberFlag returns false when IsFlag is false.");
        }

        /// <summary>
        /// Tests IsMemberFlag returns false when composite is null.
        /// </summary>
        [Fact]
        public void IsMemberFlagReturnsFalseWhenCompositeIsNull()
        {
            var item = new FlagCheckedListBoxItem(4, null); // A valid flag
            Assert.False(item.IsMemberFlag(null));
            Output("IsMemberFlag returns false when composite is null.");
        }

        /// <summary>
        /// Tests IsMemberFlag returns false when composite.Value does not overlap with Value.
        /// </summary>
        [Fact]
        public void IsMemberFlagReturnsFalseWhenNoOverlap()
        {
            var item = new FlagCheckedListBoxItem(4, null); // A valid flag
            var composite = new FlagCheckedListBoxItem(2, null); // No overlap with 4
            Assert.False(item.IsMemberFlag(composite));
            Output("IsMemberFlag returns false when composite.Value does not overlap with Value.");
        }

        /// <summary>
        /// Tests IsMemberFlag returns true when composite.Value partially overlaps with Value.
        /// </summary>
        [Fact]
        public void IsMemberFlagReturnsTrueWhenPartialOverlap()
        {
            var item = new FlagCheckedListBoxItem(4, null); // A valid flag
            var composite = new FlagCheckedListBoxItem(6, null); // Composite 6 = 4 + 2
            Assert.True(item.IsMemberFlag(composite));
            Output("IsMemberFlag returns true when composite.Value partially overlaps with Value.");
        }

        /// <summary>
        /// Tests IsMemberFlag returns true when composite.Value fully matches Value.
        /// </summary>
        [Fact]
        public void IsMemberFlagReturnsTrueWhenFullyMatches()
        {
            var item = new FlagCheckedListBoxItem(4, null); // A valid flag
            var composite = new FlagCheckedListBoxItem(4, null); // Composite matches exactly
            Assert.True(item.IsMemberFlag(composite));
            Output("IsMemberFlag returns true when composite.Value fully matches Value.");
        }

        /// <summary>
        /// to string test.
        /// </summary>
        [Fact]
        public void ToStringTest()
        {
            var caption = "Test Caption";
            var item = new FlagCheckedListBoxItem(5, caption);
            // Act / Assert
#if NET35
            Assert.Equal(0, string.Compare(item.ToString(), caption));
#else
            Assert.Equal(item.ToString(), caption);
#endif
            Output("ToString() returned the caption");
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
