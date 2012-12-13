using Echo;
using NUnit.Framework;
using System.IO;
using System.Net;
using System.Net.Http;

namespace Acceptance
{
    [TestFixture]
    class Simulate_Responses
    {
        Simulator simulator;

        [SetUp]
        public void SetUp()
        {
            //  Start the simulator on port 8080
            simulator = Simulator.Start(8080);
        }

        [Test]
        public void Single_Response()
        {
            //  Configure a single response
            simulator.Responses.Add(
                new HttpResponseMessage { Content = new StringContent("Hello World") }
            );

            //  Make a web request to the simulator
            var request = HttpWebRequest.Create("http://localhost:8080");
            var response = request.GetResponse();

            //  Check the response
            using (var reader = new StreamReader(response.GetResponseStream()))
            {
                Assert.AreEqual("Hello World", reader.ReadToEnd());
            }
        }

        [TearDown]
        public void TearDown()
        {
            simulator.Dispose();
        }
    }
}
