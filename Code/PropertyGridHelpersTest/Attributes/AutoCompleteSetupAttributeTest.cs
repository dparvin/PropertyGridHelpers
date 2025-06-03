using PropertyGridHelpers.Attributes;
using Xunit;
using System.Windows.Forms;
using System;

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

            // Assert
            Output($"AutoCompleteSource: {source}\nDropDownStyle: {style}\nAutoCompleteMode: {mode}\nValues: {values}");
            Assert.Equal(AutoCompleteSource.None, source);
            Assert.Equal(ComboBoxStyle.DropDown, style);
            Assert.Equal(AutoCompleteMode.SuggestAppend, mode);
            Assert.Empty(values);
            Assert.True(attribute.IsValid);
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

            // Assert
            Output($"AutoCompleteSource: {source}\nDropDownStyle: {style}\nAutoCompleteMode: {mode}\nValues: {values}");
            Assert.Equal(AutoCompleteSource.FileSystem, source);
            Assert.Equal(ComboBoxStyle.DropDown, style);
            Assert.Equal(AutoCompleteMode.SuggestAppend, mode);
            Assert.Empty(values);
            Assert.True(attribute.IsValid);
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

            // Assert
            Output($"AutoCompleteSource: {source}\nDropDownStyle: {style}\nAutoCompleteMode: {mode}\nValues: {values}");
            Assert.Equal(AutoCompleteSource.FileSystem, source);
            Assert.Equal(ComboBoxStyle.DropDown, style);
            Assert.Equal(AutoCompleteMode.SuggestAppend, mode);
            Assert.Empty(values);
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

            // Assert
            Output($"AutoCompleteSource: {source}\nDropDownStyle: {style}\nAutoCompleteMode: {mode}\nValues: {values}");
            Assert.Equal(AutoCompleteSource.CustomSource, source);
            Assert.Equal(ComboBoxStyle.DropDown, style);
            Assert.Equal(AutoCompleteMode.SuggestAppend, mode);
            Assert.False(attribute.IsValid);
            Assert.IsType<ArgumentException>(attribute.InitializationException);
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

            // Assert
            Output($"AutoCompleteSource: {source}\nDropDownStyle: {style}\nAutoCompleteMode: {mode}\nValues: {values}");
            Assert.Equal(AutoCompleteSource.CustomSource, source);
            Assert.Equal(ComboBoxStyle.DropDown, style);
            Assert.Equal(AutoCompleteMode.SuggestAppend, mode);
            Assert.NotNull(values);
            Assert.Equal(3, values.Length);
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

            // Assert
            Output($"AutoCompleteSource: {source}\nDropDownStyle: {style}\nAutoCompleteMode: {mode}\nValues: {values}");
            Assert.Equal(AutoCompleteSource.CustomSource, source);
            Assert.Equal(ComboBoxStyle.DropDownList, style);
            Assert.Equal(AutoCompleteMode.SuggestAppend, mode);
            Assert.NotNull(values);
            Assert.Equal(3, values.Length);
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

            // Assert
            Output($"AutoCompleteSource: {source}\nDropDownStyle: {style}\nAutoCompleteMode: {mode}\nValues: {values}");
            Assert.Equal(AutoCompleteSource.CustomSource, source);
            Assert.Equal(ComboBoxStyle.DropDown, style);
            Assert.Equal(AutoCompleteMode.Append, mode);
            Assert.NotNull(values);
            Assert.Equal(3, values.Length);
            Assert.True(attribute.IsValid);
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

            // Assert
            Output($"AutoCompleteSource: {source}\nDropDownStyle: {style}\nAutoCompleteMode: {mode}\nValues: {values}");
            Assert.Equal(AutoCompleteSource.CustomSource, source);
            Assert.Equal(ComboBoxStyle.DropDown, style);
            Assert.Equal(AutoCompleteMode.SuggestAppend, mode);
            Assert.Null(values);
            Assert.False(attribute.IsValid);
            _ = Assert.IsType<ArgumentNullException>(attribute.InitializationException);
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

            // Assert
            Output($"AutoCompleteSource: {source}\nDropDownStyle: {style}\nAutoCompleteMode: {mode}\nValues: {values}");
            Assert.Equal(AutoCompleteSource.CustomSource, source);
            Assert.Equal(ComboBoxStyle.DropDown, style);
            Assert.Equal(AutoCompleteMode.SuggestAppend, mode);
            Assert.NotNull(values);
            Assert.Equal(2, values.Length);
            Assert.True(attribute.IsValid);
            Output($"Attribute.ToString = {attribute}");
            var expectedOutput = "AutoCompleteSetupAttribute: AutoCompleteMode=SuggestAppend, AutoCompleteSource=CustomSource, DropDownStyle=DropDown, Values=[A, B]";
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
        public void AutoCompleteSetupAttribute_Style_Enum_Test()
        {
            // Arrange
            var attribute = new AutoCompleteSetupAttribute(ComboBoxStyle.DropDownList, typeof(SampleEmptyEnum));

            // Act
            var source = attribute.AutoCompleteSource;
            var style = attribute.DropDownStyle;
            var mode = attribute.AutoCompleteMode;
            var values = attribute.Values;

            // Assert
            Output($"AutoCompleteSource: {source}\nDropDownStyle: {style}\nAutoCompleteMode: {mode}\nValues: {values}");
            Assert.Equal(AutoCompleteSource.CustomSource, source);
            Assert.Equal(ComboBoxStyle.DropDownList, style);
            Assert.Equal(AutoCompleteMode.SuggestAppend, mode);
            Assert.NotNull(values);
            Assert.Empty(values);
            Assert.False(attribute.IsValid);
            _ = Assert.IsType<ArgumentException>(attribute.InitializationException);
        }

        /// <summary>
        /// Automatics the complete setup attribute class provider missing values test.
        /// </summary>
        [Fact]
        public void AutoCompleteSetupAttribute_ClassProvider_MissingValues_Test()
        {
            // Arrange
            var attribute = new AutoCompleteSetupAttribute(typeof(MissingValuesProvider));

            // Act
            var source = attribute.AutoCompleteSource;
            var style = attribute.DropDownStyle;
            var mode = attribute.AutoCompleteMode;
            var values = attribute.Values;

            // Assert
            Output($"AutoCompleteSource: {source}\nDropDownStyle: {style}\nAutoCompleteMode: {mode}\nValues: {values}");
            Assert.Equal(AutoCompleteSource.CustomSource, source);
            Assert.Equal(ComboBoxStyle.DropDown, style);
            Assert.Equal(AutoCompleteMode.SuggestAppend, mode);
            Assert.Null(values);
            Assert.False(attribute.IsValid);
            _ = Assert.IsType<ArgumentException>(attribute.InitializationException);
        }

        /// <summary>
        /// Automatics the complete setup attribute class provider values wrong type test.
        /// </summary>
        [Fact]
        public void AutoCompleteSetupAttribute_ClassProvider_ValuesWrongType_Test()
        {
            // Arrange
            var attribute = new AutoCompleteSetupAttribute(typeof(ValuesWrongTypeProvider));

            // Act
            var source = attribute.AutoCompleteSource;
            var style = attribute.DropDownStyle;
            var mode = attribute.AutoCompleteMode;
            var values = attribute.Values;

            // Assert
            Output($"AutoCompleteSource: {source}\nDropDownStyle: {style}\nAutoCompleteMode: {mode}\nValues: {values}");
            Assert.Equal(AutoCompleteSource.CustomSource, source);
            Assert.Equal(ComboBoxStyle.DropDown, style);
            Assert.Equal(AutoCompleteMode.SuggestAppend, mode);
            Assert.Null(values);
            Assert.False(attribute.IsValid);
            _ = Assert.IsType<ArgumentException>(attribute.InitializationException);
        }

        /// <summary>
        /// Automatics the complete setup attribute static class provider test.
        /// </summary>
        [Fact]
        public void AutoCompleteSetupAttribute_StaticClassProvider_Test()
        {
            // Arrange
            var attribute = new AutoCompleteSetupAttribute(typeof(StaticStringProvider));

            // Act
            var source = attribute.AutoCompleteSource;
            var style = attribute.DropDownStyle;
            var mode = attribute.AutoCompleteMode;
            var values = attribute.Values;

            // Assert
            Output($"AutoCompleteSource: {source}\nDropDownStyle: {style}\nAutoCompleteMode: {mode}\nValues: {values}");
            Assert.Equal(AutoCompleteSource.CustomSource, source);
            Assert.Equal(ComboBoxStyle.DropDown, style);
            Assert.Equal(AutoCompleteMode.SuggestAppend, mode);
            Assert.NotNull(values);
            Assert.Equal(2, values.Length);
            Assert.True(attribute.IsValid);
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

            // Assert
            Output($"AutoCompleteSource: {source}\nDropDownStyle: {style}\nAutoCompleteMode: {mode}\nValues: {values}");
            Assert.Equal(AutoCompleteSource.CustomSource, source);
            Assert.Equal(ComboBoxStyle.DropDown, style);
            Assert.Equal(AutoCompleteMode.Append, mode);
            Assert.NotNull(values);
            Assert.Equal(2, values.Length);
            Assert.True(attribute.IsValid);
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
