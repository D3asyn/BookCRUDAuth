using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace BookCRUDAuth.Entities
{
	public class Author
	{
		public int Id { get; set; }
		[StringLength(255)]
		public string Name { get; set; }
		public DateTime BirthDate { get; set; }
        public string UserId { get; set; }
        [JsonIgnore]
        public ApplicationUser User { get; set; }
        [JsonIgnore]
		public List<Book> Books { get; set; } = new();

	}
}
