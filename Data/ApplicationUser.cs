using Microsoft.AspNetCore.Identity;

namespace RememberRead.Data
{
    public class ApplicationUser : IdentityUser
    {
       public string FirstName { get; set; }
       public string LastName { get; set; }

       //Navigation property for Book list
       public ICollection<Book> Books { get; set; }
    }
}
