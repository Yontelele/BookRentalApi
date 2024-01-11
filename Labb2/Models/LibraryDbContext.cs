using Microsoft.EntityFrameworkCore;

namespace Labb2.Models;

public class LibraryDbContext : DbContext
{
    public LibraryDbContext(DbContextOptions<LibraryDbContext> options) : base(options)
    {
        
    }

    public DbSet<User> User { get; set; }
    public DbSet<Author> Author { get; set; }
    public DbSet<Book> Book { get; set; }
    public DbSet<BookRental> BookRental { get; set; }
}
