﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WelfareDenmark.Data;
using WelfareDenmark.Models;

namespace WelfareDenmark.Controllers
{
    public class ResultsController : Controller
    {
        private readonly ApplicationDbContext _db;

        public ResultsController(ApplicationDbContext db)
        {
            _db = db;
        }
        public IActionResult Index()
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