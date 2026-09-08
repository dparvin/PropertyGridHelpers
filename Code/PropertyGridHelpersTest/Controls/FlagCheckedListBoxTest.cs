using Xunit;
using System;
using PropertyGridHelpers.Controls;
using PropertyGridHelpers.Converters;
using System.Windows.Forms;
using PropertyGridHelpersTest.Enums;

#if NET35
using Xunit.Extensions;
#else
using Xunit.Abstractions;
#endif

#if NET35
namespace PropertyGridHelpersTest.net35.Controls
#elif NET452
namespace PropertyGridHelpersTest.net452.Controls
#elif NET462
namespace PropertyGridHelpersTest.net462.Controls
#elif NET472
namespace PropertyGridHelpersTest.net472.Controls
#elif NET48
namespace PropertyGridHelpersTest.net480.Controls
#elif NET481
namespace PropertyGridHelpersTest.net481.Controls
#elif NET8_0
namespace PropertyGridHelpersTest.net80.Controls
#elif NET9_0
namespace PropertyGridHelpersTest.net90.Controls
#elif NET10_0
namespace PropertyGridHelpersTest.net100.Controls
#endif
{
	/// <summary>
	/// Tests for the flag checked list box
	/// </summary>
	public class FlagCheckedListBoxTest
	{
#if NET35
#else
        private readonly ITestOutputHelper OutputHelper;

        /// <summary>
        /// Initializes a new instance of the <see cref="FlagCheckedListBoxTest"/> class.
        /// </summary>
        /// <param name="output">The output.</param>
        public FlagCheckedListBoxTest(
            ITestOutputHelper output) => OutputHelper = output;
#endif

		/// <summary>
		/// Large Enum for testing.
		/// </summary>
		[Flags]
		public enum LargeEnum
		{
			/// <summary>
			/// The large value
			/// </summary>
			LargeValue = 0x7FFFFFFF
		}

		/// <summary>
		/// Add Item Caption is set correctly test.
		/// </summary>
		[Fact]
		public void AddItemCaptionSetCorrectlyTest()
		{
#if NET5_0_OR_GREATER
            using var list = new FlagCheckedListBox();
#else
			using (var list = new FlagCheckedListBox())
#endif
			{
				list.Add(new FlagCheckedListBoxItem(1, "Test"));
				Assert.Equal("Test", ((FlagCheckedListBoxItem)list.Items[0]).DisplayText);
				Output("Caption set to 'Test' as expected");
			}
		}

		/// <summary>
		/// Add Item Caption is set correctly test.
		/// </summary>
		[Fact]
		public void AddValuesCaptionSetCorrectlyTest()
		{
#if NET5_0_OR_GREATER
            using var list = new FlagCheckedListBox();
#else
			using (var list = new FlagCheckedListBox())
#endif
			{
				list.Add(1, "Test");
				Assert.Equal("Test", ((FlagCheckedListBoxItem)list.Items[0]).DisplayText);
				Output("Caption set to 'Test' as expected");
			}
		}

		/// <summary>
		/// Value is set correctly test.
		/// </summary>
		[Fact]
		public void AddItemValueSetCorrectlyTest()
		{
#if NET5_0_OR_GREATER
            using var list = new FlagCheckedListBox();
#else
			using (var list = new FlagCheckedListBox())
#endif
			{
				list.Add(new FlagCheckedListBoxItem(1, "Test"));
				Assert.Equal(1, ((FlagCheckedListBoxItem)list.Items[0]).Value);
				Output("Value set to 1 as expected");
			}
		}

		/// <summary>
		/// Value is set correctly test.
		/// </summary>
		[Fact]
		public void AddValuesValueSetCorrectlyTest()
		{
#if NET5_0_OR_GREATER
            using var list = new FlagCheckedListBox();
#else
			using (var list = new FlagCheckedListBox())
#endif
			{
				list.Add(1, "Test");
				Assert.Equal(1, ((FlagCheckedListBoxItem)list.Items[0]).Value);
				Output("Value set to 1 as expected");
			}
		}

		/// <summary>
		/// Value is set correctly test.
		/// </summary>
		[Fact]
		public void ItemCheckedTest()
		{
#if NET5_0_OR_GREATER
            using var list = new FlagCheckedListBox();
#else
			using (var list = new FlagCheckedListBox())
#endif
			{
				list.Add(1, "Test");
				list.SetItemCheckState(0, System.Windows.Forms.CheckState.Checked);
				Assert.Equal(1, list.GetCurrentValue());
				Output("Value set to 1 as expected");
			}
		}

		/// <summary>
		/// Value is set correctly test.
		/// </summary>
		[Fact]
		public void SetEnumValueTest()
		{
#if NET5_0_OR_GREATER
            using var list = new FlagCheckedListBox();
#else
			using (var list = new FlagCheckedListBox())
#endif
			{
				list.EnumValue = Converters.EnumTextConverterTest.TestEnums.FirstEntry;
				Assert.Equal(1, list.GetCurrentValue());
				Output("Value set to FirstEntry as expected");
			}
		}

		/// <summary>
		/// Value is set correctly test.
		/// </summary>
		[Fact]
		public void SetEnumValueNullTest()
		{
#if NET5_0_OR_GREATER
            using var list = new FlagCheckedListBox();
#else
			using (var list = new FlagCheckedListBox())
#endif
			{
				Assert.Throws<ArgumentNullException>(() => list.EnumValue = null);
				Output("Value throws an exception as expected");
			}
		}

		/// <summary>
		/// Value is set correctly test.
		/// </summary>
		[Theory]
		[InlineData(Converters.EnumTextConverterTest.TestEnums.None)]
		[InlineData(Converters.EnumTextConverterTest.TestEnums.FirstEntry)]
		[InlineData(Converters.EnumTextConverterTest.TestEnums.SecondEntry)]
		[InlineData(Converters.EnumTextConverterTest.TestEnums.AllEntries)]
		[InlineData(Converters.EnumTextConverterTest.TestEnums.NoAttribute)]
		public void GetEnumValueTest(Converters.EnumTextConverterTest.TestEnums testValue)
		{
#if NET5_0_OR_GREATER
            using var list = new FlagCheckedListBox();
#else
			using (var list = new FlagCheckedListBox())
#endif
			{
				list.Converter = new EnumTextConverter(testValue.GetType());
				list.EnumValue = testValue;
				Assert.Equal(testValue, list.EnumValue);
				Output($"Value set to {testValue} as expected");
			}
		}

		/// <summary>
		/// Adds the null item throws argument null exception.
		/// </summary>
		[Fact]
		public void AddNullItemThrowsArgumentNullException()
		{
#if NET5_0_OR_GREATER
            using var list = new FlagCheckedListBox();
#else
			using (var list = new FlagCheckedListBox())
#endif
				Assert.Throws<ArgumentNullException>(() => list.Add(null));
		}

		/// <summary>
		/// Adds the duplicate caption items.
		/// </summary>
		[Fact]
		public void AddDuplicateCaptionItems()
		{
#if NET5_0_OR_GREATER
            using var list = new FlagCheckedListBox();
#else
			using (var list = new FlagCheckedListBox())
#endif
			{
				const string dup = "Duplicate";
				list.Add(1, dup);
				list.Add(2, dup);
				Assert.Equal(2, list.Items.Count);
				Assert.Equal(dup, ((FlagCheckedListBoxItem)list.Items[0]).DisplayText);
				Assert.Equal(dup, ((FlagCheckedListBoxItem)list.Items[1]).DisplayText);
			}
		}

		/// <summary>
		/// Sets the enum value without flags attribute.
		/// </summary>
		[Fact]
		public void SetEnumValueWithoutFlagsAttribute()
		{
#if NET5_0_OR_GREATER
            using var list = new FlagCheckedListBox();
#else
			using (var list = new FlagCheckedListBox())
#endif
			{
				list.Converter = new EnumTextConverter(typeof(TestEnum));
				Assert.Throws<ArgumentException>(() => list.EnumValue = TestEnum.ItemWithoutImage);
			}
		}

		/// <summary>
		/// Clears the items test.
		/// </summary>
		[Fact]
		public void ClearItemsTest()
		{
#if NET5_0_OR_GREATER
            using var list = new FlagCheckedListBox();
#else
			using (var list = new FlagCheckedListBox())
#endif
			{
				list.Add(1, "Test");
				list.Add(2, "Another Test");
				list.Clear();
				Assert.Empty(list.Items);
			}
		}

		/// <summary>
		/// Handles the large enum values test.
		/// </summary>
		[Fact]
		public void HandleLargeEnumValuesTest()
		{
#if NET5_0_OR_GREATER
            using var list = new FlagCheckedListBox();
#else
			using (var list = new FlagCheckedListBox())
#endif
			{
				list.Converter = new EnumTextConverter(typeof(LargeEnum));
				list.EnumValue = LargeEnum.LargeValue;
				Assert.Equal(LargeEnum.LargeValue, list.EnumValue);
			}
		}

		/// <summary>
		/// Checked state consistency test.
		/// </summary>
		[Fact]
		public void CheckedStateConsistencyTest()
		{
#if NET5_0_OR_GREATER
            using var list = new FlagCheckedListBox();
#else
			using (var list = new FlagCheckedListBox())
#endif
			{
				list.Add(1, "Test");
				list.SetItemCheckState(0, System.Windows.Forms.CheckState.Checked);
				Assert.Equal(System.Windows.Forms.CheckState.Checked, list.GetItemCheckState(0));
			}
		}

		/// <summary>
		/// Updates the checked items calls update checked items with zero when composite value is zero.
		/// </summary>
		[Fact]
		public void UpdateCheckedItems_CallsUpdateCheckedItemsWithZero_WhenCompositeValueIsZero()
		{
			// Arrange
#if NET5_0_OR_GREATER
            using var list = new FlagCheckedListBox();
#else
			using (var list = new FlagCheckedListBox())
#endif
			{
				var compositeItem = new FlagCheckedListBoxItem(0, "None");
				list.Add(compositeItem);

				// Act
				list.UpdateCheckedItems(compositeItem, CheckState.Checked);

				// Assert
				// Verify behavior that ensures UpdateCheckedItems(0) was called (e.g., internal state or output).
				Assert.Equal(0, list.GetCurrentValue());
			}
		}

		/// <summary>
		/// Updates the checked items calls update checked items with zero when composite is null.
		/// </summary>
		[Fact]
		public void UpdateCheckedItems_CallsUpdateCheckedItemsWithZero_WhenCompositeIsNull()
		{
			// Arrange
#if NET5_0_OR_GREATER
            using var list = new FlagCheckedListBox();
#else
			using (var list = new FlagCheckedListBox())
#endif
			{
				list.Add(new FlagCheckedListBoxItem(1, "Test Item"));

				// Act
				list.UpdateCheckedItems(null, CheckState.Unchecked);

				// Assert
				Assert.Equal(0, list.GetCurrentValue()); // Assuming UpdateCheckedItems(0) resets the value to 0
			}
		}

		/// <summary>
		/// Updates the checked items sums values of checked items.
		/// </summary>
		[Fact]
		public void UpdateCheckedItems_SumsValuesOfCheckedItems()
		{
			// Arrange
#if NET5_0_OR_GREATER
            using var list = new FlagCheckedListBox();
#else
			using (var list = new FlagCheckedListBox())
#endif
			{
				var item1 = new FlagCheckedListBoxItem(1, "Option1");
				var item2 = new FlagCheckedListBoxItem(2, "Option2");
				list.Add(item1);
				list.Add(item2);

				// Pre-check the first item
				list.SetItemCheckState(0, CheckState.Checked);

				// Act
				list.UpdateCheckedItems(item2, CheckState.Checked);

				// Assert
				// Combined value of Option1 (1) and Option2 (2) should result in a sum of 3
				Assert.Equal(3, list.GetCurrentValue());
			}
		}

		/// <summary>
		/// Updates the checked items removes item bits when unchecked.
		/// </summary>
		[Fact]
		public void UpdateCheckedItems_RemovesItemBitsWhenUnchecked()
		{
			// Arrange
#if NET5_0_OR_GREATER
            using var list = new FlagCheckedListBox();
#else
			using (var list = new FlagCheckedListBox())
#endif
			{
				var item1 = new FlagCheckedListBoxItem(1, "Option1");
				var item2 = new FlagCheckedListBoxItem(2, "Option2");
				list.Add(item1);
				list.Add(item2);

				// Pre-check both items
				list.SetItemCheckState(0, CheckState.Checked);
				list.SetItemCheckState(1, CheckState.Checked);

				// Act
				list.UpdateCheckedItems(item2, CheckState.Unchecked);

				// Assert
				// Only Option1 (1) should remain checked, so the sum is 1
				Assert.Equal(1, list.GetCurrentValue());
			}
		}

		/// <summary>
		/// Values the setter throws if not enum.
		/// </summary>
		[Fact]
		public void Value_Setter_ThrowsIfNotEnum()
		{
			var control = new FlagCheckedListBox();
			var ex = Assert.Throws<ArgumentException>(() => control.Value = "NotAnEnum");
#if NET35
			Assert.Equal(0, string.Compare("Value must be an enum", ex.Message));
#else
            Assert.Equal("Value must be an enum", ex.Message);
#endif
		}

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
