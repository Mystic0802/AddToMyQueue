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

        public string? Code { get; set; }
        public string? State { get; set; }
        public string? Error { get; set; }

        public CallbackModel(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public async void OnGet()
        {
            State = Request.Query["state"];
            Code = Request.Query["code"];
            Error = Request.Query["error"];

            Redirect("/index");

            if(State != "HelloThisIsVeryGoodState123")
                return;


            var client = new HttpClient();

            // Encome clientId + client secret
            var plainTextBytes = Encoding.UTF8.GetBytes(_configuration["Spotify:ClientId"] + ":" + _configuration["Spotify:ClientSecret"]);
            var base64AuthHeader = Convert.ToBase64String(plainTextBytes);

            var postContent = new FormUrlEncodedContent(new[] {
                new KeyValuePair<string, string>("code", Code), new KeyValuePair<string, string>("redirect_uri", _configuration["Spotify:Auth:RedirectUrl"]), new KeyValuePair<string, string>("grant_type", "authorization_code")
            });
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Basic", base64AuthHeader);
            //client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/x-www-form-urlencoded"));


            string url = _configuration["Spotify:Auth:BaseUrl"] + "/api/token";

            var response = await client.PostAsync(url, postContent);
            var responseContent = await response.Content.ReadAsStringAsync();
            var access = JsonConvert.DeserializeObject<AccessTokenReponse>(responseContent);
        }
    }
}
