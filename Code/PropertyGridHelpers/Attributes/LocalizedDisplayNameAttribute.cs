using System;

namespace PropertyGridHelpers.Attributes
{
#if NET8_0_OR_GREATER
    /// <summary>
    /// Specifies a localized display name for a property, event, or other
    /// member in a class.
    /// </summary>
    /// <seealso cref="LocalizedTextAttribute" />
    /// <remarks>
    /// Initializes a new instance of the <see cref="LocalizedDisplayNameAttribute" /> class.
    /// </remarks>
    /// <param name="resourceKey">The key identifying the localized string in the resource file.</param>
    /// <example>
    ///   <code>
    /// [LocalizedDisplayName("PropertyName_DisplayName")]
    /// public int PropertyName { get; set; }
    /// </code>
    /// </example>
    [AttributeUsage(AttributeTargets.Property | AttributeTargets.Event | AttributeTargets.Method, AllowMultiple = false)]
    public class LocalizedDisplayNameAttribute(string resourceKey) : LocalizedTextAttribute(resourceKey)
    {
    }
#else
    /// <summary>
    /// Specifies a localized display name for a property, event, or other
    /// member in a class.
    /// </summary>
    /// <seealso cref="LocalizedTextAttribute" />
    /// <remarks>
    /// Initializes a new instance of the <see cref="LocalizedDisplayNameAttribute" /> class.
    /// </remarks>
    /// <example>
    ///   <code>
    /// [LocalizedDisplayName("PropertyName_DisplayName")]
    /// public int PropertyName { get; set; }
    /// </code>
    /// </example>
    [AttributeUsage(AttributeTargets.Property | AttributeTargets.Event | AttributeTargets.Method, AllowMultiple = false)]
    public class LocalizedDisplayNameAttribute : LocalizedTextAttribute
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="LocalizedDisplayNameAttribute" /> class.
        /// </summary>
        /// <param name="resourceKey">The key identifying the localized string in the resource file.</param>
        /// <example>
        ///   <code>
        /// [LocalizedDisplayName("PropertyName_DisplayName")]
        /// public int PropertyName { get; set; }
        /// </code>
        /// </example>
        public LocalizedDisplayNameAttribute(string resourceKey) : base(resourceKey)
        {
        }
    }
#endif
}