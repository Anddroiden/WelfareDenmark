using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WelfareDenmark.Models
{
    public class BrainGameDataContext : DbContext
    {
        public DbSet<BrainGame> BrainGames { get; set; }
        public BrainGameDataContext(DbContextOptions<BrainGameDataContext> options)
        {
            Database.EnsureCreated();
        }
    }
}
