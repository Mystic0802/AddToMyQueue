using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AddToMyQueue.HostClient.Services
{
    public class AuthService : IRestService
    {
        private readonly HttpClient _httpClient;
        private readonly RestService _restService;

        public AuthService(HttpClient httpClient, string baseUrl)
        {
            _httpClient = httpClient;
            _restService = new RestService(_httpClient, baseUrl);
        }

        public async Task<(ResultStatus status, TResponse? payload, string? rawResponse)> GetAsync<TResponse>(string url)
        {
            return await _restService.GetAsync<TResponse>(url);
        }

        public async Task<(ResultStatus status, TResponse? payload, string? rawResponse)> PostAsync<TRequest, TResponse>(TRequest request, string url)
        {
            return await _restService.PostAsync<TRequest, TResponse>(request, url);
        }
    }
}
