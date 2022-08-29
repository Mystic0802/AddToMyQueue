using AddToMyQueue.Api.Models;
using AddToMyQueue.Data;
using AddToMyQueue.Data.Models;
using AddToMyQueue.Data.Models.Spotify;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace AddToMyQueue.Web.Pages
{
    public class ProfileModel : PageModel
    {
        private readonly AddToMyQueueContext _context;
        private readonly ILogger _logger;

        public User? UserProfile { get; private set; }
        public SpotifyAccount? SpotifyProfile { get; private set; }

        public bool IsViewingOwnProfile { get; set; }

        public ProfileModel(AddToMyQueueContext context/*, ILogger logger*/)
        {
            _context = context;
            //_logger = logger;
        }

        public void OnGet(string? userId)
        {
            // if no userID is entered in the URL, show users own profile
            if (userId == null || userId == string.Empty)
            {
                // Check if logged in, use their userId if so. ask to sign in/up if not
                userId = GetLoggedInUserId();
                if (userId == "")
                {
                    // Temp code.
                    Response.Redirect("/Profile/1");

                    //Response.Redirect("/login");
                    return;
                }
            }

            IsViewingOwnProfile = userId == GetLoggedInUserId();

            UserProfile = _context.Users.Where(u => u.UserId == userId).FirstOrDefault();

            if (UserProfile == null)
            {
                // if no profile is found, show "profile not found" stuff
                return;
            }

            DisplayProfileInfo(UserProfile);

            var userSpotifyAccountId = _context.UserSpotifyAccounts.Where(a => a.UserId == userId).FirstOrDefault()?.SpotifyId;
            var spotifyAccount = _context.SpotifyAccounts.Where(a => a.SpotifyId == userSpotifyAccountId).FirstOrDefault();

            if (spotifyAccount == null)
                return;

            DisplaySpotifyAccount(spotifyAccount);
        }

        private string GetLoggedInUserId()
        {
            return "";
        }

        private void DisplayProfileInfo(User userProfile)
        {
            
        }

        private void DisplaySpotifyAccount(SpotifyAccount spotifyAccount)
        { 
            
        }

        public void StartSpotifyAuthFlow()
        {
            Response.Redirect("/Authenticate");
        }
    }
}
