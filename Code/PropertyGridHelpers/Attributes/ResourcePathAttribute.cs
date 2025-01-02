using System;

namespace PropertyGridHelpers.Attributes
{
#if NET8_0_OR_GREATER
    /// <summary>
    /// Attribute to give parameters to the <see cref="UIEditors.ImageTextUIEditor"/> class
    /// </summary>
    /// <param name="resourcePath">The path to the resources where the images are stored</param>
    [AttributeUsage(AttributeTargets.Property | AttributeTargets.Enum, AllowMultiple = false)]
    public class ResourcePathAttribute(string resourcePath) : Attribute
#else
    /// <summary>
    /// Attribute to give parameters to the <see cref="UIEditors.ImageTextUIEditor"/> class
    /// </summary>
    [AttributeUsage(AttributeTargets.Property | AttributeTargets.Enum, AllowMultiple = false)]
    public class ResourcePathAttribute : Attribute
#endif
    {
        /// <summary>
        /// Gets the resource path.
        /// </summary>
        /// <value>
        /// The resource path.
        /// </value>
        public string ResourcePath
        {
            get;
#if NET8_0_OR_GREATER
        } = resourcePath;
#else
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ResourcePathAttribute"/> class.
        /// </summary>
        /// <param name="resourcePath">The resource path.</param>
        public ResourcePathAttribute(string resourcePath) => ResourcePath = resourcePath;
#endif
    }
}
