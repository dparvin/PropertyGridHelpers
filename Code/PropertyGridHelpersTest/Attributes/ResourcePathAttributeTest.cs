using Newtonsoft.Json.Linq;
using PropertyGridHelpers.Attributes;
using PropertyGridHelpers.TypeDescriptors;
using System;
using System.ComponentModel;
using System.Reflection;
using System.Windows.Forms.VisualStyles;
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

        #region Test Support objects ^^^^^^^^^^^^^^^^^^^^^^

        /// <summary>
        /// Test enum to use for testing the ResourcePathAttribute.
        /// </summary>
        [ResourcePath("FromEnum")]
        public enum AnnotatedEnum
        {
            /// <summary>
            /// The a
            /// </summary>
            A,
            /// <summary>
            /// The b
            /// </summary>
            B
        }

        /// <summary>
        /// Test class to use for testing the ResourcePathAttribute.
        /// </summary>
        private class PropertyWithAttribute
        {
            /// <summary>
            /// Gets or sets the property.
            /// </summary>
            /// <value>
            /// The property.
            /// </value>
            [ResourcePath("FromProperty")]
            public string Property
            {
                get; set;
            }

            /// <summary>
            /// Gets or sets the enum property.
            /// </summary>
            /// <value>
            /// The enum property.
            /// </value>
            public AnnotatedEnum EnumProperty
            {
                get; set;
            }
        }

        /// <summary>
        /// Test class to use for testing the ResourcePathAttribute.
        /// </summary>
        [ResourcePath("FromClass")]
        private class AnnotatedClass
        {
            /// <summary>
            /// Gets or sets the property.
            /// </summary>
            /// <value>
            /// The property.
            /// </value>
            public string Property
            {
                get; set;
            }

            /// <summary>
            /// Gets or sets the enum property.
            /// </summary>
            /// <value>
            /// The enum property.
            /// </value>
            public AnnotatedEnum EnumProperty
            {
                get; set;
            }
        }

        /// <summary>
        /// Test class with no attributes anywhere.
        /// </summary>
        private class NoAttributeAnywhere
        {
            /// <summary>
            /// Gets or sets some property.
            /// </summary>
            /// <value>
            /// Some property.
            /// </value>
            public string SomeProperty
            {
                get; set;
            }
        }

        #endregion

        #region Test routines ^^^^^^^^^^^^^^^^^^^^^^^^^^^^^

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

        /// <summary>
        /// Gets the returns null when context is null.
        /// </summary>
        [Fact]
        public void Get_ReturnsNull_WhenContextIsNull()
        {
            var result = ResourcePathAttribute.Get(null);
            Assert.Null(result);
        }

        /// <summary>
        /// Gets the returns resource path attribute when defined on property.
        /// </summary>
        [Fact]
        public void Get_ReturnsResourcePathAttribute_WhenDefinedOnProperty()
        {
            // Arrange
            var context = CustomTypeDescriptorContext.Create(typeof(PropertyWithAttribute), nameof(PropertyWithAttribute.Property));

            // Act
            var result = ResourcePathAttribute.Get(context);

            // Assert
            Assert.NotNull(result);
#if NET35
            Assert.Equal(0, string.Compare("FromProperty", result.ResourcePath));
#else
            Assert.Equal("FromProperty", result.ResourcePath);
#endif
        }

        /// <summary>
        /// Gets the type of the returns attribute when on enum.
        /// </summary>
        [Fact]
        public void Get_ReturnsAttribute_WhenOnEnumType()
        {
            // Arrange
            var context = CustomTypeDescriptorContext.Create(typeof(PropertyWithAttribute), nameof(PropertyWithAttribute.EnumProperty));

            // Act
            var result = ResourcePathAttribute.Get(context);

            // Assert
            Assert.NotNull(result);
#if NET35
            Assert.Equal(0, string.Compare("FromEnum", result.ResourcePath));
#else
            Assert.Equal("FromEnum", result.ResourcePath);
#endif
        }

        /// <summary>
        /// Gets the returns attribute when on class.
        /// </summary>
        [Fact]
        public void Get_ReturnsAttribute_WhenOnClass()
        {
            // Arrange
            var context = CustomTypeDescriptorContext.Create(typeof(AnnotatedClass), nameof(AnnotatedClass.Property));

            // Act
            var result = ResourcePathAttribute.Get(context);

            // Assert
            Assert.NotNull(result);
#if NET35
            Assert.Equal(0, string.Compare("FromClass", result.ResourcePath));
#else
            Assert.Equal("FromClass", result.ResourcePath);
#endif
        }

        /// <summary>
        /// Gets the returns null when no attribute exists.
        /// </summary>
        [Fact]
        public void Get_ReturnsNull_WhenNoAttributeExists()
        {
            // Arrange
            var context = CustomTypeDescriptorContext.Create(typeof(NoAttributeAnywhere), nameof(NoAttributeAnywhere.SomeProperty));
            // Act
            var result = ResourcePathAttribute.Get(context);

            // Assert
            Assert.Null(result);
        }

        /// <summary>
        /// Gets the returns null when instance is null.
        /// </summary>
        [Fact]
        public void Get_ReturnsNull_WhenInstanceIsNull()
        {
            var descriptor = TypeDescriptor.GetProperties(typeof(AnnotatedClass))[nameof(AnnotatedClass.Property)];
            var context = new CustomTypeDescriptorContext(descriptor, null); // Pass null instance

            var result = ResourcePathAttribute.Get(context);

            Assert.Null(result);
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
