using PropertyGridHelpers.Attributes;
using System.Linq;
using Xunit;

#if NET35
using System.Diagnostics;
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
#elif WINDOWS7_0
namespace PropertyGridHelpersTest.net60.W7.Attributes
#elif WINDOWS10_0
namespace PropertyGridHelpersTest.net60.W10.Attributes
#elif NET8_0
namespace PropertyGridHelpersTest.net80.Attributes
#elif NET9_0
namespace PropertyGridHelpersTest.net90.Attributes
#endif
{
    /// <summary>
    /// Tests for the <see cref="EnumTextAttribute"/> class
    /// </summary>
#if NET8_0_OR_GREATER
    public class EnumTextAttributeTest(ITestOutputHelper output)
#else
    public class EnumTextAttributeTest
#endif
    {
#if NET35
#elif NET8_0_OR_GREATER
        private readonly ITestOutputHelper OutputHelper = output;
#else
        private readonly ITestOutputHelper OutputHelper;

        /// <summary>
        /// Initializes a new instance of the <see cref="EnumTextAttributeTest"/> class.
        /// </summary>
        /// <param name="output">The output.</param>
        public EnumTextAttributeTest(
            ITestOutputHelper output) => OutputHelper = output;
#endif

        /// <summary>
        /// Test Enum
        /// </summary>
        public enum TestEnum
        {
            /// <summary>
            /// The test1
            /// </summary>
            [EnumText("TestItem1")]
            Test1,
            /// <summary>
            /// The test2
            /// </summary>
            Test2
        }

        /// <summary>
        /// Enums the text get enum text test.
        /// </summary>
        [Fact]
        public void EnumText_GetEnumTextTest()
        {
            // arrange

            // Act
            var enumText = EnumTextAttribute.Get(TestEnum.Test1);

            //Assert
            Output(enumText.EnumText);
#if NET35
            Assert.Equal(0, string.Compare("TestItem1", enumText.EnumText));
#else
            Assert.Equal("TestItem1", enumText.EnumText);
#endif

            Output("EnumText is 'TestItem1' as expected");
        }

        /// <summary>
        /// Enums the text get enum text blank test.
        /// </summary>
        [Fact]
        public void EnumText_GetEnumTextNoAttributeTest()
        {
            // arrange

            // Act
            var enumText = EnumTextAttribute.Get(TestEnum.Test2);

            //Assert
            Assert.Null(enumText);

            Output("EnumText is null as expected");
        }

        /// <summary>
        /// Enums the text get enum text null test.
        /// </summary>
        [Fact]
        public void EnumText_GetEnumTextNullTest()
        {
            // arrange

            // Act
            var enumText = EnumTextAttribute.Get(null);

            //Assert
            Assert.Null(enumText);

            Output("EnumText is null as expected");
        }

        /// <summary>
        /// Enums the text get enum text test.
        /// </summary>
        [Fact]
        public void EnumText_ExistsTest()
        {
            // arrange

            // Act
            var enumText = EnumTextAttribute.Exists(TestEnum.Test1);

            //Assert
            Assert.True(enumText);

            Output("EnumText exists as expected");
        }

        /// <summary>
        /// Enums the text get enum text blank test.
        /// </summary>
        [Fact]
        public void EnumText_NotExistsTest()
        {
            // arrange

            // Act
            var enumText = EnumTextAttribute.Exists(TestEnum.Test2);

            //Assert
            Assert.False(enumText);

            Output("EnumText does not exist as expected");
        }

        /// <summary>
        /// Enums the text get enum text null test.
        /// </summary>
        [Fact]
        public void EnumText_ExistsNullTest()
        {
            // arrange

            // Act
            var enumText = EnumTextAttribute.Exists(null);

            //Assert
            Assert.False(enumText);

            Output("EnumText does not exist when a null is passed as expected");
        }

        /// <summary>
        /// Outputs the specified message.
        /// </summary>
        /// <param name="message">The message.</param>
#if NET35
        private void Output(string message) => Debug.WriteLine(message);
#else
        private void Output(string message) => OutputHelper.WriteLine(message);
#endif
    }
}
