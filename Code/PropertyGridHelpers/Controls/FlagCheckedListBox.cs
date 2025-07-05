using PropertyGridHelpers.Support;
using PropertyGridHelpers.UIEditors;
using System;
using System.ComponentModel;
using System.Globalization;
using System.Windows.Forms;
using System.Drawing.Design;

namespace PropertyGridHelpers.Controls
{
    /// <summary>
    /// A custom <see cref="CheckedListBox"/> control designed for editing 
    /// properties backed by [Flags] enumerations. It presents each enum value 
    /// as a checkbox, allowing users to select multiple values that combine 
    /// into a composite flag.
    ///
    /// This control is typically hosted in a drop-down editor (e.g., 
    /// <see cref="UITypeEditor"/>) using <see cref="DropDownVisualizer{TControl}"/> 
    /// to support property editing within a <see cref="PropertyGrid"/>.
    /// </summary>
    /// <remarks>
    /// When the associated enum is set via the <see cref="EnumValue"/> property,
    /// the control automatically populates with all defined enum values and 
    /// checks those corresponding to the current composite value.
    /// As checkboxes are toggled, the internal representation is updated accordingly.
    ///
    /// This control assumes that the enum is decorated with the 
    /// <see cref="FlagsAttribute"/>; an exception will be thrown otherwise.
    /// 
    /// Optional display customization is supported via a custom 
    /// <see cref="EnumConverter"/> assigned to 
    /// <see cref="Converter"/>.
    /// </remarks>
    /// <example>
    /// Typical usage:
    /// <code>
    /// [Editor(typeof(FlagEnumUIEditor), typeof(UITypeEditor))]
    /// public MyFlagsEnum FlagsProperty { get; set; }
    /// </code>
    /// </example>
    /// <seealso cref="CheckedListBox"/>
    /// <seealso cref="IDropDownEditorControl"/>
    /// <seealso cref="DropDownVisualizer{TControl}"/>
    /// <seealso cref="FlagEnumUIEditor"/>
    /// <seealso cref="FlagEnumUIEditor{T}"/>
    public partial class FlagCheckedListBox
        : CheckedListBox, IDropDownEditorControl
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="FlagCheckedListBox" /> class.
        /// </summary>
        public FlagCheckedListBox() =>
            InitializeComponent();

        /// <summary>
        /// Occurs when the user has finished editing the selection
        /// and commits the current value.
        /// </summary>
        public event EventHandler ValueCommitted = delegate { };

        /// <summary>
        /// Releases unmanaged and - optionally - managed resources.
        /// </summary>
        /// <param name="disposing"><c>true</c> to release both managed and unmanaged resources; <c>false</c> to release only unmanaged resources.</param>
        protected override void Dispose(bool disposing) => base.Dispose(disposing);

        #region Component Designer generated code ^^^^^^^^^

        /// <summary>
        /// Initializes the component.
        /// </summary>
        private void InitializeComponent() =>
            CheckOnClick = true;

        #endregion

        /// <summary>
        /// Adds a new item to the checklist with the specified bitwise value and display caption.
        /// </summary>
        /// <param name="value">The bitwise integer value representing a flag.</param>
        /// <param name="caption">The display text for the item.</param>
        /// <returns>
        /// A <see cref="FlagCheckedListBoxItem"/> representing the added item.
        /// </returns>
        public FlagCheckedListBoxItem Add(int value, string caption)
        {
            var item = new FlagCheckedListBoxItem(value, caption);
            _ = Items.Add(item);
            return item;
        }

        /// <summary>
        /// Removes all items from the checklist and resets its state.
        /// </summary>
        public void Clear() =>
            Items.Clear();

        /// <summary>
        /// Adds an existing <see cref="FlagCheckedListBoxItem"/> to the checklist.
        /// </summary>
        /// <param name="item">The item to add.</param>
        /// <returns>
        /// The added <see cref="FlagCheckedListBoxItem"/>.
        /// </returns>
        public FlagCheckedListBoxItem Add(FlagCheckedListBoxItem item)
        {
            _ = Items.Add(item);
            return item;
        }

