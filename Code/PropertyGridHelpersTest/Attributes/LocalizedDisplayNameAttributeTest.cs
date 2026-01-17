using PropertyGridHelpers.Attributes;
using Xunit;
using System;
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
#elif NET48
namespace PropertyGridHelpersTest.net480.Attributes
#elif NET481
namespace PropertyGridHelpersTest.net481.Attributes
#elif NET8_0
namespace PropertyGridHelpersTest.net80.Attributes
#elif NET9_0
namespace PropertyGridHelpersTest.net90.Attributes
#elif NET10_0
namespace PropertyGridHelpersTest.net100.Attributes
#endif
{
#if NET35
	/// <summary>
	/// Test for the Localized Description Attribute
	/// </summary>
	public class LocalizedDisplayNameAttributeTest
	{
#elif NET8_0_OR_GREATER
    /// <summary>
    /// Test for the Localized Description Attribute
    /// </summary>
    /// <param name="output">system to use to output information to test runner</param>
    public class LocalizedDisplayNameAttributeTest(ITestOutputHelper output)
    {
        private readonly ITestOutputHelper OutputHelper = output;
#else
    /// <summary>
    /// Test for the Localized Description Attribute
    /// </summary>
    public class LocalizedDisplayNameAttributeTest
    {
        private readonly ITestOutputHelper OutputHelper;
        /// <summary>
        /// Initializes a new instance of the <see cref="LocalizedDisplayNameAttributeTest"/> class.
        /// </summary>
        /// <param name="output">system to use to output information to test runner</param>
        public LocalizedDisplayNameAttributeTest(ITestOutputHelper output) => OutputHelper = output;
#endif
		private static readonly Type ResourceSource = typeof(Properties.Resources);

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
			[LocalizedDisplayName("SomeResourceKey")]
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
		public void LocalizedDisplayNameAttribute_Remembers_ResourceKey()
		{
			// Arrange
			const string Some_Resource_Key = "SOME_RESOURCE_KEY";

			// Act
			var attribute = new LocalizedDisplayNameAttribute(Some_Resource_Key);

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
		/// </summary>
		[Fact]
		public void GetLocalizedDisplayNameAttribute_ReturnsAttribute_IfPresent()
		{
			// Arrange
			var instance = new TestClass();
			var propDesc = TypeDescriptor.GetProperties(instance)["ResourceItem"];
			var context = new CustomTypeDescriptorContext(propDesc, instance);

			// Act
			var attr = LocalizedDisplayNameAttribute.Get(context);

			// Assert
			Output($"LocalizedDisplayNameAttribute.ResourceKey: {attr?.ResourceKey}");
			Assert.NotNull(attr);
			Assert.NotEmpty(attr.ResourceKey);
		}

		/// <summary>
		/// Gets the localized description attribute returns null if not present.
		/// </summary>
		[Fact]
		public void GetLocalizedDisplayNameAttribute_ReturnsNull_IfNotPresent()
		{
			// Arrange
			var instance = new TestClass();
			var propDesc = TypeDescriptor.GetProperties(instance)["OtherItem"];
			var context = new CustomTypeDescriptorContext(propDesc, null);

			// Act
			var attr = LocalizedDisplayNameAttribute.Get(context);

			// Assert
			Assert.Null(attr);
			Output("Null was returned by the LocalizedDisplayNameAttribute.Get call.");
		}

		/// <summary>
		/// Gets the localized category attribute returns null if no attribute.
		/// </summary>
		[Fact]
		public void GetLocalizedDisplayNameAttribute_ReturnsNull_IfNoAttribute()
		{
			// Arrange
			var instance = new TestClass();
			var propDesc = TypeDescriptor.GetProperties(instance)["ItemWithoutAttribute"];
			var context = new CustomTypeDescriptorContext(propDesc, instance);

			// Act
			var attr = LocalizedDisplayNameAttribute.Get(context);

			// Assert
			Assert.Null(attr);
			Output("Null was returned by the LocalizedDisplayNameAttribute.Get call.");
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
