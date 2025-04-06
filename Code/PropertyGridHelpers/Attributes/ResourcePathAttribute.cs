using System;
using System.Reflection;

namespace PropertyGridHelpers.Attributes
{
#if NET8_0_OR_GREATER
    /// <summary>
    /// Specifies a resource path for use with the 
    /// <see cref="UIEditors.ImageTextUIEditor"/> class.
    /// </summary>
    /// <param name="resourcePath">The path to the resource.</param>
    /// <param name="resourceAssembly">The resource assembly.</param>
    /// <remarks>
    /// This attribute is applied to properties or enum values to specify 
    /// the resource path that the <see cref="UIEditors.ImageTextUIEditor"/> 
    /// should use when displaying images.
    /// </remarks>
    [AttributeUsage(AttributeTargets.Property | AttributeTargets.Enum | AttributeTargets.Class, AllowMultiple = false)]
    public class ResourcePathAttribute(string resourcePath, string resourceAssembly = null) : Attribute
#else
    /// <summary>
    /// Specifies a resource path for use with the 
    /// <see cref="UIEditors.ImageTextUIEditor"/> class.
    /// </summary>
    /// <remarks>
    /// This attribute is applied to properties or enum values to specify 
    /// the resource path that the <see cref="UIEditors.ImageTextUIEditor"/> 
    /// should use when displaying images.
    /// </remarks>
    [AttributeUsage(AttributeTargets.Property | AttributeTargets.Enum | AttributeTargets.Class, AllowMultiple = false)]
    public class ResourcePathAttribute : Attribute
#endif
    {
        /// <summary>
        /// Gets the resource path associated with the property or enum value.
        /// </summary>
        /// <value>
        /// A string representing the resource path.
        /// </value>
        public string ResourcePath
        {
            get;
#if NET8_0_OR_GREATER
        } = resourcePath;
#else
        }
#endif

        /// <summary>
        /// Gets the resource assembly.
        /// </summary>
        /// <value>
        /// The resource assembly.
        /// </value>
        public string ResourceAssembly
        {
            get;
#if NET8_0_OR_GREATER
        } = resourceAssembly;
#else
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ResourcePathAttribute" />
        /// class with the specified resource path.
        /// </summary>
        /// <param name="resourcePath">The path to the resource.</param>
        /// <param name="resourceAssembly">The resource assembly.</param>
        public ResourcePathAttribute(string resourcePath, string resourceAssembly = null)
        {
            ResourcePath = resourcePath;
            ResourceAssembly = resourceAssembly;
        }
#endif

        /// <summary>
        /// Resolves the assembly object from the stored assembly name.
        /// </summary>
        public Assembly GetAssembly() => string.IsNullOrEmpty(ResourceAssembly) ?
                                         Assembly.GetCallingAssembly() :
                                         Assembly.Load(ResourceAssembly);
    }
}
