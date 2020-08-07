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
    public class FlagCheckedListBoxItemTest
    {
#if NET35
#else
        readonly ITestOutputHelper Output;

        /// <summary>
        /// Initializes a new instance of the <see cref="FlagCheckedListBoxItemTest"/> class.
        /// </summary>
        /// <param name="output">The output.</param>
        public FlagCheckedListBoxItemTest(
            ITestOutputHelper output)

        {
            Output = output;
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
#if NET35
            Console.WriteLine("Caption set to 'Test' as expected");
#else
            Output.WriteLine("Caption set to 'Test' as expected");
#endif
        }

        /// <summary>
        /// Value is set correctly test.
        /// </summary>
        [Fact]
        public void ValueSetCorrectlyTest()
        {
            var item = new FlagCheckedListBoxItem(1, "Test");
            Assert.Equal(1, item.Value);
#if NET35
            Console.WriteLine("Value set to 1 as expected");
#else
            Output.WriteLine("Value set to 1 as expected");
#endif
        }

        /// <summary>
        /// Null the parameters handled correctly test.
        /// </summary>
        [Fact]
        public void NullParametersHandledCorrectlyTest()
        {
            var item = new FlagCheckedListBoxItem(0, null);
            Assert.Null(item.Caption);
#if NET35
            Console.WriteLine("Caption set to null as expected");
#else
            Output.WriteLine("Caption set to null as expected");
#endif
        }

        /// <summary>
        /// Determines whether IsFlag is true test.
        /// </summary>
        [Fact]
        public void IsFlagIsTrueTest()
        {
            var item = new FlagCheckedListBoxItem(4, null);
            Assert.True(item.IsFlag);
#if NET35
            Console.WriteLine("IsFlag returns true as expected");
#else
            Output.WriteLine("IsFlag returns true as expected");
#endif
        }

        /// <summary>
        /// Determines whether IsFlag is false test.
        /// </summary>
        [Fact]
        public void IsFlagIsFalseTest()
        {
            var item = new FlagCheckedListBoxItem(5, null);
            Assert.False(item.IsFlag);
#if NET35
            Console.WriteLine("IsFlag returns false as expected");
#else
            Output.WriteLine("IsFlag returns false as expected");
#endif
        }

        /// <summary>
        /// Determines whether IsMemberFlag is true test.
        /// </summary>
        [Fact]
        public void IsMemberFlagIsTrueTest()
        {
            var item = new FlagCheckedListBoxItem(4, null);
            Assert.True(item.IsMemberFlag(new FlagCheckedListBoxItem(5, null)));
#if NET35
            Console.WriteLine("IsMemberFlag returns true as expected");
#else
            Output.WriteLine("IsMemberFlag returns true as expected");
#endif
        }

        /// <summary>
        /// Determines whether IsMemberFlag is false test.
        /// </summary>
        [Fact]
        public void IsMemberFlagIsFalseTest()
        {
            var item = new FlagCheckedListBoxItem(5, null);
            Assert.False(item.IsMemberFlag(new FlagCheckedListBoxItem(4, null)));
#if NET35
            Console.WriteLine("IsMemberFlag returns false as expected");
#else
            Output.WriteLine("IsMemberFlag returns false as expected");
#endif
        }
    }
}
