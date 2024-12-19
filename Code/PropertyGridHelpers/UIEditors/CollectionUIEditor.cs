using System;
using System.Collections.Generic;
using System.ComponentModel.Design;

namespace PropertyGridHelpers.UIEditors
{
    /// <summary>
    /// Generic class for dealing with a list of items of a specific type
    /// </summary>
    /// <typeparam name="T">type of item in the list</typeparam>
    /// <seealso cref="CollectionEditor" />
    public class CollectionUIEditor<T> : CollectionEditor
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="CollectionUIEditor&lt;T&gt;" /> class.
        /// </summary>
        public CollectionUIEditor() : base(type: typeof(List<T>))
        {

        }

        /// <summary>
        /// Creates the instance.
        /// </summary>
        /// <param name="itemType">Type of the item.</param>
        /// <returns></returns>
        protected override object CreateInstance(Type itemType)
        {
            return itemType == typeof(string) ? string.Empty : (object)Activator.CreateInstance(itemType);
        }
    }
}
