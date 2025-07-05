using System.ComponentModel;
using System.Windows.Forms;

namespace PropertyGridHelpers.UIEditors
{
    /// <summary>
    /// Provides a type-safe <see cref="AutoCompleteComboBoxEditor"/> for editing enum-based
    /// values in a <see cref="PropertyGrid"/>. This generic variant allows specifying a custom
    /// <see cref="EnumConverter"/> to control display and parsing behavior.
    /// </summary>
    /// <typeparam name="T">
    /// A custom <see cref="EnumConverter"/> type that provides conversion logic for the target enum.
    /// </typeparam>
    /// <remarks>
    /// This editor automatically creates an instance of <typeparamref name="T"/> and
    /// assigns it to the <see cref="AutoCompleteComboBoxEditor.Converter"/> property, allowing
    /// for customized enum text representation in the drop-down.
    /// </remarks>
    /// <seealso cref="AutoCompleteComboBoxEditor"/>
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
