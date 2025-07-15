using System;
using System.ComponentModel;
using System.Reflection;

namespace PropertyGridHelpers.Attributes
{
#if NET8_0_OR_GREATER
    /// <summary>
    /// Specifies a localized text representation for an enum field or property,
    /// retrieving its display text from a resource file at runtime.
    /// </summary>
    /// <param name="resourceKey">
    /// The key identifying the localized text in the resource file.
    /// </param>
    /// <seealso cref="LocalizedTextAttribute"/>
    /// <example>
    /// <code>
    /// public enum Status
    /// {
    ///     [LocalizedEnumText("PendingApproval")]
    ///     Pending,
    ///
    ///     [LocalizedEnumText("Approved")]
    ///     Approved,
    ///
    ///     [LocalizedEnumText("Rejected")]
    ///     Rejected
    /// }
    /// </code>
    /// </example>
    /// <remarks>
    /// Use this attribute to replace raw enum field names with user-friendly,
    /// localized display text based on a resource key.
    /// </remarks>
    [AttributeUsage(AttributeTargets.Property | AttributeTargets.Enum | AttributeTargets.Field, AllowMultiple = false)]
    public class LocalizedEnumTextAttribute(string resourceKey) : LocalizedTextAttribute(resourceKey)
    {
#else
    /// <summary>
    /// Specifies a localized text representation for an enum field or property,
    /// retrieving its display text from a resource file at runtime.
    /// </summary>
    /// <seealso cref="LocalizedTextAttribute"/>
    /// <remarks>
    /// Initializes a new instance of the <see cref="LocalizedEnumTextAttribute"/> class
    /// to link an enum field or property to a resource string key.
    /// </remarks>
    /// <example>
    /// <code>
    /// public enum Status
    /// {
    ///     [LocalizedEnumText("PendingApproval")]
    ///     Pending,
    ///
    ///     [LocalizedEnumText("Approved")]
    ///     Approved,
    ///
    ///     [LocalizedEnumText("Rejected")]
    ///     Rejected
    /// }
    /// </code>
    /// </example>
    [AttributeUsage(AttributeTargets.Property | AttributeTargets.Enum | AttributeTargets.Field, AllowMultiple = false)]
    public class LocalizedEnumTextAttribute : LocalizedTextAttribute
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="LocalizedEnumTextAttribute"/> class.
        /// </summary>
        /// <param name="resourceKey">
        /// The key identifying the localized text in the resource file.
        /// </param>
        /// <example>
        ///   <code>
        /// [LocalizedEnumText("PropertyName_EnumText")]
        /// public int PropertyName { get; set; }
        ///   </code>
        /// </example>
        public LocalizedEnumTextAttribute(string resourceKey) : base(resourceKey)
        {
        }
#endif

        /// <summary>
        /// Retrieves the <see cref="LocalizedEnumTextAttribute"/> applied to the given
        /// <paramref name="value"/>, if present.
        /// </summary>
        /// <param name="value">The enum value to look up.</param>
        /// <returns>
        /// The associated <see cref="LocalizedEnumTextAttribute"/>, or <c>null</c> if no attribute is applied.
        /// </returns>
        public static LocalizedEnumTextAttribute Get(Enum value) =>
            value == null ? null
                : Support.Support.GetFirstCustomAttribute<LocalizedEnumTextAttribute>(
                    Support.Support.GetEnumField(value));

        /// <summary>
        /// Gets the specified FieldInfo.
        /// </summary>
        /// <param name="fi">The FieldInfo.</param>
        /// <returns></returns>
        public static LocalizedEnumTextAttribute Get(FieldInfo fi)
        {
            if (fi == null || !fi.IsLiteral || !fi.IsStatic)
                return null;

            try
            {
                var enumValue = (Enum)Enum.Parse(fi.FieldType, fi.Name);
                return Get(enumValue);
            }
            catch
            {
                return null;
            }
        }

        /// <summary>
        /// Retrieves the <see cref="LocalizedEnumTextAttribute"/> from the given
        /// <see cref="ITypeDescriptorContext"/>, if present.
        /// </summary>
        /// <param name="context">The type descriptor context.</param>
        /// <returns>
        /// The <see cref="LocalizedEnumTextAttribute"/>, or <c>null</c> if not found.
        /// </returns>
        public static new LocalizedEnumTextAttribute Get(ITypeDescriptorContext context) =>
            context == null || context.Instance == null || context.PropertyDescriptor == null
                ? null
                : Get((Enum)context.PropertyDescriptor.GetValue(context.Instance));
    }
}