        /// <summary>
        /// Raises the <see cref="CheckedListBox.ItemCheck" /> event.
        /// </summary>
        /// <param name="ice">The <see cref="ItemCheckEventArgs" /> instance containing the event data.</param>
        protected override void OnItemCheck(ItemCheckEventArgs ice)
        {
            base.OnItemCheck(ice);

            if (ice == null) return;

            if (isUpdatingCheckStates)
                return;

            // Get the checked/unchecked item
            var item = Items[ice.Index] as FlagCheckedListBoxItem;
            // Update other items
            UpdateCheckedItems(item, ice.NewValue);
        }

        /// <summary>
        /// Updates the check state of all items based on the provided bitwise value.
        /// </summary>
        /// <param name="value">
        /// A bitwise integer representing the currently active flags.
        /// </param>
        protected internal void UpdateCheckedItems(int value)
        {
            isUpdatingCheckStates = true;

            // Iterate over all items
            for (var i = 0; i < Items.Count; i++)
            {
                var item = Items[i] as FlagCheckedListBoxItem;

                switch (item.Value)
                {
                    case 0:
                        SetItemChecked(i, value == 0);
                        break;
                    default:
                        // If the bit for the current item is on in the bit value, check it
                        SetItemChecked(i, (item.Value & value) == item.Value && item.Value != 0);
                        break;
                }
            }

            isUpdatingCheckStates = false;
        }

        /// <summary>
        /// Updates the check states of items in response to a change in one item,
        /// maintaining the consistency of the composite flag value.
        /// </summary>
        /// <param name="composite">The item that changed.</param>
        /// <param name="cs">The new check state of that item.</param>
        protected internal void UpdateCheckedItems(
            FlagCheckedListBoxItem composite,
            CheckState cs)
        {
            // If the value of the item is 0, call directly.
            if (composite?.Value == 0)
                UpdateCheckedItems(0);

            // Get the total value of all checked items
            var sum = 0;
            for (var i = 0; i < Items.Count; i++)
            {
                var item = Items[i] as FlagCheckedListBoxItem;

                // If item is checked, add its value to the sum.
                if (GetItemChecked(i))
                    sum |= item.Value;
            }

            // Check if composite is null before accessing its Value
            if (composite != null)
            {
                // If the item has been unchecked, remove its bits from the sum
                if (cs == CheckState.Unchecked)
                    sum &= ~composite.Value;
                // If the item has been checked, combine its bits with the sum
                else
                    sum |= composite.Value;
            }

            // Update all items in the CheckListBox based on the final bit value
            UpdateCheckedItems(sum);
        }

        private bool isUpdatingCheckStates;

        /// <summary>
        /// Gets the composite integer value representing all currently checked flags.
        /// </summary>
        /// <returns>
        /// An <see cref="int"/> value encoding the combined flags of checked items.
        /// </returns>
        public int GetCurrentValue()
        {
            var sum = 0;

            for (var i = 0; i < Items.Count; i++)
            {
                var item = Items[i] as FlagCheckedListBoxItem;

                if (GetItemChecked(i))
                    sum |= item.Value;
            }

            return sum;
        }

        /// <summary>
        /// The type of the enum currently bound to this control.
        /// </summary>
        private Type enumType;
        /// <summary>
        /// The current enum value represented by the checked items.
        /// </summary>
        private Enum enumValue;

        /// <summary>
        /// Populates the checklist with entries corresponding to each member of the bound enum type.
        /// </summary>
        private void FillEnumMembers()
        {
            foreach (var name in Enum.GetNames(enumType))
            {
                var val = Enum.Parse(enumType, name);
                var caption = Converter == null ? name : (string)Converter.ConvertTo(val, typeof(string));
                var intVal = (int)Convert.ChangeType(val, typeof(int), CultureInfo.CurrentCulture);

                _ = Add(intVal, caption);
            }
        }

        /// <summary>
        /// Synchronizes the check state of items in the list to reflect the current <see cref="EnumValue"/>.
        /// </summary>
        /// <remarks>
        /// This ensures the visual checklist matches the current bitwise value.
        /// </remarks>
        private void ApplyEnumValue()
        {
            var intVal = (int)Convert.ChangeType(enumValue, typeof(int), CultureInfo.CurrentCulture);
            UpdateCheckedItems(intVal);
        }

