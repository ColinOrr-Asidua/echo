using System;
using System.Net.Http;

namespace Echo
{
    /// <summary>
    /// A canned-response to an HTTP request.
    /// </summary>
    public class Response
    {
        /// <summary>
        /// Gets or sets the rule used to decide if the response should be returned.
        /// </summary>
        /// <remarks>If no rule is defined, then the simulator will return this response for any request.</remarks>
        public Predicate<Request> Rule { get; set; }

        /// <summary>
        /// Gets the HttpResponseMessage to be returned.
        /// </summary>
        public Func<HttpResponseMessage> Message { get; private set; }

        #region Constructors
        /// <summary>
        /// Constructs a new Response with the specified properties.
        /// </summary>
        /// <param name="message">A function that builds the HttpResponseMessage to be returned.</param>
        public Response(Func<HttpResponseMessage> message) : this(null, message)
        {
        }

        /// <summary>
        /// Constructs a new Response with the specified properties.
        /// </summary>
        /// <param name="rule">The rule used to decide if the response should be returned.</param>
        /// <param name="message">A function that builds the HttpResponseMessage to be returned.</param>
        public Response(Predicate<Request> rule, Func<HttpResponseMessage> message)
        {
            if (message == null) throw new ArgumentNullException("message");

            Rule = rule;
            Message = message;
        }

        #endregion
    }
}
