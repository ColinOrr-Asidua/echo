using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Web.Http.Dependencies;

namespace Echo.Controllers
{
    /// <summary>
    /// Represents a dependency injection container.
    /// </summary>
    [SuppressMessage("Microsoft.Design", "CA1063:ImplementIDisposableCorrectly")]
    public class DependencyResolver : IDependencyResolver
    {
        #region Fields

        //  Dependencies
        private readonly Simulator simulator;

        #endregion

        #region Constructors
        /// <summary>
        /// Constructs new DependencyResolver with the specified dependencies.
        /// </summary>
        /// <param name="simulator">The simulator to be used when constructing services.</param>
        public DependencyResolver(Simulator simulator)
        {
            if (simulator == null) throw new ArgumentNullException("simulator");

            this.simulator = simulator;
        }

        #endregion

        #region Methods
        /// <summary>
        /// Starts a resolution scope.
        /// </summary>
        /// <returns>The dependency scope.</returns>
        public IDependencyScope BeginScope()
        {
            return this;
        }

        /// <summary>
        /// Retrieves a service from the scope.
        /// </summary>
        /// <param name="serviceType">The service to be retrieved.</param>
        /// <returns>The retrieved service.</returns>
        public object GetService(Type serviceType)
        {
            if (serviceType == typeof(SimulatorController))
            {
                return new SimulatorController(simulator);
            }

            return null;
        }

        /// <summary>
        /// Retrieves a collection of services from the scope.
        /// </summary>
        /// <param name="serviceType">The collection of services to be retrieved.</param>
        /// <returns>The retrieved collection of services.</returns>
        public IEnumerable<object> GetServices(Type serviceType)
        {
            return new[] { GetService(serviceType) };
        }

        /// <summary>
        /// Performs application-defined tasks associated with freeing, releasing, or
        /// resetting unmanaged resources.
        /// </summary>
        [SuppressMessage("Microsoft.Design", "CA1063:ImplementIDisposableCorrectly")]
        public void Dispose()
        {
        }

        #endregion
    }
}
