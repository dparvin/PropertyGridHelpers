using PropertyGridHelpers.Attributes;
using Xunit;
using System.Windows.Forms;
using System;
using System.ComponentModel;
using PropertyGridHelpers.TypeDescriptors;

#if NET35
using System.Diagnostics;
using Xunit.Extensions;
#else
using Xunit.Abstractions;
#endif

#if NET35
namespace PropertyGridHelpersTest.net35.Attributes
#elif NET452
namespace PropertyGridHelpersTest.net452.Attributes
#elif NET462
namespace PropertyGridHelpersTest.net462.Attributes
#elif NET472
namespace PropertyGridHelpersTest.net472.Attributes
#elif NET481
namespace PropertyGridHelpersTest.net481.Attributes
#elif NET8_0
namespace PropertyGridHelpersTest.net80.Attributes
#elif NET9_0
namespace PropertyGridHelpersTest.net90.Attributes
#elif NET10_0
namespace PropertyGridHelpersTest.net100.Attributes
#endif
{
#if NET8_0_OR_GREATER
    /// <summary>
    /// AutoCompleteSetupAttribute Tests
    /// </summary>
    /// <param name="output">The output from the unit test.</param>
    public class AutoCompleteSetupAttributeTest(ITestOutputHelper output)
#else
    /// <summary>
    /// AutoCompleteSetupAttribute Tests
    /// </summary>
    public class AutoCompleteSetupAttributeTest
#endif
    {
#if NET35
#elif NET8_0_OR_GREATER
        private readonly ITestOutputHelper OutputHelper = output;
#elif NET40_OR_GREATER
        private readonly ITestOutputHelper OutputHelper;

        /// <summary>
        /// Initializes a new instance of the <see cref="AllowBlankAttributeTest"/> class.
        /// </summary>
        /// <param name="output">The output from the unit test.</param>
        public AutoCompleteSetupAttributeTest(
            ITestOutputHelper output) => OutputHelper = output;
#endif

        #region Test Support objects ^^^^^^^^^^^^^^^^^^^^^^

        /// <summary>
        /// Provides missing values for testing purposes.
        /// </summary>
        public class TestClass
        {
            /// <summary>
            /// Gets or sets the other item.
            /// </summary>
            /// <value>
            /// The other item.
            /// </value>
            [AutoCompleteSetup()]
            public string AutoCompleteItem
            {
                get; set;
            }

            /// <summary>
            /// Gets or sets the item without attribute.
            /// </summary>
            /// <value>
            /// The item without attribute.
            /// </value>
            public string ItemWithoutAttribute { get; set; } = "No attribute here";
        }

        #endregion

        #region Test Support objects ^^^^^^^^^^^^^^^^^^^^^^

        /// <summary>
        /// Sample enum for testing purposes.
        /// </summary>
        public enum SampleEnum
        {
            /// <summary>
            /// a
            /// </summary>
            A,
            /// <summary>
            /// b
            /// </summary>
            B
        }

        /// <summary>
        /// Sample empty enum for testing purposes.
        /// </summary>
        public enum SampleEmptyEnum
        {
        }

        /// <summary>
        /// Static string provider for testing purposes.
        /// </summary>
        public static class StaticStringProvider
        {
            /// <summary>
            /// Gets the values.
            /// </summary>
            /// <value>
            /// The values.
            /// </value>
            public static string[] Values =>
#if NET5_0_OR_GREATER
                ["Alpha", "Beta"];
#else
                new[] { "Alpha", "Beta" };
#endif
        }

        /// <summary>
        /// Provides missing values for testing purposes.
        /// </summary>
        public static class MissingValuesProvider
        {
        }

        /// <summary>
        /// Provides missing values for testing purposes.
        /// </summary>
        public static class ValuesWrongTypeProvider
        {
            /// <summary>
            /// Gets the values.
            /// </summary>
            /// <value>
            /// The values.
            /// </value>
            public static int[] Values =>
#if NET5_0_OR_GREATER
                [1, 2];
#else
                new[] { 1, 2 };
#endif
        }

        #endregion

        #region Test routines ^^^^^^^^^^^^^^^^^^^^^^^^^^^^^

        /// <summary>
        /// Automatics the complete setup attribute default test.
        /// </summary>
        [Fact]
        public void AutoCompleteSetupAttribute_Default_Test()
        {
            // Arrange
            var attribute = new AutoCompleteSetupAttribute();

            // Act
            var source = attribute.AutoCompleteSource;
            var style = attribute.DropDownStyle;
            var mode = attribute.AutoCompleteMode;
            var values = attribute.Values;
            var providerType = attribute.ProviderType;
            var sourceMode = attribute.Mode;

            // Assert
            Output($"AutoCompleteSource: {source}\nDropDownStyle: {style}\nAutoCompleteMode: {mode}\nValues: {values}\nProviderType: {(providerType == null ? "null" : providerType.ToString())}\nMode: {sourceMode}");
            Assert.Equal(AutoCompleteSource.None, source);
            Assert.Equal(ComboBoxStyle.DropDown, style);
            Assert.Equal(AutoCompleteMode.SuggestAppend, mode);
            Assert.Empty(values);
            Assert.Null(providerType);
            Assert.Equal(AutoCompleteSetupAttribute.SourceMode.Values, sourceMode);
            Output($"Attribute.ToString = {attribute}");
            var expectedOutput = "AutoCompleteSetupAttribute: AutoCompleteMode=SuggestAppend, AutoCompleteSource=None, DropDownStyle=DropDown";
#if NET35
            Assert.Equal(0, string.Compare(expectedOutput, attribute.ToString()));
#else
            Assert.Equal(expectedOutput, attribute.ToString());
#endif
        }

        /// <summary>
        /// Automatics the complete setup attribute source test.
        /// </summary>
        [Fact]
        public void AutoCompleteSetupAttribute_Source_Test()
        {
            // Arrange
            var attribute = new AutoCompleteSetupAttribute(AutoCompleteSource.FileSystem);

            // Act
            var source = attribute.AutoCompleteSource;
            var style = attribute.DropDownStyle;
            var mode = attribute.AutoCompleteMode;
            var values = attribute.Values;
            var providerType = attribute.ProviderType;
            var sourceMode = attribute.Mode;

            // Assert
            Output($"AutoCompleteSource: {source}\nDropDownStyle: {style}\nAutoCompleteMode: {mode}\nValues: {values}\nProviderType: {(providerType == null ? "null" : providerType.ToString())}\nMode: {sourceMode}");
            Assert.Equal(AutoCompleteSource.FileSystem, source);
            Assert.Equal(ComboBoxStyle.DropDown, style);
            Assert.Equal(AutoCompleteMode.SuggestAppend, mode);
            Assert.Empty(values);
            Assert.Null(providerType);
            Assert.Equal(AutoCompleteSetupAttribute.SourceMode.Values, sourceMode);
            Output($"Attribute.ToString = {attribute}");
            var expectedOutput = "AutoCompleteSetupAttribute: AutoCompleteMode=SuggestAppend, AutoCompleteSource=FileSystem, DropDownStyle=DropDown";
#if NET35
            Assert.Equal(0, string.Compare(expectedOutput, attribute.ToString()));
#else
            Assert.Equal(expectedOutput, attribute.ToString());
#endif
        }

        /// <summary>
        /// Automatics the complete setup attribute source test.
        /// </summary>
        [Fact]
        public void AutoCompleteSetupAttribute_Source_Style_Test()
        {
            // Arrange
            var attribute = new AutoCompleteSetupAttribute(AutoCompleteSource.FileSystem, ComboBoxStyle.DropDown);

            // Act
            var source = attribute.AutoCompleteSource;
            var style = attribute.DropDownStyle;
            var mode = attribute.AutoCompleteMode;
            var values = attribute.Values;
            var providerType = attribute.ProviderType;
            var sourceMode = attribute.Mode;

            // Assert
            Output($"AutoCompleteSource: {source}\nDropDownStyle: {style}\nAutoCompleteMode: {mode}\nValues: {values}\nProviderType: {(providerType == null ? "null" : providerType.ToString())}\nMode: {sourceMode}");
            Assert.Equal(AutoCompleteSource.FileSystem, source);
            Assert.Equal(ComboBoxStyle.DropDown, style);
            Assert.Equal(AutoCompleteMode.SuggestAppend, mode);
            Assert.Empty(values);
            Assert.Null(providerType);
            Assert.Equal(AutoCompleteSetupAttribute.SourceMode.Values, sourceMode);
            Output($"Attribute.ToString = {attribute}");
            var expectedOutput = "AutoCompleteSetupAttribute: AutoCompleteMode=SuggestAppend, AutoCompleteSource=FileSystem, DropDownStyle=DropDown";
#if NET35
            Assert.Equal(0, string.Compare(expectedOutput, attribute.ToString()));
#else
            Assert.Equal(expectedOutput, attribute.ToString());
#endif
        }

        /// <summary>
        /// Automatics the complete setup attribute values test.
        /// </summary>
        [Fact]
        public void AutoCompleteSetupAttribute_EmptyValues_Test()
        {
            // Arrange
#if NET35 || NET452
            var attribute = new AutoCompleteSetupAttribute(new string[0]);
#elif NET5_0_OR_GREATER
            var attribute = new AutoCompleteSetupAttribute([]);
#else
                    var attribute = new AutoCompleteSetupAttribute(Array.Empty<string>());
#endif

            // Act
            var source = attribute.AutoCompleteSource;
            var style = attribute.DropDownStyle;
            var mode = attribute.AutoCompleteMode;
            var values = attribute.Values;
            var providerType = attribute.ProviderType;
            var sourceMode = attribute.Mode;

            // Assert
            Output($"AutoCompleteSource: {source}\nDropDownStyle: {style}\nAutoCompleteMode: {mode}\nValues: {values}\nProviderType: {(providerType == null ? "null" : providerType.ToString())}\nMode: {sourceMode}");
            Assert.Equal(AutoCompleteSource.CustomSource, source);
            Assert.Equal(ComboBoxStyle.DropDown, style);
            Assert.Equal(AutoCompleteMode.SuggestAppend, mode);
            Assert.Null(providerType);
            Assert.Equal(AutoCompleteSetupAttribute.SourceMode.Values, sourceMode);
            Output($"Attribute.ToString = {attribute}");
            var expectedOutput = "AutoCompleteSetupAttribute: AutoCompleteMode=SuggestAppend, AutoCompleteSource=CustomSource, DropDownStyle=DropDown, Values=[]";
#if NET35
            Assert.Equal(0, string.Compare(expectedOutput, attribute.ToString()));
#else
            Assert.Equal(expectedOutput, attribute.ToString());
#endif
        }

        /// <summary>
        /// Automatics the complete setup attribute values test.
        /// </summary>
        [Fact]
        public void AutoCompleteSetupAttribute_Values_Test()
        {
            // Arrange
            var attribute = new AutoCompleteSetupAttribute("A", "B", "C");

            // Act
            var source = attribute.AutoCompleteSource;
            var style = attribute.DropDownStyle;
            var mode = attribute.AutoCompleteMode;
            var values = attribute.Values;
            var providerType = attribute.ProviderType;
            var sourceMode = attribute.Mode;

            // Assert
            Output($"AutoCompleteSource: {source}\nDropDownStyle: {style}\nAutoCompleteMode: {mode}\nValues: {values}\nProviderType: {(providerType == null ? "null" : providerType.ToString())}\nMode: {sourceMode}");
            Assert.Equal(AutoCompleteSource.CustomSource, source);
            Assert.Equal(ComboBoxStyle.DropDown, style);
            Assert.Equal(AutoCompleteMode.SuggestAppend, mode);
            Assert.NotNull(values);
            Assert.Equal(3, values.Length);
            Assert.Null(providerType);
            Assert.Equal(AutoCompleteSetupAttribute.SourceMode.Values, sourceMode);
            Output($"Attribute.ToString = {attribute}");
            var expectedOutput = "AutoCompleteSetupAttribute: AutoCompleteMode=SuggestAppend, AutoCompleteSource=CustomSource, DropDownStyle=DropDown, Values=[A, B, C]";
#if NET35
            Assert.Equal(0, string.Compare(expectedOutput, attribute.ToString()));
#else
            Assert.Equal(expectedOutput, attribute.ToString());
#endif
        }

        /// <summary>
        /// Automatics the complete setup attribute style values test.
        /// </summary>
        [Fact]
        public void AutoCompleteSetupAttribute_Style_Values_Test()
        {
            // Arrange
            var attribute = new AutoCompleteSetupAttribute(ComboBoxStyle.DropDownList, "A", "B", "C");

            // Act
            var source = attribute.AutoCompleteSource;
            var style = attribute.DropDownStyle;
            var mode = attribute.AutoCompleteMode;
            var values = attribute.Values;
            var providerType = attribute.ProviderType;
            var sourceMode = attribute.Mode;

            // Assert
            Output($"AutoCompleteSource: {source}\nDropDownStyle: {style}\nAutoCompleteMode: {mode}\nValues: {values}\nProviderType: {(providerType == null ? "null" : providerType.ToString())}\nMode: {sourceMode}");
            Assert.Equal(AutoCompleteSource.CustomSource, source);
            Assert.Equal(ComboBoxStyle.DropDownList, style);
            Assert.Equal(AutoCompleteMode.SuggestAppend, mode);
            Assert.NotNull(values);
            Assert.Equal(3, values.Length);
            Assert.Null(providerType);
            Assert.Equal(AutoCompleteSetupAttribute.SourceMode.Values, sourceMode);
            Output($"Attribute.ToString = {attribute}");
            var expectedOutput = "AutoCompleteSetupAttribute: AutoCompleteMode=SuggestAppend, AutoCompleteSource=CustomSource, DropDownStyle=DropDownList, Values=[A, B, C]";
#if NET35
            Assert.Equal(0, string.Compare(expectedOutput, attribute.ToString()));
#else
            Assert.Equal(expectedOutput, attribute.ToString());
#endif
        }

        /// <summary>
        /// Automatics the complete setup attribute mode values test.
        /// </summary>
        [Fact]
        public void AutoCompleteSetupAttribute_Mode_Values_Test()
        {
            // Arrange
            var attribute = new AutoCompleteSetupAttribute(AutoCompleteMode.Append, "A", "B", "C");

            // Act
            var source = attribute.AutoCompleteSource;
            var style = attribute.DropDownStyle;
            var mode = attribute.AutoCompleteMode;
            var values = attribute.Values;
            var providerType = attribute.ProviderType;
            var sourceMode = attribute.Mode;

            // Assert
            Output($"AutoCompleteSource: {source}\nDropDownStyle: {style}\nAutoCompleteMode: {mode}\nValues: {values}\nProviderType: {(providerType == null ? "null" : providerType.ToString())}\nMode: {sourceMode}");
            Assert.Equal(AutoCompleteSource.CustomSource, source);
            Assert.Equal(ComboBoxStyle.DropDown, style);
            Assert.Equal(AutoCompleteMode.Append, mode);
            Assert.NotNull(values);
            Assert.Equal(3, values.Length);
            Assert.Null(providerType);
            Assert.Equal(AutoCompleteSetupAttribute.SourceMode.Values, sourceMode);
            Output($"Attribute.ToString = {attribute}");
            var expectedOutput = "AutoCompleteSetupAttribute: AutoCompleteMode=Append, AutoCompleteSource=CustomSource, DropDownStyle=DropDown, Values=[A, B, C]";
#if NET35
            Assert.Equal(0, string.Compare(expectedOutput, attribute.ToString()));
#else
            Assert.Equal(expectedOutput, attribute.ToString());
#endif
        }

        /// <summary>
        /// Automatics the complete setup attribute static class provider test.
        /// </summary>
        [Fact]
        public void AutoCompleteSetupAttribute_NullProvider_Test()
        {
            // Arrange
            var attribute = new AutoCompleteSetupAttribute(providerType: null);

            // Act
            var source = attribute.AutoCompleteSource;
            var style = attribute.DropDownStyle;
            var mode = attribute.AutoCompleteMode;
            var values = attribute.Values;
            var providerType = attribute.ProviderType;
            var sourceMode = attribute.Mode;

            // Assert
            Output($"AutoCompleteSource: {source}\nDropDownStyle: {style}\nAutoCompleteMode: {mode}\nValues: {values}\nProviderType: {(providerType == null ? "null" : providerType.ToString())}\nMode: {sourceMode}");
            Assert.Equal(AutoCompleteSource.CustomSource, source);
            Assert.Equal(ComboBoxStyle.DropDown, style);
            Assert.Equal(AutoCompleteMode.SuggestAppend, mode);
            Assert.Null(values);
            Assert.Null(providerType);
            Assert.Equal(AutoCompleteSetupAttribute.SourceMode.Provider, sourceMode);
            Output($"Attribute.ToString = {attribute}");
            var expectedOutput = "AutoCompleteSetupAttribute: AutoCompleteMode=SuggestAppend, AutoCompleteSource=CustomSource, DropDownStyle=DropDown";
#if NET35
            Assert.Equal(0, string.Compare(expectedOutput, attribute.ToString()));
#else
            Assert.Equal(expectedOutput, attribute.ToString());
#endif
        }

        /// <summary>
        /// Automatics the complete setup attribute enum provider test.
        /// </summary>
        [Fact]
        public void AutoCompleteSetupAttribute_EnumProvider_Test()
        {
            // Arrange
            var attribute = new AutoCompleteSetupAttribute(typeof(SampleEnum));

            // Act
            var source = attribute.AutoCompleteSource;
            var style = attribute.DropDownStyle;
            var mode = attribute.AutoCompleteMode;
            var values = attribute.Values;
            var providerType = attribute.ProviderType;
            var sourceMode = attribute.Mode;

            // Assert
            Output($"AutoCompleteSource: {source}\nDropDownStyle: {style}\nAutoCompleteMode: {mode}\nValues: {values}\nProviderType: {(providerType == null ? "null" : providerType.ToString())}\nMode: {sourceMode}");
            Assert.Equal(AutoCompleteSource.CustomSource, source);
            Assert.Equal(ComboBoxStyle.DropDown, style);
            Assert.Equal(AutoCompleteMode.SuggestAppend, mode);
            Assert.NotNull(providerType);
            Output($"Attribute.ToString = {attribute}");
            var expectedOutput = "AutoCompleteSetupAttribute: AutoCompleteMode=SuggestAppend, AutoCompleteSource=CustomSource, DropDownStyle=DropDown, ProviderType=SampleEnum";
#if NET35
            Assert.Equal(0, string.Compare(expectedOutput, attribute.ToString()));
#else
            Assert.Equal(expectedOutput, attribute.ToString());
#endif
            Assert.Equal(AutoCompleteSetupAttribute.SourceMode.Provider, sourceMode);
        }

        /// <summary>
        /// Automatics the complete setup attribute enum provider test.
        /// </summary>
        [Fact]
        public void AutoCompleteSetupAttribute_Style_Enum_Test()
        {
            // Arrange
            var attribute = new AutoCompleteSetupAttribute(ComboBoxStyle.DropDownList, typeof(SampleEmptyEnum));

            // Act
            var source = attribute.AutoCompleteSource;
            var style = attribute.DropDownStyle;
            var mode = attribute.AutoCompleteMode;
            var values = attribute.Values;
            var providerType = attribute.ProviderType;
            var sourceMode = attribute.Mode;

            // Assert
            Output($"AutoCompleteSource: {source}\nDropDownStyle: {style}\nAutoCompleteMode: {mode}\nValues: {values}\nProviderType: {(providerType == null ? "null" : providerType.ToString())}\nMode: {sourceMode}");
            Assert.Equal(AutoCompleteSource.CustomSource, source);
            Assert.Equal(ComboBoxStyle.DropDownList, style);
            Assert.Equal(AutoCompleteMode.SuggestAppend, mode);
            Assert.Null(values);
            Assert.NotNull(providerType);
            Assert.Equal(AutoCompleteSetupAttribute.SourceMode.Provider, sourceMode);
            Output($"Attribute.ToString = {attribute}");
            var expectedOutput = "AutoCompleteSetupAttribute: AutoCompleteMode=SuggestAppend, AutoCompleteSource=CustomSource, DropDownStyle=DropDownList, ProviderType=SampleEmptyEnum";
#if NET35
            Assert.Equal(0, string.Compare(expectedOutput, attribute.ToString()));
#else
            Assert.Equal(expectedOutput, attribute.ToString());
#endif
        }

        /// <summary>
        /// Automatics the complete setup attribute mode type test.
        /// </summary>
        [Fact]
        public void AutoCompleteSetupAttribute_Mode_Type_Test()
        {
            // Arrange
            var attribute = new AutoCompleteSetupAttribute(AutoCompleteMode.Append, typeof(SampleEnum));

            // Act
            var source = attribute.AutoCompleteSource;
            var style = attribute.DropDownStyle;
            var mode = attribute.AutoCompleteMode;
            var values = attribute.Values;
            var providerType = attribute.ProviderType;
            var sourceMode = attribute.Mode;

            // Assert
            Output($"AutoCompleteSource: {source}\nDropDownStyle: {style}\nAutoCompleteMode: {mode}\nValues: {values}\nProviderType: {(providerType == null ? "null" : providerType.ToString())}\nMode: {sourceMode}");
            Assert.Equal(AutoCompleteSource.CustomSource, source);
            Assert.Equal(ComboBoxStyle.DropDown, style);
            Assert.Equal(AutoCompleteMode.Append, mode);
            Assert.NotNull(providerType);
            Assert.Equal(AutoCompleteSetupAttribute.SourceMode.Provider, sourceMode);
            Output($"Attribute.ToString = {attribute}");
            var expectedOutput = "AutoCompleteSetupAttribute: AutoCompleteMode=Append, AutoCompleteSource=CustomSource, DropDownStyle=DropDown, ProviderType=SampleEnum";
#if NET35
            Assert.Equal(0, string.Compare(expectedOutput, attribute.ToString()));
#else
            Assert.Equal(expectedOutput, attribute.ToString());
#endif
        }

        /// <summary>
        /// Gets the dynamic path source attribute returns attribute if present.
        /// </summary>
        [Fact]
        public void GetAutoCompleteSetupAttribute_ReturnsAttribute_IfPresent()
        {
            // Arrange
            var instance = new TestClass();
            var propDesc = TypeDescriptor.GetProperties(instance)[nameof(TestClass.AutoCompleteItem)];
            var context = new CustomTypeDescriptorContext(propDesc, instance);

            // Act
            var attr = AutoCompleteSetupAttribute.Get(context);

            // Assert
            Output($"AutoCompleteSetupAttribute.AutoCompleteMode: {attr?.AutoCompleteMode}");
            Assert.NotNull(attr);
            Assert.Equal(AutoCompleteMode.SuggestAppend, attr.AutoCompleteMode);
        }

        /// <summary>
        /// Gets the dynamic path source attribute returns null if not present.
        /// </summary>
        [Fact]
        public void GetAutoCompleteSetupAttribute_ReturnsNull_IfNotPresent()
        {
            // Arrange
            var instance = new TestClass();
            var propDesc = TypeDescriptor.GetProperties(instance)[nameof(TestClass.AutoCompleteItem)];
            var context = new CustomTypeDescriptorContext(propDesc, null);

            // Act
            var attr = AutoCompleteSetupAttribute.Get(context);

            // Assert
            Assert.Null(attr);
            Output("Null was returned by the AutoCompleteSetupAttribute.Get call.");
        }

        /// <summary>
        /// Gets the dynamic path source attribute returns null if no attribute.
        /// </summary>
        [Fact]
        public void GetAutoCompleteSetupAttribute_ReturnsNull_IfNoAttribute()
        {
            // Arrange
            var instance = new TestClass();
            var propDesc = TypeDescriptor.GetProperties(instance)[nameof(TestClass.ItemWithoutAttribute)];
            var context = new CustomTypeDescriptorContext(propDesc, instance);

            // Act
            var attr = AutoCompleteSetupAttribute.Get(context);

            // Assert
            Assert.Null(attr);
            Output("Null was returned by the AutoCompleteSetupAttribute.Get call.");
        }

        #endregion

        /// <summary>
        /// Outputs the specified message.
        /// </summary>
        /// <param name="message">The message.</param>
#if NET35
        private void Output(string message) =>
            Debug.WriteLine(message);
#else
        private void Output(string message) =>
            OutputHelper.WriteLine(message);
#endif
    }
}
