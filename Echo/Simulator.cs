using Echo.Providers;
using System;
using System.Collections.Generic;
using System.Web.Http.SelfHost;

namespace Echo
{
    /// <summary>
    /// An in-process HTTP server that collects requests and returns canned responses.
    /// </summary>
    public sealed class Simulator : IDisposable
    {
        #region Fields

        //  Dependencies
        private readonly HttpSelfHostServer server;

        //  Private fields used to back the properties
        private static SelfHostProvider hostingProvider;

        #endregion

        #region Properties
        /// <summary>
        /// Gets or sets the hosting provider used to create the web server.
        /// </summary>
        public static SelfHostProvider HostingProvider
        {
            get { return hostingProvider ?? new SelfHostProvider(); }
            set { hostingProvider = value; }
        }

        /// <summary>
        /// Gets a list of canned responses to be used when requests are received.
        /// </summary>
        /// <remarks>
        /// Responses are evaluated on order with the first matching response being returned.
        /// If no responses are matched, then the simulator will return a standard HTTP 200 response.
        /// </remarks>
        public IList<Response> Responses { get; private set; }

        #endregion

        #region Constructors
        /// <summary>
        /// Default constructor.
        /// </summary>
        private Simulator(HttpSelfHostServer server)
        {
            this.server = server;
            Responses = new List<Response>();
        }

        #endregion

        #region Methods
        /// <summary>
        /// Creates a simulator and starts it running on the specified port.
        /// </summary>
        /// <param name="port">The port number of the simulator's HTTP port.</param>
        /// <returns>The simulator that can be configured with responses, and whose requests can be examined.</returns>
        public static Simulator Start(int port)
        {
            //  Start the HTTP server
            var configuration = new HttpSelfHostConfiguration("http://localhost:" + port);
            var server = new HttpSelfHostServer(configuration);
            HostingProvider.Open(server);

            return new Simulator(server);
        }

        /// <summary>
        /// Performs application-defined tasks associated with freeing, releasing, or
        /// resetting unmanaged resources.
        /// </summary>
        public void Dispose()
        {
            server.Dispose();
        }

        #endregion
    }
}
