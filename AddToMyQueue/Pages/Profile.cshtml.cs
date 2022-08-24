using AddToMyQueue.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

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
            //var userProfile = _context.Users

            // if no profile is found, show "profile not found" stuff
        }
    }
}
