using PropertyGridHelpers.Controls;
using PropertyGridHelpersTest.Support;
using System;
using System.Reflection;
using System.Threading;
using System.Windows.Forms;
using Xunit;

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
#elif NET10_0
namespace PropertyGridHelpersTest.net100.Controls
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

        #region Test routines ^^^^^^^^^^^^^^^^^^^^^^^^^^^^^

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
                    Assert.Null(ctl.Culture);
                    Assert.Null(ctl.Context);
#else
                    Assert.Equal("Test", ctl.Text);
                    Assert.Null(ctl.Culture);
                    Assert.Null(ctl.Context);
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
        /// Called when selected value changed raises value committed.
        /// </summary>
        [Fact]
        public void OnSelectedValueChanged_RaisesValueCommitted() =>
            StaTestHelper.Run(() =>
            {
                var comboBox = new AutoCompleteComboBox();
                var eventRaised = false;

                comboBox.ValueCommitted += (s, e) => eventRaised = true;

                // Simulate selecting a value
                _ = comboBox.Items.Add("Test");
                _ = comboBox.Items.Add("Test1");
                comboBox.SelectedIndex = 1;

                Output($"{nameof(eventRaised)} = {eventRaised}");
                Assert.True(eventRaised, "ValueCommitted should be raised on SelectedValueChanged");
            });

        /// <summary>
        /// Called when leave raises value committed.
        /// </summary>
        [Fact]
        public void OnLeave_RaisesValueCommitted() =>
            StaTestHelper.Run(() =>
            {
                var comboBox = new AutoCompleteComboBox();
                var eventRaised = false;
                comboBox.ValueCommitted += (s, e) => eventRaised = true;

                // Directly invoke OnLeave
                var eventArgs = EventArgs.Empty;
                comboBox.GetType()
                        .GetMethod("OnLeave", BindingFlags.NonPublic | BindingFlags.Instance)
#if NET5_0_OR_GREATER
                        .Invoke(comboBox, [eventArgs]);
#else
                        .Invoke(comboBox, new object[] { eventArgs });
#endif

                Output($"{nameof(eventRaised)} = {eventRaised}");
                Assert.True(eventRaised, "ValueCommitted should be raised on Leave");
            });

        /// <summary>
        /// Automatics the complete ComboBox value setter selects matching item wrapper.
        /// </summary>
        [Fact]
        public void AutoCompleteComboBox_ValueSetter_SelectsMatchingItemWrapper() =>
            StaTestHelper.Run(() =>
            {
                var combo = new AutoCompleteComboBox();
                var item1 = new ItemWrapper<object>("One", 1);
                var item2 = new ItemWrapper<object>("Two", 2);

                _ = combo.Items.Add(item1);
                _ = combo.Items.Add(item2);
                combo.SelectedIndex = 0; // Select first item

                Assert.Equal(1, combo.Value);
                Assert.Equal(item1, combo.SelectedItem);

                combo.Value = 2;

                Assert.Equal(item2, combo.SelectedItem);
            });

        #endregion

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
