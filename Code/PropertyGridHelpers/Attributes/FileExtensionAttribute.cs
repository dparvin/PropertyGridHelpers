using System;

namespace PropertyGridHelpers.Attributes
{
#if NET8_0_OR_GREATER
    /// <summary>
    /// File Extension Attribute for modifying the way that resources are loaded 
    /// into the <see cref="UIEditors.ImageTextUIEditor"/> class.
    /// </summary>
    /// <seealso cref="Attribute" />
    [AttributeUsage(AttributeTargets.Property, AllowMultiple = false)]
    public class FileExtensionAttribute(string propertyName) : Attribute
#else
    /// <summary>
    /// File Extension Attribute for modifying the way that resources are loaded 
    /// into the <see cref="UIEditors.ImageTextUIEditor"/> class.
    /// </summary>
    /// <seealso cref="Attribute" />
    [AttributeUsage(AttributeTargets.Property, AllowMultiple = false)]
    public class FileExtensionAttribute : Attribute
#endif
    {
        /// <summary>
        /// Gets the name of the property.
        /// </summary>
        /// <value>
        /// The name of the property.
        /// </value>
        public string PropertyName
        {
            get;
#if NET8_0_OR_GREATER
        } = propertyName;
#else
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="FileExtensionAttribute"/> class.
        /// </summary>
        /// <param name="propertyName">Name of the property.</param>
        public FileExtensionAttribute(string propertyName) => PropertyName = propertyName;
#endif
    }
}
