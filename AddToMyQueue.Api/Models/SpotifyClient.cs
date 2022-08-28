﻿using AddToMyQueue.Api.Extensions;
using Newtonsoft.Json;
using System.Net.Http.Headers;
using System.Security.Cryptography;
using System.Text;

namespace AddToMyQueue.Api.Models
{
    public class SpotifyClient
    {
        private readonly HttpClient _httpClient;
        private readonly SpotifyApiData _apiData;

        private string _state;

        private string? refreshToken;
        public string? accessToken;
        
        public SpotifyClient(HttpClient httpClient, SpotifyApiData apiData)
        {
            _httpClient = httpClient;
            _apiData = apiData;

            _state = _apiData.State;
        }

        public string GetAuthUrl()
        {
            var uri = _apiData.AuthBaseUrl + $"/authorize?client_id={_apiData.ClientId}&response_type=code&redirect_uri={_apiData.RedirectUrl}";
            uri += !_state.IsNullOrEmpty() ? "&state=" + _state : "";
            uri += !_apiData.Scopes.IsNullOrEmpty() ? "&scope=" + _apiData.Scopes : "";
            uri += _apiData.ShowDialog ? "&show_dialog=" + _apiData.ShowDialog : "";
            return uri;
        }

        public bool ConfirmState(string receivedState)
        {
            return _state == receivedState;
        }

        public async Task GetAccessToken(string code)
        {
            var postContent = new FormUrlEncodedContent(new[] {
                new KeyValuePair<string, string>("code", code), new KeyValuePair<string, string>("redirect_uri", _apiData.RedirectUrl), new KeyValuePair<string, string>("grant_type", "authorization_code")
            });

            var anonObj = new
            {
                access_token = "",
                token_type = "",
                expires_in = "",
                refresh_token = "",
                scope = ""
            };

            var responseJsonString = await PostToken(postContent);
            var obj = JsonConvert.DeserializeAnonymousType(responseJsonString, anonObj);

            accessToken = obj?.access_token;
            refreshToken = obj?.refresh_token;
        }

        // Potential duplicate code cleanup here /\ \/
        public async Task RefreshAccessToken()
        {
            if (refreshToken == null)
                return;

            var postContent = new FormUrlEncodedContent(new[] {
                new KeyValuePair<string, string>("refresh_token", refreshToken), new KeyValuePair<string, string>("grant_type", "refresh_token")
            });

            var anonObj = new
            {
                access_token = "",
                token_type = "",
                expires_in = "",
                scope = ""
            };

            var responseJsonString = await PostToken(postContent);
            var obj = JsonConvert.DeserializeAnonymousType(responseJsonString, anonObj);

            accessToken = obj?.access_token;
        }

        private async Task<string> PostToken(FormUrlEncodedContent postContent)
        {
            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Basic", _apiData.AuthHeader);

            string url = _apiData.AuthBaseUrl + "/api/token";

            var response = await _httpClient.PostAsync(url, postContent);
            return await response.Content.ReadAsStringAsync();
        }
    }
}
