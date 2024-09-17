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
    public class ApprovalsUserMatricesController : Controller
    {
        private readonly ApplicationDbContext _context;

        public ApprovalsUserMatricesController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: ApprovalsUserMatrices
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.ApprovalsUserMatrixs.Include(a => a.DocumentType).Include(a => a.User).Include(a => a.WorkFlowUserGroup);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: ApprovalsUserMatrices/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var approvalsUserMatrix = await _context.ApprovalsUserMatrixs
                .Include(a => a.DocumentType)
                .Include(a => a.User)
                .Include(a => a.WorkFlowUserGroup)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (approvalsUserMatrix == null)
            {
                return NotFound();
            }

            return View(approvalsUserMatrix);
        }

        // GET: ApprovalsUserMatrices/Create
        public IActionResult Create()
        {
            ViewData["DocumentTypeId"] = new SelectList(_context.SystemCodeDetails.Include(x=>x.SystemCode).Where(x=>x.SystemCode.Code=="DocumentTypes"), "Id", "Description");
            ViewData["UserId"] = new SelectList(_context.Users, "Id", "FullName");
            ViewData["WorkFlowUserGroupId"] = new SelectList(_context.WorkFlowUserGroups, "Id", "Description");
            return View();
        }

        // POST: ApprovalsUserMatrices/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(ApprovalsUserMatrix approvalsUserMatrix)
        {
            //if (ModelState.IsValid)
            {
                var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
                approvalsUserMatrix.CreatedById = userId;
                approvalsUserMatrix.CreatedOn = DateTime.Now;
                _context.Add(approvalsUserMatrix);
                await _context.SaveChangesAsync(userId);
                ViewData["DocumentTypeId"] = new SelectList(_context.SystemCodeDetails.Include(x => x.SystemCode).Where(x => x.SystemCode.Code == "DocumentTypes"), "Id", "Description", approvalsUserMatrix.DocumentTypeId);
                ViewData["UserId"] = new SelectList(_context.Users, "Id", "FullName", approvalsUserMatrix.UserId);
                ViewData["WorkFlowUserGroupId"] = new SelectList(_context.WorkFlowUserGroups, "Id", "Description", approvalsUserMatrix.WorkFlowUserGroupId);
                return RedirectToAction(nameof(Index));
            }
            
            //return View(approvalsUserMatrix);
        }

        // GET: ApprovalsUserMatrices/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var approvalsUserMatrix = await _context.ApprovalsUserMatrixs.FindAsync(id);
            if (approvalsUserMatrix == null)
            {
                return NotFound();
            }
            ViewData["DocumentTypeId"] = new SelectList(_context.SystemCodeDetails.Include(x => x.SystemCode).Where(x => x.SystemCode.Code == "DocumentTypes"), "Id", "Description", approvalsUserMatrix.DocumentTypeId);
            ViewData["UserId"] = new SelectList(_context.Users, "Id", "FullName", approvalsUserMatrix.UserId);
            ViewData["WorkFlowUserGroupId"] = new SelectList(_context.WorkFlowUserGroups, "Id", "Description", approvalsUserMatrix.WorkFlowUserGroupId);
            return View(approvalsUserMatrix);
        }

        // POST: ApprovalsUserMatrices/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,UserId,DocumentTypeId,WorkFlowUserGroupId,IsActive,CreatedById,CreatedOn,ModifiedById,ModifiedOn")] ApprovalsUserMatrix approvalsUserMatrix)
        {
            if (id != approvalsUserMatrix.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(approvalsUserMatrix);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ApprovalsUserMatrixExists(approvalsUserMatrix.Id))
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
            ViewData["DocumentTypeId"] = new SelectList(_context.SystemCodeDetails.Include(x => x.SystemCode).Where(x => x.SystemCode.Code == "DocumentTypes"), "Id", "Description", approvalsUserMatrix.DocumentTypeId);
            ViewData["UserId"] = new SelectList(_context.Users, "Id", "FullName", approvalsUserMatrix.UserId);
            ViewData["WorkFlowUserGroupId"] = new SelectList(_context.WorkFlowUserGroups, "Id", "Description", approvalsUserMatrix.WorkFlowUserGroupId);
            return View(approvalsUserMatrix);
        }

        // GET: ApprovalsUserMatrices/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var approvalsUserMatrix = await _context.ApprovalsUserMatrixs
                .Include(a => a.DocumentType)
                .Include(a => a.User)
                .Include(a => a.WorkFlowUserGroup)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (approvalsUserMatrix == null)
            {
                return NotFound();
            }

            return View(approvalsUserMatrix);
        }

        // POST: ApprovalsUserMatrices/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var approvalsUserMatrix = await _context.ApprovalsUserMatrixs.FindAsync(id);
            if (approvalsUserMatrix != null)
            {
                _context.ApprovalsUserMatrixs.Remove(approvalsUserMatrix);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ApprovalsUserMatrixExists(int id)
        {
            return _context.ApprovalsUserMatrixs.Any(e => e.Id == id);
        }
    }
}
