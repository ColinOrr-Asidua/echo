using System.Web.Http.SelfHost;

namespace Echo.Providers
{
    /// <summary>
    /// Provides a thin wrapper over the standard <see cref="HttpSelfHostServer"/> functionality.
    /// </summary>
    public class SelfHostProvider
    {
        /// <summary>
        /// Opens the specified server.
        /// </summary>
        /// <param name="server">The HttpSelfHostServer server to open.</param>
        public virtual void Open(HttpSelfHostServer server)
        {
            server.OpenAsync().Wait();
        }
    }
}
