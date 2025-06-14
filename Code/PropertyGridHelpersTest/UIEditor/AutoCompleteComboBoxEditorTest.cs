using PropertyGridHelpers.Attributes;
using PropertyGridHelpers.Converters;
using PropertyGridHelpers.ServiceProviders;
using PropertyGridHelpers.TypeDescriptors;
using PropertyGridHelpers.UIEditors;
using PropertyGridHelpersTest.Support;
using System;
using System.ComponentModel;
using System.Drawing.Design;
using System.Windows.Forms;
using System.Windows.Forms.Design;
using Xunit;

#if NET35
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
    public class AutoCompleteComboBoxEditorTest(ITestOutputHelper output)
#else
    /// <summary>
    /// AutoCompleteComboBoxEditor Tests
    /// </summary>
    public class AutoCompleteComboBoxEditorTest
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
        /// Initializes a new instance of the <see cref="AutoCompleteComboBoxEditorTest"/> class.
        /// </summary>
        public AutoCompleteComboBoxEditorTest()
        {
        }
#elif NET8_0_OR_GREATER
#else
        /// <summary>
        /// Collection UI Editor Test
        /// </summary>
        /// <param name="output">xunit output implementation</param>
        public AutoCompleteComboBoxEditorTest(ITestOutputHelper output) =>
            OutputHelper = output;
