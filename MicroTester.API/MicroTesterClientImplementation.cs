using Newtonsoft.Json;
using System;
using System.Net.Http;
using System.Threading.Tasks;

namespace MicroTester.API
{
    class MicroTesterClientImplementation : IMicroTesterClient
    {
        private readonly HttpClient _client;

        public MicroTesterClientImplementation(HttpClient client)
        {
            _client = client ?? throw new ArgumentNullException(nameof(client));
        }

        public async Task<ListCasesResponse> ListCasesAsync(ListCasesRequest request)
        {
            var body = JsonConvert.SerializeObject(request);
            var message = new HttpRequestMessage(HttpMethod.Post, MicroTesterClient.ListCasesEndpointPath);
            message.Content = new StringContent(body);
            message.Content.Headers.ContentType = new System.Net.Http.Headers.MediaTypeHeaderValue("application/json");
            var response = await _client.SendAsync(message);
            response.EnsureSuccessStatusCode();
            var content = await response.Content.ReadAsStringAsync();

            return JsonConvert.DeserializeObject<ListCasesResponse>(content);
        }
    }
}
