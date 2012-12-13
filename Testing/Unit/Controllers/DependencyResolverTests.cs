using Echo;
using Echo.Controllers;
using Machine.Specifications;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http.Dependencies;

namespace Unit.Controllers.DependencyResolverTests
{
    [Subject(typeof(DependencyResolver))]
    class Context
    {
        public static Simulator simulator;
        public static DependencyResolver resolver;

        public Context()
        {
            simulator = new Simulator(null);
            resolver = new DependencyResolver(simulator);
        }
    }

    #region Constructor Tests

    class when_I_call_construct_with_no_simulator : Context
	{
        It throws_an_ArgumentNullException = () =>
        {
            var exception = Catch.Exception(() => new DependencyResolver(null));
            exception.ShouldBeOfType<ArgumentNullException>();
        };
	}

    #endregion

    #region .BeginScope() Tests

    class when_I_call_BeginScope : Context
    {
        static IDependencyScope result;

        Because of = () =>
        {
            result = resolver.BeginScope();
        };

        It should_return_the_current_instance_of_the_DependencyResolver = () =>
        {
            result.ShouldBeTheSameAs(resolver);
        };
    }

    #endregion

    #region .GetService(Type serviceType) Tests

    class when_I_call_GetService_for_the_SimulatorController : Context
    {
        static object result;

        Because of = () =>
        {
            result = resolver.GetService(typeof(SimulatorController));
        };

        It returns_a_new_instance_of_the_SimulatorController = () =>
        {
            result.ShouldBeOfType<SimulatorController>();
        };
    }

    class when_I_call_GetService_for_any_other_type : Context
    {
        static object result;

        Because of = () =>
        {
            result = resolver.GetService(typeof(string));
        };

        It returns_null = () =>
        {
            result.ShouldBeNull();
        };
    }

    #endregion

    #region .GetServices(Type serviceType) Tests

    class when_I_call_GetServices_for_the_SimulatorController : Context
    {
        static IEnumerable<object> results;

        Because of = () =>
        {
            results = resolver.GetServices(typeof(SimulatorController));
        };

        It returns_a_single_new_instance_of_the_SimulatorController = () =>
        {
            results.Single().ShouldBeOfType<SimulatorController>();
        };
    }


    #endregion
}
