using AddToMyQueue.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace AddToMyQueue.Web.Pages
{
    public class IndexModel : PageModel
    {
        private readonly ILogger<IndexModel> _logger;
        private readonly AddToMyQueueContext _context;

        public IndexModel(ILogger<IndexModel> logger, AddToMyQueueContext context)
        {
            _logger = logger;
            _context = context;
        }

        public void OnGet()
        {
        }
    }
}