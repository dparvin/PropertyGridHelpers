﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms.Design;
using System.Windows.Forms;
using PropertyGridHelpers.Controls;
#if NET35
using PropertyGridHelpersTest.net35.UIEditor;
#elif NET452
using PropertyGridHelpersTest.net452.UIEditor;
#elif NET462
using PropertyGridHelpersTest.net462.UIEditor;
#elif NET472
using PropertyGridHelpersTest.net472.UIEditor;
#elif NET481
using PropertyGridHelpersTest.net481.UIEditor;
#elif NET8_0
using PropertyGridHelpersTest.net80.UIEditor;
#elif NET9_0
using PropertyGridHelpersTest.net90.UIEditor;
#endif

namespace PropertyGridHelpersTest.Support
{
    /// <summary>
    /// 
    /// </summary>
    /// <seealso cref="System.Windows.Forms.Design.IWindowsFormsEditorService" />
    public class TestWindowsFormsEditorService : IWindowsFormsEditorService
    {
        /// <summary>
        /// Gets the last shown control.
        /// </summary>
        /// <value>
        /// The last shown control.
        /// </value>
        public Control LastShownControl
        {
            get; private set;
        }

        /// <summary>
        /// Drops down control.
        /// </summary>
        /// <param name="control">The control.</param>
        public void DropDownControl(Control control)
        {
            LastShownControl = control;

            if (control is FlagCheckedListBox flagListBox)
            {
                // Simulate user selection in FlagCheckedListBox
                flagListBox.EnumValue = FlagEnumUIEditorTest.TestEnums.SecondEntry;
            }
        }

        /// <summary>
        /// Closes the drop down.
        /// </summary>
        public void CloseDropDown()
        {
            // Simulate closing the dropDown
        }

        /// <summary>
        /// Shows the dialog.
        /// </summary>
        /// <param name="dialog">The dialog.</param>
        public void ShowDialog(Form dialog)
        {
            // Simulate showing a dialog if necessary
        }

        DialogResult IWindowsFormsEditorService.ShowDialog(Form dialog) => DialogResult.OK;
    }
}
