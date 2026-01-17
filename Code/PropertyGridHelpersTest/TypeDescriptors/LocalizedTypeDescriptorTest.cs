using PropertyGridHelpers.PropertyDescriptors;
using PropertyGridHelpers.TypeDescriptors;
using System;
using System.ComponentModel;
using Xunit;
#if NET35
#else
using Xunit.Abstractions;
#endif

#if NET35
namespace PropertyGridHelpersTest.net35.TypeDescriptors
#elif NET452
namespace PropertyGridHelpersTest.net452.TypeDescriptors
#elif NET462
namespace PropertyGridHelpersTest.net462.TypeDescriptors
#elif NET472
namespace PropertyGridHelpersTest.net472.TypeDescriptors
#elif NET48
namespace PropertyGridHelpersTest.net480.TypeDescriptors
#elif NET481
namespace PropertyGridHelpersTest.net481.TypeDescriptors
#elif NET8_0
namespace PropertyGridHelpersTest.net80.TypeDescriptors
#elif NET9_0
namespace PropertyGridHelpersTest.net90.TypeDescriptors
#elif NET10_0
namespace PropertyGridHelpersTest.net100.TypeDescriptors
#endif
{
#if NET8_0_OR_GREATER
    /// <summary>
    /// Localized Type Descriptor Test
    /// </summary>
    /// <param name="output">xunit output implementation</param>
    public class LocalizedTypeDescriptorTest(ITestOutputHelper output)
    {
#else
	/// <summary>
	/// Localized Type Descriptor Test
	/// </summary>
	public class LocalizedTypeDescriptorTest
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
        public LocalizedTypeDescriptorTest(ITestOutputHelper output) =>
            OutputHelper = output;
#endif
#endif

		#region Test Methods ^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^

		/// <summary>
		/// Test Class
		/// </summary>
		private class TestClass
		{
			/// <summary>
			/// Gets or sets the property1.
			/// </summary>
			/// <value>
			/// The property1.
			/// </value>
			public string Property1
			{
				get; set;
			}
			/// <summary>
			/// Gets or sets the property2.
			/// </summary>
			/// <value>
			/// The property2.
			/// </value>
			public int Property2
			{
				get; set;
			}
		}

#if NET8_0_OR_GREATER
        /// <summary>
        /// Mock Custom Type Descriptor
        /// </summary>
        /// <seealso cref="CustomTypeDescriptor" />
        /// <remarks>
        /// Initializes a new instance of the <see cref="MockCustomTypeDescriptor"/> class.
        /// </remarks>
        /// <param name="properties">The properties.</param>
        private class MockCustomTypeDescriptor(PropertyDescriptorCollection properties) : CustomTypeDescriptor
        {
            private readonly PropertyDescriptorCollection properties = properties;
#else
		/// <summary>
		/// Mock Custom Type Descriptor
		/// </summary>
		/// <seealso cref="CustomTypeDescriptor" />
		private class MockCustomTypeDescriptor : CustomTypeDescriptor
		{
			private readonly PropertyDescriptorCollection properties;

			/// <summary>
			/// Initializes a new instance of the <see cref="MockCustomTypeDescriptor"/> class.
			/// </summary>
			/// <param name="properties">The properties.</param>
			public MockCustomTypeDescriptor(PropertyDescriptorCollection properties) =>
				this.properties = properties;
#endif

			/// <summary>
			/// Gets the properties.
			/// </summary>
			/// <param name="attributes">The attributes.</param>
			/// <returns></returns>
			public override PropertyDescriptorCollection GetProperties(Attribute[] attributes) => properties;

			/// <summary>
			/// Gets the properties.
			/// </summary>
			/// <returns></returns>
			public override PropertyDescriptorCollection GetProperties() => properties;
		}

		/// <summary>
		/// Constructors the sets parent descriptor.
		/// </summary>
		[Fact]
		public void Constructor_SetsParentDescriptor()
		{
			// Arrange
			var mockDescriptor = new MockCustomTypeDescriptor(TypeDescriptor.GetProperties(typeof(TestClass)));

			// Act
			var descriptor = new LocalizedTypeDescriptor(mockDescriptor);

			// Assert
			Assert.NotNull(descriptor);
		}

		/// <summary>
		/// Gets the properties returns localized property descriptors.
		/// </summary>
		[Fact]
		public void GetProperties_ReturnsLocalizedPropertyDescriptors()
		{
			// Arrange
			var originalProperties = TypeDescriptor.GetProperties(typeof(TestClass));
			var mockDescriptor = new MockCustomTypeDescriptor(originalProperties);
			var descriptor = new LocalizedTypeDescriptor(mockDescriptor);

			// Act
			var properties = descriptor.GetProperties();

			// Assert
			Assert.NotNull(properties);
			Assert.Equal(originalProperties.Count, properties.Count);
			foreach (PropertyDescriptor prop in properties)
			{
				_ = Assert.IsType<LocalizedPropertyDescriptor>(prop);
			}
		}

		/// <summary>
		/// Gets the properties with attributes returns localized property descriptors.
		/// </summary>
		[Fact]
		public void GetProperties_WithAttributes_ReturnsLocalizedPropertyDescriptors()
		{
			// Arrange
			var originalProperties = TypeDescriptor.GetProperties(typeof(TestClass));
			var mockDescriptor = new MockCustomTypeDescriptor(originalProperties);
			var descriptor = new LocalizedTypeDescriptor(mockDescriptor);

			// Act
			var properties = descriptor.GetProperties(null);

			// Assert
			Assert.NotNull(properties);
			Output($"properties.Count = {properties.Count}");
			Assert.Equal(originalProperties.Count, properties.Count);
			foreach (PropertyDescriptor prop in properties)
			{
				_ = Assert.IsType<LocalizedPropertyDescriptor>(prop);
			}
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
