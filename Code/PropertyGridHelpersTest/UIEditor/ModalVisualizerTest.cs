using PropertyGridHelpers.ServiceProviders;
using PropertyGridHelpers.TypeDescriptors;
using PropertyGridHelpers.UIEditors;
using PropertyGridHelpersTest.Controls;
using PropertyGridHelpersTest.Support;
using System.ComponentModel;
using System.Drawing.Design;
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
#elif NET48
namespace PropertyGridHelpersTest.net480.UIEditor
#elif NET481
namespace PropertyGridHelpersTest.net481.UIEditor
#elif NET8_0
namespace PropertyGridHelpersTest.net80.UIEditor
#elif NET9_0
namespace PropertyGridHelpersTest.net90.UIEditor
#elif NET10_0
namespace PropertyGridHelpersTest.net100.UIEditor
#endif
{
#if NET5_0_OR_GREATER
    /// <summary>
    /// Tests for the <see cref="ModalVisualizer{TForm}" />
    /// </summary>
    /// <param name="output">xunit output implementation</param>
    public class ModalVisualizerTest(ITestOutputHelper output)
#else
	/// <summary>
	/// Tests for the <see cref="ModalVisualizer{TForm}" />
	/// </summary>
	public class ModalVisualizerTest
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
        public ModalVisualizerTest(ITestOutputHelper output) =>
            OutputHelper = output;
#endif

		#region Test Methods ^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^

		/// <summary>
		/// Modal visualizer should return edited value.
		/// </summary>
		[Fact]
		public void ModalVisualizer_ShouldReturnEditedValue()
		{
			// Arrange
			var editor = new ModalVisualizer<FakeEditorForm>();
			var initialValue = "InitialValue";

			// Act
			var result = editor.EditValue(null, null, initialValue);
			var Style = editor.GetEditStyle(null);

			// Assert
			Assert.Equal("NewTestValue", result);
			Assert.Equal(UITypeEditorEditStyle.Modal, Style);

			Output($"ModalVisualizer returned: {result}");
		}

		/// <summary>
		/// Modal visualizer should return original value when canceled.
		/// </summary>
		[Fact]
		public void ModalVisualizer_ShouldReturnOriginalValue_WhenCanceled()
		{
			// Arrange
			var editor = new ModalVisualizer<FakeEditorCancelForm>();
			var initialValue = "InitialValue";

			// Act
			var result = editor.EditValue(null, null, initialValue);
			var Style = editor.GetEditStyle(null);

			// Assert
			Assert.Equal("InitialValue", result);
			Assert.Equal(UITypeEditorEditStyle.Modal, Style);

			Output($"ModalVisualizer returned: {result}");
		}

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
