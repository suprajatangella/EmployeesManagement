using EmployeesManagement.Data;
using EmployeesManagement.Models;
using EmployeesManagement.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

namespace EmployeesManagement.Controllers
{
    public class ProfilesController : Controller
    {
        private readonly ApplicationDbContext _context;
        public ProfilesController(ApplicationDbContext context)
        {
            _context = context;
        }
        public async Task<ActionResult> Index()
        {
            var tasks = new ProfileViewModel();
            var roles = await _context.Roles.OrderBy(x => x.Name).ToListAsync();
            ViewBag.Roles = new SelectList(roles, "Id", "Name");
            var systemtasks = await _context.SystemProfiles.
                Include("Children.Children.Children")
                //.Where(t=> t.ProfileId == null)
                .OrderBy(x => x.Order)
                .ToListAsync();

            ViewBag.Tasks = new SelectList(systemtasks, "Id", "Name");

            return View(tasks);
        }

        public async Task<ActionResult> AssignRights(ProfileViewModel vm)
        {
            var UserId= User.FindFirstValue(ClaimTypes.NameIdentifier);
            var roles = new RoleProfile
            {
                TaskId = vm.TaskId,
                RoleId = vm.RoleId
            };
            _context.RoleProfiles.Add(roles);
            await _context.SaveChangesAsync(UserId);
            return RedirectToAction("Index");
        }
        [HttpGet]
        public async Task<ActionResult> UserRights(string id)
        {
            var tasks = new ProfileViewModel();
            tasks.RoleId = id;
            tasks.Profiles = await _context.SystemProfiles.
                Include(s=>s.Profile).
                Include("Children.Children.Children")
                .OrderBy(x => x.Order)
                .ToListAsync();

            tasks.RolesProfilesIds= await _context.RoleProfiles.Where(x=>x.RoleId==id).Select(r=> r.TaskId).ToListAsync();

            return View(tasks);
        }

        [HttpPost]
        public async Task<ActionResult> UserGroupRights(string id,ProfileViewModel vm)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            foreach (var taskId in vm.Ids)
            { 
                var role = new RoleProfile
                { 
                    TaskId = taskId,
                    RoleId = id
                };

                _context.RoleProfiles.Add(role);
                await _context.SaveChangesAsync(userId);
            }
            return View(vm);
        }
            
            
    }
}
