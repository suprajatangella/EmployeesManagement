using EmployeesManagement.Data;
using EmployeesManagement.Models;
using EmployeesManagement.ViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace EmployeesManagement.Controllers
{
    public class UsersController : Controller
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly ApplicationDbContext _context;

        public UsersController(RoleManager<IdentityRole> roleManager, 
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
            var users= await _context.Users.Include(r=>r.Role).ToListAsync();
            return View(users);
        }

        public async Task<ActionResult> Create()
        {
            ViewData["RoleId"] = new SelectList(_context.Roles, "Id", "Name");
            return View();
        }

        [HttpPost]
        public async Task<ActionResult> Create(UserViewModel model)
        {
            ApplicationUser user=new ApplicationUser();
            user.UserName = model.UserName;

            user.FirstName = model.FirstName;
            user.MiddleName = model.MiddleName;
            user.LastName = model.LastName;
            user.NationalId = model.NationalId;


            user.NormalizedUserName = model.UserName; 
            user.Email = model.Email;
            user.EmailConfirmed = true;
            user.PhoneNumber = model.PhoneNumber;
            user.PhoneNumberConfirmed = true;
            user.CreatedOn= DateTime.Now;
            user.CreatedById = "Test Code";
            user.RoleId=model.RoleId;

            var result = await _userManager.CreateAsync(user, model.Password);
            ViewData["RoleId"] = new SelectList(_context.Roles, "Id", "Name", model.RoleId);
            if (result.Succeeded)
            {
                return RedirectToAction("Index");
            }
            else
            {
                return View(model);
            }
            
        }
    }
}
