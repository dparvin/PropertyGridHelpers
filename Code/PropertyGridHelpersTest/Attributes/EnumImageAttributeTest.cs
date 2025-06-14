using PropertyGridHelpers.Attributes;
using Xunit;
using System.ComponentModel;
using PropertyGridHelpers.TypeDescriptors;

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
        #region Test Support objects ^^^^^^^^^^^^^^^^^^^^^^

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
        /// Provides missing values for testing purposes.
        /// </summary>
        public class TestClass
        {
            /// <summary>
            /// Gets or sets the test enum property.
            /// </summary>
            /// <value>
            /// The test enum property.
            /// </value>
            public TestEnum TestEnumProperty { get; set; } = TestEnum.Test1;
        }

        #endregion

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
        public void EnumImageGet_ContextNull_ReturnsNullTest()
        {
            // Retrieve the attributes from TestEnum.Test2
            var attribute = EnumImageAttribute.Get((ITypeDescriptorContext)null);

            // Cast to EnumImageAttribute and verify the EnumImage property
            Assert.Null(attribute);
            Output("EnumImage set to null as expected");
        }
        /// <summary>
        /// Enums the image get instance null returns null test.
        /// </summary>
        [Fact]
        public void EnumImageGet_InstanceNull_ReturnsNullTest()
        {
            // arrange
            var context = CustomTypeDescriptorContext.Create(null, null);

            // Act
            var attribute = EnumImageAttribute.Get(context);

            //Assert
            Assert.Null(attribute);
            Output("EnumImage set to null as expected");
        }

        /// <summary>
        /// Enums the image get property descriptor null returns null test.
        /// </summary>
        [Fact]
        public void EnumImageGet_PropertyDescriptorNull_ReturnsNullTest()
        {
            // arrange
            var context = CustomTypeDescriptorContext.Create(typeof(TestClass), null);

            // Act
            var attribute = EnumImageAttribute.Get(context);

            //Assert
            Assert.Null(attribute);
            Output("EnumImage set to null as expected");
        }

        /// <summary>
        /// Enums the image get get enum text happy path returns expected.
        /// </summary>
        [Fact]
        public void EnumImageGet_GetEnumText_HappyPath_ReturnsExpected()
        {
            // Arrange
            var context = CustomTypeDescriptorContext.Create(typeof(TestClass), nameof(TestClass.TestEnumProperty));

            // Act
            var attribute = EnumImageAttribute.Get(context);

            // Assert
            Assert.NotNull(attribute);
#if NET35
            Assert.Equal(0, string.Compare("TestItem1", attribute.EnumImage));
#else
            Assert.Equal("TestItem1", attribute.EnumImage);
#endif
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
