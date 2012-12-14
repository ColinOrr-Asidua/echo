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
        public Predicate<HttpRequestMessage> Rule { get; set; }

        /// <summary>
        /// Gets the HttpResponseMessage to be returned.
        /// </summary>
        public HttpResponseMessage Message { get; private set; }

        /// <summary>
        /// Constructs a new Response with the specified properties.
        /// </summary>
        /// <param name="rule">The rule used to decide if the response should be returned.</param>
        /// <param name="message">The HttpResponseMessage to be returned.</param>
        public Response(Predicate<HttpRequestMessage> rule, HttpResponseMessage message)
        {
            if (message == null) throw new ArgumentNullException("message");
            
            Rule = rule;
            Message = message;
        }

        /// <summary>
        /// Converts an HttpResponseMessage to a Response.
        /// </summary>
        /// <param name="message">The HttpResponseMessage to be converted.</param>
        /// <returns>A new Response with no rule.</returns>
        public static implicit operator Response(HttpResponseMessage message)
        {
            return new Response(null, message);
        }
    }
}
