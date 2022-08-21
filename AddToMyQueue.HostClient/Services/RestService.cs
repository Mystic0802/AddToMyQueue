using Newtonsoft.Json;
using System.Text;

namespace AddToMyQueue.HostClient.Services
{
    public interface IRestService
    {
        Task<(ResultStatus status, TResponse? payload, string? rawResponse)> GetAsync<TResponse>(string url);
        Task<(ResultStatus status, TResponse? payload, string? rawResponse)> PostAsync<TRequest, TResponse>(TRequest request, string url);
    }
    public enum ResultStatus
    {
        Success = 0,
        ConnectionFailed,
        Unauthorized,
        BadResponse,
        BadPayload,
        // Map HttpStatus codes to additional enum members here ...
        Other
    }

    public class RestService : IRestService
    {
        private readonly HttpClient _httpClient;
        private readonly string _baseUrl;

        public RestService(HttpClient httpClient, string baseUrl)
        {
            _httpClient = httpClient;
            _baseUrl = baseUrl;
        }


        public async Task<(ResultStatus status, TResponse? payload, string? rawResponse)> GetAsync<TResponse>(string url)
        {
            string uri = Path.Combine(_baseUrl, url);

            var response = await _httpClient.GetAsync(Sanitise(uri));

            if (response.IsSuccessStatusCode == true)
            {
                string rawData = await response.Content.ReadAsStringAsync();

                // Turn our JSON string into a csharp object (or object-graph)
                var result = JsonConvert.DeserializeObject<TResponse>(rawData);

                // Return a response of type TResult.
                return (ResultStatus.Success, result, rawData);
            }
            else
            {
                return (ResultStatus.Other, default, null);
            }
        }

        public async Task<(ResultStatus status, TResponse? payload, string? rawResponse)> PostAsync<TRequest, TResponse>(TRequest request, string path)
        {
            string uri = Path.Combine(_baseUrl, path);

            string json = JsonConvert.SerializeObject(request);
            var data = new StringContent(json, Encoding.UTF8, "application/json");

            var response = await _httpClient.PostAsync(Sanitise(uri), data);

            if (response.IsSuccessStatusCode == true)
            {
                string rawData = await response.Content.ReadAsStringAsync();

                // Turn our JSON string into a csharp object (or object-graph)
                var result = JsonConvert.DeserializeObject<TResponse>(rawData);

                // Return a response of type TResult.
                return (ResultStatus.Success, result, rawData);

            }
            else
            {
                return (ResultStatus.Other, default, null);
            }
        }

        private string Sanitise(string uri)
        {
            return uri.Replace('\\', '/');
        }
    }
}
