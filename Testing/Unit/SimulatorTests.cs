using Echo;
using Echo.Providers;
using Machine.Specifications;
using NSubstitute;
using System.Web.Http.SelfHost;

namespace Unit.SimulatorTests
{
    [Subject(typeof(Simulator))]
    class Context
    {
        public static Simulator simulator;

        public Context()
        {
            Simulator.HostingProvider = Substitute.For<SelfHostProvider>();
            simulator = Simulator.Start(8080);
        }
    }

    #region Constructor Tests

    class when_I_construct : Context
    {
        It should_initialize_the_Responses_list = () =>
        {
            simulator.Responses.ShouldBeEmpty();
        };
    }

    #endregion

    #region .HostingProvider Tests

    class when_I_set_HostingProvider_to_null : Context
    {
        Because of = () =>
        {
            Simulator.HostingProvider = null;
        };

        It should_return_a_new_SelfHostProvider_by_default = () =>
        {
            Simulator.HostingProvider.ShouldBeOfType<SelfHostProvider>();
        };
    }

    #endregion

    #region .Start(int port) Tests

    class when_I_call_Start : Context
    {
        static HttpSelfHostServer server;

        Establish context = () =>
        {
            Simulator.HostingProvider.Open(Arg.Do<HttpSelfHostServer>(s => server = s));
        };

        Because of = () =>
        {
            simulator = Simulator.Start(12345);
        };

        It should_return_a_new_simulator = () =>
        {
            simulator.ShouldNotBeNull();
        };

        It should_open_a_new_HttpSelfHostServer = () =>
        {
            server.ShouldNotBeNull();
        };

        It should_configure_the_server_to_use_the_specified_port = () =>
        {
            var configuration = (HttpSelfHostConfiguration)server.Configuration;
            configuration.BaseAddress.ToString().ShouldEqual("http://localhost:12345/");
        };
    }

    #endregion
}
