using Echo;
using Machine.Specifications;
using System;
using System.Net.Http;

namespace Unit.RequestTests
{
    [Subject(typeof(Request))]
    class Context
    {
        public static Request request;
    }

    #region Constructor Tests

    class when_I_call_construct : Context
    {
        static HttpRequestMessage message;

        Establish context = () =>
        {
            message = new HttpRequestMessage
            {
                Content    = new StringContent("Content"),
                Method     = HttpMethod.Delete,
                RequestUri = new Uri("http://localhost/"),
                Version    = new Version("1.2.3.4"),
            };
        };

        Because of = () =>
        {
            request = new Request(message);
        };

        It should_map_the_Content = () =>
        {
            request.Content.ShouldEqual("Content");
        };

        It should_map_the_Headers = () =>
        {
            request.Headers.ShouldBeTheSameAs(message.Headers);
        };

        It should_map_the_Method = () =>
        {
            request.Method.ShouldEqual(HttpMethod.Delete);
        };

        It should_map_the_Properties = () =>
        {
            request.Properties.ShouldBeTheSameAs(message.Properties);
        };

        It should_map_the_RequestUri = () =>
        {
            request.RequestUri.ToString().ShouldEqual("http://localhost/");
        };

        It should_map_the_Version = () =>
        {
            request.Version.ToString().ShouldEqual("1.2.3.4");
        };
    }

    class when_I_call_construct_with_no_message : Context
	{
        It throws_an_ArgumentNullException = () =>
        {
            var exception = Catch.Exception(() => new Request(null));
            exception.ShouldBeOfType<ArgumentNullException>();
        };
	}

    #endregion

    #region operator Request(HttpRequestMessage message) Tests

    class when_I_convert_a_HttpRequestMessage : Context
    {
        static HttpRequestMessage message;

        Establish context = () =>
        {
            message = new HttpRequestMessage
            {
                Method = HttpMethod.Delete,
            };
        };

        Because of = () =>
        {
            request = message;
        };

        It should_return_a_new_Request_based_on_the_message = () =>
        {
            request.Method.ShouldEqual(HttpMethod.Delete);
        };
    }

    #endregion
}
