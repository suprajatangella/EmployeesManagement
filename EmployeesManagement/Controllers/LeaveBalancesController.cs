using EmployeesManagement.Data;
using EmployeesManagement.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

namespace EmployeesManagement.Controllers
{
    public class LeaveBalancesController : Controller
    {
        private readonly ApplicationDbContext _context;
        public LeaveBalancesController(ApplicationDbContext context)
        {
            _context = context;
        }
        public async Task<ActionResult> Index()
        {
            var result = await _context.Employees
                .Include(x => x.Status)
                .ToListAsync();
            return View(result);
        }
        [HttpGet]
        public IActionResult AdjustLeaveBalance(int id)
        {
            LeaveAdjustmentEntry leaveadjustment = new();
            leaveadjustment.EmployeeId = id;
            ViewData["AdjustmentTypeId"] = new SelectList(_context.SystemCodeDetails
                .Include(y=>y.SystemCode)
                .Where(x=>x.SystemCode.Code== "LeaveAdjustment"), "Id", "Description");
            ViewData["EmployeeId"] = new SelectList(_context.Employees, "Id", "FullName", id);
            ViewData["LeavePeriodId"] = new SelectList(_context.leavePeriods.Where(x=>x.Closed==false), "Id", "Name");
            return View(leaveadjustment);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AdjustLeaveBalance(LeaveAdjustmentEntry leaveAdjustmentEntry)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            if (ModelState.IsValid)
            {
                var adjustmenttype = _context.SystemCodeDetails
                .Include(s => s.SystemCode)
                .Where(y => y.SystemCode.Code == "LeaveAdjustment" && y.Id == leaveAdjustmentEntry.AdjustmentTypeId)
                .FirstOrDefault();
                leaveAdjustmentEntry.Id = 0;
                leaveAdjustmentEntry.AdjustmentDescription += "-" + adjustmenttype?.Description;
                _context.Add(leaveAdjustmentEntry);
                await _context.SaveChangesAsync(userId);
                

                var employee = await _context.Employees.FindAsync(leaveAdjustmentEntry.EmployeeId);
                if (adjustmenttype.Code == "Positive")
                {
                    employee.LeaveOutStandingBalance = (employee.AllocatedLeaveDays + leaveAdjustmentEntry.NoOfDays);
                }
                else
                {
                    employee.LeaveOutStandingBalance = (employee.AllocatedLeaveDays - leaveAdjustmentEntry.NoOfDays);
                }
                _context.Update(employee);
                await _context.SaveChangesAsync(userId);
                
            }
            ViewData["AdjustmentTypeId"] = new SelectList(_context.SystemCodeDetails
                .Include(y => y.SystemCode)
                .Where(x => x.SystemCode.Code == "LeaveAdjustment"), "Id", "Description");
            ViewData["EmployeeId"] = new SelectList(_context.Employees, "Id", "FullName", leaveAdjustmentEntry.EmployeeId);
            ViewData["LeavePeriodId"] = new SelectList(_context.leavePeriods.Where(x => x.Closed == false), "Id", "Name",leaveAdjustmentEntry.LeavePeriodId);
            return RedirectToAction(nameof(Index));
        }
    }
}
