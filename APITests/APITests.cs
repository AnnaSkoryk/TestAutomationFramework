using NUnit.Framework.Internal;
using RestSharp;
using Allure.NUnit;
using Allure.NUnit.Attributes;
using Newtonsoft.Json;
using APITests.Models;
using NUnit.Framework.Legacy;
using System.Net;

namespace APITests
{
    [AllureNUnit]
    [AllureSuite("API Smoke Test")]
    [TestFixture(Category = "API SmokeTest")]
    public class SmokeTests
    {
        private RestClient restClient;
        private string apiResource;
        private TestMethods method;

        [SetUp]
        public void Setup()
        {
            restClient = new RestClient(new RestClientOptions
            {
                BaseUrl = new Uri("https://automationexercise.com")
            });
            method = new TestMethods(restClient);
        }

        [Test]
        [AllureId(1)]
        public void GetAllProductsList()
        {
            apiResource = "api/productsList";

            RestResponse response; 
            method.SendRequest(apiResource, Method.Get, HttpStatusCode.OK, out response);
            Products jsonResponse;
            method.TryDeserializeResponseToJSON<Products>(response, out jsonResponse);
        }

        [Test]
        [AllureId(2)]
        public void TryPostToAllProductsList()
        {
            apiResource = "api/productsList";
            string expectedText = "This request method is not supported.";

            RestResponse response;
            method.SendRequest(apiResource, Method.Post, HttpStatusCode.MethodNotAllowed, out response);
            ClassicAssert.IsTrue(response.Content.Contains(expectedText));
        }

        [Test]
        [AllureId(3)]
        public void GetAllBrandsList()
        {
            apiResource = "api/brandsList";

            RestResponse response;
            method.SendRequest(apiResource, Method.Get, HttpStatusCode.OK, out response);
            Brands jsonResponse;
            method.TryDeserializeResponseToJSON<Brands>(response, out jsonResponse);
        }

        [Test]
        [AllureId(4)]
        public void PutToAllBrandsList()
        {
            apiResource = "api/brandsList";
            string expectedText = "This request method is not supported.";

            RestResponse response;
            method.SendRequest(apiResource, Method.Put, HttpStatusCode.MethodNotAllowed, out response);
            ClassicAssert.IsTrue(response.Content.Contains(expectedText));
        }

        //[Test]
        //[AllureId(5)]
        //public void PostToSearchProduct()
        //{
        //    apiResource = "api/searchProduct";

        //    RestResponse response;
        //    method.SendRequest(apiResource, Method.Get, HttpStatusCode.OK, out response);
            
        //    Products jsonResponse;
        //    method.TryDeserializeResponseToJSON<Products>(response, out jsonResponse);
        //}

        [TearDown]
        public void TearDown()
        {
            restClient.Dispose();
        }
    }
}
