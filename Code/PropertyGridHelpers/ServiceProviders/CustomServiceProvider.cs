using System;
using System.Collections.Generic;

namespace PropertyGridHelpers.ServiceProviders
{

    /// <summary>
    /// Custom Service Provider
    /// </summary>
    /// <seealso cref="IServiceProvider" />
    public class CustomServiceProvider : IServiceProvider
    {
#if NET8_0_OR_GREATER
        private readonly Dictionary<Type, object> _services = [];
#else
        private readonly Dictionary<Type, object> _services = new Dictionary<Type, object>();
#endif

        /// <summary>
        /// Adds the service.
        /// </summary>
        /// <param name="serviceType">Type of the service.</param>
        /// <param name="serviceInstance">The service instance.</param>
        public void AddService(Type serviceType, object serviceInstance) => _services[serviceType] = serviceInstance;

        /// <summary>
        /// Gets the service.
        /// </summary>
        /// <param name="serviceType">Type of the service.</param>
        /// <returns></returns>
        public object GetService(Type serviceType)
        {
            _ = _services.TryGetValue(serviceType, out var service);
            return service;
        }
    }
}
