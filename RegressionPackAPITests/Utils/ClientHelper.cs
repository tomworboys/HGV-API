using Newtonsoft.Json;
using RegressionPackAPITests.Constants;
using RegressionPackAPITests.POCO.RequestResponse;
using RegressionPackAPITests.POCO.Setup;
using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace RegressionPackAPITests.Utils
{
    public static class ClientHelper
    {
        public static HttpClient GetClient()
        {
            var client = new HttpClient();

            client.BaseAddress = new Uri(ExecutionConfig.BaseUrl);
            client.Timeout = new TimeSpan(0, 0, 30);
            client.DefaultRequestHeaders.Clear();

            return client;
        }

        public static async Task<T> GetRestResonse<T>(string endpoint, HttpMethod requestType, HttpClient client, object bodyData = null)
        {
            var request = new HttpRequestMessage(requestType, endpoint);
            request.Headers.Accept.Add(new MediaTypeWithQualityHeaderValue(MimeTypes.ApplicationJson));

            if (bodyData != null)
            {
                request.Content = new StringContent(JsonConvert.SerializeObject(bodyData), Encoding.UTF8, MimeTypes.ApplicationJson);
            }

            var response = await client.SendAsync(request);
            var content = await response.Content.ReadAsStringAsync();

            return JsonConvert.DeserializeObject<T>(content);
        }

        public static PostTokenRequest GetTokenRequest()
        {
            return new PostTokenRequest
            {
                ApiKey = ExecutionConfig.ApiKey,
                UserName = ExecutionConfig.Username,
                Password = ExecutionConfig.Password
            };
        }

        public static PostTokenResponse PostToken(HttpClient client, PostTokenRequest postTokenRequest)
        {
            return GetRestResonse<PostTokenResponse>(
                ApiEndpoints.PostTokens, HttpMethod.Post, client, postTokenRequest).Result;
        }
    }
}
