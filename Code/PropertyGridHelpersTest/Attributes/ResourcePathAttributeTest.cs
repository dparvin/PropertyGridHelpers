using PropertyGridHelpers.Attributes;
using System;
using System.Reflection;
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
#elif NET8_0
namespace PropertyGridHelpersTest.net80.Attributes
#elif NET9_0
namespace PropertyGridHelpersTest.net90.Attributes
#endif
{
#if NET8_0_OR_GREATER
    /// <summary>
    /// Resource Path Attribute Test
    /// </summary>
    /// <param name="output">xunit output implementation</param>
    public class ResourcePathAttributeTest(ITestOutputHelper output)
    {
#else
    /// <summary>
    /// Resource Path Attribute Test
    /// </summary>
    public class ResourcePathAttributeTest
    {
#endif
#if NET35
#else
#if NET8_0_OR_GREATER
        private readonly ITestOutputHelper OutputHelper = output;
#else
        private readonly ITestOutputHelper OutputHelper;

        /// <summary>
        /// Localized Text Attribute Test
        /// </summary>
        /// <param name="output">xunit output implementation</param>
        public ResourcePathAttributeTest(ITestOutputHelper output) => OutputHelper = output;
#endif
#endif
        #region Test Methods ^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^

        /// <summary>
        /// Gets the assembly returns executing assembly when resource assembly is null or empty.
        /// </summary>
        [Fact]
        public void GetAssembly_ReturnsExecutingAssembly_WhenResourceAssemblyIsNullOrEmpty()
        {
            // Arrange
            var attr = new ResourcePathAttribute("Some.Path"); // resourceAssembly is not provided

            // Act
            var result = attr.GetAssembly();

            // Assert
            Output($"Test Results: {result}");
            Assert.Equal(Assembly.GetExecutingAssembly(), result);
        }

        /// <summary>
        /// Gets the assembly loads specified assembly when resource assembly is provided.
        /// </summary>
        [Fact]
        public void GetAssembly_LoadsSpecifiedAssembly_WhenResourceAssemblyIsProvided()
        {
            // Arrange
            var currentAssemblyName = Assembly.GetExecutingAssembly().GetName().Name;
            var attr = new ResourcePathAttribute("Some.Path", currentAssemblyName);

            // Act
            var result = attr.GetAssembly();

            // Assert
#if NET8_0_OR_GREATER
            Assert.Equal(Assembly.GetExecutingAssembly().FullName, result.FullName);
#else
            Assert.Equal(0, string.Compare(Assembly.GetExecutingAssembly().FullName, result.FullName, StringComparison.OrdinalIgnoreCase));
#endif
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
