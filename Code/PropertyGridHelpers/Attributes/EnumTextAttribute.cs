using System;
using System.ComponentModel;

namespace PropertyGridHelpers.Attributes
{
#if NET8_0_OR_GREATER
    /// <summary>
    /// Specifies a user-friendly text representation for an Enum field,
    /// primarily for display in a property grid or UI elements.
    /// </summary>
    /// <seealso cref="Attribute" />
    /// <param name="text">The display text for the Enum field.</param>
    /// <remarks>
    /// This attribute can be applied to individual Enum fields to provide a
    /// custom display text. This is useful when showing Enum values in a
    /// PropertyGrid, dropdowns, or UI components where a more descriptive
    /// label is needed instead of the raw Enum name.
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
    /// </code>
    /// </example>
    [AttributeUsage(AttributeTargets.Field, AllowMultiple = false, Inherited = false)]
    public class EnumTextAttribute(string text) : Attribute
    {
#else
    /// <summary>
    /// Specifies a user-friendly text representation for an Enum field,
    /// primarily for display in a property grid or UI elements.
    /// </summary>
    /// <seealso cref="Attribute" />
    /// <remarks>
    /// This attribute can be applied to individual Enum fields to provide a
    /// custom display text. This is useful when showing Enum values in a
    /// PropertyGrid, dropdowns, or UI components where a more descriptive
    /// label is needed instead of the raw Enum name.
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
    /// </code>
    /// </example>
    [AttributeUsage(AttributeTargets.Field, AllowMultiple = false, Inherited = false)]
    public class EnumTextAttribute : Attribute
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="EnumTextAttribute" /> class.
        /// </summary>
        /// <param name="text">The display text for the Enum field.</param>
        public EnumTextAttribute(string text) => EnumText = text;
#endif

        /// <summary>
        /// Gets the custom text associated with the enum field.
        /// </summary>
        /// <value>
        /// A user-friendly string to display instead of the enum's raw name.
        /// </value>
        public string EnumText
        {
            get;
#if NET8_0_OR_GREATER
        } = text;
#else
        }
#endif

        /// <summary>
        /// Determines whether the specified enum value has an associated
        /// <see cref="EnumTextAttribute"/>.
        /// </summary>
        /// <param name="value">The enum value to check.</param>
        /// <returns>
        /// <c>true</c> if the attribute is applied to the enum field; otherwise, <c>false</c>.
        /// </returns>
        public static bool Exists(Enum value) => Get(value) != null;

        /// <summary>
        /// Retrieves the <see cref="EnumTextAttribute"/> applied to the specified enum value.
        /// </summary>
        /// <param name="value">The enum value to retrieve the attribute from.</param>
        /// <returns>
        /// The <see cref="EnumTextAttribute"/> instance, or <c>null</c> if not found.
        /// </returns>
        public static EnumTextAttribute Get(Enum value) =>
            value == null ? null
                : Support.Support.GetFirstCustomAttribute<EnumTextAttribute>(
                    Support.Support.GetEnumField(value));

        /// <summary>
        /// Retrieves the <see cref="EnumTextAttribute"/> using the supplied type descriptor context.
        /// </summary>
        /// <param name="context">
        /// The context providing information about the component and its property.
        /// </param>
        /// <returns>
        /// The <see cref="EnumTextAttribute"/> if found, or <c>null</c> if not available.
        /// </returns>
        public static EnumTextAttribute Get(ITypeDescriptorContext context) =>
            context == null
                ? null
                : context.Instance == null || context.PropertyDescriptor == null
                    ? null
                    : Get((Enum)context.PropertyDescriptor.GetValue(context.Instance));
    }
}
