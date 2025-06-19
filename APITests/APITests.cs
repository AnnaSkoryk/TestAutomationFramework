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
            Products jsonResponse = method.GetJsonResponse<Products>(apiResource, Method.Get, out response);
            ClassicAssert.AreEqual((int)HttpStatusCode.OK, jsonResponse.responseCode);  
        }

        [Test]
        [AllureId(2)]
        public void TryPostToAllProductsList()
        {
            apiResource = "api/productsList";
            string expectedText = "This request method is not supported.";

            RestResponse response;
            Message jsonResponse = method.GetJsonResponse<Message>(apiResource, Method.Post, out response);

            Assert.Multiple(() =>
            {
                ClassicAssert.AreEqual((int)HttpStatusCode.MethodNotAllowed, jsonResponse.responseCode);
                ClassicAssert.AreEqual(expectedText, jsonResponse.message);
            });
        }

        [Test]
        [AllureId(3)]
        public void GetAllBrandsList()
        {
            apiResource = "api/brandsList";

            RestResponse response;
            Brands jsonResponse = method.GetJsonResponse<Brands>(apiResource, Method.Get, out response);
            ClassicAssert.AreEqual((int)HttpStatusCode.OK, jsonResponse.responseCode);
        }

        [Test]
        [AllureId(4)]
        public void TryPutToAllBrandsList()
        {
            apiResource = "api/brandsList";
            string expectedText = "This request method is not supported.";

            RestResponse response;
            Message jsonResponse = method.GetJsonResponse<Message>(apiResource, Method.Put, out response);

            Assert.Multiple(() =>
            {
                ClassicAssert.AreEqual((int)HttpStatusCode.MethodNotAllowed, jsonResponse.responseCode);
                ClassicAssert.AreEqual(expectedText, jsonResponse.message);
            });
        }

        [Test]
        [AllureId(5)]
        public void PostToSearchProductWithParam()
        {
            apiResource = "api/searchProduct";
            Dictionary<string, string> param = new Dictionary<string, string>();
            param.Add("search_product", "top");

            RestResponse response;
            Products jsonResponse = method.GetJsonResponse<Products>(apiResource, Method.Post, out response, sendWithParams: true, param);
            ClassicAssert.AreEqual((int)HttpStatusCode.OK, jsonResponse.responseCode);
        }
        
        [Test]
        [AllureId(6)]
        public void TryPostToSearchWithoutParam()
        {
            apiResource = "api/searchProduct";
            string expectedText = "Bad request, search_product parameter is missing in POST request.";
            
            RestResponse response;
            Message jsonResponse = method.GetJsonResponse<Message>(apiResource, Method.Post, out response);

            Assert.Multiple(() =>
            {
                ClassicAssert.AreEqual((int)HttpStatusCode.BadRequest, jsonResponse.responseCode);
                ClassicAssert.AreEqual(expectedText, jsonResponse.message);
            });
        }

        [Test]
        [AllureId(7)]
        public void PostToLoginWithValidParams()
        {
            apiResource = "api/verifyLogin";
            Dictionary<string, string> param = new Dictionary<string, string>();
            param.Add("email", "j.k@e.com");
            param.Add("password", "SecureP@ss123");
            string expectedText = "User exists!";

            RestResponse response;
            Message jsonResponse = method.GetJsonResponse<Message>(apiResource, Method.Post, out response, sendWithParams: true, param);

            Assert.Multiple(() =>
            {
                ClassicAssert.AreEqual((int)HttpStatusCode.OK, jsonResponse.responseCode);
                ClassicAssert.AreEqual(expectedText, jsonResponse.message);
            });
        }

        [Test]
        [AllureId(8)]
        public void TryPostToLoginWithoutEmailParam()
        {
            apiResource = "api/verifyLogin"; 
            Dictionary<string, string> param = new Dictionary<string, string>();
            param.Add("password", "SecureP@ss123");
            string expectedText = "Bad request, email or password parameter is missing in POST request.";

            RestResponse response;
            Message jsonResponse = method.GetJsonResponse<Message>(apiResource, Method.Post, out response, sendWithParams: true, param);

            Assert.Multiple(() =>
            {
                ClassicAssert.AreEqual((int)HttpStatusCode.BadRequest, jsonResponse.responseCode);
                ClassicAssert.AreEqual(expectedText, jsonResponse.message);
            });
        }

        [Test]
        [AllureId(9)]
        public void TryDeleteToVerifyLogins()
        {
            apiResource = "api/verifyLogin";
            string expectedText = "This request method is not supported.";

            RestResponse response;
            Message jsonResponse = method.GetJsonResponse<Message>(apiResource, Method.Delete, out response);

            Assert.Multiple(() =>
            {
                ClassicAssert.AreEqual((int)HttpStatusCode.MethodNotAllowed, jsonResponse.responseCode);
                ClassicAssert.AreEqual(expectedText, jsonResponse.message);
            });
        }

        [Test]
        [AllureId(10)]
        public void TryPostToLoginWithInvalidParams()
        {
            apiResource = "api/verifyLogin";
            Dictionary<string, string> param = new Dictionary<string, string>();
            param.Add("email", "j.m@e.com");
            param.Add("password", "SecureP@ss");
            string expectedText = "User not found!";

            RestResponse response;
            Message jsonResponse = method.GetJsonResponse<Message>(apiResource, Method.Post, out response, sendWithParams: true, param);

            Assert.Multiple(() =>
            {
                ClassicAssert.AreEqual((int)HttpStatusCode.NotFound, jsonResponse.responseCode);
                ClassicAssert.AreEqual(expectedText, jsonResponse.message);
            });
        }

        [Test]
        [AllureId(11)]
        [AllureTag("Can be FAILED if user not deleted in prev. RUN")]
        public void PostToCreateUserAccount()
        {
            apiResource = "api/createAccount";
            Dictionary<string, string> param = new Dictionary<string, string>();
            param.Add("name", "John");
            param.Add("email", "j.d@e.com");
            param.Add("password", "SecureP@ss123");
            param.Add("title", "Mr");
            param.Add("birth_date", "16");
            param.Add("birth_month", "1");
            param.Add("birth_year", "1999");
            param.Add("firstname", "John");
            param.Add("lastname", "Doe");
            param.Add("company", "Doe Industries");
            param.Add("address1", "123 Main Street");
            param.Add("address2", "Suite 456");
            param.Add("country", "United States");
            param.Add("zipcode", "California");
            param.Add("state", "California");
            param.Add("city", "Los Angeles");
            param.Add("mobile_number", "+1-555-123-4567");

            
            string expectedText = "User created!";

            RestResponse response;
            Message jsonResponse = method.GetJsonResponse<Message>(apiResource, Method.Post, out response, sendWithParams: true, param);

            Assert.Multiple(() =>
            {
                ClassicAssert.AreEqual((int)HttpStatusCode.Created, jsonResponse.responseCode);
                ClassicAssert.AreEqual(expectedText, jsonResponse.message);
            });
        }

        [Test]
        [AllureId(12)]
        public void PutToUpdateUserAccount()
        {
            apiResource = "api/updateAccount";
            Dictionary<string, string> param = new Dictionary<string, string>();
            param.Add("name", "J");
            param.Add("email", "j.d@e.com");
            param.Add("password", "SecureP@ss123");
            param.Add("title", "Mr");
            param.Add("birth_date", "16");
            param.Add("birth_month", "1");
            param.Add("birth_year", "1999");
            param.Add("firstname", "John");
            param.Add("lastname", "Doe");
            param.Add("company", "Doe Industries");
            param.Add("address1", "123 Main Street");
            param.Add("address2", "Suite 456");
            param.Add("country", "United States");
            param.Add("zipcode", "California");
            param.Add("state", "California");
            param.Add("city", "Los Angeles");
            param.Add("mobile_number", "+1-555-123-4567");


            string expectedText = "User updated!";

            RestResponse response;
            Message jsonResponse = method.GetJsonResponse<Message>(apiResource, Method.Put, out response, sendWithParams: true, param);

            Assert.Multiple(() =>
            {
                ClassicAssert.AreEqual((int)HttpStatusCode.OK, jsonResponse.responseCode);
                ClassicAssert.AreEqual(expectedText, jsonResponse.message);
            });
        }

        [Test]
        [AllureId(13)]
        public void DeleteCreatedAccount()
        {
            apiResource = "api/deleteAccount";
            Dictionary<string, string> param = new Dictionary<string, string>();
            param.Add("email", "j.d@e.com");
            param.Add("password", "SecureP@ss123");

            string expectedText = "Account deleted!";

            RestResponse response;
            Message jsonResponse = method.GetJsonResponse<Message>(apiResource, Method.Delete, out response, sendWithParams: true, param);

            Assert.Multiple(() =>
            {
                ClassicAssert.AreEqual((int)HttpStatusCode.OK, jsonResponse.responseCode);
                ClassicAssert.AreEqual(expectedText, jsonResponse.message);
            });
        }

        [TearDown]
        public void TearDown()
        {
            restClient.Dispose();
        }
    }
}
