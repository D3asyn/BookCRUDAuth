using BookCRUDAuth.Dbcontexts;
using BookCRUDAuth.DTOs;
using BookCRUDAuth.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using static System.Reflection.Metadata.BlobBuilder;

namespace BookCRUDAuth.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class AuthorController(DatabaseContext context) : ControllerBase
	{
		[HttpGet]
		[Authorize]
		public async Task<IActionResult> Get()
		{
			var authors = await context.Authors.Include(a => a.Books).ToListAsync();

			if(authors == null)
			{
				return BadRequest("No authors found");
			}

			var result = authors.Select(a => new GetAuthorDto
			{
				Name = a.Name,
				BirthDate = a.BirthDate,
				BookTitles = a.Books.Select(b => b.Title).ToList()
			}).ToList();

			return Ok(result);
		}

		[HttpGet("getById/{id}")]
        [Authorize]
        public async Task<IActionResult> GetById(int id)
		{
			var author = context.Authors.Include(a => a.Books).SingleOrDefault(x => x.Id == id);

			if(author == null)
			{
				return BadRequest("No author found");
			}

			var result = new GetAuthorDto
			{
				Name = author.Name,
				BirthDate = author.BirthDate,
				BookTitles = author.Books.Select(b => b.Title).ToList()
			};

			return Ok(result);
		}

		[HttpPost]
        [Authorize]
        public async Task<IActionResult> Post([FromBody] AuthorDTO authorDto)
		{

			if(authorDto == null)
			{
				return BadRequest("Input empty");
			}

			var author = new Author
			{
				Name = authorDto.Name,
				BirthDate = authorDto.BirthDate,
			};

			await context.Authors.AddAsync(author);
			await context.SaveChangesAsync();
			return CreatedAtAction(nameof(GetById), new { id = author.Id }, author);
		}

		[HttpPut("update/{id}")]
        [Authorize]
        public async Task<IActionResult> Put(int id, [FromBody] AuthorDTO authorDto)
		{
			var author = context.Authors.SingleOrDefault(x => x.Id == id);

			if(author == null)
			{
				return BadRequest("No author found");
			}

			author.Name = authorDto.Name;
			author.BirthDate = authorDto.BirthDate;

			await context.SaveChangesAsync();
			return Ok("Update successfull");
		}

		[HttpDelete("delete/{id}")]
        [Authorize]
        public async Task<IActionResult> Delete(int id)
		{
			var author = context.Authors.SingleOrDefault(x => x.Id == id);

			if(author == null)
			{
				return BadRequest("No author found");
			}


			context.Authors.Remove(author);

			await context.SaveChangesAsync();

			return Ok("Removed Successfully");
		}
	}
}
