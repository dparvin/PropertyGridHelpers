using System;
using System.Collections.Generic;
using System.ComponentModel.Design;

namespace PropertyGridHelpers.UIEditors
{
    /// <summary>
    /// Provides a strongly-typed collection editor for editing <see cref="List{T}"/> properties
    /// in a <see cref="System.Windows.Forms.PropertyGrid"/>. 
    /// This allows users to add, remove, and edit items of type <typeparamref name="T"/>
    /// using the standard collection editor dialog.
    /// </summary>
    /// <typeparam name="T">The type of the items in the collection being edited.</typeparam>
    /// <seealso cref="CollectionEditor"/>
    /// <remarks>
    /// This editor automatically uses a <see cref="List{T}"/> as the collection type,
    /// and supports simple creation of string elements or any other type with a
    /// parameterless constructor.
    /// </remarks>
    public class CollectionUIEditor<T> : CollectionEditor
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="CollectionUIEditor{T}"/> class,
        /// targeting collections of type <see cref="List{T}"/>.
        /// </summary>
        public CollectionUIEditor() : base(type: typeof(List<T>))
        {

        }

        /// <summary>
        /// Creates a new instance of the specified <paramref name="itemType"/> to add to the collection.
        /// </summary>
        /// <param name="itemType">The type of the item to create.</param>
        /// <returns>
        /// A new instance of <paramref name="itemType"/>. For strings, an empty string is returned.
        /// </returns>
        /// <remarks>
        /// This override ensures that <c>string</c> elements are initialized to an empty value,
        /// while other types use their parameterless constructor.
        /// </remarks>
        protected override object CreateInstance(Type itemType) =>
            itemType == typeof(string) ? string.Empty : (object)Activator.CreateInstance(itemType);
    }
}