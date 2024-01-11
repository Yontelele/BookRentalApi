namespace Labb2.Models;

public class BookRental
{
    public int Id { get; set; }

    public Book Book { get; set; }

    public User User { get; set; }

    public bool IsRented { get; set; } = false;

    public DateTime RentalDate { get; set; }

    public DateTime? ReturnDate { get; set; }
}
