using PropertyGridHelpers.UIEditors;
using Xunit;
using System.Drawing.Design;
using System.ComponentModel;
using PropertyGridHelpers.TypeDescriptors;
using PropertyGridHelpers.ServiceProviders;
using System.Windows.Forms.Design;
using PropertyGridHelpersTest.Support;
using PropertyGridHelpers.Attributes;


#if NET35
using Xunit.Abstractions;
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
#if NET8_0_OR_GREATER
    /// <summary>
    /// Class for testing the ResourcePathEditorTest class
    /// </summary>
    /// <param name="output">The output.</param>
    public class ResourcePathEditorTest(ITestOutputHelper output)
#else
    /// <summary>
    /// Class for testing the ResourcePathEditorTest class
    /// </summary>
    public class ResourcePathEditorTest
#endif
    {
#if NET35
#elif NET8_0_OR_GREATER
        private readonly ITestOutputHelper OutputHelper = output;
#else
        private readonly ITestOutputHelper OutputHelper;
#endif

#if NET35
        /// <summary>
        /// Resource Path Editor Test
        /// </summary>
        public ResourcePathEditorTest()
        {
        }
#elif NET8_0_OR_GREATER
#else
        /// <summary>
        /// Resource Path Editor Test
        /// </summary>
        /// <param name="output">xunit output implementation</param>
        public ResourcePathEditorTest(ITestOutputHelper output) 
            : base() => 
            OutputHelper = output;
#endif

        #region Test classes ^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^

        /// <summary>
        /// Test class with attribute.
        /// </summary>
        private class TestClassWithAttribute
        {
            /// <summary>
            /// Gets or sets the property without attribute.
            /// </summary>
            /// <value>
            /// The property without attribute.
            /// </value>
            public string PropertyWithoutAttribute
            {
                get; set;
            }

            /// <summary>
            /// Gets or sets the property with attribute.
            /// </summary>
            /// <value>
            /// The property with attribute.
            /// </value>
            [AllowBlank(includeItem: true, resourceItem: "Blank_ResourcePath")]
            public string PropertyWithAttribute
            {
                get; set;
            }
        }

        #endregion

        #region Unit Tests ^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^

        /// <summary>
        /// Gets the edit style returns drop down.
        /// </summary>
        [Fact]
        public void GetEditStyle_ReturnsDropDown()
        {
            var editor = new ResourcePathEditor();
            Assert.Equal(UITypeEditorEditStyle.DropDown, editor.GetEditStyle(null));
        }

        /// <summary>
        /// Gets the paint value supported returns false.
        /// </summary>
        [Fact]
        public void GetPaintValueSupported_ReturnsFalse()
        {
            var editor = new ResourcePathEditor();
            Assert.False(editor.GetPaintValueSupported(null));
        }

        /// <summary>
        /// Determines whether is drop down resizable returns false.
        /// </summary>
        [Fact]
        public void IsDropDownResizable_ReturnsFalse()
        {
            var editor = new ResourcePathEditor();
            Assert.False(editor.IsDropDownResizable);
        }

        /// <summary>
        /// Edits the value with null context returns original value.
        /// </summary>
        [Fact]
        public void EditValue_WithNullContext_ReturnsOriginalValue()
        {
            var editor = new ResourcePathEditor();
            var result = editor.EditValue(null, null, "original");
            Output($"result = '{result}'");
            Assert.Equal("original", result);
        }

        /// <summary>
        /// Edits the name of the value uses extractor and returns selected value first base.
        /// </summary>
        [Fact]
        public void EditValue_UsesExtractorAndReturnsSelectedValue_FirstBaseName()
        {
            // Arrange
            var instance = new TestClassWithAttribute();

            var propDesc = TypeDescriptor.GetProperties(instance)["PropertyWithoutAttribute"];
            var context = new CustomTypeDescriptorContext(propDesc, instance);

            var editor = new ResourcePathEditor();
            var serviceProvider = new CustomServiceProvider();
            serviceProvider.AddService(typeof(IWindowsFormsEditorService), new FakeEditorService());

            // Act
            var result = editor.EditValue(context, serviceProvider, "(none)");

            // Assert
            Output($"Results = '{result}'");
#if NET5_0_OR_GREATER
            Assert.Equal("EmptyResourceFile", result);
#else
            Assert.Equal(0, string.Compare("EmptyResourceFile", (string)result));
#endif
        }

        /// <summary>
        /// Edits the name of the value uses extractor and returns selected value first base.
        /// </summary>
        [Fact]
        public void EditValue_UsesExtractorAndReturnsSelectedValue_BlankReplacement()
        {
            // Arrange
            var instance = new TestClassWithAttribute();

            var propDesc = TypeDescriptor.GetProperties(instance)["PropertyWithAttribute"];
            var context = new CustomTypeDescriptorContext(propDesc, instance);

            var editor = new ResourcePathEditor();
            var serviceProvider = new CustomServiceProvider();
            serviceProvider.AddService(typeof(IWindowsFormsEditorService), new FakeEditorService());

            // Act
            var result = editor.EditValue(context, serviceProvider, "(none)");

            // Assert
            Output($"Results = '{result}'");
            Assert.Empty((string)result);
        }

        /// <summary>
        /// Edits the name of the value uses extractor and returns selected value first base.
        /// </summary>
        [Fact]
        public void EditValue_UsesExtractorAndReturnsSelectedValue_PassedItem()
        {
            // Arrange
            var instance = new TestClassWithAttribute();

            var propDesc = TypeDescriptor.GetProperties(instance)["PropertyWithAttribute"];
            var context = new CustomTypeDescriptorContext(propDesc, instance);

            var editor = new ResourcePathEditor();
            var serviceProvider = new CustomServiceProvider();
            serviceProvider.AddService(typeof(IWindowsFormsEditorService), new FakeEditorService());

            // Act
            var result = editor.EditValue(context, serviceProvider, "EmptyResourceFile");

            // Assert
            Output($"Results = '{result}'");
#if NET5_0_OR_GREATER
            Assert.Equal("EmptyResourceFile", result);
#else
            Assert.Equal(0, string.Compare("EmptyResourceFile", (string)result));
#endif
        }

        /// <summary>
        /// Edits the value ignores value when not string.
        /// </summary>
        [Fact]
        public void EditValue_IgnoresValue_WhenNotString()
        {
            // Arrange
            var instance = new TestClassWithAttribute();

            var propDesc = TypeDescriptor.GetProperties(instance)["PropertyWithAttribute"];
            var context = new CustomTypeDescriptorContext(propDesc, instance);

            var editor = new ResourcePathEditor();
            var serviceProvider = new CustomServiceProvider();
            serviceProvider.AddService(typeof(IWindowsFormsEditorService), new FakeEditorService());

            // Act
            var result = editor.EditValue(context, serviceProvider, 42);

            // Assert
            Output($"Results = '{result}'");
            Assert.Empty((string)result);
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
