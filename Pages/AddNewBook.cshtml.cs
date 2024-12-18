using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using RememberRead.Data;

namespace RememberRead.Pages
{
    public class AddNewBookModel : PageModel
    {
        private readonly ILogger<AddNewBookModel> _logger;
        private readonly ApplicationDbContext _context;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;

        public AddNewBookModel(ILogger<AddNewBookModel> logger, ApplicationDbContext context, UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager)
        {
            _logger = logger;
            _context = context;
            _userManager = userManager;
            _signInManager = signInManager;
        }

        public void OnGet()
        {

        }

        public async Task<IActionResult> OnPostAsync(string AddBookTitle, string AddBookAuthor, string? AddAdditionalNotes)
        {
            var userId = _userManager.GetUserId(User);

            if (userId == null)
            {
                return NotFound();
            }

            var NewBook = new Book();

            if (AddAdditionalNotes != null)
            {
                NewBook = new Book { BookTitle = AddBookTitle, BookAuthor = AddBookAuthor, AdditionalNotes = AddAdditionalNotes, UserId = userId };
            }
            else
            {
                NewBook = new Book { BookTitle = AddBookTitle, BookAuthor = AddBookAuthor, AdditionalNotes = "N/A", UserId = userId };
            }

            if (_context.Books != null)
            {
                _context.Books.Add(NewBook);
                await _context.SaveChangesAsync();
                return RedirectToPage("/BookList");
            }
            else
            {
                return NotFound();
            }
        }
    }
}
