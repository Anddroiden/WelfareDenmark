using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using WelfareDenmark.Models;

namespace WelfareDenmark.Controllers
{
    public class ResultsController : Controller
    {
        public IActionResult Index()
        {
            ViewData["Message"] = "Your application description page.";

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