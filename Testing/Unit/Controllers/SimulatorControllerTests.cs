using Echo;
using Echo.Controllers;
using Machine.Specifications;
using System;
using System.Net;
using System.Net.Http;

namespace Unit.Controllers.SimulatorControllerTests
{
    [Subject(typeof(SimulatorController))]
    class Context
    {
        public static Simulator simulator;
        public static SimulatorController controller;

        public Context()
        {
            simulator = new Simulator(null);
            controller = new SimulatorController(simulator);
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

    class when_I_call_Simulate_with_no_responses : Context
    {
        static HttpResponseMessage result;

        Because of = () =>
        {
            result = controller.Simulate();
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

    #endregion
}
