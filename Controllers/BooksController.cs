using BookCRUDAuth.Dbcontexts;
using BookCRUDAuth.DTOs;
using BookCRUDAuth.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using static System.Reflection.Metadata.BlobBuilder;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace BookCRUDAuth.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class BooksController(DatabaseContext context) : ControllerBase
	{
		[HttpGet]
        [Authorize]
        public async Task<IActionResult> Get()
		{
			var books = await context.Books.Include(b => b.Author).ToListAsync();

			if(books == null)
			{
				return BadRequest("No books found");
			}

			var result = books.Select(b => new GetBookDto
			{
				Title = b.Title,
				PublishedDate = b.PublishedDate,
				AuthorName = b.Author?.Name
			}).ToList();

			return Ok(result);
		}

		[HttpGet("getById/{id}")]
        [Authorize]
        public async Task<IActionResult> GetById(int id)
		{
			var book = context.Books.Include(b => b.Author).SingleOrDefault(x => x.Id == id);

			if(book == null)
			{
				return BadRequest("No book found");
			}

			var result = new GetBookDto
			{
				Title = book.Title,
				PublishedDate = book.PublishedDate,
				AuthorName = book.Author?.Name
			};

			return Ok(result);
		}

		[HttpPost]
        [Authorize]
        public async Task<IActionResult> Post([FromBody] BookDTO bookDto)
		{

			if (bookDto == null)
			{
				return BadRequest("Input empty");
			}
			var book = new Book
			{
				Title = bookDto.Title,
				PublishedDate = bookDto.PublishedDate,
				AuthorId = bookDto.AuthorId,
			};

			await context.Books.AddAsync(book);
			await context.SaveChangesAsync();
			return CreatedAtAction(nameof(GetById), new { id = book.Id }, book);
		}

		[HttpPut("update/{id}")]
        [Authorize]
        public async Task<IActionResult> Put(int id, [FromBody] BookDTO bookDto)
		{
			var book = context.Books.SingleOrDefault(x => x.Id == id);

			if (book == null)
			{
				return BadRequest("No book found");
			}

			book.AuthorId = bookDto.AuthorId;
			book.Title = bookDto.Title;
			book.PublishedDate = bookDto.PublishedDate;

			await context.SaveChangesAsync();
			return Ok("Update successfull");
		}

		[HttpDelete("delete/{id}")]
        [Authorize]
        public async Task<IActionResult> Delete(int id)
		{
			var book = context.Books.SingleOrDefault(x => x.Id == id);

			if(book == null)
			{
				return BadRequest("No book found");
			}


			context.Books.Remove(book);

			await context.SaveChangesAsync();

			return Ok("Removed Successfully");
		}
	}
}
