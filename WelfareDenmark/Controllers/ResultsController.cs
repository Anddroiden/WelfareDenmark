﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WelfareDenmark.Data;
using WelfareDenmark.Models;

namespace WelfareDenmark.Controllers {
    public class ResultsController : Controller {
        private readonly ApplicationDbContext _db;

        public ResultsController(ApplicationDbContext db) {
            _db = db;
        }

        public IActionResult Index() {
            var gameResults = _db.Results.Include(r => r.BrainGame).Where(r => r.Player == User.Identity.Name);
            var results = new[] {
                new GameResult {
                    BrainGame = new BrainGame {Name = "Reaction"},
                    DateTime = new DateTime(),
                    Id = 3,
                    Player = "username is cool",
                    Score = 5867
                }
            };
            return View(gameResults);
        }
    }
}