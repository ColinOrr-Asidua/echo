using Echo;
using NUnit.Framework;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Net.Http;

namespace Acceptance
{
    [TestFixture]
    class Simulate_Responses
    {
        #region Setup

        Simulator simulator;

        [SetUp]
        public void SetUp()
        {
            //  Start the simulator on port 3000
            simulator = Simulator.Start(3000);
        }

        #endregion

        [Test]
        public void Single_Response()
        {
            //  Configure a single response
            simulator.Responses.Add(
                new Response(() => new HttpResponseMessage { Content = new StringContent("Hello World") })
            );

            //  Make a web request to the simulator
            var request = HttpWebRequest.Create("http://localhost:3000");
            var response = request.GetResponse();

            //  Check the response
            using (var reader = new StreamReader(response.GetResponseStream()))
            {
                Assert.AreEqual("Hello World", reader.ReadToEnd());
            }
        }

        [Test]
        public void Multiple_Responses()
        {
            //  Configure multiple responses
            var responses = new List<Response>
            {
                new Response(
                    rule: r => r.Method == HttpMethod.Get,
                    message: () => new HttpResponseMessage { Content = new StringContent("Response A") }
                ),
                new Response(
                    rule: r => r.Method == HttpMethod.Delete,
                    message: () => new HttpResponseMessage { Content = new StringContent("Response B") }
                ),
                new Response(
                    rule: r => r.Method == HttpMethod.Post,
                    message: () => new HttpResponseMessage { Content = new StringContent("Response C") }
                ),
            };

            responses.ForEach(r => simulator.Responses.Add(r));

            //  Make a web request to the simulator
            var request = HttpWebRequest.Create("http://localhost:3000");
            request.Method = "DELETE";
            var response = request.GetResponse();

            //  Check the response
            using (var reader = new StreamReader(response.GetResponseStream()))
            {
                Assert.AreEqual("Response B", reader.ReadToEnd());
            }
        }

        [Test]
        public void Reuse_Response()
        {
            //  Configure a single response
            simulator.Responses.Add(
                new Response(() => new HttpResponseMessage { Content = new StringContent("Hello World") })
            );

            //  Make two identical web requests
            var requestA = HttpWebRequest.Create("http://localhost:3000");
            requestA.GetResponse().GetResponseStream().Dispose();

            var requestB = HttpWebRequest.Create("http://localhost:3000");
            requestB.GetResponse().GetResponseStream().Dispose();
        }

        [Test]
        public void No_Responses()
        {
            //  Make a web request to the simulator
            var request = HttpWebRequest.Create("http://localhost:3000");
            var response = (HttpWebResponse)request.GetResponse();

            //  Check the response
            Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);
        }

        #region Tear Down

        [TearDown]
        public void TearDown()
        {
            simulator.Dispose();
        }

        #endregion
    }
}
