using AddToMyQueue.Web.Models.Response;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Newtonsoft.Json;
using System.Net.Http.Headers;
using System.Text;

namespace AddToMyQueue.Web.Pages
{
    public class CallbackModel : PageModel
    {
        private readonly IConfiguration _configuration;
        private readonly ILogger _logger;

        public string? Code { get; set; }
        public string? ReceivedState { get; set; }
        public string? Error { get; set; }

        public CallbackModel(IConfiguration configuration, ILogger logger)
        {
            _configuration = configuration;
            _logger = logger;
        }

        public async void OnGet()
        {
            ReceivedState = Request.Query["state"];
            Code = Request.Query["code"];
            Error = Request.Query["error"];

            Response.Redirect("/index");

            var ExpectedState = _configuration["Spotify:Auth:State"];

            // Check state first to confirm response is from Spotify
            if (ReceivedState != ExpectedState)
            {
                _logger.LogWarning($"State received is different. Received State: {ReceivedState}, Expected State: {ExpectedState}");
                return;
            }

            // Then check for any errors
            if (Error != null && Error != "")
                _logger.LogError(Error);

            await GetAndStoreAccessToken();
        }

        async Task GetAndStoreAccessToken()
        {
            var client = new HttpClient();

            // Encode clientId + client secret
            var plainTextBytes = Encoding.UTF8.GetBytes(_configuration["Spotify:ClientId"] + ":" + _configuration["Spotify:ClientSecret"]);
            var base64AuthHeader = Convert.ToBase64String(plainTextBytes);

            var postContent = new FormUrlEncodedContent(new[] {
                new KeyValuePair<string, string>("code", Code), new KeyValuePair<string, string>("redirect_uri", _configuration["Spotify:Auth:RedirectUrl"]), new KeyValuePair<string, string>("grant_type", "authorization_code")
            });
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Basic", base64AuthHeader);

            string url = _configuration["Spotify:Auth:BaseUrl"] + "/api/token";

            var response = await client.PostAsync(url, postContent);
            var responseContent = await response.Content.ReadAsStringAsync();
            var access = JsonConvert.DeserializeObject<AccessTokenReponse>(responseContent);
            _configuration.Bind("Spotify:AccessToken", access?.access_token);
        }
    }
}
