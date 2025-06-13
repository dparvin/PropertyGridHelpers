﻿using PropertyGridHelpers.Attributes;
using Xunit;
using System;
using System.ComponentModel;
using PropertyGridHelpers.TypeDescriptors;

#if NET35
using Xunit.Extensions;
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
#if NET35
    /// <summary>
    /// Test for the Localized Category Attribute
    /// </summary>
    public class LocalizedCategoryAttributeTest
    {
#elif NET8_0_OR_GREATER
    /// <summary>
    /// Test for the Localized Category Attribute
    /// </summary>
    /// <param name="output">system to use to output information to test runner</param>
    public class LocalizedCategoryAttributeTest(ITestOutputHelper output)
    {
        private readonly ITestOutputHelper OutputHelper = output;
#else
    /// <summary>
    /// Test for the Localized Category Attribute
    /// </summary>
    public class LocalizedCategoryAttributeTest
    {
        private readonly ITestOutputHelper OutputHelper;
        /// <summary>
        /// Initializes a new instance of the <see cref="LocalizedCategoryAttributeTest"/> class.
        /// </summary>
        /// <param name="output">system to use to output information to test runner</param>
        public LocalizedCategoryAttributeTest(ITestOutputHelper output) => OutputHelper = output;
#endif

        #region Test Support objects ^^^^^^^^^^^^^^^^^^^^^^

        /// <summary>
        /// Provides missing values for testing purposes.
        /// </summary>
        public class TestClass
        {
            /// <summary>
            /// Gets or sets the resource item.
            /// </summary>
            /// <value>
            /// The resource item.
            /// </value>
            [LocalizedCategory("SomeResourceKey")]
            public string ResourceItem
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
        /// Localized category attribute remembers resource key.
        /// </summary>
        [Fact]
        public void LocalizedCategoryAttribute_Remembers_ResourceKey()
        {
            // Arrange
            const string Some_Resource_Key = "SOME_RESOURCE_KEY";

            // Act
            var attribute = new LocalizedCategoryAttribute(Some_Resource_Key);

            // Assert
            Assert.NotNull(attribute);
#if NET35
            Assert.Equal(0, string.Compare(Some_Resource_Key, attribute.ResourceKey));
#else
            Assert.Equal(Some_Resource_Key, attribute.ResourceKey); 
#endif
            Output($"The returned Category resource key is: {attribute.ResourceKey}");
        }

        /// <summary>
        /// Gets the localized category attribute returns attribute if present.
        /// </summary>
        [Fact]
        public void GetLocalizedCategoryAttribute_ReturnsAttribute_IfPresent()
        {
            // Arrange
            var instance = new TestClass();
            var propDesc = TypeDescriptor.GetProperties(instance)["ResourceItem"];
            var context = new CustomTypeDescriptorContext(propDesc, instance);

            // Act
            var attr = LocalizedCategoryAttribute.Get(context);

            // Assert
            Output($"LocalizedCategoryAttribute.ResourceKey: {attr?.ResourceKey}");
            Assert.NotNull(attr);
            Assert.NotEmpty(attr.ResourceKey);
        }

        /// <summary>
        /// Gets the localized category attribute returns null if not present.
        /// </summary>
        [Fact]
        public void GetLocalizedCategoryAttribute_ReturnsNull_IfNotPresent()
        {
            // Arrange
            var instance = new TestClass();
            var propDesc = TypeDescriptor.GetProperties(instance)["OtherItem"];
            var context = new CustomTypeDescriptorContext(propDesc, null);

            // Act
            var attr = LocalizedCategoryAttribute.Get(context);

            // Assert
            Assert.Null(attr);
            Output("Null was returned by the LocalizedCategoryAttribute.Get call.");
        }

        /// <summary>
        /// Gets the localized category attribute returns null if no attribute.
        /// </summary>
        [Fact]
        public void GetLocalizedCategoryAttribute_ReturnsNull_IfNoAttribute()
        {
            // Arrange
            var instance = new TestClass();
            var propDesc = TypeDescriptor.GetProperties(instance)["ItemWithoutAttribute"];
            var context = new CustomTypeDescriptorContext(propDesc, instance);

            // Act
            var attr = LocalizedCategoryAttribute.Get(context);

            // Assert
            Assert.Null(attr);
            Output("Null was returned by the LocalizedCategoryAttribute.Get call.");
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
