using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using RememberRead.Data;

namespace RememberRead.Pages
{
    public class EditBookModel : PageModel
    {
        private readonly ILogger<EditBookModel> _logger;
        private readonly ApplicationDbContext _context;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        public Book? BookToEdit { get; set; }

        public EditBookModel(ILogger<EditBookModel> logger, ApplicationDbContext context, UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager)
        {
            _logger = logger;
            _context = context;
            _userManager = userManager;
            _signInManager = signInManager;
        }
        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            if(_context.Books != null)
            {
                BookToEdit = await _context.Books.FindAsync(id);

                if (BookToEdit == null)
                {
                    return NotFound();
                }

                return Page();

            }
            else
            {
                return NotFound();
            }
        }

        public async Task<IActionResult> OnPostAsync(int id, string EditBookTitle, string EditBookAuthor, string? EditAdditionalNotes)  
        {
            if(!ModelState.IsValid)
            {
                return Page();
            }

            if(_context.Books != null)
            {
                Book NewBook = await _context.Books.FindAsync(id);

                if (NewBook == null)
                {
                    return NotFound();
                }

                if (EditAdditionalNotes == null)
                {
                    EditAdditionalNotes = "N/A";
                }

                NewBook.BookTitle = EditBookTitle;
                NewBook.BookAuthor = EditBookAuthor;
                NewBook.AdditionalNotes = EditAdditionalNotes;

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
