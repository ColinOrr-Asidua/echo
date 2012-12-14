using Echo;
using Echo.Controllers;
using Machine.Specifications;
using System;
using System.Linq;
using System.Net;
using System.Net.Http;

namespace Unit.Controllers.SimulatorControllerTests
{
    [Subject(typeof(SimulatorController))]
    class Context
    {
        public static Simulator simulator;
        public static SimulatorController controller;
        public static HttpRequestMessage request;

        public Context()
        {
            simulator = new Simulator(null);
            controller = new SimulatorController(simulator);
            controller.Request = request = new HttpRequestMessage(HttpMethod.Delete, "http://localhost/");
        }
    }

    #region Constructor Tests

    class when_I_call_construct_with_no_simulator : Context
	{
        It throws_an_an_ArgumentNullException = () =>
        {
            var exception = Catch.Exception(() => new SimulatorController(null));
            exception.ShouldBeOfType<ArgumentNullException>();
        };
	}

    #endregion

    #region .Simulate() Tests

    class when_I_call_Simulate : Context
    {
        static HttpResponseMessage result;

        Because of = () =>
        {
            result = controller.Simulate();
        };

        It should_add_the_request_to_the_simulator_Requests_list = () =>
        {
            simulator.Requests.Single().RequestUri.ToString().ShouldEqual("http://localhost/");
        };

        It should_return_an_HttpResponseMessage_with_an_OK_status = () =>
        {
            result.StatusCode.ShouldEqual(HttpStatusCode.OK);
        };
    }

    class when_I_call_Simulate_with_a_canned_response : Context
    {
        static HttpResponseMessage response;
        static HttpResponseMessage result;

        Establish context = () =>
        {
            response = new HttpResponseMessage();
            simulator.Responses.Add(response);
        };

        Because of = () =>
        {
            result = controller.Simulate();
        };

        It should_return_the_canned_response = () =>
        {
            result.ShouldBeTheSameAs(response);
        };
    }

    class when_I_call_Simulate_with_multiple_canned_responses : Context
    {
        static HttpResponseMessage responseA;
        static HttpResponseMessage responseB;
        static HttpResponseMessage responseC;
        static HttpResponseMessage result;

        Establish context = () =>
        {
            responseA = new HttpResponseMessage();
            responseB = new HttpResponseMessage();
            responseC = new HttpResponseMessage();

            simulator.Responses.Add(responseA);
            simulator.Responses.Add(responseB);
            simulator.Responses.Add(responseC);
        };

        Because of = () =>
        {
            result = controller.Simulate();
        };

        It should_return_the_first_canned_response = () =>
        {
            result.ShouldBeTheSameAs(responseA);
        };
    }

    class when_I_call_Simulate_with_responses_containing_rules : Context
    {
        static HttpResponseMessage responseA;
        static HttpResponseMessage responseB;
        static HttpResponseMessage responseC;
        static HttpResponseMessage result;

        Establish context = () =>
        {
            controller.Request = new HttpRequestMessage();

            responseA = new HttpResponseMessage();
            responseB = new HttpResponseMessage();
            responseC = new HttpResponseMessage();

            simulator.Responses.Add(new Response(r => false, responseA));
            simulator.Responses.Add(new Response(r => true, responseB));
            simulator.Responses.Add(new Response(r => true, responseC));
        };

        Because of = () =>
        {
            result = controller.Simulate();
        };

        It should_return_the_first_response_with_a_valid_rule = () =>
        {
            result.ShouldBeTheSameAs(responseB);
        };
    }

    #endregion
}
