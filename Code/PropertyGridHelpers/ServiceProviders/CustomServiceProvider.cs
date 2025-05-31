using System;
using System.Collections.Generic;

namespace PropertyGridHelpers.ServiceProviders
{

    /// <summary>
    /// A lightweight implementation of <see cref="IServiceProvider"/> that allows manual registration 
    /// and retrieval of service instances by type.
    /// </summary>
    /// <remarks>
    /// This class is useful in testing scenarios or situations where full-fledged dependency injection 
    /// is unnecessary. It is also used internally by the <c>PropertyGridHelpers</c> library to support 
    /// dynamic or programmatic access to services that would otherwise be resolved via attributes.
    /// </remarks>
    /// <example>
    /// <code>
    /// var provider = new CustomServiceProvider();
    /// provider.AddService(typeof(IMyService), new MyService());
    /// var service = provider.GetService(typeof(IMyService)) as IMyService;
    /// </code>
    /// </example>
    public class CustomServiceProvider : IServiceProvider
    {
#if NET8_0_OR_GREATER
        private readonly Dictionary<Type, object> _services = [];
#else
        private readonly Dictionary<Type, object> _services = new Dictionary<Type, object>();
#endif

        /// <summary>
        /// Registers a service instance with the specified service type.
        /// </summary>
        /// <param name="serviceType">The type that identifies the service.</param>
        /// <param name="serviceInstance">The instance of the service to associate with the type.</param>
        /// <exception cref="ArgumentNullException">
        /// Thrown if <paramref name="serviceType"/> or <paramref name="serviceInstance"/> is <c>null</c>.
        /// </exception>
        public void AddService(Type serviceType, object serviceInstance) => _services[serviceType] = serviceInstance;

        /// <summary>
        /// Retrieves a service object of the specified type.
        /// </summary>
        /// <param name="serviceType">An object that specifies the type of service object to retrieve.</param>
        /// <returns>
        /// A service object of type <paramref name="serviceType"/>, or <c>null</c> if no service of that type is registered.
        /// </returns>
        public object GetService(Type serviceType)
        {
            _ = _services.TryGetValue(serviceType, out var service);
            return service;
        }
    }
}
