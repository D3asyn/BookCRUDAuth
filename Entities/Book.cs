using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace BookCRUDAuth.Entities
{
	public class Book
	{
		public int Id { get; set; }
		[StringLength(255)]
		public string Title { get; set; }
		public DateTime PublishedDate { get; set; }
		public int AuthorId { get; set; }
		[JsonIgnore]
		public Author Author { get; set; }
	}
}
