using PropertyGridHelpers.Attributes;
using System;
using System.ComponentModel;
using System.Windows.Forms;

namespace PropertyGridHelpers.Converters
{
    /// <summary>
    /// A strongly-typed generic version of <see cref="EnumTextConverter"/> for handling enums with
    /// display text annotations in a <see cref="PropertyGrid"/>.
    /// </summary>
    /// <typeparam name="T">
    /// The enumeration type whose members are decorated with <see cref="EnumTextAttribute"/>.
    /// </typeparam>
    /// <remarks>
    /// This converter simplifies using <see cref="EnumTextAttribute"/> on enum members
    /// by automatically associating the generic parameter <typeparamref name="T"/> as
    /// the enum to convert. This enables the property grid to display friendly
    /// names for enum values without requiring manual type configuration.
    /// </remarks>
    /// <example>
    /// <code>
    /// public enum Status
    /// {
    ///     [EnumText("Pending Approval")]
    ///     Pending,
    ///
    ///     [EnumText("Approved")]
    ///     Approved,
    ///
    ///     [EnumText("Rejected")]
    ///     Rejected
    /// }
    ///
    /// [TypeConverter(typeof(EnumTextConverter&lt;Status&gt;))]
    /// public Status CurrentStatus { get; set; }
    /// </code>
    /// </example>
    /// <seealso cref="EnumTextAttribute"/>
    /// <seealso cref="EnumConverter"/>
    public partial class EnumTextConverter<T> : EnumTextConverter where T : Enum
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="EnumTextConverter" /> class.
        /// </summary>
        public EnumTextConverter() : base(typeof(T)) { }
    }
}