using PropertyGridHelpers.PropertyDescriptors;
using System;
using System.ComponentModel;

namespace PropertyGridHelpers.TypeDescriptors
{
#if NET8_0_OR_GREATER
    /// <summary>
    /// Localized Type Descriptor
    /// </summary>
    /// <seealso cref="CustomTypeDescriptor" />
    /// <remarks>
    /// Initializes a new instance of the <see cref="LocalizedTypeDescriptor"/> class.
    /// </remarks>
    /// <param name="parent">The parent.</param>
    public class LocalizedTypeDescriptor(ICustomTypeDescriptor parent) : CustomTypeDescriptor(parent)
    {
#else
    /// <summary>
    /// Localized Type Descriptor
    /// </summary>
    /// <seealso cref="CustomTypeDescriptor" />
    public class LocalizedTypeDescriptor : CustomTypeDescriptor
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="LocalizedTypeDescriptor"/> class.
        /// </summary>
        /// <param name="parent">The parent.</param>
        public LocalizedTypeDescriptor(ICustomTypeDescriptor parent)
            : base(parent) { }
#endif

        /// <summary>
        /// Gets the properties.
        /// </summary>
        /// <returns></returns>
        public override PropertyDescriptorCollection GetProperties() => GetProperties(null);

        /// <summary>
        /// Gets the properties.
        /// </summary>
        /// <param name="attributes">The attributes.</param>
        /// <returns></returns>
        public override PropertyDescriptorCollection GetProperties(Attribute[] attributes)
        {
            var baseProps = base.GetProperties(attributes);
            var newProps = new PropertyDescriptor[baseProps.Count];
            for (var i = 0; i < baseProps.Count; i++)
                newProps[i] = new LocalizedPropertyDescriptor(baseProps[i]);
            return new PropertyDescriptorCollection(newProps);
        }
    }
}
