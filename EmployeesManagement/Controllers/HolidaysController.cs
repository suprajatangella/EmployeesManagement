using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using EmployeesManagement.Data;
using EmployeesManagement.Models;
using System.Security.Claims;
using EmployeesManagement.ViewModels;

namespace EmployeesManagement.Controllers
{
    public class HolidaysController : Controller
    {
        private readonly ApplicationDbContext _context;

        public HolidaysController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Holidays
        public async Task<IActionResult> Index(HolidayViewModel vm)
        {
            var holidays = _context.Holidays.AsQueryable();

            if(!string.IsNullOrEmpty(vm.Title))
            {
                holidays = holidays.Where(x=>x.Title.Contains(vm.Title));
            }

            if (!string.IsNullOrEmpty(vm.Description))
            {
                holidays = holidays.Where(x => x.Description.Contains(vm.Description));
            }
            if (vm.EndDate!=DateTime.MinValue)
            {
                holidays = holidays.Where(x => x.EndDate.Date==vm.EndDate.Date);
            }
            if (vm.StartDate != DateTime.MinValue)
            {
                holidays = holidays.Where(x => x.StartDate.Date == vm.StartDate.Date);
            }

            vm.Holidays = await holidays.ToListAsync();

            return View(vm);
        }

        // GET: Holidays/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var holiday = await _context.Holidays
                .FirstOrDefaultAsync(m => m.Id == id);
            if (holiday == null)
            {
                return NotFound();
            }

            return View(holiday);
        }

        // GET: Holidays/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Holidays/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Holiday holiday)
        {
            var UserId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            holiday.CreatedById = UserId;
            holiday.CreatedOn = DateTime.Now;
            //if (ModelState.IsValid)
            {
                _context.Holidays.Add(holiday);
                await _context.SaveChangesAsync(UserId);
                return RedirectToAction(nameof(Index));
            }
            //return View(holiday);
        }

        // GET: Holidays/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var holiday = await _context.Holidays.FindAsync(id);
            if (holiday == null)
            {
                return NotFound();
            }
            return View(holiday);
        }

        // POST: Holidays/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id,Holiday holiday)
        {
            if (id != holiday.Id)
            {
                return NotFound();
            }

            //if (ModelState.IsValid)
            {
                try
                {
                    var UserId = User.FindFirstValue(ClaimTypes.NameIdentifier);
                    holiday.ModifiedById = UserId;
                    holiday.ModifiedOn = DateTime.Now;
                    _context.Holidays.Update(holiday);
                    await _context.SaveChangesAsync(UserId);
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!HolidayExists(holiday.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            //return View(holiday);
        }

        // GET: Holidays/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var holiday = await _context.Holidays
                .FirstOrDefaultAsync(m => m.Id == id);
            if (holiday == null)
            {
                return NotFound();
            }

            return View(holiday);
        }

        // POST: Holidays/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var holiday = await _context.Holidays.FindAsync(id);
            if (holiday != null)
            {
                _context.Holidays.Remove(holiday);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool HolidayExists(int id)
        {
            return _context.Holidays.Any(e => e.Id == id);
        }
    }
}
