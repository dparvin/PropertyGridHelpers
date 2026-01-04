using PropertyGridHelpers.TypeDescriptionProviders;
using PropertyGridHelpers.TypeDescriptors;
using System;
using Xunit;
#if NET35
#else
using Xunit.Abstractions;
#endif

#if NET35
namespace PropertyGridHelpersTest.net35.TypeDescriptionProviders
#elif NET452
namespace PropertyGridHelpersTest.net452.TypeDescriptionProviders
#elif NET462
namespace PropertyGridHelpersTest.net462.TypeDescriptionProviders
#elif NET472
namespace PropertyGridHelpersTest.net472.TypeDescriptionProviders
#elif NET481
namespace PropertyGridHelpersTest.net481.TypeDescriptionProviders
#elif NET8_0
namespace PropertyGridHelpersTest.net80.TypeDescriptionProviders
#elif NET9_0
namespace PropertyGridHelpersTest.net90.TypeDescriptionProviders
#elif NET10_0
namespace PropertyGridHelpersTest.net100.TypeDescriptionProviders
#endif
{
#if NET8_0_OR_GREATER
    /// <summary>
    /// Localized Type Description Provider Test
    /// </summary>
    /// <param name="output">xunit output implementation</param>
    public class LocalizedTypeDescriptionProviderTest(ITestOutputHelper output)
    {
#else
    /// <summary>
    /// Localized Type Description Provider Test
    /// </summary>
    public class LocalizedTypeDescriptionProviderTest
    {
#endif
#if NET35
#else
#if NET8_0_OR_GREATER
        private readonly ITestOutputHelper OutputHelper = output;
#else
        private readonly ITestOutputHelper OutputHelper;

        /// <summary>
        /// Localized Property Descriptor Test
        /// </summary>
        /// <param name="output">xunit output implementation</param>
        public LocalizedTypeDescriptionProviderTest(ITestOutputHelper output) =>
            OutputHelper = output;
#endif
#endif

        #region Test Methods ^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^

        private class TestClass
        {
        }

        /// <summary>
        /// Constructors the sets base provider.
        /// </summary>
        [Fact]
        public void Constructor_SetsBaseProvider()
        {
            // Arrange & Act
            var provider = new LocalizedTypeDescriptionProvider(typeof(TestClass));

            // Assert
            Assert.NotNull(provider);
        }

        /// <summary>
        /// Defaults the type of the constructor uses object.
        /// </summary>
        [Fact]
        public void DefaultConstructor_UsesObjectType()
        {
            // Arrange & Act
            var provider = new LocalizedTypeDescriptionProvider();

            // Assert
            Assert.NotNull(provider);
        }

        /// <summary>
        /// Gets the type descriptor throws argument null exception when object type is null.
        /// </summary>
        [Fact]
        public void GetTypeDescriptor_ThrowsArgumentNullException_WhenObjectTypeIsNull()
        {
            // Arrange
            var provider = new LocalizedTypeDescriptionProvider(typeof(TestClass));

            // Act & Assert
            var exception = Assert.Throws<ArgumentNullException>(() => provider.GetTypeDescriptor(null, new TestClass()));
            Output($"exception.Message = {exception.Message}");
            Output($"exception.ParamName = {exception.ParamName}");
#if NET35
            Assert.Equal(0, string.Compare("objectType", exception.ParamName, StringComparison.OrdinalIgnoreCase));
#else
            Assert.Equal("objectType", exception.ParamName);
#endif
        }

        /// <summary>
        /// Gets the type descriptor returns localized type descriptor.
        /// </summary>
        [Fact]
        public void GetTypeDescriptor_ReturnsLocalizedTypeDescriptor()
        {
            // Arrange
            var provider = new LocalizedTypeDescriptionProvider(typeof(TestClass));

            // Act
            var descriptor = provider.GetTypeDescriptor(typeof(TestClass), new TestClass());

            // Assert
            Assert.NotNull(descriptor);
            _ = Assert.IsType<LocalizedTypeDescriptor>(descriptor);
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
