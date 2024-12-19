using PropertyGridHelpers.Attributes;
using System.Linq;
using Xunit;
#if NET35
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
    /// Tests for the Enum Image Attribute
    /// </summary>
    public class EnumImageAttributeTest
    {
#if NET35
#else
        readonly ITestOutputHelper OutputHelper;

        /// <summary>
        /// Initializes a new instance of the <see cref="EnumImageAttributeTest"/> class.
        /// </summary>
        /// <param name="output">The output.</param>
        public EnumImageAttributeTest(
            ITestOutputHelper output)

        {
            OutputHelper = output;
        }
#endif
        enum TestEnum
        {
            [EnumImage("TestItem1")]
            Test1,
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
            var memberInfo = typeof(TestEnum).GetField(nameof(TestEnum.Test1));
            var attributes = memberInfo.GetCustomAttributes(typeof(EnumImageAttribute), false);

            // Ensure an attribute is found
            Assert.NotEmpty(attributes);

            // Cast to EnumImageAttribute and verify the EnumImage property
            var attribute = (EnumImageAttribute)attributes.FirstOrDefault();
            Assert.NotNull(attribute);
            Assert.Equal("TestItem1", attribute.EnumImage);
        }

        /// <summary>
        /// Verify the EnumImageAttribute without any parameter.
        /// </summary>
        [Fact]
        public void EnumImageWithoutTextTest()
        {
            // Retrieve the attributes from TestEnum.Test2
            var memberInfo = typeof(TestEnum).GetField(nameof(TestEnum.Test2));
            var attributes = memberInfo.GetCustomAttributes(typeof(EnumImageAttribute), false);

            // Ensure an attribute is found
            Assert.NotEmpty(attributes);

            // Cast to EnumImageAttribute and verify the EnumImage property
            var attribute = (EnumImageAttribute)attributes.FirstOrDefault();
            Assert.NotNull(attribute);
            Assert.Null(attribute.EnumImage);
        }
    }
}
