using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Internal;
using WelfareDenmark.Data;
using WelfareDenmark.Models;

namespace WelfareDenmark.Controllers {
    
    public class ResultsController : Controller {
        private readonly ApplicationDbContext _db;

        public ResultsController(ApplicationDbContext db) {
            _db = db;
        }

        [Authorize(Policy = PolicyConstants.IsPatient)]
        [Route("[controller]")]
        public IActionResult Index() {
            var games = _db.BrainGames.Include(b => b.GameResults);
            var filteredResults = games.Select(b => new BrainGame {
                GameResults = b.GameResults.Where(r => r.Player == User.Identity.Name).ToArray(),
                Name = b.Name,
                Id = b.Id
            });
            var brainGames = filteredResults.Where(b => b.GameResults.Any()).ToArray();
            return View(brainGames);
        }

        [Authorize(Policy = PolicyConstants.IsPatient)]
        [Route("[controller]/details")]
        public IActionResult Details(long id) {
            var gameResult = _db.Results.Include(r => r.BrainGame).First(r => r.Id == id);
            if (gameResult is null) return NotFound();
            if (gameResult.Player != User.Identity.Name) return Unauthorized();

            return View(gameResult);
        }

        [Authorize(Policy = PolicyConstants.CanCreatePatient)]
        [Route("users/{id}/results")]
        public IActionResult Index(string id) {
            var user = _db.Users.Find(id);
            
            var loggedInUser = _db.Users.Include(applicationUser => applicationUser.Patients)
                .First(au => au.UserName == User.Identity.Name);
            
            if (loggedInUser.Patients.Any(p => p.Id == user.Id) == false) {
                return Unauthorized();
            }

            var games = _db.BrainGames.Include(b => b.GameResults);
            var filteredResults = games.Select(b => new BrainGame {
                GameResults = b.GameResults.Where(r => r.Player == user.UserName).ToArray(),
                Name = b.Name,
                Id = b.Id
            });
            var brainGames = filteredResults.Where(b => b.GameResults.Any()).ToArray();
            return View(brainGames);
        }
    }
}