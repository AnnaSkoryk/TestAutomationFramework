using Newtonsoft.Json;
using NUnit.Framework.Legacy;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

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
                jsonResponse = JsonConvert.DeserializeObject<T>(response.Content);
                ClassicAssert.IsNotNull(jsonResponse, "Deserialization returned null.");
            }
            catch (JsonReaderException ex)
            {
                Console.WriteLine("Invalid JSON format: " + ex.Message);
            }
            catch (JsonSerializationException ex)
            {
                Console.WriteLine("JSON doesn't match the model: " + ex.Message);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Unexpected error: " + ex.Message);
            }
        }

        public void SendRequest(string apiResource, Method method, HttpStatusCode expectedStatusCode, out RestResponse response)
        {
            var request = new RestRequest(apiResource, method);
            response = restClient.Execute(request);
            ClassicAssert.AreEqual(expectedStatusCode, response.StatusCode);
        }
    }
}
