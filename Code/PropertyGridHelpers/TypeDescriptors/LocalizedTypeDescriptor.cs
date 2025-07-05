using PropertyGridHelpers.PropertyDescriptors;
using System;
using System.ComponentModel;

namespace PropertyGridHelpers.TypeDescriptors
{
#if NET8_0_OR_GREATER
    /// <summary>
    /// A type descriptor that provides localization for property names, categories, and descriptions.
    /// </summary>
    /// <param name="parent">
    /// The parent <see cref="ICustomTypeDescriptor"/> to wrap.
    /// </param>
    /// <seealso cref="CustomTypeDescriptor"/>
    public class LocalizedTypeDescriptor(ICustomTypeDescriptor parent) : CustomTypeDescriptor(parent)
    {
#else
    /// <summary>
    /// A type descriptor that provides localization for property names, categories, and descriptions.
    /// </summary>
    /// <seealso cref="CustomTypeDescriptor"/>
    public class LocalizedTypeDescriptor : CustomTypeDescriptor
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="LocalizedTypeDescriptor"/> class.
        /// </summary>
        /// <param name="parent">
        /// The parent <see cref="ICustomTypeDescriptor"/> to wrap.
        /// </param>
        public LocalizedTypeDescriptor(ICustomTypeDescriptor parent)
            : base(parent) { }
#endif

        /// <summary>
        /// Returns the collection of properties for the current object, using the default attributes.
        /// </summary>
        /// <returns>
        /// A <see cref="PropertyDescriptorCollection"/> containing the localized properties.
        /// </returns>
        public override PropertyDescriptorCollection GetProperties() => GetProperties(null);

        /// <summary>
        /// Returns the collection of properties for the current object that match the specified attributes,
        /// with each property descriptor decorated to support localization.
        /// </summary>
        /// <param name="attributes">
        /// An array of attributes to use as a filter. Only properties marked with one or more of these
        /// attributes will be included in the result.
        /// </param>
        /// <returns>
        /// A <see cref="PropertyDescriptorCollection"/> containing the filtered and localized properties.
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
