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
#elif NET8_0
namespace PropertyGridHelpersTest.net80.Attributes
#elif NET9_0
namespace PropertyGridHelpersTest.net90.Attributes
#endif
{
    /// <summary>
    /// Tests for the Enum Image Attribute
    /// </summary>
#if NET8_0_OR_GREATER
    public class EnumImageAttributeTest(ITestOutputHelper output)
#else
    public class EnumImageAttributeTest
#endif
    {
#if NET35
#elif NET8_0_OR_GREATER
        private readonly ITestOutputHelper OutputHelper = output;
#else
        private readonly ITestOutputHelper OutputHelper;

        /// <summary>
        /// Initializes a new instance of the <see cref="EnumImageAttributeTest"/> class.
        /// </summary>
        /// <param name="output">The output.</param>
        public EnumImageAttributeTest(
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
            [EnumImage("TestItem1")]
            Test1,
            /// <summary>
            /// The test2
            /// </summary>
            [EnumImage]
            Test2
        }

        /// <summary>
        /// Get the attribute with a string test
        /// </summary>
        [Fact]
        public void EnumImageWithTextTest()
        {
            // Retrieve the attributes from TestEnum.Test1
            var memberInfo = EnumImageAttribute.Get(TestEnum.Test1);

            // Ensure an attribute is found
            Assert.NotNull(memberInfo);

            // Cast to EnumImageAttribute and verify the EnumImage property
            Assert.Equal("TestItem1", memberInfo.EnumImage);
            Output("EnumImage set to 'TestItem1' as expected");
        }

        /// <summary>
        /// Verify the EnumImageAttribute without any parameter.
        /// </summary>
        [Fact]
        public void EnumImageWithoutTextTest()
        {
            // Retrieve the attributes from TestEnum.Test2
            var attribute = EnumImageAttribute.Get(TestEnum.Test2);

            // Cast to EnumImageAttribute and verify the EnumImage property
            Assert.NotNull(attribute);
            Assert.Null(attribute.EnumImage);
            Output("EnumImage set to null as expected");
        }

        /// <summary>
        /// Enums the image with null test.
        /// </summary>
        [Fact]
        public void EnumImageWithNullTest()
        {
            // Retrieve the attributes from TestEnum.Test2
            var attribute = EnumImageAttribute.Get(null);

            // Cast to EnumImageAttribute and verify the EnumImage property
            Assert.Null(attribute);
            Output("EnumImage set to null as expected");
        }

        /// <summary>
        /// Enums the image exists test.
        /// </summary>
        [Fact]
        public void EnumImageExistsTest()
        {
            // Retrieve the attributes from TestEnum.Test2
            var exists = EnumImageAttribute.Exists(TestEnum.Test1);

            // Cast to EnumImageAttribute and verify the EnumImage property
            Assert.True(exists);
            Output("EnumImage exists as expected");
        }

        /// <summary>
        /// Enums the image not exists test.
        /// </summary>
        [Fact]
        public void EnumImageNotExistsTest()
        {
            // Retrieve the attributes from TestEnum.Test2
            var exists = EnumImageAttribute.Exists(null);

            // Cast to EnumImageAttribute and verify the EnumImage property
            Assert.False(exists);
            Output("EnumImage not exists as expected");
        }

        /// <summary>
        /// Enums the image get enum image test.
        /// </summary>
        [Fact]
        public void EnumImage_GetEnumImageTest()
        {
            // arrange

            // Act
            var enumImage = EnumImageAttribute.GetEnumImage(TestEnum.Test1);

            //Assert
            Output(enumImage);
#if NET35
            Assert.Equal(0, string.Compare("TestItem1", enumImage));
#else
            Assert.Equal("TestItem1",enumImage);
#endif

            Output("EnumImage exists as expected");
        }

        /// <summary>
        /// Enums the image get enum image test.
        /// </summary>
        [Fact]
        public void EnumImage_GetEnumImageBlankTest()
        {
            // arrange

            // Act
            var enumImage = EnumImageAttribute.GetEnumImage(null);

            //Assert
            Output(enumImage);
            Assert.Empty(enumImage);

            Output("EnumImage blank as expected");
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
