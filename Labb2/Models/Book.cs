namespace Labb2.Models;

public class Book
{
    public int BookId { get; set; }

    public string BookTitle { get; set; } = string.Empty;

    public string ISBN { get; set; } = string.Empty;

    public int ReleaseYear { get; set; }

    public int BookRating { get; set; }

    public ICollection<Author> Author { get; set; } = new List<Author>();

}
