using Microsoft.EntityFrameworkCore;

namespace PhoneBookApp.Server.Data
{
    public class PhoneBookDbContext : DbContext
    {
        public PhoneBookDbContext(DbContextOptions<PhoneBookDbContext> options) : base(options)
        {

        }

        public DbSet<Person> People { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Person>().HasData(
                new Person
                {
                    Id = 1,
                    Name = "Khanyiso",
                    Surname = "Luzipo",
                    PhoneNumber = "0632790866",
                    EmailAddress = "khanyiso.luzipo@outlook.com"
                } );
        }
    }
}
