using System;
using System.ComponentModel;

namespace PropertyGridHelpers.Attributes
{
#if NET8_0_OR_GREATER
    /// <summary>
    /// File Extension Attribute for modifying the way that resources are loaded 
    /// into the <see cref="UIEditors.ImageTextUIEditor"/> class.
    /// </summary>
    /// <param name="propertyName">Name of the property to get the file extension from.</param>
    /// <seealso cref="Attribute" />
    /// <remarks>
    /// When this is used on a property to point to an enum that is used to get an image,
    /// then this attribute points to a property that contains the file extension. This will 
    /// work in conjunction with the <see cref="ResourcePathAttribute"/> or the
    /// <see cref="DynamicPathSourceAttribute"/> and the <see cref="EnumImageAttribute"/> to
    /// form the overall name of the resource to load.
    /// </remarks>
    [AttributeUsage(AttributeTargets.Property, AllowMultiple = false)]
    public class FileExtensionAttribute(string propertyName) : Attribute
#else
    /// <summary>
    /// File Extension Attribute for modifying the way that resources are loaded 
    /// into the <see cref="UIEditors.ImageTextUIEditor"/> class.
    /// </summary>
    /// <seealso cref="Attribute" />
    /// <remarks>
    /// When this is used on a property to point to an enum that is used to get an image,
    /// then this attribute points to a property that contains the file extension. This will 
    /// work in conjunction with the <see cref="ResourcePathAttribute"/> or the
    /// <see cref="DynamicPathSourceAttribute"/> and the <see cref="EnumImageAttribute"/> to
    /// form the overall name of the resource to load.
    /// </remarks>
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
        /// <summary>
        /// Exists the specified value.
        /// </summary>
        /// <param name="context">The context.</param>
        /// <returns></returns>
        public static bool Exists(ITypeDescriptorContext context) => Get(context) != null;

        /// <summary>
        /// Gets the File Extension Attribute from the enum value.
        /// </summary>
        /// <param name="context">The context.</param>
        /// <returns></returns>
        public static FileExtensionAttribute Get(ITypeDescriptorContext context)
        {
            if (context == null ||
                context.Instance == null ||
                context.PropertyDescriptor == null)
                return null;
            var propertyInfo = context.PropertyDescriptor.ComponentType.GetProperty(context.PropertyDescriptor.Name) ??
                throw new InvalidOperationException($"Property '{context.PropertyDescriptor.Name}' not found on type '{context.PropertyDescriptor.ComponentType}'.");
            return (FileExtensionAttribute)GetCustomAttribute(propertyInfo, typeof(FileExtensionAttribute));
        }
    }
}
