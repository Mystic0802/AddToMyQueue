using AddToMyQueue.Api.Extensions;
using AddToMyQueue.Api.Models;
using AddToMyQueue.Data;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace AddToMyQueue.Web.Pages
{
    public class CallbackModel : PageModel
    {
        private readonly ILogger _logger;
        private readonly SpotifyClients _spotifyClients;
        private readonly AddToMyQueueContext _context;
        private string _code;
        private string _receivedState;
        private string _error;

        public CallbackModel(/*ILogger logger,*/ SpotifyClients spotifyClients, AddToMyQueueContext context)
        {
            //_logger = logger;
            _spotifyClients = spotifyClients;
            _context = context;
        }

        public async Task OnGet()
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


            var client = _spotifyClients.TryGetClient(userId);
            if (client == null || client.accessToken != null) // access token part is temp code for single user
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

            await client.GetAccessToken(_code); // Must complete, before saving.
            await client.SaveClientToDb(_context, userId);
            Response.Redirect("/index");
        }
    }
}
