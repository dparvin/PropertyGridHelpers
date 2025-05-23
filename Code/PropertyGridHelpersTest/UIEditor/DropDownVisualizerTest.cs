using PropertyGridHelpers.ServiceProviders;
using PropertyGridHelpers.TypeDescriptors;
using PropertyGridHelpers.UIEditors;
using PropertyGridHelpersTest.Controls;
using PropertyGridHelpersTest.Support;
using System.ComponentModel;
using System.Windows.Forms.Design;
using Xunit;

#if NET35
using System;
#else
using Xunit.Abstractions;
#endif

#if NET35
namespace PropertyGridHelpersTest.net35.UIEditor
#elif NET452
namespace PropertyGridHelpersTest.net452.UIEditor
#elif NET462
namespace PropertyGridHelpersTest.net462.UIEditor
#elif NET472
namespace PropertyGridHelpersTest.net472.UIEditor
#elif NET481
namespace PropertyGridHelpersTest.net481.UIEditor
#elif NET8_0
namespace PropertyGridHelpersTest.net80.UIEditor
#elif NET9_0
namespace PropertyGridHelpersTest.net90.UIEditor
#endif
{
#if NET5_0_OR_GREATER
    /// <summary>
    /// Tests for the <see cref="DropDownVisualizer{TControl}" />
    /// </summary>
    /// <param name="output">xunit output implementation</param>
    public class DropDownVisualizerTest(ITestOutputHelper output)
#else
    /// <summary>
    /// Tests for the <see cref="DropDownVisualizer{TControl}" />
    /// </summary>
    public class DropDownVisualizerTest
#endif
    {
#if NET35
#elif NET5_0_OR_GREATER
        private readonly ITestOutputHelper OutputHelper = output;
#else
        private readonly ITestOutputHelper OutputHelper;

        /// <summary>
        /// Initializes a new instance of the <see cref="DropDownVisualizerTest"/> class.
        /// </summary>
        /// <param name="output">xunit output implementation</param>
        public DropDownVisualizerTest(ITestOutputHelper output) =>
            OutputHelper = output;
#endif

        /// <summary>
        /// Test class with attribute.
        /// </summary>
        public class TestClassWithAttribute
        {
            /// <summary>
            /// Gets or sets some item.
            /// </summary>
            /// <value>
            /// Some item.
            /// </value>
            public string PropertyWithoutAttribute
            {
                get; set;
            }
        }

        /// <summary>
        /// Edits the value closes drop down when value committed is raised.
        /// </summary>
        [Fact]
        public void EditValue_SetsValueAndClosesDropDown_WhenValueCommittedIsRaised()
        {
            // Arrange
            var instance = new TestClassWithAttribute();

            var propDesc = TypeDescriptor.GetProperties(instance)["PropertyWithoutAttribute"];
            var context = new CustomTypeDescriptorContext(propDesc, instance);

            var editor = new DropDownVisualizer<FakeEditorControl>();
            var serviceProvider = new CustomServiceProvider();
            var fakeEditorService = new FakeEditorService();
            serviceProvider.AddService(typeof(IWindowsFormsEditorService), fakeEditorService);

            // Act
            var result = editor.EditValue(context, serviceProvider, "(none)");

            // Assert
            Output($"Results = '{result}'");
#if NET5_0_OR_GREATER
            Assert.Equal("EmptyResourceFile", result);
#else
            Assert.Equal(0, string.Compare("EmptyResourceFile", (string)result));
#endif
            Assert.True(fakeEditorService.DropDownClosed, "Expected the drop-down to be closed when ValueCommitted was raised.");
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