        /// <summary>
        /// Gets or sets the current enum value represented by the checked items
        /// in the list. This property serves as the main interface for binding 
        /// a flags-based enum to the control.
        /// </summary>
        /// <remarks>
        /// <para>
        /// When setting this property, the control performs the following:
        /// </para>
        /// <list type="bullet">
        ///   <item><description>Verifies the provided value is a valid enum marked with the <see cref="FlagsAttribute"/>.</description></item>
        ///   <item><description>Clears any existing items in the list.</description></item>
        ///   <item><description>Populates the list with all members of the enum type.</description></item>
        ///   <item><description>Automatically checks the items corresponding to the current flags set in the value.</description></item>
        /// </list>
        /// <para>
        /// When getting this property, it returns the composite enum value 
        /// corresponding to the checked items.
        /// </para>
        /// <para>
        /// You must set this property to initialize the control's content. 
        /// Attempting to use the control without setting a valid enum value 
        /// will result in incorrect or empty state.
        /// </para>
        /// </remarks>
        /// <exception cref="ArgumentNullException">
        /// Thrown when the provided value is <c>null</c>.
        /// </exception>
        /// <exception cref="ArgumentException">
        /// Thrown if the value is not an enum or is not decorated with the <see cref="FlagsAttribute"/>.
        /// </exception>
        /// <example>
        /// <code>
        /// var listBox = new FlagCheckedListBox();
        /// listBox.EnumValue = MyFlagsEnum.OptionA | MyFlagsEnum.OptionC;
        /// var selected = listBox.EnumValue; // Returns combined enum value
        /// </code>
        /// </example>
        /// <value>
        /// A <see cref="Enum"/> value representing the checked state of the control.
        /// </value>
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public Enum EnumValue
        {
            get
            {
                var e = Enum.ToObject(enumType, GetCurrentValue());
                return (Enum)e;
            }
            set
            {
#if NET5_0_OR_GREATER
                ArgumentNullException.ThrowIfNull(value);
#else
                if (value == null)
                    throw new ArgumentNullException(nameof(value));
#endif
                enumType = value.GetType();

                if (!Attribute.IsDefined(enumType, typeof(FlagsAttribute)))
                    throw new ArgumentException($"Enum type '{enumType.Name}' must be an enum and have the [Flags] attribute.", nameof(value));
                Items.Clear();
                enumValue = value;              // Store the current enum value
                FillEnumMembers();              // Add items for enum members
                ApplyEnumValue();               // Check/uncheck items depending on enum value
            }
        }

        /// <summary>
        /// Gets or sets the <see cref="EnumConverter"/> used to control how the enum 
        /// values are displayed in the list.
        /// </summary>
        /// <remarks>
        /// This property allows you to provide a custom converter that defines 
        /// how enum values are rendered in the UI—for example, displaying localized 
        /// or user-friendly names instead of the raw enum identifiers.
        /// 
        /// If no converter is set, the enum field names are used as-is.
        /// This is especially useful when the enum values are annotated with custom 
        /// attributes (e.g., <c>EnumTextAttribute</c>) and you want those descriptions 
        /// to be shown in the UI.
        /// 
        /// This property should be assigned before setting <see cref="EnumValue"/>,
        /// as it affects how the list is populated.
        /// </remarks>
        /// <value>
        /// A custom <see cref="EnumConverter"/> instance for customizing enum display.
        /// </value>
        /// <example>
        /// <code>
        /// var listBox = new FlagCheckedListBox();
        /// listBox.Converter = new EnumTextConverter(typeof(MyFlagsEnum));
        /// listBox.EnumValue = MyFlagsEnum.OptionA | MyFlagsEnum.OptionB;
        /// </code>
        /// </example>
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public EnumConverter Converter
        {
            get; set;
        }

        /// <summary>
        /// Gets or sets the composite enum value represented by the checked items,
        /// in a non-generic interface-friendly way.
        /// </summary>
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public object Value
        {
            get => EnumValue;
            set => EnumValue = value is Enum e ? e : throw new ArgumentException("Value must be an enum");
        }
    }
}