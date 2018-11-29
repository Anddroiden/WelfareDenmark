using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WelfareDenmark.Data;
using WelfareDenmark.Models;

namespace WelfareDenmark.APIControllers {
    [Route("api/BrainGames")]
    [ApiController]
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
            /* Why do we need this?
             From ms docs
             When ModelState.IsValid evaluates to false in web API controllers using the [ApiController] attribute, 
             an automatic HTTP 400 response containing issue details is returned. For more information.
             */
            if (!ModelState.IsValid) return BadRequest(ModelState);

            var brainGame = await _context.BrainGames.FindAsync(id);

            if (brainGame == null) return NotFound();

            return Ok(brainGame);
        }

        // POST: api/BrainGames
        [HttpPost]
        public async Task<IActionResult> PostBrainGame([FromBody] BrainGame brainGame) {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            _context.BrainGames.Add(brainGame);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetBrainGame", new {id = brainGame.Id}, brainGame);
        }

        // DELETE: api/BrainGames/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteBrainGame([FromRoute] long id) {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            var brainGame = await _context.BrainGames.FindAsync(id);
            if (brainGame == null) return NotFound();

            _context.BrainGames.Remove(brainGame);
            await _context.SaveChangesAsync();

            return Ok(brainGame);
        }

        private bool BrainGameExists(long id) {
            return _context.BrainGames.Any(e => e.Id == id);
        }
    }
}