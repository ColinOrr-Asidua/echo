using Echo;
using Machine.Specifications;
using System.Net.Http;

namespace Unit.ResponseTests
{
    [Subject(typeof(Response))]
    class Context { }

    #region operator Response(HttpResponseMessage message) Tests

    class when_I_convert_a_HttpResponseMessage : Context
    {
        static HttpResponseMessage message;
        static Response result;

        Establish context = () =>
        {
            message = new HttpResponseMessage();
        };

        Because of = () =>
        {
            result = message;
        };

        It should_assign_the_message_to_the_Message_property = () =>
        {
            result.Message.ShouldBeTheSameAs(message);
        };

        It should_set_the_Rule_property_to_null = () =>
        {
            result.Rule.ShouldBeNull();
        };
    }

    #endregion
}
