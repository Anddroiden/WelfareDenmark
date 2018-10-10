﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WelfareDenmark.Data;
using WelfareDenmark.Models;

namespace WelfareDenmark.Controllers {
//    [Authorize(Policy = "IsPatient")]
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
            var games = _db.BrainGames.Include(b => b.GameResults);
            var filteredResults = games.Select(b => new BrainGame {
                GameResults = b.GameResults.Where(r => r.Player == User.Identity.Name).ToArray(),
                Name = b.Name,
                Id = b.Id
            });
            var brainGames = filteredResults.Where(b=>b.GameResults.Any()).ToArray();
            return View(brainGames);
        }

        public IActionResult Details(long id) {
            var gameResult = _db.Results.Include(r => r.BrainGame).First(r => r.Id == id);
            if (gameResult is null) {
                return NotFound();
            }

            return View(gameResult);
        }
    }
}