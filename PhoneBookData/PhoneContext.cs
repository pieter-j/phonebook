using Microsoft.EntityFrameworkCore;
using PhoneBookModels;

namespace PhoneBookData
{
   public class PhoneContext : DbContext
   {

      public DbSet<PhoneBook> PhoneBooks { get; set; }
      public DbSet<PhoneBookEntry> PhoneBookEntries { get; set; }

      public PhoneContext(DbContextOptions<PhoneContext> options)
             : base(options)
      {

      }
   }
}
