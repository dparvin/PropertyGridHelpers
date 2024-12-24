using System;
using System.ComponentModel;
using System.Globalization;
using System.Windows.Forms;

namespace PropertyGridHelpers.Controls
{
    /// <summary>
    /// Control Combo box which is used to select multiple elements of a
    /// flag Enum.
    /// </summary>
    /// <seealso cref="CheckedListBox" />
    public class FlagCheckedListBox : CheckedListBox
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="FlagCheckedListBox" /> class.
        /// </summary>
        public FlagCheckedListBox() =>
            // This call is required by the Windows.Forms Form Designer.
            InitializeComponent();

        /// <summary>
        /// Releases unmanaged and - optionally - managed resources.
        /// </summary>
        /// <param name="disposing"><c>true</c> to release both managed and unmanaged resources; <c>false</c> to release only unmanaged resources.</param>
        protected override void Dispose(bool disposing) => base.Dispose(disposing);

        #region Component Designer generated code

        /// <summary>
        /// Initializes the component.
        /// </summary>
        private void InitializeComponent()
        {
            //
            // FlaggedCheckedListBox
            //
            CheckOnClick = true;
        }

        #endregion

        /// <summary>
        /// Adds the specified value.
        /// </summary>
        /// <param name="v">The value.</param>
        /// <param name="c">The caption.</param>
        /// <returns></returns>
        public FlagCheckedListBoxItem Add(int v, string c)
        {
            var item = new FlagCheckedListBoxItem(v, c);
            Items.Add(item);
            return item;
        }

        /// <summary>
        /// Adds the specified item.
        /// </summary>
        /// <param name="item">The item.</param>
        /// <returns></returns>
        public FlagCheckedListBoxItem Add(FlagCheckedListBoxItem item)
        {
            Items.Add(item);
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

        // Checks/Unchecks items depending on the give bit value
        /// <summary>
        /// Updates the checked items.
        /// </summary>
        /// <param name="value">The value.</param>
        protected void UpdateCheckedItems(int value)
        {
            isUpdatingCheckStates = true;

            // Iterate over all items
            for (var i = 0; i < Items.Count; i++)
            {
                var item = Items[i] as FlagCheckedListBoxItem;

                if (item.Value == 0)
                    SetItemChecked(i, value == 0);
                else
                {

                    // If the bit for the current item is on in the bit value, check it
                    if ((item.Value & value) == item.Value && item.Value != 0)
                        SetItemChecked(i, true);
                    // Otherwise uncheck it
                    else
                        SetItemChecked(i, false);
                }
            }

            isUpdatingCheckStates = false;
        }

        // Updates items in the CheckListBox
        // composite = The item that was checked/unchecked
        // cs = The check state of that item
        /// <summary>
        /// Updates the checked items.
        /// </summary>
        /// <param name="composite">The composite.</param>
        /// <param name="cs">The cs.</param>
        protected void UpdateCheckedItems(
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

            // If the item has been unchecked, remove its bits from the sum
            if (cs == CheckState.Unchecked)
                sum &= ~composite.Value;
            // If the item has been checked, combine its bits with the sum
            else
                sum |= composite.Value;

            // Update all items in the CheckListBox based on the final bit value
            UpdateCheckedItems(sum);
        }

        private bool isUpdatingCheckStates;

        // Gets the current bit value corresponding to all checked items
        /// <summary>
        /// Gets the current value.
        /// </summary>
        /// <returns></returns>
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

        private Type enumType;
        private Enum enumValue;

        // Adds items to the CheckListBox based on the members of the enum
        /// <summary>
        /// Fills the enum members.
        /// </summary>
        private void FillEnumMembers()
        {
            foreach (var name in Enum.GetNames(enumType))
            {
                var val = Enum.Parse(enumType, name);
                var caption = Converter == null ? name : (string)Converter.ConvertTo(val, typeof(string));
                var intVal = (int)Convert.ChangeType(val, typeof(int), CultureInfo.CurrentCulture);

                Add(intVal, caption);
            }
        }

        // Checks/unchecks items based on the current value of the enum variable
        /// <summary>
        /// Applies the enum value.
        /// </summary>
        private void ApplyEnumValue()
        {
            var intVal = (int)Convert.ChangeType(enumValue, typeof(int), CultureInfo.CurrentCulture);
            UpdateCheckedItems(intVal);
        }

        /// <summary>
        /// Gets or sets the enum value.
        /// </summary>
        /// <value>
        /// The enum value.
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
                if (value != null)
                {
                    Items.Clear();
                    enumValue = value;              // Store the current enum value
                    enumType = value.GetType();     // Store enum type
                    FillEnumMembers();              // Add items for enum members
                    ApplyEnumValue();               // Check/uncheck items depending on enum value
                }
            }
        }

        /// <summary>
        /// Gets or sets the converter.
        /// </summary>
        /// <value>
        /// The converter.
        /// </value>
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public EnumConverter Converter { get; set; }
    }
}