using System;
using System.ComponentModel;

namespace PropertyGridHelpersTest.Support
{
#if NET8_0_OR_GREATER
    /// <summary>
    /// A generic fake property descriptor for testing.
    /// </summary>
    /// <param name="name">The name of the property.</param>
    /// <param name="componentType">The type of the component.</param>
    /// <param name="propertyType">The type of the property.</param>
    /// <param name="attributes">The attributes.</param>
    public class FakePropertyDescriptor(
            string name,
            Type componentType,
            Type propertyType,
            params Attribute[] attributes) : 
            PropertyDescriptor(name, attributes)
    {
        private readonly Type _componentType = componentType ?? throw new ArgumentNullException(nameof(componentType));
        private readonly Type _propertyType = propertyType ?? throw new ArgumentNullException(nameof(propertyType));
        private readonly AttributeCollection _attributes = attributes != null && attributes.Length > 0 ? new AttributeCollection(attributes) : new AttributeCollection(null);
#else
    /// <summary>
    /// A generic fake property descriptor for testing.
    /// </summary>
    public class FakePropertyDescriptor : PropertyDescriptor
    {
        private readonly Type _componentType;
        private readonly Type _propertyType;
        private readonly AttributeCollection _attributes;

        /// <summary>
        /// Initializes a new instance of the <see cref="FakePropertyDescriptor" /> class.
        /// </summary>
        /// <param name="name">The name of the property.</param>
        /// <param name="componentType">The type of the component.</param>
        /// <param name="propertyType">The type of the property.</param>
        /// <param name="attributes">The attributes.</param>
        /// <exception cref="ArgumentNullException">
        /// componentType
        /// or
        /// propertyType
        /// </exception>
        public FakePropertyDescriptor(
            string name,
            Type componentType,
            Type propertyType,
            params Attribute[] attributes)
            : base(name, attributes)
        {
            _componentType = componentType ?? throw new ArgumentNullException(nameof(componentType));
            _propertyType = propertyType ?? throw new ArgumentNullException(nameof(propertyType));
            _attributes = attributes != null && attributes.Length > 0 ? new AttributeCollection(attributes) : new AttributeCollection(null);
        }
#endif

        /// <summary>
        /// Gets the type of the component.
        /// </summary>
        /// <value>
        /// The type of the component.
        /// </value>
        public override Type ComponentType => _componentType;

        /// <summary>
        /// Gets a value indicating whether this instance is read only.
        /// </summary>
        /// <value>
        ///   <c>true</c> if this instance is read only; otherwise, <c>false</c>.
        /// </value>
        public override bool IsReadOnly => false;

        /// <summary>
        /// Gets the type of the property.
        /// </summary>
        /// <value>
        /// The type of the property.
        /// </value>
        public override Type PropertyType => _propertyType;

        /// <summary>
        /// Determines whether this instance can reset value the specified component.
        /// </summary>
        /// <param name="component">The component.</param>
        /// <returns>
        ///   <c>true</c> if this instance can reset value the specified component; otherwise, <c>false</c>.
        /// </returns>
        public override bool CanResetValue(object component) => false;

        /// <summary>
        /// Gets the value.
        /// </summary>
        /// <param name="component">The component.</param>
        /// <returns></returns>
        public override object GetValue(object component) => null;

        /// <summary>
        /// Resets the value.
        /// </summary>
        /// <param name="component">The component.</param>
        public override void ResetValue(object component)
        {
        }

        /// <summary>
        /// Sets the value.
        /// </summary>
        /// <param name="component">The component.</param>
        /// <param name="value">The value.</param>
        public override void SetValue(object component, object value)
        {
        }

        /// <summary>
        /// Should serialize value.
        /// </summary>
        /// <param name="component">The component.</param>
        /// <returns></returns>
        public override bool ShouldSerializeValue(object component) => false;

        /// <summary>
        /// Gets the attributes.
        /// </summary>
        /// <value>
        /// The attributes.
        /// </value>
        public override AttributeCollection Attributes => _attributes;
    }
}
