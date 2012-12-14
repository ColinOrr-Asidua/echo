using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;

namespace Echo
{
    /// <summary>
    /// Represents a HTTP request message.
    /// </summary>
    public class Request
    {
        #region Fields

        private readonly HttpRequestMessage message;

        #endregion

        /// <summary>
        /// Gets the contents of the HTTP message.
        /// </summary>
        public string Content { get; private set; }
        
        /// <summary>
        /// Gets the collection of HTTP request headers.
        /// </summary>
        public HttpRequestHeaders RequestHeaders { get { return message.Headers; } }

        /// <summary>
        /// Gets the HTTP content headers as defined in RFC 2616.
        /// </summary>
        public HttpContentHeaders ContentHeaders { get { return message.Content.Headers; } }
        
        /// <summary>
        /// Gets the HTTP method used by the HTTP request message.
        /// </summary>
        public HttpMethod Method { get { return message.Method; } }
        
        /// <summary>
        /// Gets a set of properties for the HTTP request.
        /// </summary>
        public IDictionary<string, object> Properties { get { return message.Properties; } }
        
        /// <summary>
        /// Gets the Uri used for the HTTP request.
        /// </summary>
        public Uri RequestUri { get { return message.RequestUri; } }
        
        /// <summary>
        /// Gets the HTTP message version.
        /// </summary>
        public Version Version { get { return message.Version; } }

        #region Constructors
        /// <summary>
        /// Constructs a new Request based on the specified message.
        /// </summary>
        /// <param name="message">The HttpRequestMessage to base this request upon.</param>
        public Request(HttpRequestMessage message)
        {
            if (message == null) throw new ArgumentNullException("message");

            this.message = message;

            if (message.Content != null)
            {
                Content = message.Content.ReadAsStringAsync().Result;
            }
        }

        #endregion

        #region Operators
        /// <summary>
        /// Converts an HttpRequestMessage to a Request.
        /// </summary>
        /// <param name="message">The HttpRequestMessage to be converted.</param>
        /// <returns>A new Request based on the message.</returns>
        public static implicit operator Request(HttpRequestMessage message)
        {
            return new Request(message);
        }

        #endregion
    }
}
