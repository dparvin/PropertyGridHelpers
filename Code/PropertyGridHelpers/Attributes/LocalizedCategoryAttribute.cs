using System;

namespace PropertyGridHelpers.Attributes
{
#if NET8_0_OR_GREATER
    /// <summary>
    /// Attribute for specifying a localized category name for a property or event.
    /// </summary>
    /// <remarks>
    /// This attribute allows category names displayed in property grids to be localized
    /// by retrieving the category name from a resource file.
    /// </remarks>
    /// <seealso cref="LocalizedTextAttribute" />
    /// <remarks>
    /// Initializes a new instance of the <see cref="LocalizedCategoryAttribute" /> class.
    /// </remarks>
    /// <param name="resourceKey">The key used to retrieve the localized category name from the resource file.</param>
    /// <example>
    ///   <code>
    /// [LocalizedCategory("PropertyName_Category")]
    /// public int PropertyName { get; set; }
    ///   </code>
    /// </example>
    /// <remarks>
    /// The constructor fetches the localized category name using the specified resource key.
    /// </remarks>
    [AttributeUsage(AttributeTargets.Property | AttributeTargets.Event | AttributeTargets.Method, AllowMultiple = false)]
    public class LocalizedCategoryAttribute(string resourceKey) : LocalizedTextAttribute(resourceKey)
    {
    }
#else
    /// <summary>
    /// Attribute for specifying a localized category name for a property or event.
    /// </summary>
    /// <remarks>
    /// This attribute allows category names displayed in property grids to be localized
    /// by retrieving the category name from a resource file.
    /// </remarks>
    /// <seealso cref="LocalizedTextAttribute" />
    /// <example>
    ///   <code>
    /// [LocalizedCategory("PropertyName_Category")]
    /// public int PropertyName { get; set; }
    ///   </code>
    /// </example>
    [AttributeUsage(AttributeTargets.Property | AttributeTargets.Event | AttributeTargets.Method, AllowMultiple = false)]
    public class LocalizedCategoryAttribute : LocalizedTextAttribute
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="LocalizedCategoryAttribute" /> class.
        /// </summary>
        /// <param name="resourceKey">The key used to retrieve the localized category name from the resource file.</param>
        /// <example>
        ///   <code>
        /// [LocalizedCategory("PropertyName_Category")]
        /// public int PropertyName { get; set; }
        ///   </code>
        /// </example>
        /// <remarks>
        /// The constructor fetches the localized category name using the specified resource key.
        /// </remarks>
        public LocalizedCategoryAttribute(string resourceKey) : base(resourceKey)
        {
        }
    }
#endif
}
