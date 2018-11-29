using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using WelfareDenmark.Models;

namespace WelfareDenmark.Data {
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser> {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options) {
        }

        public virtual DbSet<BrainGame> BrainGames { get; set; }
        public DbSet<GameResult> Results { get; set; }
    }
}