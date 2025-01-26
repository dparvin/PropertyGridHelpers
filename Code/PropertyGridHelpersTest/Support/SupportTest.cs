using System;
using System.Drawing;
using System.IO;
using Xunit;
using Xunit.Abstractions;

#if NET35
namespace PropertyGridHelpersTest.net35.Support
#elif NET452
namespace PropertyGridHelpersTest.net452.Support
#elif NET462
namespace PropertyGridHelpersTest.net462.Support
#elif NET472
namespace PropertyGridHelpersTest.net472.Support
#elif NET481
namespace PropertyGridHelpersTest.net481.Support
#elif NET8_0
namespace PropertyGridHelpersTest.net80.Support
#elif NET9_0
namespace PropertyGridHelpersTest.net90.Support
#endif
{
    /// <summary>
    /// Tests for the <see cref="PropertyGridHelpers.Support.Support"/> class.
    /// </summary>
#if NET8_0_OR_GREATER
    /// <summary>
    /// Class for testing the ImageTextUIEditor class
    /// </summary>
    /// <param name="output">The output.</param>
    public class SupportTest(ITestOutputHelper output)
#else
    public class SupportTest
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
        /// Enum Text Converter Test
        /// </summary>
        public SupportTest()
        {
        }
#elif NET8_0_OR_GREATER
#else
        /// <summary>
        /// Enum Text Converter Test
        /// </summary>
        /// <param name="output"></param>
        public SupportTest(ITestOutputHelper output)
            : base() =>
            OutputHelper = output;
#endif

        /// <summary>
        /// Gets the resources names when enum type is null throws argument null exception.
        /// </summary>
        [Fact]
        public void GetResourcesNames_WhenEnumTypeIsNull_ThrowsArgumentNullException()
        {
            var ex = Assert.Throws<ArgumentNullException>(() => PropertyGridHelpers.Support.Support.GetResourcesNames(null));
            // Assert
            Assert.Contains("Value cannot be null.", ex.Message);
            Output(ex.Message);
        }

        /// <summary>
        /// Gets the resources names when enum type is not enum throws argument exception.
        /// </summary>
        [Fact]
        public void GetResourcesNames_WhenEnumTypeIsNotEnum_ThrowsArgumentException()
        {
            var ex = Assert.Throws<ArgumentException>(() => PropertyGridHelpers.Support.Support.GetResourcesNames(typeof(SupportTest)));
            // Assert
            Assert.Contains("The provided type must be an enum.", ex.Message);
            Output(ex.Message);
        }

        /// <summary>
        /// Gets the resources names when enum type is enum returns resource names.
        /// </summary>
        [Fact]
        public void GetResourcesNames_WhenEnumTypeIsEnum_ReturnsResourceNames()
        {
            var resourceNames = PropertyGridHelpers.Support.Support.GetResourcesNames(typeof(Enums.ImageFileExtension));
            Assert.NotNull(resourceNames);
            Output("Resource Names:");
            foreach (var resourceName in resourceNames)
            {
                Output($"  {resourceName}");
            }
        }

        /// <summary>
        /// Checks the resource type when assembly is null throws argument null exception.
        /// </summary>
        [Fact]
        public void CheckResourceType_WhenAssemblyIsNull_ThrowsArgumentNullException()
        {
            var ex = Assert.Throws<ArgumentNullException>(() => PropertyGridHelpers.Support.Support.CheckResourceType(null));
            // Assert
            Assert.Contains("Value cannot be null.", ex.Message);
            Output(ex.Message);
        }

        /// <summary>
        /// Checks the resource type when assembly is not null does not throw exception.
        /// </summary>
        [Fact]
        public void CheckResourceType_WhenAssemblyIsNotNull_DoesNotThrowException()
        {
            var stringWriter = new StringWriter();
            Console.SetOut(stringWriter);

            PropertyGridHelpers.Support.Support.CheckResourceType(typeof(SupportTest).Assembly);

            var output = stringWriter.ToString();
            Console.SetOut(new StreamWriter(Console.OpenStandardOutput()) { AutoFlush = true });
            Output(output);

            // Assert
            Assert.Contains("Checking resources in assembly:", output);
        }

        #region Test Support Methods ^^^^^^^^^^^^^^^^^^^^^^

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

        #endregion
    }
}
