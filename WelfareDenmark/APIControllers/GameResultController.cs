using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WelfareDenmark.Data;
using WelfareDenmark.Models;

namespace WelfareDenmark.APIControllers
{
    [Route("api/Results")]
    [ApiController]
    public class GameResultController: ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public GameResultController(ApplicationDbContext context)
        {
            _context = context;
        }
        [HttpGet("{id}")]
        public async Task<IActionResult> GetGameResult([FromRoute] long id)
        {
            var gameResult = await _context.Results.FindAsync(id);
            if (gameResult == null)
            {
                return NotFound();
            }
            return Ok(gameResult);
        }
    }
}
