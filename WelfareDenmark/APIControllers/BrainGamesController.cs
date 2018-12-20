using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WelfareDenmark.Data;
using WelfareDenmark.Models;

namespace WelfareDenmark.APIControllers {
    [Route("api/BrainGames")]
    [ApiController]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class BrainGamesController : ControllerBase {
        private readonly ApplicationDbContext _context;

        public BrainGamesController(ApplicationDbContext context) {
            _context = context;
        }

        // GET: api/BrainGames
        [HttpGet]
        public IEnumerable<BrainGame> GetBrainGames() {
            return _context.BrainGames;
        }

        // GET: api/BrainGames/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetBrainGame([FromRoute] long id) {

            var brainGame = await _context.BrainGames.FindAsync(id);

            if (brainGame == null) return NotFound();

            return Ok(brainGame);
        }

        // POST: api/BrainGames
        [HttpPost]
        public async Task<IActionResult> PostBrainGame([FromBody] BrainGame brainGame) {

            _context.BrainGames.Add(brainGame);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetBrainGame", new {id = brainGame.Id}, brainGame);
        }

        // DELETE: api/BrainGames/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteBrainGame([FromRoute] long id) {

            var brainGame = await _context.BrainGames.FindAsync(id);
            if (brainGame == null) return NotFound();

            _context.BrainGames.Remove(brainGame);
            await _context.SaveChangesAsync();

            return Ok(brainGame);
        }
    }
}