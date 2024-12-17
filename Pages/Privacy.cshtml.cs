using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Identity;
using RememberRead.Data;

namespace RememberRead.Pages
{
    public class PrivacyModel : PageModel
    {
        private readonly ILogger<PrivacyModel> _logger;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly ApplicationDbContext _context;

        public PrivacyModel(ILogger<PrivacyModel> logger, UserManager<ApplicationUser> userManager, ApplicationDbContext context)
        {
            _logger = logger;
            _userManager = userManager;
            _context = context;
        }

        public async Task OnGetAsync()
        {
            var user = await _userManager.FindByEmailAsync("petroy.holyland@gmail.com");

            if (user == null)
            {
                Console.WriteLine("User was Null");
                return;
            }

            var bookExample = new Book
            {
                BookTitle = "Example",
                BookAuthor = "Example Author",
                AdditionalNotes = "This is a test to see if the tables are setup correctly",
                UserId = user.Id
            };

            _context.Books.Add(bookExample);
            await _context.SaveChangesAsync();
        }
    }

}
