using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WelfareDenmark.Data;
using WelfareDenmark.Models;

namespace WelfareDenmark.Controllers
{
    public class PatientDataController : Controller
    {        private readonly ApplicationDbContext _db;

        public PatientDataController(ApplicationDbContext db) {
            _db = db;
        }
        [Route("/results")]
        public IActionResult Results()
        {
            ViewData["Message"] = "Your application description page.";

            var gameResults = _db.Results.Include(r=>r.BrainGame);
            return View(new[] {
                new GameResult {
                    BrainGame = new BrainGame(), 
                    DateTime = new DateTime(),
                    Id = 3,
                    Player = "username is cool",
                    Score = 5867 
                } 
            });
        }
    }
}