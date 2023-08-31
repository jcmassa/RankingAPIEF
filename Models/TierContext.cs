using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace RankingAPI.Models
{
    public class TierContext : DbContext
    {
        public TierContext(DbContextOptions<TierContext> options): base(options)
        {
        }

        public DbSet<TierModel> Tiers { get; set; }

        //public override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        //{ }

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
