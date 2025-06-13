using PropertyGridHelpers.Attributes;
using Xunit;
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
#endif
{
#if NET8_0_OR_GREATER
    /// <summary>
    /// AutoCompleteSetupAttribute Tests
    /// </summary>
    /// <param name="output">The output from the unit test.</param>
    public class DynamicPathSourceAttributeTest(ITestOutputHelper output)
#else
    /// <summary>
    /// AutoCompleteSetupAttribute Tests
    /// </summary>
    public class DynamicPathSourceAttributeTest
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
        public DynamicPathSourceAttributeTest(
            ITestOutputHelper output) => OutputHelper = output;
#endif

        #region Test Support objects ^^^^^^^^^^^^^^^^^^^^^^

        /// <summary>
        /// Provides missing values for testing purposes.
        /// </summary>
        public class TestClass
        {
            /// <summary>
            /// Gets or sets the resource path.
            /// </summary>
            /// <value>
            /// The resource path.
            /// </value>
            public string ResourcePath { get; set; } = "MyNamespace.Resources";

            /// <summary>
            /// Gets or sets the resource item.
            /// </summary>
            /// <value>
            /// The resource item.
            /// </value>
            [DynamicPathSource(nameof(ResourcePath))]
            public string ResourceItem
            {
                get; set;
            }

            /// <summary>
            /// Gets or sets the other item.
            /// </summary>
            /// <value>
            /// The other item.
            /// </value>
            [DynamicPathSource("SomeplaceElse")]
            public string OtherItem
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

        #region Test routines ^^^^^^^^^^^^^^^^^^^^^^^^^^^^^

        /// <summary>
        /// Gets the dynamic path source attribute returns attribute if present.
        /// </summary>
        [Fact]
        public void GetDynamicPathSourceAttribute_ReturnsAttribute_IfPresent()
        {
            // Arrange
            var instance = new TestClass();
            var propDesc = TypeDescriptor.GetProperties(instance)["ResourceItem"];
            var context = new CustomTypeDescriptorContext(propDesc, instance);

            // Act
            var attr = DynamicPathSourceAttribute.Get(context);

            // Assert
            Output($"DynamicPathSourceAttribute.PathPropertyName: {attr?.PathPropertyName}");
            Assert.NotNull(attr);
            Assert.NotEmpty(attr.PathPropertyName);
        }

        /// <summary>
        /// Gets the dynamic path source attribute returns null if not present.
        /// </summary>
        [Fact]
        public void GetDynamicPathSourceAttribute_ReturnsNull_IfNotPresent()
        {
            // Arrange
            var instance = new TestClass();
            var propDesc = TypeDescriptor.GetProperties(instance)["OtherItem"];
            var context = new CustomTypeDescriptorContext(propDesc, null);

            // Act
            var attr = DynamicPathSourceAttribute.Get(context);

            // Assert
            Assert.Null(attr);
            Output("Null was returned by the DynamicPathSourceAttribute.Get call.");
        }

        /// <summary>
        /// Gets the dynamic path source attribute returns null if no attribute.
        /// </summary>
        [Fact]
        public void GetDynamicPathSourceAttribute_ReturnsNull_IfNoAttribute()
        {
            // Arrange
            var instance = new TestClass();
            var propDesc = TypeDescriptor.GetProperties(instance)["ItemWithoutAttribute"];
            var context = new CustomTypeDescriptorContext(propDesc, instance);

            // Act
            var attr = DynamicPathSourceAttribute.Get(context);

            // Assert
            Assert.Null(attr);
            Output("Null was returned by the DynamicPathSourceAttribute.Get call.");
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
