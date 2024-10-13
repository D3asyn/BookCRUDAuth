using BookCRUDAuth.Entities;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace BookCRUDAuth.Dbcontexts
{
	public class DatabaseContext(DbContextOptions options) : IdentityDbContext<ApplicationUser>(options)
    {
		public DbSet<Book> Books { get; set; }
		public DbSet<Author> Authors { get; set; }

		protected override void OnModelCreating(ModelBuilder modelBuilder)
		{
			base.OnModelCreating(modelBuilder);

			modelBuilder.Entity<Book>()
				.HasOne(b => b.Author)
				.WithMany(a => a.Books)
				.HasForeignKey(b => b.AuthorId);

		}
	}
}
