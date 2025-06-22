using APITests.Models;
using Newtonsoft.Json;
using NUnit.Framework.Legacy;
using RestSharp;
using System.Net;

namespace APITests
{
    public class TestMethods
    {
        private RestClient restClient;

        public TestMethods(RestClient _restClient) 
        {
            restClient = _restClient;
        }

        public void TryDeserializeResponseToJSON<T>(RestResponse response, out T jsonResponse)
        {
            jsonResponse = default!;
            try
            {
                var settings = new JsonSerializerSettings
                {
                    MissingMemberHandling = MissingMemberHandling.Error
                };
                jsonResponse = JsonConvert.DeserializeObject<T>(response.Content, settings);

                ClassicAssert.IsNotNull(jsonResponse, "Deserialization returned null.");
            }
            catch (JsonReaderException ex)
            {
                Assert.Fail("Invalid JSON format: " + ex.Message);
            }
            catch (JsonSerializationException ex)
            {
                Assert.Fail("JSON doesn't match the model: " + ex.Message);
            }
            catch (Exception ex)
            {
                Assert.Fail("Unexpected error: " + ex.Message);
            }
        }

        public void SendRequest(string apiResource, Method method, HttpStatusCode expectedStatusCode, out RestResponse response, bool sendWithParams = false, Dictionary<string, string> parameters = null)
        {
            var request = new RestRequest(apiResource, method);
            if (sendWithParams)
                foreach (var param in parameters)
                {
                    request.AddParameter(param.Key, param.Value);
                }
            response = restClient.Execute(request);
        }

        public T GetJsonResponse<T>(string apiResource, Method method, out RestResponse response, bool sendWithParams = false, Dictionary<string, string> parameters = null)
        where T : IModel
        {
            var request = new RestRequest(apiResource, method);
            if (sendWithParams)
                foreach (var param in parameters)
                {
                    request.AddParameter(param.Key, param.Value);
                }
            response = restClient.Execute(request);

            T jsonResponse;
            TryDeserializeResponseToJSON<T>(response, out jsonResponse);
            return jsonResponse;
        }
    }
}
