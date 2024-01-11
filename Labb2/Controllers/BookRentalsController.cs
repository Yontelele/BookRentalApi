using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Labb2.Models;

namespace Labb2.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BookRentalsController : ControllerBase
    {
        private readonly LibraryDbContext _context;

        public BookRentalsController(LibraryDbContext context)
        {
            _context = context;
        }

        // GET: api/BookRentals
        [HttpGet]
        public async Task<ActionResult<IEnumerable<BookRental>>> GetBookRental()
        {
            return await _context.BookRental.Include(u => u.User).Include(b => b.Book).ThenInclude(a => a.Author).ToListAsync();
        }

        // GET: api/BookRentals/5
        [HttpGet("{id}")]
        public async Task<ActionResult<BookRental>> GetBookRental(int id)
        {
            var bookRental = await _context.BookRental.FindAsync(id);

            if (bookRental == null)
            {
                return NotFound();
            }

            return bookRental;
        }

        // PUT: api/BookRentals/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutBookRental(int id, BookRental bookRental)
        {
            if (id != bookRental.Id)
            {
                return BadRequest();
            }

            _context.Entry(bookRental).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!BookRentalExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }



        // PUT: api/BookRentals/returnbook/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("returnbook/{id}")]
        public async Task<IActionResult> ReturnBook(int id)
        {
            BookRental? bookRental = await _context.BookRental.FindAsync(id);
            
            if(bookRental == null)
            {
                return NotFound(id);
            }

            bookRental.ReturnDate = DateTime.Now;
            bookRental.IsRented = false;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!BookRentalExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }


        // POST: api/BookRentals
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<BookRental>> PostBookRental(BookRental bookRental)
        {
            _context.BookRental.Add(bookRental);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetBookRental", new { id = bookRental.Id }, bookRental);
        }

        
        [HttpPost("rentbook")]
        public async Task<ActionResult<BookRental>> RentBook(BookRental bookRental)
        {
            Book? book = await _context.Book.FindAsync(bookRental.Book.BookId);
            User? user = await _context.User.FindAsync(bookRental.User.Id);

            if (user == null)
            {
                return NotFound(bookRental.User);
            }

            if (book == null)
            {
                return NotFound(bookRental.Book);
            }

            var rentBook = new BookRental
            {
                Book = book,
                User = user,
                RentalDate = DateTime.Now,
                IsRented = true
            };

            _context.BookRental.Add(rentBook);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetBookRental", new { id = rentBook.Id }, rentBook);
        }



        // DELETE: api/BookRentals/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteBookRental(int id)
        {
            var bookRental = await _context.BookRental.FindAsync(id);
            if (bookRental == null)
            {
                return NotFound();
            }

            _context.BookRental.Remove(bookRental);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool BookRentalExists(int id)
        {
            return _context.BookRental.Any(e => e.Id == id);
        }
    }
}
