using BulkyBoodExtended.Models;
using Microsoft.EntityFrameworkCore;

namespace BulkyBoodExtended.Data
{
    public class ApplicationDBContext: DbContext
    {
        public ApplicationDBContext(DbContextOptions<ApplicationDBContext> options) : base(options) { }
        public DbSet<Category> Categories { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Category>().HasData(
                new Category { Id = 1, Name = "Action", DisplayOrder = 1},
                new Category { Id = 2, Name = "Science", DisplayOrder = 2 },
                new Category { Id = 3, Name = "Fiction", DisplayOrder = 3 }
                );
        }
    }
}
