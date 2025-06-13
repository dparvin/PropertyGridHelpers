using System;
using System.ComponentModel;

namespace PropertyGridHelpers.TypeDescriptors
{
#if NET8_0_OR_GREATER
    /// <summary>
    /// Custom Type Descriptor Context
    /// </summary>
    /// <param name="propertyDescriptor">The property descriptor.</param>
    /// <param name="instance">The instance.</param>
    /// <seealso cref="ITypeDescriptorContext" />
    public partial class CustomTypeDescriptorContext(
            PropertyDescriptor propertyDescriptor,
            object instance) : ITypeDescriptorContext
    {
#else
    /// <summary>
    /// Custom Type Descriptor Context
    /// </summary>
    /// <seealso cref="ITypeDescriptorContext" />
    public partial class CustomTypeDescriptorContext : ITypeDescriptorContext
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="CustomTypeDescriptorContext"/> class.
        /// </summary>
        /// <param name="propertyDescriptor">The property descriptor.</param>
        /// <param name="instance">The instance.</param>
        public CustomTypeDescriptorContext(
            PropertyDescriptor propertyDescriptor,
            object instance)
        {
            PropertyDescriptor = propertyDescriptor;
            Instance = instance;
        }
#endif

        /// <summary>
        /// Gets the container.
        /// </summary>
        /// <value>
        /// The container.
        /// </value>
        public IContainer Container => null;

        /// <summary>
        /// Gets the instance.
        /// </summary>
        /// <value>
        /// The instance.
        /// </value>
        public object Instance
        {
            get;
#if NET8_0_OR_GREATER
        } = instance;
#else
        }
#endif
        /// <summary>
        /// Gets the property descriptor.
        /// </summary>
        /// <value>
        /// The property descriptor.
        /// </value>
        public PropertyDescriptor PropertyDescriptor
        {
            get;
#if NET8_0_OR_GREATER
        } = propertyDescriptor;
#else
        }
#endif

        /// <summary>
        /// Gets the service.
        /// </summary>
        /// <param name="serviceType">Type of the service.</param>
        /// <returns></returns>
        public object GetService(Type serviceType) => null; // Provide any required services here.

        /// <summary>
        /// Called when component changed.
        /// </summary>
        public void OnComponentChanged()
        {
        }

        /// <summary>
        /// Called when component changing.
        /// </summary>
        /// <returns></returns>
        public bool OnComponentChanging() => true;

        /// <summary>
        /// Creates the context.
        /// </summary>
        /// <param name="type">The type.</param>
        /// <param name="propertyName">Name of the property.</param>
        /// <returns></returns>
        public static ITypeDescriptorContext Create(Type type, string propertyName)
        {
            var instance = Activator.CreateInstance(type);
            var propertyDescriptor = TypeDescriptor.GetProperties(type)[propertyName];
            return new CustomTypeDescriptorContext(propertyDescriptor, instance);
        }
    }
}
