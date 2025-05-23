using PropertyGridHelpers.Controls;
using Xunit;
using System.Reflection;
using System;
using System.Threading;

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
#elif NET472
namespace PropertyGridHelpersTest.net472.Controls
#elif NET481
namespace PropertyGridHelpersTest.net481.Controls
#elif NET8_0
namespace PropertyGridHelpersTest.net80.Controls
#elif NET9_0
namespace PropertyGridHelpersTest.net90.Controls
#endif
{
#if NET5_0_OR_GREATER
    /// <summary>
    /// Test class for <see cref="AutoCompleteComboBox"/>.
    /// </summary>
    public class AutoCompleteComboBoxTest(ITestOutputHelper output)
#else
    /// <summary>
    /// Test class for <see cref="AutoCompleteComboBox"/>.
    /// </summary>
    public class AutoCompleteComboBoxTest
#endif
    {
#if NET35
#else
#if NET5_0_OR_GREATER
        private readonly ITestOutputHelper OutputHelper = output;
#else
        private readonly Xunit.Abstractions.ITestOutputHelper OutputHelper;

        /// <summary>
        /// Initializes a new instance of the <see cref="AutoCompleteComboBoxTest"/> class.
        /// </summary>
        /// <param name="output">The output.</param>
        public AutoCompleteComboBoxTest(
            Xunit.Abstractions.ITestOutputHelper output) => OutputHelper = output;
#endif
#endif

        /// <summary>
        /// Should touch real code.
        /// </summary>
        [Fact]
        public void ShouldTouchRealCode()
        {
            Exception exception = null;

            var thread = new Thread(() =>
            {
                try
                {
                    var ctl = new AutoCompleteComboBox() { Text = "Test" };
#if NET35
                    Assert.Equal("Test", ctl.Value);
#else
            Assert.Equal("Test", ctl.Text);
#endif
                }
                catch (Exception ex)
                {
                    exception = ex;
                }
            });

            thread.SetApartmentState(ApartmentState.STA);
            thread.Start();
            thread.Join();

            if (exception != null)
                throw new TargetInvocationException(exception);
        }

        /// <summary>
        /// Outputs the specified message.
        /// </summary>
        /// <param name="message">The message.</param>
#if NET35
        private static void Output(string message) =>
            Console.WriteLine(message);
#else
        private void Output(string message) =>
            OutputHelper.WriteLine(message);
#endif
    }
}
