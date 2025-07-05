using System;
using System.ComponentModel;

namespace PropertyGridHelpers.Attributes
{
#if NET8_0_OR_GREATER
    /// <summary>
    /// Specifies a property containing the file extension to use when resolving
    /// resource names for an <see cref="UIEditors.ImageTextUIEditor"/>.
    /// </summary>
    /// <param name="propertyName">
    /// The name of the property on the same object that provides the file extension value.
    /// </param>
    /// <remarks>
    /// This attribute is intended for scenarios where an image resource is dynamically
    /// constructed from an enum value plus a file extension. It works together with
    /// <see cref="ResourcePathAttribute"/>, <see cref="DynamicPathSourceAttribute"/>, and
    /// <see cref="EnumImageAttribute"/> to build a complete resource path for images or icons.
    /// </remarks>
    /// <seealso cref="Attribute"/>
    [AttributeUsage(AttributeTargets.Property, AllowMultiple = false)]
    public class FileExtensionAttribute(string propertyName) : Attribute
#else
    /// <summary>
    /// Specifies a property containing the file extension to use when resolving
    /// resource names for an <see cref="UIEditors.ImageTextUIEditor"/>.
    /// </summary>
    /// <remarks>
    /// This attribute is intended for scenarios where an image resource is dynamically
    /// constructed from an enum value plus a file extension. It works together with
    /// <see cref="ResourcePathAttribute"/>, <see cref="DynamicPathSourceAttribute"/>, and
    /// <see cref="EnumImageAttribute"/> to build a complete resource path for images or icons.
    /// </remarks>
    /// <seealso cref="Attribute"/>
    [AttributeUsage(AttributeTargets.Property, AllowMultiple = false)]
    public class FileExtensionAttribute : Attribute
#endif
    {
        /// <summary>
        /// Gets the name of the property containing the file extension.
        /// </summary>
        /// <value>
        /// The name of the property on the target object that will be used to determine
        /// the file extension when building the resource path.
        /// </value>
        public string PropertyName
        {
            get;
#if NET8_0_OR_GREATER
        } = propertyName;
#else
        }

        /// <summary>
        /// Gets the name of the property containing the file extension.
        /// </summary>
        /// <value>
        /// The name of the property on the target object that will be used to determine
        /// the file extension when building the resource path.
        /// </value>
        public FileExtensionAttribute(string propertyName) => PropertyName = propertyName;
#endif

        /// <summary>
        /// Checks whether a <see cref="FileExtensionAttribute"/> exists on the current
        /// property within the given context.
        /// </summary>
        /// <param name="context">The type descriptor context.</param>
        /// <returns>
        /// <c>true</c> if the attribute is applied; otherwise, <c>false</c>.
        /// </returns>
        public static bool Exists(ITypeDescriptorContext context) =>
            Get(context) != null;

        /// <summary>
        /// Retrieves the <see cref="FileExtensionAttribute"/> applied to the property
        /// described by the given context, if any.
        /// </summary>
        /// <param name="context">The type descriptor context.</param>
        /// <returns>
        /// The <see cref="FileExtensionAttribute"/>, or <c>null</c> if none is applied.
        /// </returns>
        public static FileExtensionAttribute Get(ITypeDescriptorContext context) =>
            context == null || context.Instance == null || context.PropertyDescriptor == null
                ? null
                : Support.Support.GetFirstCustomAttribute<FileExtensionAttribute>(
                    Support.Support.GetPropertyInfo(context));
    }
}
