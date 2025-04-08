using PropertyGridHelpers.PropertyDescriptors;
using System;
using System.ComponentModel;

namespace PropertyGridHelpers.TypeDescriptors
{
#if NET8_0_OR_GREATER
    /// <summary>
    /// Type Descriptor used to localize property names, categories and descriptions.
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
    /// Type Descriptor used to localize property names, categories and descriptions.
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
        /// Gets the properties of the referenced class.
        /// </summary>
        /// <returns>
        /// a <see cref="PropertyDescriptorCollection" /> with the properties of the object.
        /// </returns>
        public override PropertyDescriptorCollection GetProperties() => GetProperties(null);

        /// <summary>
        /// Gets the properties.
        /// </summary>
        /// <param name="attributes">
        /// The array of attributes used to find the properties that use one or more of them.
        /// </param>
        /// <returns>
        /// a <see cref="PropertyDescriptorCollection" /> with the properties of the object.
        /// </returns>
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
