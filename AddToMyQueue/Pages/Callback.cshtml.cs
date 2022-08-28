using AddToMyQueue.Api.Extensions;
using AddToMyQueue.Api.Models;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace AddToMyQueue.Web.Pages
{
    public class CallbackModel : PageModel
    {
        private readonly ILogger _logger;
        private readonly SpotifyClients _spotifyClients;

        private string _code;
        private string _receivedState;
        private string _error;

        public CallbackModel(/*ILogger logger,*/ SpotifyClients spotifyClients)
        {
            //_logger = logger;
            _spotifyClients = spotifyClients;
        }

        public async void OnGet()
        {
            var userId = "1"; //GetLoggedInUser();

            if (userId == null)
            {
                // Either redirect to /index or redirect to login(?)
                Response.Redirect("/index");
                return;
            }

            _code = Request.Query["code"];
            _receivedState = Request.Query["state"];
            _error = Request.Query["error"];

            Response.Redirect("/index");

            var client = _spotifyClients.GetClient(userId);
            if (client == null || client.accessToken != null)
            {
                // Either redirect to /index or show a "Authentication failed, try again later" message.
                Response.Redirect("/index");
                return;
            }

            // Check state first to confirm response is from Spotify & for the correct user.
            if (!client.ConfirmState(_receivedState))
            {
                //_logger.LogWarning("{userId} received incorrect state! {_receivedState}", userId, _receivedState);
                return;
            }

            // Then check for any errors
            if (!_error.IsNullOrEmpty())
            {
                //_logger.LogError("{_error}", _error);
                return;
            }

            await client.GetAccessToken(_code);
        }
    }
}
