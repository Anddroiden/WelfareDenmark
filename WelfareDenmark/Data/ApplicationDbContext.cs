using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using WelfareDenmark.Models;

namespace WelfareDenmark.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public DbSet<BrainGame> BrainGames { get; set; }
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }
    }
}
