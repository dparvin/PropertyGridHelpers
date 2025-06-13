using System;
using System.ComponentModel;

namespace PropertyGridHelpers.Attributes
{
#if NET8_0_OR_GREATER
    /// <summary>
    /// Specifies a localized text representation for an Enum field,
    /// retrieving the display text from a resource file.
    /// </summary>
    /// <seealso cref="LocalizedTextAttribute" />
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
    /// Initializes a new instance of the <see cref="LocalizedEnumTextAttribute" /> class.
    /// </remarks>
    /// <param name="resourceKey">The key identifying the localized text in the resource file.</param>
    /// <example>
    ///   <code>
    /// [LocalizedEnumText("PropertyName_EnumText")]
    /// public int PropertyName { get; set; }
    ///   </code>
    /// </example>
    [AttributeUsage(AttributeTargets.Property | AttributeTargets.Enum | AttributeTargets.Field, AllowMultiple = false)]
    public class LocalizedEnumTextAttribute(string resourceKey) : LocalizedTextAttribute(resourceKey)
    {
#else
    /// <summary>
    /// Specifies a localized text representation for an Enum field,
    /// retrieving the display text from a resource file.
    /// </summary>
    /// <seealso cref="LocalizedTextAttribute" />
    /// <example>
    ///   <code>
    /// public enum Status
    /// {
    /// [LocalizedEnumText("PendingApproval")]
    /// Pending,
    /// [LocalizedEnumText("Approved")]
    /// Approved,
    /// [LocalizedEnumText("Rejected")]
    /// Rejected
    /// }
    /// </code>
    /// </example>
    [AttributeUsage(AttributeTargets.Property | AttributeTargets.Enum | AttributeTargets.Field, AllowMultiple = false)]
    public class LocalizedEnumTextAttribute : LocalizedTextAttribute
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="LocalizedEnumTextAttribute" /> class.
        /// </summary>
        /// <param name="resourceKey">The key identifying the localized text in the resource file.</param>
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
        /// Gets the <see cref="LocalizedEnumTextAttribute"/> for the specified Enum value.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <returns></returns>
        public static LocalizedEnumTextAttribute Get(Enum value) =>
            value == null ? null
                : Support.Support.GetFirstCustomAttribute<LocalizedEnumTextAttribute>(
                    Support.Support.GetEnumField(value));

        /// <summary>
        /// Gets the specified context.
        /// </summary>
        /// <param name="context">The context.</param>
        /// <returns></returns>
        public static new LocalizedEnumTextAttribute Get(ITypeDescriptorContext context) =>
            context == null || context.Instance == null || context.PropertyDescriptor == null
                ? null
                : Get((Enum)context.PropertyDescriptor.GetValue(context.Instance));
    }
}