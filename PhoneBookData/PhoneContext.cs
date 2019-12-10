using Microsoft.EntityFrameworkCore;
using PhoneBookModels;

namespace PhoneBookData
{
	public class PhoneContext : DbContext
	{

		public DbSet<PhoneBook> PhoneBooks { get; set; }
		public DbSet<PhoneBookEntry> PhoneBookEntries { get; set; }

		protected override void OnModelCreating(ModelBuilder modelBuilder)
		{
			modelBuilder.Entity<PhoneBook>()
				.HasKey(pb => pb.Id);
			modelBuilder.Entity<PhoneBook>()
			.HasMany(pb => pb.Entries)
			.WithOne()
			.HasForeignKey(p=> p.PhoneBookId);

			modelBuilder.Entity<PhoneBookEntry>()
				.HasKey(pbe => pbe.Id);

		}

		public PhoneContext(DbContextOptions<PhoneContext> options)
			   : base(options)
		{

		}
	}
}
