using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using WelfareDenmark.Models;

namespace WelfareDenmark.Data {
    public class ApplicationDbContext : IdentityDbContext {
        public virtual DbSet<BrainGame> BrainGames { get; set; }
//        public DbSet<GameResult> Results { get; set; }

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options) {
        }

        protected override void OnModelCreating(ModelBuilder builder) {
            builder.Entity<BrainGame>().HasMany(bg => bg.GameResults)
                .WithOne(g => g.BrainGame);
            builder.Entity<GameResult>().HasOne(b => b.BrainGame).WithMany(g => g.GameResults);
            base.OnModelCreating(builder);
        }
    }
}