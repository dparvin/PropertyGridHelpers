using System;
using System.ComponentModel;
using System.Windows.Forms;

namespace PropertyGridHelpers.Controls
{
    /// <summary>
    ///
    /// </summary>
    /// <seealso cref="System.Windows.Forms.CheckedListBox" />
    public class FlagCheckedListBox : CheckedListBox
    {
        private Container components = null;

        /// <summary>
        /// Initializes a new instance of the <see cref="FlagCheckedListBox"/> class.
        /// </summary>
        public FlagCheckedListBox()
        {
            // This call is required by the Windows.Forms Form Designer.
            InitializeComponent();

            // TODO: Add any initialization after the InitForm call

        }

        /// <summary>
        /// Releases unmanaged and - optionally - managed resources.
        /// </summary>
        /// <param name="disposing"><c>true</c> to release both managed and unmanaged resources; <c>false</c> to release only unmanaged resources.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                if (components != null)
                    components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Component Designer generated code

        /// <summary>
        /// Initializes the component.
        /// </summary>
        private void InitializeComponent()
        {
            //
            // FlaggedCheckedListBox
            //
            this.CheckOnClick = true;

        }

        #endregion

        // Adds an integer value and its associated description
        /// <summary>
        /// Adds the specified value.
        /// </summary>
        /// <param name="v">The value.</param>
        /// <param name="c">The caption.</param>
        /// <returns></returns>
        public FlagCheckedListBoxItem Add(int v, string c)
        {
            FlagCheckedListBoxItem item = new FlagCheckedListBoxItem(v, c);
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
        /// Raises the <see cref="E:ItemCheck" /> event.
        /// </summary>
        /// <param name="e">The <see cref="ItemCheckEventArgs"/> instance containing the event data.</param>
        protected override void OnItemCheck(ItemCheckEventArgs e)
        {
            base.OnItemCheck(e);

            if (isUpdatingCheckStates)
                return;

            // Get the checked/unchecked item
            FlagCheckedListBoxItem item = Items[e.Index] as FlagCheckedListBoxItem;
            // Update other items
            UpdateCheckedItems(item, e.NewValue);
        }

        // Checks/Unchecks items depending on the give bitvalue
        /// <summary>
        /// Updates the checked items.
        /// </summary>
        /// <param name="value">The value.</param>
        protected void UpdateCheckedItems(int value)
        {

            isUpdatingCheckStates = true;

            // Iterate over all items
            for (int i = 0; i < Items.Count; i++)
            {
                FlagCheckedListBoxItem item = Items[i] as FlagCheckedListBoxItem;

                if (item.value == 0)
                {
                    SetItemChecked(i, value == 0);
                }
                else
                {

                    // If the bit for the current item is on in the bitvalue, check it
                    if ((item.value & value) == item.value && item.value != 0)
                        SetItemChecked(i, true);
                    // Otherwise uncheck it
                    else
                        SetItemChecked(i, false);
                }
            }

            isUpdatingCheckStates = false;

        }

        // Updates items in the checklistbox
        // composite = The item that was checked/unchecked
        // cs = The check state of that item
        /// <summary>
        /// Updates the checked items.
        /// </summary>
        /// <param name="composite">The composite.</param>
        /// <param name="cs">The cs.</param>
        protected void UpdateCheckedItems(FlagCheckedListBoxItem composite, CheckState cs)
        {

            // If the value of the item is 0, call directly.
            if (composite.value == 0)
                UpdateCheckedItems(0);


            // Get the total value of all checked items
            int sum = 0;
            for (int i = 0; i < Items.Count; i++)
            {
                FlagCheckedListBoxItem item = Items[i] as FlagCheckedListBoxItem;

                // If item is checked, add its value to the sum.
                if (GetItemChecked(i))
                    sum |= item.value;
            }

            // If the item has been unchecked, remove its bits from the sum
            if (cs == CheckState.Unchecked)
                sum = sum & (~composite.value);
            // If the item has been checked, combine its bits with the sum
            else
                sum |= composite.value;

            // Update all items in the checklistbox based on the final bit value
            UpdateCheckedItems(sum);

        }

        private bool isUpdatingCheckStates = false;

        // Gets the current bit value corresponding to all checked items
        /// <summary>
        /// Gets the current value.
        /// </summary>
        /// <returns></returns>
        public int GetCurrentValue()
        {
            int sum = 0;

            for (int i = 0; i < Items.Count; i++)
            {
                FlagCheckedListBoxItem item = Items[i] as FlagCheckedListBoxItem;

                if (GetItemChecked(i))
                    sum |= item.value;
            }

            return sum;
        }

        Type enumType;
        Enum enumValue;

        // Adds items to the checklistbox based on the members of the enum
        /// <summary>
        /// Fills the enum members.
        /// </summary>
        private void FillEnumMembers()
        {
            foreach (string name in Enum.GetNames(enumType))
            {
                object val = Enum.Parse(enumType, name);
                int intVal = (int)Convert.ChangeType(val, typeof(int));

                Add(intVal, name);
            }
        }

        // Checks/unchecks items based on the current value of the enum variable
        /// <summary>
        /// Applies the enum value.
        /// </summary>
        private void ApplyEnumValue()
        {
            int intVal = (int)Convert.ChangeType(enumValue, typeof(int));
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
                object e = Enum.ToObject(enumType, GetCurrentValue());
                return (Enum)e;
            }
            set
            {

                Items.Clear();
                enumValue = value; // Store the current enum value
                enumType = value.GetType(); // Store enum type
                FillEnumMembers(); // Add items for enum members
                ApplyEnumValue(); // Check/uncheck items depending on enum value

            }
        }
    }
}
