using System;
using System.ComponentModel;

namespace PropertyGridHelpersTest.Support
{
    /// <summary>
    /// A generic fake property descriptor for testing.
    /// </summary>
    public class FakePropertyDescriptor : PropertyDescriptor
    {
        private readonly Type _componentType;
        private readonly Type _propertyType;

        /// <summary>
        /// Initializes a new instance of the <see cref="FakePropertyDescriptor"/> class.
        /// </summary>
        /// <param name="name">The name of the property.</param>
        /// <param name="componentType">The type of the component.</param>
        /// <param name="propertyType">The type of the property.</param>
        public FakePropertyDescriptor(string name, Type componentType, Type propertyType)
            : base(name, null)
        {
            _componentType = componentType ?? throw new ArgumentNullException(nameof(componentType));
            _propertyType = propertyType ?? throw new ArgumentNullException(nameof(propertyType));
        }

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
        /// Determines whether this instance [can reset value] the specified component.
        /// </summary>
        /// <param name="component">The component.</param>
        /// <returns>
        ///   <c>true</c> if this instance [can reset value] the specified component; otherwise, <c>false</c>.
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
    }
}
