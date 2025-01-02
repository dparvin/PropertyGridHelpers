using System;
using System.ComponentModel;
using System.Drawing.Design;

namespace PropertyGridHelpers.UIEditors
{
    /// <summary>
    /// UITypeEditor for flag Enums
    /// </summary>
    /// <typeparam name="T">EnumConverter to use to make the text in the drop-down list</typeparam>
    /// <seealso cref="UITypeEditor" />
    /// <seealso cref="IDisposable" />
    /// <seealso cref="UITypeEditor" />
    public partial class FlagEnumUIEditor<T> : FlagEnumUIEditor where T : EnumConverter, new()
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="FlagEnumUIEditor" /> class.
        /// </summary>
        public FlagEnumUIEditor() : base() => FlagEnumCB.Converter = new T();
    }
}