using BookCRUDAuth.Entities;

namespace BookCRUDAuth.DTOs
{
	public class GetAuthorDto
	{
		public string Name { get; set; }
		public DateTime BirthDate { get; set; }
		public List<string> BookTitles { get; set; }
	}
}
