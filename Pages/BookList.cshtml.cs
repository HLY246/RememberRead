using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Identity;
using RememberRead.Data;

namespace RememberRead.Pages
{
    public class BookListModel : PageModel
    {
        private readonly ILogger<BookListModel> _logger;
        private readonly ApplicationDbContext _context;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly UserManager<ApplicationUser> _userManager;
        public List<Book>? BookList { get; set; }

        public BookListModel(ILogger<BookListModel> logger, ApplicationDbContext context, SignInManager<ApplicationUser> signInManager, UserManager<ApplicationUser> userManager)
        {
            _logger = logger;
            _context = context;
            _signInManager = signInManager;
            _userManager = userManager;
        }

        public void OnGet()
        {
            if (User.Identity != null)
            {
                if(User.Identity.IsAuthenticated)
                {
                    var userId = _userManager.GetUserId(User);

                    if (_context.Books != null)
                    {
                        BookList = _context.Books
                            .Where(book => book.UserId == userId)
                            .ToList();
                    }
                }
            }
            else
            {
                RedirectToPage("/Error");
            }
        }

        public async Task<IActionResult> OnPostAsync(int id)
        {
            if (_context.Books != null)
            {
                var BookToDelete = await _context.Books.FindAsync(id);

                if (BookToDelete == null)
                {
                    return NotFound();
                }

                _context.Books.Remove(BookToDelete);
                await _context.SaveChangesAsync();

                return RedirectToPage();
            }
            else
            {
                return NotFound();
            }
        }

        public PageResult OnPostSearch(string searchBy, string searchString)
        {
            if(User.Identity != null)
            {
                if(User.Identity.IsAuthenticated)
                {
                    var userId = _userManager.GetUserId(User);

                    if (searchBy == "title")
                    {
                        if (_context.Books != null)
                        {
                            BookList = _context.Books
                                .Where(book => book.UserId == userId && book.BookTitle.Contains(searchString))
                                .ToList();
                        }
                    }
                    else if (searchBy == "author")
                    {
                        if (_context.Books != null)
                        {
                            BookList = _context.Books
                                .Where(book => book.UserId == userId && book.BookAuthor.Contains(searchString))
                                .ToList();
                        }
                    }     
                }
            }
            else
            {
                RedirectToPage("/Error");
            }

            return Page();
        }
    }
}
