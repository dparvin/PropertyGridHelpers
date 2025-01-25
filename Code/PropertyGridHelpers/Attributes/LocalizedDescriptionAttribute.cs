using System;
using System.ComponentModel;

namespace PropertyGridHelpers.Attributes
{
#if NET8_0_OR_GREATER
    /// <summary>
    /// Attribute for localized descriptions.
    /// </summary>
    /// <param name="resourceKey">The resource key.</param>
    /// <param name="resourceSource">The resource source.</param>
    /// <seealso cref="DescriptionAttribute" />
    public class LocalizedDescriptionAttribute(string resourceKey, Type resourceSource) :
        DescriptionAttribute(Support.Support.GetResourceString(resourceKey, resourceSource))
    {
    }
#else
    /// <summary>
    /// Attribute for localized descriptions.
    /// </summary>
    /// <seealso cref="DescriptionAttribute" />
    public class LocalizedDescriptionAttribute : DescriptionAttribute
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="LocalizedDescriptionAttribute"/> class.
        /// </summary>
        /// <param name="resourceKey">The resource key.</param>
        /// <param name="resourceSource">The resource source.</param>
        public LocalizedDescriptionAttribute(string resourceKey, Type resourceSource)
            : base(Support.Support.GetResourceString(resourceKey, resourceSource)) { }
    }
#endif
}
