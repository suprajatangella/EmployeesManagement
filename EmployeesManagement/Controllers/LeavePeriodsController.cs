﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using EmployeesManagement.Data;
using EmployeesManagement.Models;
using System.Security.Claims;

namespace EmployeesManagement.Controllers
{
    public class LeavePeriodsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public LeavePeriodsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: LeavePeriods
        public async Task<IActionResult> Index()
        {
            return View(await _context.leavePeriods.ToListAsync());
        }

        // GET: LeavePeriods/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var leavePeriod = await _context.leavePeriods
                .FirstOrDefaultAsync(m => m.Id == id);
            if (leavePeriod == null)
            {
                return NotFound();
            }

            return View(leavePeriod);
        }

        // GET: LeavePeriods/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: LeavePeriods/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(LeavePeriod leavePeriod)
        {
                var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
                leavePeriod.CreatedById = userId;
                leavePeriod.CreatedOn = DateTime.Now;
                _context.Add(leavePeriod);
                await _context.SaveChangesAsync(userId);
                return RedirectToAction(nameof(Index));
        }

        // GET: LeavePeriods/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var leavePeriod = await _context.leavePeriods.FindAsync(id);
            if (leavePeriod == null)
            {
                return NotFound();
            }
            return View(leavePeriod);
        }

        // POST: LeavePeriods/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name,Description,StartDate,EndDate,Closed,Locked,CreatedById,CreatedOn,ModifiedById,ModifiedOn")] LeavePeriod leavePeriod)
        {
            if (id != leavePeriod.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(leavePeriod);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!LeavePeriodExists(leavePeriod.Id))
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
            return View(leavePeriod);
        }

        // GET: LeavePeriods/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var leavePeriod = await _context.leavePeriods
                .FirstOrDefaultAsync(m => m.Id == id);
            if (leavePeriod == null)
            {
                return NotFound();
            }

            return View(leavePeriod);
        }

        // POST: LeavePeriods/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var leavePeriod = await _context.leavePeriods.FindAsync(id);
            if (leavePeriod != null)
            {
                _context.leavePeriods.Remove(leavePeriod);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool LeavePeriodExists(int id)
        {
            return _context.leavePeriods.Any(e => e.Id == id);
        }
    }
}
