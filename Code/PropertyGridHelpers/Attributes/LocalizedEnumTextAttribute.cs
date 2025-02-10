using System;

namespace PropertyGridHelpers.Attributes
{
#if NET8_0_OR_GREATER
    /// <summary>
    /// Attribute for localized display names.
    /// </summary>
    /// <param name="resourceKey">The resource key.</param>
    /// <param name="resourceSource">The resource source.</param>
    /// <seealso cref="EnumTextAttribute" />
    public class LocalizedEnumTextAttribute(string resourceKey, Type resourceSource) :
        EnumTextAttribute(Support.Support.GetResourceString(resourceKey, resourceSource))
    {
    }
#else
    /// <summary>
    /// Attribute for localized enum text.
    /// </summary>
    public class LocalizedEnumTextAttribute : EnumTextAttribute
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="LocalizedEnumTextAttribute" /> class.
        /// </summary>
        /// <param name="resourceKey">The resource key.</param>
        /// <param name="resourceSource">The resource source.</param>
        public LocalizedEnumTextAttribute(string resourceKey, Type resourceSource)
            : base(Support.Support.GetResourceString(resourceKey, resourceSource)) { }
    }
#endif
}
