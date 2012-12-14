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
        static HttpResponseMessage message;

        Establish context = () =>
        {
            rule = r => true;
            message = new HttpResponseMessage();
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
            var exception = Catch.Exception(() => new Response(r => true, null));
            exception.ShouldBeOfType<ArgumentNullException>();
        };
    }

    #endregion

    #region operator Response(HttpResponseMessage message) Tests

    class when_I_convert_a_HttpResponseMessage : Context
    {
        static HttpResponseMessage message;

        Establish context = () =>
        {
            message = new HttpResponseMessage();
        };

        Because of = () =>
        {
            response = message;
        };

        It should_assign_the_message_to_the_Message_property = () =>
        {
            response.Message.ShouldBeTheSameAs(message);
        };

        It should_set_the_Rule_property_to_null = () =>
        {
            response.Rule.ShouldBeNull();
        };
    }

    #endregion
}
