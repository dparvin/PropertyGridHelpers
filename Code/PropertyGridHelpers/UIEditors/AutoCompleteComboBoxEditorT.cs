﻿using System.ComponentModel;
using System.Windows.Forms;

namespace PropertyGridHelpers.UIEditors
{
    /// <summary>
    /// A generic <see cref="AutoCompleteComboBoxEditor"/> for editing enum values in a <see cref="PropertyGrid"/>.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <seealso cref="AutoCompleteComboBoxEditor" />
    public class AutoCompleteComboBoxEditor<T> : AutoCompleteComboBoxEditor
        where T : EnumConverter, new()
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="AutoCompleteComboBoxEditor{T}"/> class.
        /// </summary>
        public AutoCompleteComboBoxEditor() =>
            Converter = new T();
    }
}