#endif

        #region Test classes ^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^

        /// <summary>
        /// Test Enum
        /// </summary>
        public enum TestEnum
        {
            /// <summary>
            /// The test1
            /// </summary>
            [EnumImage("TestItem1")]
            Test1,
            /// <summary>
            /// The test2
            /// </summary>
            [EnumImage]
            Test2
        }

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
            [Editor(typeof(AutoCompleteComboBoxEditor), typeof(UITypeEditor))]
            [AutoCompleteSetup(AutoCompleteSource.CustomSource, AutoCompleteMode.SuggestAppend, "Red", "Green", "Blue")]
            public string PropertyWithAttribute
            {
                get; set;
            }

            /// <summary>
            /// Gets or sets the property with attribute missing values.
            /// </summary>
            /// <value>
            /// The property with attribute missing values.
            /// </value>
            [AllowBlank(includeItem: true, resourceItem: "Blank_ResourcePath")]
            [Editor(typeof(AutoCompleteComboBoxEditor), typeof(UITypeEditor))]
            [AutoCompleteSetup(AutoCompleteSource.CustomSource, AutoCompleteMode.SuggestAppend)]
            public string PropertyWithAttributeMissingValues
            {
                get; set;
            }

            /// <summary>
            /// Gets or sets the property with attribute null values.
            /// </summary>
            /// <value>
            /// The property with attribute null values.
            /// </value>
            [AllowBlank(includeItem: true, resourceItem: "Blank_ResourcePath")]
            [Editor(typeof(AutoCompleteComboBoxEditor), typeof(UITypeEditor))]
            [AutoCompleteSetup(AutoCompleteSource.CustomSource, AutoCompleteMode.SuggestAppend, null)]
            public string PropertyWithAttributeNullValues
            {
                get; set;
            }
        }

        #endregion

        #region Unit Tests ^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^

        /// <summary>
        /// Edits the value with null context returns original value.
        /// </summary>
        [Fact]
        public void EditValue_ReturnsNull_WithNullEntriesTest() =>
            StaTestHelper.Run(() =>
            {
                var editor = new AutoCompleteComboBoxEditor();
                var result = editor.EditValue(null, null, "original");
                Output($"result = '{result}'");
                Assert.Null(result);
            });

        /// <summary>
        /// Edits the value returns values test.
        /// </summary>
        [Fact]
        public void EditValue_ReturnsValuesTest() =>
            StaTestHelper.Run(() =>
            {
                // Arrange
                var instance = new TestClassWithAttribute();

                var propDesc = TypeDescriptor.GetProperties(instance)["PropertyWithoutAttribute"];
                var context = new CustomTypeDescriptorContext(propDesc, instance);

                var editor = new AutoCompleteComboBoxEditor();
                var serviceProvider = new CustomServiceProvider();
                serviceProvider.AddService(typeof(IWindowsFormsEditorService), new FakeEditorService());

                // Act
                var result = editor.EditValue(context, serviceProvider, "(none)");

                // Assert
                Output($"Results = '{result}'");
#if NET5_0_OR_GREATER
                Assert.Equal("(none)", result);
#else
                Assert.Equal(0, string.Compare("(none)", (string)result));
#endif
            });

        /// <summary>
        /// Edits the value with property returns values test.
        /// </summary>
        [Fact]
        public void EditValue_WithProperty_ReturnsValuesTest() =>
            StaTestHelper.Run(() =>
            {
                // Arrange
                var instance = new TestClassWithAttribute();

                var propDesc = TypeDescriptor.GetProperties(instance)["PropertyWithAttribute"];
                var context = new CustomTypeDescriptorContext(propDesc, instance);

                var editor = new AutoCompleteComboBoxEditor();
                var serviceProvider = new CustomServiceProvider();
                serviceProvider.AddService(typeof(IWindowsFormsEditorService), new FakeEditorService());

                // Act
                var result = editor.EditValue(context, serviceProvider, "(none)");

                // Assert
                Output($"Results = '{result}'");
#if NET5_0_OR_GREATER
                Assert.Equal("(none)", result);
#else
                Assert.Equal(0, string.Compare("(none)", (string)result));
#endif
            });

        /// <summary>
        /// Edits the value with property missing values returns null test.
        /// </summary>
        [Fact]
        public void EditValue_WithProperty_MissingValues_ReturnsNullTest() =>
            StaTestHelper.Run(() =>
            {
                // Arrange
                var instance = new TestClassWithAttribute();

                var propDesc = TypeDescriptor.GetProperties(instance)["PropertyWithAttributeMissingValues"];
                var context = new CustomTypeDescriptorContext(propDesc, instance);

                var editor = new AutoCompleteComboBoxEditor();
                var serviceProvider = new CustomServiceProvider();
                serviceProvider.AddService(typeof(IWindowsFormsEditorService), new FakeEditorService());

                // Act & Assert
                var ex = Assert.Throws<InvalidOperationException>(() =>
                {
                    _ = editor.EditValue(context, serviceProvider, "(none)");
                });

                Output($"Caught expected exception: {ex.Message}");
                if (ex.InnerException != null)
                    Output($"    With the inner exception of: {ex.InnerException.Message}");
                Assert.Contains("The AutoCompleteSetupAttribute on property 'PropertyWithAttributeMissingValues' could not be initialized.", ex.Message);
            });

        /// <summary>
        /// Edits the value with property null values returns null test.
        /// </summary>
        [Fact]
        public void EditValue_WithProperty_NullValues_ReturnsNullTest() =>
            StaTestHelper.Run(() =>
            {
                // Arrange
                var instance = new TestClassWithAttribute();

                var propDesc = TypeDescriptor.GetProperties(instance)["PropertyWithAttributeNullValues"];
                var context = new CustomTypeDescriptorContext(propDesc, instance);

                var editor = new AutoCompleteComboBoxEditor();
                var serviceProvider = new CustomServiceProvider();
                serviceProvider.AddService(typeof(IWindowsFormsEditorService), new FakeEditorService());

                // Act & Assert
                var ex = Assert.Throws<InvalidOperationException>(() =>
                {
                    _ = editor.EditValue(context, serviceProvider, "(none)");
                });

                Output($"Caught expected exception: {ex.Message}");
                if (ex.InnerException != null)
                    Output($"    With the inner exception of: {ex.InnerException.Message}");
                Assert.Contains("The AutoCompleteSetupAttribute on property 'PropertyWithAttributeNullValues' could not be initialized.", ex.Message);
            });

        /// <summary>
        /// Automatics the complete ComboBox editor generic assigns expected converter.
        /// </summary>
        [Fact]
        public void AutoCompleteComboBoxEditor_Generic_AssignsExpectedConverter() =>
            StaTestHelper.Run(() =>
            {
                // Arrange
                var editor = new AutoCompleteComboBoxEditor<EnumTextConverter<TestEnum>>();

                // Act
                var converter = editor.Converter;

                // Assert
                Assert.NotNull(converter);
                _ = Assert.IsType<EnumTextConverter<TestEnum>>(converter);

                Output("AutoCompleteComboBoxEditor<T> assigns the correct EnumTextConverter<T>.");
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
