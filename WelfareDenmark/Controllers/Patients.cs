using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WelfareDenmark.Data;
using WelfareDenmark.Models;

namespace WelfareDenmark.Controllers {
    [Authorize(Policy = PolicyConstants.CanCreatePatient)]
    public class Patients : Controller {
        private readonly ApplicationDbContext _dbContext;
        private readonly UserManager<ApplicationUser> _userManager;

        public Patients(ApplicationDbContext dbContext, UserManager<ApplicationUser> userManager) {
            _dbContext = dbContext;
            _userManager = userManager;
        }

        public async Task<ViewResult> Index() {
            var user = await _userManager.GetUserAsync(User);

            var applicationUser = _dbContext.Users.Include(t => t.Patients).First(u => u.Id == user.Id);
            return View(applicationUser.Patients);
        }
    }
}