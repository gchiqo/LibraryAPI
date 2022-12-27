using LibraryAPI.Data;
using LibraryAPI.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace LibraryAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BooksController : ControllerBase
    {

        public readonly BookDbContext _context;

        public BooksController(BookDbContext context) => _context = context;

        [HttpGet]
        public async Task<IEnumerable<Book>> Get()
            => await _context.Books.ToListAsync();

        [HttpGet("id")]
        [ProducesResponseType(typeof(Book), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetById(int id)
        {
            var Book = await _context.Books.FindAsync(id);
            return Book == null ? NotFound() : Ok(Book);
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        public async Task<IActionResult> Create(Book Book)
        {
            await _context.Books.AddAsync(Book);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetById), new { id = Book.Id }, Book);
        }

        [HttpPut("id")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Update(int id, Book Book)
        {
            if (id != Book.Id) return BadRequest();

            _context.Entry(Book).State = EntityState.Modified;
            await _context.SaveChangesAsync();

            return NoContent();
        }

        [HttpDelete("id")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Delete(int id)
        {
            var BookToDelete = await _context.Books.FindAsync(id);
            if (BookToDelete == null) return BadRequest();

            _context.Books.Remove(BookToDelete);
            await _context.SaveChangesAsync();

            return NoContent();
        }

    }
}
