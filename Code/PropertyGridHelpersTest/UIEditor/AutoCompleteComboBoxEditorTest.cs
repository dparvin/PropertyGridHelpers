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
using Xunit.Extensions;
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
        /// Test Enum with no values.
        /// </summary>
        public enum EmptyEnum
        {
        }

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
        /// Test class with no attribute.
        /// </summary>
        public class NoValuesProperty
        {
        }

        /// <summary>
        /// Test class with no attribute.
        /// </summary>
        public class InvalidValuesType
        {
            /// <summary>
            /// Gets the values.
            /// </summary>
            /// <value>
            /// The values.
            /// </value>
            public static int[] Values =>
#if NET5_0_OR_GREATER
                [1, 2, 3];
#else
                new[] { 1, 2, 3 };
#endif
        }

        /// <summary>
        /// Test class with no attribute that returns null values.
        /// </summary>
        public class ValuesReturnsNull
        {
            /// <summary>
            /// Gets the values.
            /// </summary>
            /// <value>
            /// The values.
            /// </value>
            public static string[] Values => null;
        }

        /// <summary>
        /// Test class with values provider.
        /// </summary>
        public class ProviderWithValues
        {
            /// <summary>
            /// Gets the values.
            /// </summary>
            /// <value>
            /// The values.
            /// </value>
#if NET5_0_OR_GREATER
            public static string[] Values => ["Red", "Green", "Blue"];
#else
            public static string[] Values => new[] { "Red", "Green", "Blue" };
#endif
        }

        /// <summary>
        /// Test class with no values provider.
        /// </summary>
        public class ProviderWithNoValues
        {
            /// <summary>
            /// Gets the values.
            /// </summary>
            /// <value>
            /// The values.
            /// </value>
#if NET5_0_OR_GREATER
            public static string[] Values => [];
#else
            public static string[] Values => new string[0];
#endif
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

            /// <summary>
            /// Gets or sets the provider type null cannot infer.
            /// </summary>
            /// <value>
            /// The provider type null cannot infer.
            /// </value>
            [Editor(typeof(AutoCompleteComboBoxEditor), typeof(UITypeEditor))]
            [AutoCompleteSetup(providerType: null)]
            public object ProviderTypeNullCannotInfer
            {
                get; set;
            }

            [Editor(typeof(AutoCompleteComboBoxEditor), typeof(UITypeEditor))]
            [AutoCompleteSetup(providerType: null)] // sets Mode = Provider, and ProviderType = null
            public TestEnum PropertyUsingInferredProvider
            {
                get; set;
            }

            /// <summary>
            /// Gets or sets the empty type of the enum.
            /// </summary>
            /// <value>
            /// The empty type of the enum.
            /// </value>
            [Editor(typeof(AutoCompleteComboBoxEditor), typeof(UITypeEditor))]
            [AutoCompleteSetup(typeof(EmptyEnum))]
            public EmptyEnum EmptyEnumType
            {
                get; set;
            }

            /// <summary>
            /// Gets or sets the provider missing values property.
            /// </summary>
            /// <value>
            /// The provider missing values property.
            /// </value>
            [Editor(typeof(AutoCompleteComboBoxEditor), typeof(UITypeEditor))]
            [AutoCompleteSetup(typeof(NoValuesProperty))]
            public string ProviderMissingValuesProperty
            {
                get; set;
            }

            /// <summary>
            /// Gets or sets the type of the provider with invalid values.
            /// </summary>
            /// <value>
            /// The type of the provider with invalid values.
            /// </value>
            [Editor(typeof(AutoCompleteComboBoxEditor), typeof(UITypeEditor))]
            [AutoCompleteSetup(providerType: typeof(InvalidValuesType))]
            public string ProviderWithInvalidValuesType
            {
                get; set;
            }

            /// <summary>
            /// Gets or sets the provider values returns null.
            /// </summary>
            /// <value>
            /// The provider values returns null.
            /// </value>
            [Editor(typeof(AutoCompleteComboBoxEditor), typeof(UITypeEditor))]
            [AutoCompleteSetup(providerType: typeof(ValuesReturnsNull))]
            public string ProviderValuesReturnsNull
            {
                get; set;
            }

            /// <summary>
            /// Gets or sets the property with empty values.
            /// </summary>
            /// <value>
            /// The property with empty values.
            /// </value>
            [Editor(typeof(AutoCompleteComboBoxEditor), typeof(UITypeEditor))]
            [AutoCompleteSetup(typeof(ProviderWithNoValues))]
            public string PropertyWithEmptyValues
            {
                get; set;
            }

            /// <summary>
            /// Gets or sets the property with valid provider values.
            /// </summary>
            /// <value>
            /// The property with valid provider values.
            /// </value>
            [Editor(typeof(AutoCompleteComboBoxEditor), typeof(UITypeEditor))]
            [AutoCompleteSetup(typeof(ProviderWithValues))]
            public string PropertyWithValidProviderValues
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
                Output($"result = '{(result ?? "(null)")}'");
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
                Assert.Contains("At least one value must be specified when using AutoCompleteSetupAttribute with a value list.", ex.Message);
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
                Assert.Contains("At least one value must be specified when using AutoCompleteSetupAttribute with a value list.", ex.Message);
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

        /// <summary>
        /// Edits the value throws when setup attribute is missing.
        /// </summary>
        [Theory]
        [InlineData(nameof(TestClassWithAttribute.EmptyEnumType), "does not define any members")]
        [InlineData(nameof(TestClassWithAttribute.ProviderMissingValuesProperty), "must define a public static property named")]
        [InlineData(nameof(TestClassWithAttribute.ProviderWithInvalidValuesType), "must be of type string[]")]
        [InlineData(nameof(TestClassWithAttribute.ProviderValuesReturnsNull), "returned no items")]
        public void EditValue_Throws_ProviderSetupErrors(string propertyName, string expectedMessage) =>
            StaTestHelper.Run(() =>
            {
                var context = CustomTypeDescriptorContext.Create(typeof(TestClassWithAttribute), propertyName);
                var editor = new AutoCompleteComboBoxEditor();

                var ex = Assert.Throws<InvalidOperationException>(() =>
                    editor.EditValue(context, new CustomServiceProvider(), null));

                Output($"Exception Message Received: {ex.Message}");
                Assert.Contains(expectedMessage, ex.Message);
            });

        /// <summary>
        /// Edits the value uses property type when provider type is null.
        /// </summary>
        [Fact]
        public void EditValue_UsesPropertyType_WhenProviderTypeIsNull() =>
            StaTestHelper.Run(() =>
            {
                var context = CustomTypeDescriptorContext.Create(
                    typeof(TestClassWithAttribute),
                    nameof(TestClassWithAttribute.PropertyUsingInferredProvider));

                var editor = new AutoCompleteComboBoxEditor();
                var provider = new CustomServiceProvider(); // attribute comes from the property, so nothing to inject
                provider.AddService(typeof(IWindowsFormsEditorService), new FakeEditorService());
                var result = editor.EditValue(context, provider, TestEnum.Test1);

                Assert.NotNull(result); // Success path proves fallback is used
            });

        /// <summary>
        /// Edits the value throws when provider values returns null or empty.
        /// </summary>
        [Fact]
        public void EditValue_Throws_WhenProviderValuesReturnsNull() =>
            StaTestHelper.Run(() =>
            {
                var context = CustomTypeDescriptorContext.Create(
                    typeof(TestClassWithAttribute),
                    nameof(TestClassWithAttribute.ProviderValuesReturnsNull));

                var editor = new AutoCompleteComboBoxEditor();
                var provider = new CustomServiceProvider();

                var ex = Assert.Throws<InvalidOperationException>(() =>
                    editor.EditValue(context, provider, null));

                Assert.Contains("returned no items", ex.Message);
            });

        /// <summary>
        /// Edits the value throws when provider values returns empty.
        /// </summary>
        [Fact]
        public void EditValue_Throws_WhenProviderValuesReturnsEmpty() =>
            StaTestHelper.Run(() =>
            {
                var context = CustomTypeDescriptorContext.Create(
                    typeof(TestClassWithAttribute),
                    nameof(TestClassWithAttribute.PropertyWithEmptyValues));

                var editor = new AutoCompleteComboBoxEditor();
                var provider = new CustomServiceProvider();

                var ex = Assert.Throws<InvalidOperationException>(() =>
                    editor.EditValue(context, provider, null));

                Assert.Contains("returned no items", ex.Message);
            });

        /// <summary>
        /// Edits the value uses values when provider returns non empty array.
        /// </summary>
        [Fact]
        public void EditValue_UsesValues_WhenProviderReturnsNonEmptyArray() =>
            StaTestHelper.Run(() =>
            {
                var context = CustomTypeDescriptorContext.Create(
                    typeof(TestClassWithAttribute),
                    nameof(TestClassWithAttribute.PropertyWithValidProviderValues));

                var editor = new AutoCompleteComboBoxEditor();
                var provider = new CustomServiceProvider();
                provider.AddService(typeof(IWindowsFormsEditorService), new FakeEditorService());

                var result = editor.EditValue(context, provider, null);

                Assert.NotNull(result);
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
