using System;
using System.ComponentModel;

namespace PropertyGridHelpers.TypeDescriptors
{
#if NET8_0_OR_GREATER
    /// <summary>
    /// Provides a custom <see cref="ITypeDescriptorContext"/> implementation
    /// for use in property editors, supporting design-time or test-time scenarios.
    /// </summary>
    /// <param name="propertyDescriptor">
    /// The <see cref="PropertyDescriptor"/> describing the property being edited.
    /// </param>
    /// <param name="instance">
    /// The object instance whose property is being edited.
    /// </param>
    /// <seealso cref="ITypeDescriptorContext"/>
    public partial class CustomTypeDescriptorContext(
            PropertyDescriptor propertyDescriptor,
            object instance) : ITypeDescriptorContext
    {
#else
    /// <summary>
    /// Provides a custom <see cref="ITypeDescriptorContext"/> implementation
    /// for use in property editors, supporting design-time or test-time scenarios.
    /// </summary>
    /// <seealso cref="ITypeDescriptorContext"/>
    public partial class CustomTypeDescriptorContext : ITypeDescriptorContext
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="CustomTypeDescriptorContext"/> class.
        /// </summary>
        /// <param name="propertyDescriptor">
        /// The <see cref="PropertyDescriptor"/> describing the property being edited.
        /// </param>
        /// <param name="instance">
        /// The object instance whose property is being edited.
        /// </param>
        public CustomTypeDescriptorContext(
            PropertyDescriptor propertyDescriptor,
            object instance)
        {
            PropertyDescriptor = propertyDescriptor;
            Instance = instance;
        }
#endif

        /// <summary>
        /// Gets the container object associated with this context. Always returns <c>null</c>
        /// since this implementation does not provide container services.
        /// </summary>
        public IContainer Container => null;

        /// <summary>
        /// Gets the instance whose property is being edited.
        /// </summary>
        public object Instance
        {
            get;
#if NET8_0_OR_GREATER
        } = instance;
#else
        }
#endif
        /// <summary>
        /// Gets the <see cref="PropertyDescriptor"/> describing the property being edited.
        /// </summary>
        public PropertyDescriptor PropertyDescriptor
        {
            get;
#if NET8_0_OR_GREATER
        } = propertyDescriptor;
#else
        }
#endif

        /// <summary>
        /// Gets a service object of the specified type. Always returns <c>null</c>
        /// in this implementation, as no services are provided.
        /// </summary>
        /// <param name="serviceType">The type of service requested.</param>
        /// <returns>Always <c>null</c>.</returns>
        public object GetService(Type serviceType) => null; // Provide any required services here.

        /// <summary>
        /// Called when a component has changed. This implementation does nothing.
        /// </summary>
        public void OnComponentChanged()
        {
        }

        /// <summary>
        /// Called before a component is changed. This implementation always returns <c>true</c>,
        /// indicating that changes are permitted.
        /// </summary>
        /// <returns>Always <c>true</c>.</returns>
        public bool OnComponentChanging() => true;

        /// <summary>
        /// Creates a new <see cref="CustomTypeDescriptorContext"/> for the specified type and property name.
        /// </summary>
        /// <param name="type">The type whose property is to be described.</param>
        /// <param name="propertyName">The name of the property to describe.</param>
        /// <returns>
        /// An instance of <see cref="CustomTypeDescriptorContext"/> representing the requested type and property.
        /// </returns>
        public static ITypeDescriptorContext Create(Type type, string propertyName)
        {
            var instance = type == null ? null : Activator.CreateInstance(type);
            var propertyDescriptor = type == null || string.IsNullOrEmpty(propertyName) ?
                                        null : TypeDescriptor.GetProperties(type)[propertyName];
            return new CustomTypeDescriptorContext(propertyDescriptor, instance);
        }
    }
}
