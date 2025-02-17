using System;

namespace PropertyGridHelpers.Attributes
{
#if NET8_0_OR_GREATER
    /// <summary>
    /// Specifies a localized text representation for an Enum field, 
    /// retrieving the display text from a resource file.
    /// </summary>
    /// <seealso cref="EnumTextAttribute" />
    /// <param name="resourceKey">The key identifying the localized text in the resource file.</param>
    /// <param name="resourceSource">The type that contains the resource manager.</param>
    /// <remarks>
    /// This attribute extends <see cref="EnumTextAttribute" /> to support localization.
    /// Instead of providing a static string, it fetches the text from a resource file
    /// using a specified resource key and source type.
    /// This is useful when displaying Enum values in UI components that require
    /// translated text based on the application's culture settings.
    /// 
    /// The localized strings should be defined in the resource file (e.g., `Resources.resx`).
    /// </remarks>
    /// <example>
    /// <code>
    /// public enum Status
    /// {
    ///     [LocalizedEnumText("PendingApproval", typeof(Resources))]
    ///     Pending,
    ///     
    ///     [LocalizedEnumText("Approved", typeof(Resources))]
    ///     Approved,
    ///     
    ///     [LocalizedEnumText("Rejected", typeof(Resources))]
    ///     Rejected
    /// }
    /// </code>
    /// </example>
    public class LocalizedEnumTextAttribute(string resourceKey, Type resourceSource) :
        EnumTextAttribute(Support.Support.GetResourceString(resourceKey, resourceSource))
    {
    }
#else
    /// <summary>
    /// Specifies a localized text representation for an Enum field,
    /// retrieving the display text from a resource file.
    /// </summary>
    /// <seealso cref="EnumTextAttribute" />
    /// <remarks>
    /// This attribute extends <see cref="EnumTextAttribute" /> to support localization.
    /// Instead of providing a static string, it fetches the text from a resource file
    /// using a specified resource key and source type.
    /// This is useful when displaying Enum values in UI components that require
    /// translated text based on the application's culture settings.
    /// 
    /// The localized strings should be defined in the resource file (e.g., `Resources.resx`).
    /// </remarks>
    /// <example>
    /// <code>
    /// public enum Status
    /// {
    ///     [LocalizedEnumText("PendingApproval", typeof(Resources))]
    ///     Pending,
    ///     
    ///     [LocalizedEnumText("Approved", typeof(Resources))]
    ///     Approved,
    ///     
    ///     [LocalizedEnumText("Rejected", typeof(Resources))]
    ///     Rejected
    /// }
    /// </code>
    /// </example>
    public class LocalizedEnumTextAttribute : EnumTextAttribute
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="LocalizedEnumTextAttribute" /> class.
        /// </summary>
        /// <param name="resourceKey">The key identifying the localized text in the resource file.</param>
        /// <param name="resourceSource">The type that contains the resource manager.</param>
        /// <remarks>
        /// The <paramref name="resourceSource" /> should be a class that provides access to
        /// localization resources, typically the auto-generated `Resources` class from
        /// a `.resx` file.
        /// </remarks>
        public LocalizedEnumTextAttribute(string resourceKey, Type resourceSource)
            : base(Support.Support.GetResourceString(resourceKey, resourceSource)) { }
    }
#endif
}
