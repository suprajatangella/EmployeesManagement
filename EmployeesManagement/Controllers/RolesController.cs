using EmployeesManagement.Data;
using EmployeesManagement.Models;
using EmployeesManagement.ViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace EmployeesManagement.Controllers
{
    public class RolesController : Controller
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly ApplicationDbContext _context;

        public RolesController(RoleManager<IdentityRole> roleManager,
            UserManager<ApplicationUser> userManager,
            SignInManager<ApplicationUser> signInManager, ApplicationDbContext context)
        {
            _userManager = userManager;
            _roleManager = roleManager;
            _signInManager = signInManager;
            _context = context;
        }
        public async Task<ActionResult> Index()
        {
            var roles = await _context.Roles.ToListAsync();
            return View(roles);
        }

        public async Task<ActionResult> Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<ActionResult> Create(RoleViewModel model)
        {
            IdentityRole role = new IdentityRole();
            role.Name = model.RoleName;
            
            var result = await _roleManager.CreateAsync(role);
            if (result.Succeeded)
            {
                return RedirectToAction("Index");
            }
            else
            {
                return View(model);
            }

        }

        public async Task<ActionResult> Edit(string id)
        {
            var role = new RoleViewModel();
            var result = await _roleManager.FindByIdAsync(id);
            role.RoleName = result.Name; 
            role.RoleId = result.Id;
            return View(role);
        }

        [HttpPost]
        public async Task<ActionResult> Edit(string id,RoleViewModel model)
        {
            var checkifexists = await _roleManager.RoleExistsAsync(model.RoleName);
            if (!checkifexists)
            {
                var result = await _roleManager.FindByIdAsync(id);
                result.Name = model.RoleName;

                var fresult = await _roleManager.UpdateAsync(result);
                if (fresult.Succeeded)
                {
                    return RedirectToAction("Index");
                }
                else
                {
                    return View(model);
                }
            }
            return View(model);
        }
    }
}
