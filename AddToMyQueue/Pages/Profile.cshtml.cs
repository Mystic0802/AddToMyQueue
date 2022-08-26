using AddToMyQueue.Data;
using AddToMyQueue.Data.Models;
using AddToMyQueue.Data.Models.Spotify;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Reflection.Metadata.Ecma335;

namespace AddToMyQueue.Web.Pages
{
    public class ProfileModel : PageModel
    {
        private readonly AddToMyQueueContext _context;

        public string Username { get; private set; }

        public ProfileModel(AddToMyQueueContext context)
        {
            _context = context;
        }

        public void OnGet(string userId)
        {
            // if no userID is entered in the URL, show users own profile
            if (userId == null || userId == string.Empty)
            {
                // Check if logged in, use their userId if so. ask to sign in/up if not
                userId = TryGetCurrentUserId() ?? "";

                if (userId == "")
                {
                    Response.Redirect("/login");
                    return;
                }
                

            }
            
            var userProfile = _context.Users.Where(u => u.UserId == userId).FirstOrDefault();

            if (userProfile == null)
            {
                // if no profile is found, show "profile not found" stuff
                return;
            }

            Username = userProfile.Username;

            DisplayProfileInfo(userProfile);

            var userSpotifyAccountId = _context.UserSpotifyAccounts.Where(a => a.UserId == userId).FirstOrDefault()?.SpotifyId;
            var userSpotifyAccount = _context.SpotifyAccounts.Where(a => a.SpotifyId == userSpotifyAccountId).FirstOrDefault();

            if (userSpotifyAccount == null)
            {
                // if not spotify user found, show spotify auth stuff
                return;
            }

            DisplaySpotifyAccount(userSpotifyAccount);
        }

        private string? TryGetCurrentUserId()
        {
            return null;
        }

        private void DisplayProfileInfo(User userProfile)
        {
            
        }

        private void DisplaySpotifyAccount(SpotifyAccount spotifyAccount)
        { 
            
        }

        public void StartSpotifyAuthFlor()
        {

        }
    }
}
