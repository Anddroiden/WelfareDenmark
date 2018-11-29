using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using WelfareDenmark.Data;

namespace WelfareDenmark.APIControllers {
    [Route("api/Results")]
    [ApiController]
    public class GameResultController : ControllerBase {
        private readonly ApplicationDbContext _context;

        public GameResultController(ApplicationDbContext context) {
            _context = context;
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetGameResult([FromRoute] long id) {
            var gameResult = await _context.Results.FindAsync(id);
            if (gameResult == null) return NotFound();
            return Ok(gameResult);
        }
    }
}