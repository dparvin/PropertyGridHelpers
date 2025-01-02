using PropertyGridHelpers.Attributes;
using System;
using System.ComponentModel;

namespace PropertyGridHelpers.Converters
{
    /// <summary>
    /// Generic version of Enum Text Converter
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <seealso cref="EnumConverter" />
    /// <seealso cref="IDisposable" />
    /// <remarks>
    /// This converter is used to display specialized text in the PropertyGrid
    /// where the text is tied to the elements of an Enum.  Use the
    /// <see cref="EnumTextAttribute" /> to attach the text to the Enum
    /// elements.
    /// It uses the specified enum type set as a generic type for the class.
    /// </remarks>
    /// <seealso cref="EnumConverter" />
    public partial class EnumTextConverter<T> : EnumTextConverter where T : Enum
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="EnumTextConverter" /> class.
        /// </summary>
        public EnumTextConverter() : base(typeof(T)) { }
    }
}