using Echo;
using NUnit.Framework;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;

namespace Acceptance
{
    [TestFixture]
    class Verify_Requests
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
        public void Single_Request()
        {
            //  Make a web request to the simulator
            var data = Encoding.Default.GetBytes("Hello World");
            var request = HttpWebRequest.Create("http://localhost:3000/system/path/");
            request.Method = "POST";
            request.ContentType = "text/plain";
            request.ContentLength = data.Length;
            request.GetRequestStream().Write(data, 0, data.Length);

            request.GetResponse().Dispose();
            
            //  Verify that the request was received
            var captured = simulator.Requests.Single();
            Assert.AreEqual("Hello World", captured.Content);
            Assert.AreEqual("http://localhost:3000/system/path/", captured.RequestUri.ToString());
            Assert.AreEqual(HttpMethod.Post, captured.Method);
            Assert.AreEqual("text/plain", captured.ContentHeaders.ContentType.MediaType);
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
