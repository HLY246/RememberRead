
namespace RememberRead.Data
{
    public class Book
    {
        public int Id { get; set; }
        public string? BookTitle { get; set; }
        public string? BookAuthor { get; set; }
        public string? AdditionalNotes { get; set; }

        //Foreign key to Application User
        public string? UserId { get; set; }
        public ApplicationUser? User { get; set; }
    }
}
