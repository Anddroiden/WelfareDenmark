using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WelfareDenmark.Data;
using WelfareDenmark.Models;

namespace WelfareDenmark.APIControllers {
    [Route("api/patients")]
    [ApiController]
    [Authorize(Policy = PolicyConstants.CanCreatePatient, AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]

    public class PatientsController : ControllerBase {
        private readonly ApplicationDbContext _dbContext;
        private readonly UserManager<ApplicationUser> _userManager;

        public PatientsController(ApplicationDbContext dbContext, UserManager<ApplicationUser> userManager) {
            _dbContext = dbContext;
            _userManager = userManager;
        }
        [HttpGet]
        public async Task<IActionResult> GetAllPatientsForConsultant() {
            var user = await _userManager.GetUserAsync(User);

            var applicationUser = _dbContext.Users.Include(u => u.Patients).First(u => u.Id == user.Id);

            var patients = applicationUser.Patients;
            var pr = patients.Select(p => {
                return new {
                    Name = p.UserName,
                    Results = _dbContext.Results.Where(result => result.Player == p.UserName)
                };
            });
            return Ok(pr);
        }
    }
}