using PropertyGridHelpers.Attributes;
using Xunit;
using System.ComponentModel;
using PropertyGridHelpers.TypeDescriptors;
using System.Reflection;

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
	/// <summary>
	/// Tests for the <see cref="EnumTextAttribute"/> class
	/// </summary>
#if NET8_0_OR_GREATER
    public class EnumTextAttributeTest(ITestOutputHelper output)
#else
	public class EnumTextAttributeTest
#endif
	{
#if NET35
#elif NET8_0_OR_GREATER
        private readonly ITestOutputHelper OutputHelper = output;
#else
        private readonly ITestOutputHelper OutputHelper;

        /// <summary>
        /// Initializes a new instance of the <see cref="EnumTextAttributeTest"/> class.
        /// </summary>
        /// <param name="output">The output.</param>
        public EnumTextAttributeTest(
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
			[EnumText("TestItem1")]
			Test1,
			/// <summary>
			/// The test2
			/// </summary>
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

			/// <summary>
			/// The not an enum
			/// </summary>
			public const int NotAnEnum = 999;
		}

		#endregion

		/// <summary>
		/// Enums the text get enum text test.
		/// </summary>
		[Fact]
		public void EnumText_GetEnumTextTest()
		{
			// arrange

			// Act
			var enumText = EnumTextAttribute.Get(TestEnum.Test1);

			//Assert
			Output(enumText.EnumText);
#if NET35
			Assert.Equal(0, string.Compare("TestItem1", enumText.EnumText));
#else
            Assert.Equal("TestItem1", enumText.EnumText);
#endif

			Output("EnumText is 'TestItem1' as expected");
		}

		/// <summary>
		/// Enums the text get enum text blank test.
		/// </summary>
		[Fact]
		public void EnumText_GetEnumTextNoAttributeTest()
		{
			// arrange

			// Act
			var enumText = EnumTextAttribute.Get(TestEnum.Test2);

			//Assert
			Assert.Null(enumText);

			Output("EnumText is null as expected");
		}

		/// <summary>
		/// Enums the text get null context returns null test.
		/// </summary>
		[Fact]
		public void EnumTextGet_NullContext_ReturnsNullTest()
		{
			// arrange

			// Act
			var enumText = EnumTextAttribute.Get((ITypeDescriptorContext)null);

			//Assert
			Assert.Null(enumText);

			Output("EnumText is null as expected");
		}

		/// <summary>
		/// Enums the text get instance null returns null test.
		/// </summary>
		[Fact]
		public void EnumTextGet_InstanceNull_ReturnsNullTest()
		{
			// arrange
			var context = CustomTypeDescriptorContext.Create(null, null);

			// Act
			var enumText = EnumTextAttribute.Get(context);

			//Assert
			Assert.Null(enumText);

			Output("EnumText is null as expected");
		}

		/// <summary>
		/// Enums the text get enum text null property descriptor test.
		/// </summary>
		[Fact]
		public void EnumText_PropertyDescriptorNull_ReturnsNullTest()
		{
			// arrange
			var context = CustomTypeDescriptorContext.Create(typeof(TestClass), null);

			// Act
			var enumText = EnumTextAttribute.Get(context);

			//Assert
			Assert.Null(enumText);

			Output("EnumTextAttribute is null as expected");
		}

		/// <summary>
		/// Enums the text get enum text null instance but has property descriptor.
		/// </summary>
		[Fact]
		public void EnumText_GetEnumText_NullInstance_ButHasPropertyDescriptor()
		{
			// Arrange
			var pd = TypeDescriptor.GetProperties(typeof(TestClass))["TestEnumProperty"];
			var context = new CustomTypeDescriptorContext(pd, null);

			// Act
			var result = EnumTextAttribute.Get(context);

			// Assert
			Assert.Null(result);
			Output("Instance was null, so EnumTextAttribute is null as expected.");
		}

		/// <summary>
		/// Enums the text get enum text happy path returns expected.
		/// </summary>
		[Fact]
		public void EnumText_GetEnumText_HappyPath_ReturnsExpected()
		{
			// Arrange
			var context = CustomTypeDescriptorContext.Create(typeof(TestClass), nameof(TestClass.TestEnumProperty));

			// Act
			var result = EnumTextAttribute.Get(context);

			// Assert
			Assert.NotNull(result);
#if NET35
			Assert.Equal(0, string.Compare("TestItem1", result.EnumText));
#else
            Assert.Equal("TestItem1", result.EnumText);
#endif
		}

		/// <summary>
		/// Enums the text get enum text test.
		/// </summary>
		[Fact]
		public void EnumText_ExistsTest()
		{
			// arrange

			// Act
			var enumText = EnumTextAttribute.Exists(TestEnum.Test1);

			//Assert
			Assert.True(enumText);

			Output("EnumText exists as expected");
		}

		/// <summary>
		/// Enums the text get enum text blank test.
		/// </summary>
		[Fact]
		public void EnumText_NotExistsTest()
		{
			// arrange

			// Act
			var enumText = EnumTextAttribute.Exists(TestEnum.Test2);

			//Assert
			Assert.False(enumText);

			Output("EnumText does not exist as expected");
		}

		/// <summary>
		/// Enums the text get enum text null test.
		/// </summary>
		[Fact]
		public void EnumText_ExistsNullTest()
		{
			// arrange

			// Act
			var enumText = EnumTextAttribute.Exists(null);

			//Assert
			Assert.False(enumText);

			Output("EnumText does not exist when a null is passed as expected");
		}

		/// <summary>
		/// Gets the returns null when field information is null.
		/// </summary>
		[Fact]
		public void Get_ReturnsNull_WhenFieldInfoIsNull()
		{
			var result = EnumTextAttribute.Get((FieldInfo)null);
			Assert.Null(result);
		}

		/// <summary>
		/// Gets the returns null when enum parse fails.
		/// </summary>
		[Fact]
		public void Get_ReturnsNull_WhenEnumParseFails()
		{
			var fakeField = typeof(TestClass).GetField(nameof(TestClass.NotAnEnum)); // Field that is not part of an enum
			var result2 = EnumTextAttribute.Get(fakeField);

			Assert.Null(result2);
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
