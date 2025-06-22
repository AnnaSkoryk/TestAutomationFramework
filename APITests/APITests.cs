using NUnit.Framework.Internal;
using RestSharp;
using Allure.NUnit;
using Allure.NUnit.Attributes;
using APITests.Models;
using NUnit.Framework.Legacy;
using System.Net;
using Newtonsoft.Json.Linq;

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

        [Test, TestCaseSource(typeof(TestDataLoader), nameof(TestDataLoader.LoadTestData), new object[] { "TestData/MethodNotSupported.json" })]
        [AllureId(2)]
        public void TryPostToAllProductsList(JToken testData)
        {
            apiResource = "api/productsList";
            string expectedText = testData["errorText"]?.ToString();

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

        [Test, TestCaseSource(typeof(TestDataLoader), nameof(TestDataLoader.LoadTestData), new object[] { "TestData/MethodNotSupported.json" })]
        [AllureId(4)]
        public void TryPutToAllBrandsList(JToken testData)
        {
            apiResource = "api/brandsList";
            string expectedText = testData["errorText"]?.ToString();

            RestResponse response;
            Message jsonResponse = method.GetJsonResponse<Message>(apiResource, Method.Put, out response);

            Assert.Multiple(() =>
            {
                ClassicAssert.AreEqual((int)HttpStatusCode.MethodNotAllowed, jsonResponse.responseCode);
                ClassicAssert.AreEqual(expectedText, jsonResponse.message);
            });
        }

        [Test, TestCaseSource(typeof(TestDataLoader), nameof(TestDataLoader.LoadTestData), new object[] { "TestData/SearchProduct.json" })]
        [AllureId(5)]
        public void PostToSearchProductWithParam(JToken testData)
        {
            apiResource = "api/searchProduct";
            Dictionary<string, string> param = new Dictionary<string, string>
            {
                { "search_product", testData["searchProduct"]?.ToString() }
            };

            RestResponse response;
            Products jsonResponse = method.GetJsonResponse<Products>(apiResource, Method.Post, out response, sendWithParams: true, param);
            ClassicAssert.AreEqual((int)HttpStatusCode.OK, jsonResponse.responseCode);
        }

        [Test, TestCaseSource(typeof(TestDataLoader), nameof(TestDataLoader.LoadTestData), new object[] { "TestData/BadRequest.json" })]
        [AllureId(6)]
        public void TryPostToSearchWithoutParam(JToken testData)
        {
            apiResource = "api/searchProduct";
            string expectedText = testData["errorText"]?.ToString();
            
            RestResponse response;
            Message jsonResponse = method.GetJsonResponse<Message>(apiResource, Method.Post, out response);

            Assert.Multiple(() =>
            {
                ClassicAssert.AreEqual((int)HttpStatusCode.BadRequest, jsonResponse.responseCode);
                ClassicAssert.AreEqual(expectedText, jsonResponse.message);
            });
        }

        [Test, TestCaseSource(typeof(TestDataLoader), nameof(TestDataLoader.LoadTestData), new object[] { "TestData/Login.json" })]
        [AllureId(7)]
        public void PostToLoginWithValidParams(JToken testData)
        {
            apiResource = "api/verifyLogin";
            Dictionary<string, string> param = new Dictionary<string, string>
            {
                { "email", testData["email"]?.ToString() },
                { "password", testData["password"]?.ToString() }
            };
            string expectedText = testData["message"]?.ToString();

            RestResponse response;
            Message jsonResponse = method.GetJsonResponse<Message>(apiResource, Method.Post, out response, sendWithParams: true, param);

            Assert.Multiple(() =>
            {
                ClassicAssert.AreEqual((int)HttpStatusCode.OK, jsonResponse.responseCode);
                ClassicAssert.AreEqual(expectedText, jsonResponse.message);
            });
        }

        [Test, TestCaseSource(typeof(TestDataLoader), nameof(TestDataLoader.LoadTestData), new object[] { "TestData/Login.json" })]
        [AllureId(8)]
        public void TryPostToLoginWithoutEmailParam(JToken testData)
        {
            apiResource = "api/verifyLogin"; 
            Dictionary<string, string> param = new Dictionary<string, string>
            {
                { "password", testData["paramVal2"]?.ToString() }
            };
            string expectedText = testData["badRequestText"]?.ToString();

            RestResponse response;
            Message jsonResponse = method.GetJsonResponse<Message>(apiResource, Method.Post, out response, sendWithParams: true, param);

            Assert.Multiple(() =>
            {
                ClassicAssert.AreEqual((int)HttpStatusCode.BadRequest, jsonResponse.responseCode);
                ClassicAssert.AreEqual(expectedText, jsonResponse.message);
            });
        }

        [Test, TestCaseSource(typeof(TestDataLoader), nameof(TestDataLoader.LoadTestData), new object[] { "TestData/Login.json" })]
        [AllureId(9)]
        public void TryDeleteToVerifyLogins(JToken testData)
        {
            apiResource = "api/verifyLogin";
            string expectedText = testData["notSupportedText"]?.ToString();

            RestResponse response;
            Message jsonResponse = method.GetJsonResponse<Message>(apiResource, Method.Delete, out response);

            Assert.Multiple(() =>
            {
                ClassicAssert.AreEqual((int)HttpStatusCode.MethodNotAllowed, jsonResponse.responseCode);
                ClassicAssert.AreEqual(expectedText, jsonResponse.message);
            });
        }

        [Test, TestCaseSource(typeof(TestDataLoader), nameof(TestDataLoader.LoadTestData), new object[] { "TestData/LoginWithNotValidParams.json" })]
        [AllureId(10)]
        public void TryPostToLoginWithInvalidParams(JToken testData)
        {
            apiResource = "api/verifyLogin";
            Dictionary<string, string> param = new Dictionary<string, string>
            {
                { "email", testData["email"]?.ToString() },
                { "password", testData["password"]?.ToString() }
            };
            string expectedText = testData["message"]?.ToString();

            RestResponse response;
            Message jsonResponse = method.GetJsonResponse<Message>(apiResource, Method.Post, out response, sendWithParams: true, param);

            Assert.Multiple(() =>
            {
                ClassicAssert.AreEqual((int)HttpStatusCode.NotFound, jsonResponse.responseCode);
                ClassicAssert.AreEqual(expectedText, jsonResponse.message);
            });
        }

        [Test, TestCaseSource(typeof(TestDataLoader), nameof(TestDataLoader.LoadTestData), new object[] { "TestData/CreateUser.json" })]
        [AllureId(11)]
        [AllureTag("Can be FAILED if user not deleted in prev. RUN")]
        public void PostToCreateUserAccount(JToken testData)
        {
            apiResource = "api/createAccount";
            Dictionary<string, string> param = new Dictionary<string, string>
            {
                { "name", testData["name"]?.ToString() },
                { "email", testData["email"]?.ToString()},
                { "password", testData["password"]?.ToString()},
                { "title", testData["title"]?.ToString() },
                { "birth_date", testData["birth_date"]?.ToString() },
                { "birth_month", testData["birth_month"]?.ToString() },
                { "birth_year", testData["birth_year"]?.ToString() },
                { "firstname", testData["firstname"]?.ToString() },
                { "lastname", testData["lastname"]?.ToString() },
                { "company", testData["company"]?.ToString() },
                { "address1", testData["address1"]?.ToString() },
                { "address2", testData["address2"]?.ToString() },
                { "country", testData["country"]?.ToString() },
                { "zipcode", testData["zipcode"]?.ToString() },
                { "state", testData["state"]?.ToString() },
                { "city", testData["city"]?.ToString() },
                { "mobile_number", testData["mobile_number"]?.ToString() }
            };

            string expectedText = testData["created_message"]?.ToString();

            RestResponse response;
            Message jsonResponse = method.GetJsonResponse<Message>(apiResource, Method.Post, out response, sendWithParams: true, param);

            Assert.Multiple(() =>
            {
                ClassicAssert.AreEqual((int)HttpStatusCode.Created, jsonResponse.responseCode);
                ClassicAssert.AreEqual(expectedText, jsonResponse.message);
            });
        }

        [Test, TestCaseSource(typeof(TestDataLoader), nameof(TestDataLoader.LoadTestData), new object[] { "TestData/CreateUser.json" })]
        [AllureId(12)]
        public void PutToUpdateUserAccount(JToken testData)
        {
            apiResource = "api/updateAccount";
            Dictionary<string, string> param = new Dictionary<string, string>
            {
                { "name", testData["name"]?.ToString() },
                { "email", testData["email"]?.ToString()},
                { "password", testData["password"]?.ToString()},
                { "title", testData["title"]?.ToString() },
                { "birth_date", testData["birth_date"]?.ToString() },
                { "birth_month", testData["birth_month"]?.ToString() },
                { "birth_year", testData["birth_year"]?.ToString() },
                { "firstname", testData["firstname"]?.ToString() },
                { "lastname", testData["lastname"]?.ToString() },
                { "company", testData["company"]?.ToString() },
                { "address1", testData["address1"]?.ToString() },
                { "address2", testData["address2"]?.ToString() },
                { "country", testData["country"]?.ToString() },
                { "zipcode", testData["zipcode"]?.ToString() },
                { "state", testData["state"]?.ToString() },
                { "city", testData["city"]?.ToString() },
                { "mobile_number", testData["new_mobile_number"]?.ToString() }
            };
            string expectedText = testData["updated_message"]?.ToString();

            RestResponse response;
            Message jsonResponse = method.GetJsonResponse<Message>(apiResource, Method.Put, out response, sendWithParams: true, param);

            Assert.Multiple(() =>
            {
                ClassicAssert.AreEqual((int)HttpStatusCode.OK, jsonResponse.responseCode);
                ClassicAssert.AreEqual(expectedText, jsonResponse.message);
            });
        }

        [Test, TestCaseSource(typeof(TestDataLoader), nameof(TestDataLoader.LoadTestData), new object[] { "TestData/CreateUser.json" })]
        [AllureId(13)]
        public void DeleteCreatedAccount(JToken testData)
        {
            apiResource = "api/deleteAccount";
            Dictionary<string, string> param = new Dictionary<string, string>
            {
                { "email", testData["email"]?.ToString() },
                { "password", testData["password"]?.ToString() }
            };

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
