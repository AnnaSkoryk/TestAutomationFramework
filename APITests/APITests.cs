using NUnit.Framework.Internal;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium;
using NUnit.Framework.Legacy;

namespace APITests
{
    [TestFixture(Category = "SmokeTest")]
    public class SmokeTests
    {
        private HttpClient httpClient;
        private string apiUri;

        [SetUp]
        public void Setup()
        {
            httpClient = new HttpClient();
        }

        //1
        [Test]
        public void GetAllProductsList()
        {
            apiUri = "https://automationexercise.com/api/productsList";

            Task<HttpResponseMessage> response = httpClient.GetAsync(apiUri);
            HttpResponseMessage responseMessage = response.Result;
            Task<string> responseBody = httpClient.GetStringAsync(apiUri);

            ClassicAssert.AreEqual((int)responseMessage.StatusCode, 200);
            //httpClient.GetAsync(apiUri);
            //Assert.Pass();
        }

        [TearDown]
        public void TearDown()
        {
            httpClient.Dispose();
        }
    }
}
