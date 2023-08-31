using Microsoft.EntityFrameworkCore;
namespace RankingAPI.Models
{
    public class ItemContext : DbContext
    {
        public ItemContext(DbContextOptions<ItemContext> options)
    : base(options)
        {
        }

        public DbSet<ItemModel> ItemItems { get; set; } = null!;


        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {

        }

        public new async Task<int> SaveChanges()
        {
            return await base.SaveChangesAsync();
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {

        }
    }
}
