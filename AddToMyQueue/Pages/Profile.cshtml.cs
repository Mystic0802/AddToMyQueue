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
        
        public User? UserProfile { get; private set; }
        public SpotifyAccount? SpotifyProfile { get; private set; }

        public bool IsViewingOwnProfile { get; set; } = false;

        public ProfileModel(AddToMyQueueContext context)
        {
            _context = context;
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
                    Response.Redirect("/login");
                    return;
                }
                IsViewingOwnProfile = true;
            }
            else if (userId == GetLoggedInUserId())
            {
                IsViewingOwnProfile = true;
            }

            UserProfile = _context.Users.Where(u => u.UserId == userId).FirstOrDefault();

            if (UserProfile == null)
            {
                // if no profile is found, show "profile not found" stuff

                return;
            }
            

            DisplayProfileInfo(UserProfile);

            var userSpotifyAccountId = _context.UserSpotifyAccounts.Where(a => a.UserId == userId).FirstOrDefault()?.SpotifyId;
            var userSpotifyAccount = _context.SpotifyAccounts.Where(a => a.SpotifyId == userSpotifyAccountId).FirstOrDefault();

            if (userSpotifyAccount == null )
            {
                return;
            }

            DisplaySpotifyAccount(userSpotifyAccount);
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

        }
    }
}
