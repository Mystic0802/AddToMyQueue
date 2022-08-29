using AddToMyQueue.Api.Models;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace AddToMyQueue.Web.Pages
{
    public class AuthenticateModel : PageModel
    {
        private readonly ILogger _logger;
        private readonly SpotifyClients _spotifyClients;
        private readonly SpotifyApiData _apiData;
        private readonly HttpClient _httpClient;

        public AuthenticateModel(/*ILogger logger,*/ SpotifyClients spotifyClients, SpotifyApiData apiData)
        {
            //_logger = logger;
            _spotifyClients = spotifyClients;
            _apiData = apiData;

            _httpClient = new HttpClient();
        }

        public void OnGet()
        {
            var userId = "1"; //GetLoggedInUser();

            var client = _spotifyClients.TryGetClient(userId);
            if (client == null)
            {
                client = new SpotifyClient(_httpClient, _apiData);
                _spotifyClients.AddClient(userId, client);
            }

            // temp code for single user.
            if(client.accessToken != null)
            {
                Response.Redirect("/index");
                return;
            }
            
            Response.Redirect(client.GetAuthUrl());
        }
    }
}
