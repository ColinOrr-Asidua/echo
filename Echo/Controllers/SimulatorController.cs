using System;
using System.Net.Http;
using System.Web.Http;

namespace Echo.Controllers
{
    /// <summary>
    /// The API controller that simulates reponses to web requests.
    /// </summary>
    public class SimulatorController : ApiController
    {
        #region Fields

        //  Dependencies
        private readonly Simulator simulator;

        #endregion

        #region Constructors
        /// <summary>
        /// Constructs a new SimulatorController with the specified dependencies.
        /// </summary>
        /// <param name="simulator">The simulator used to record requests and find responses.</param>
        public SimulatorController(Simulator simulator)
        {
            if (simulator == null) throw new ArgumentNullException("simulator");

            this.simulator = simulator;
        }

        #endregion

        #region Methods
        /// <summary>
        /// Simulates a response to a web request.
        /// </summary>
        /// <returns>The simulated response message.</returns>
        [HttpGet, HttpPut, HttpPost, HttpDelete]
        public HttpResponseMessage Simulate()
        {
            foreach (var response in simulator.Responses)
            {
                if (response.Rule == null || response.Rule(this.Request))
                {
                    return response.Message;
                }
            }

            return new HttpResponseMessage();
        }

        #endregion
    }
}
