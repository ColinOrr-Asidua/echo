using Echo;
using Machine.Specifications;
using System;
using System.Net.Http;

namespace Unit.ResponseTests
{
    [Subject(typeof(Response))]
    class Context
    {
        public static Response response;
    }

    #region Constructor Tests

    class when_I_construct : Context
    {
        static Predicate<Request> rule;
        static Func<HttpResponseMessage> message;

        Establish context = () =>
        {
            rule = r => true;
            message = () => new HttpResponseMessage();
        };

        Because of = () =>
        {
            response = new Response(rule, message);
        };

        It should_set_the_Rule_property = () =>
        {
            response.Rule.ShouldBeTheSameAs(rule);
        };

        It should_set_the_Message_property = () =>
        {
            response.Message.ShouldBeTheSameAs(message);
        };
    }

    class when_I_call_construct_with_no_message
    {
        It throws_an_ArgumentNullException = () =>
        {
            var exception = Catch.Exception(() => new Response(null));
            exception.ShouldBeOfType<ArgumentNullException>();
        };
    }

    #endregion
}
