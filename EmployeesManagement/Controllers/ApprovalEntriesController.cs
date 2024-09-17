using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using EmployeesManagement.Data;
using EmployeesManagement.Models;

namespace EmployeesManagement.Controllers
{
    public class ApprovalEntriesController : Controller
    {
        private readonly ApplicationDbContext _context;

        public ApprovalEntriesController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: ApprovalEntries
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.approvalEntries.Include(a => a.Approver).Include(a => a.DocumentType).Include(a => a.Status);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: ApprovalEntries/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var approvalEntry = await _context.approvalEntries
                .Include(a => a.Approver)
                .Include(a => a.DocumentType)
                .Include(a => a.Status)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (approvalEntry == null)
            {
                return NotFound();
            }

            return View(approvalEntry);
        }

        // GET: ApprovalEntries/Create
        public IActionResult Create()
        {
            ViewData["ApproverId"] = new SelectList(_context.Users, "Id", "Id");
            ViewData["DocumentTypeId"] = new SelectList(_context.SystemCodeDetails, "Id", "Id");
            ViewData["statusId"] = new SelectList(_context.SystemCodeDetails, "Id", "Id");
            return View();
        }

        // POST: ApprovalEntries/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,RecordId,DocumentTypeId,SequenceNo,ApproverId,statusId,DateSentForApproval,LastModifiedOn,LastModifiedBy,LastModifiedById,CreatedById,CreatedOn,ModifiedById,ModifiedOn")] ApprovalEntry approvalEntry)
        {
            if (ModelState.IsValid)
            {
                _context.Add(approvalEntry);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["ApproverId"] = new SelectList(_context.Users, "Id", "Id", approvalEntry.ApproverId);
            ViewData["DocumentTypeId"] = new SelectList(_context.SystemCodeDetails, "Id", "Id", approvalEntry.DocumentTypeId);
            ViewData["statusId"] = new SelectList(_context.SystemCodeDetails, "Id", "Id", approvalEntry.statusId);
            return View(approvalEntry);
        }

        // GET: ApprovalEntries/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var approvalEntry = await _context.approvalEntries.FindAsync(id);
            if (approvalEntry == null)
            {
                return NotFound();
            }
            ViewData["ApproverId"] = new SelectList(_context.Users, "Id", "Id", approvalEntry.ApproverId);
            ViewData["DocumentTypeId"] = new SelectList(_context.SystemCodeDetails, "Id", "Id", approvalEntry.DocumentTypeId);
            ViewData["statusId"] = new SelectList(_context.SystemCodeDetails, "Id", "Id", approvalEntry.statusId);
            return View(approvalEntry);
        }

        // POST: ApprovalEntries/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,RecordId,DocumentTypeId,SequenceNo,ApproverId,statusId,DateSentForApproval,LastModifiedOn,LastModifiedBy,LastModifiedById,CreatedById,CreatedOn,ModifiedById,ModifiedOn")] ApprovalEntry approvalEntry)
        {
            if (id != approvalEntry.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(approvalEntry);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ApprovalEntryExists(approvalEntry.Id))
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
            ViewData["ApproverId"] = new SelectList(_context.Users, "Id", "Id", approvalEntry.ApproverId);
            ViewData["DocumentTypeId"] = new SelectList(_context.SystemCodeDetails, "Id", "Id", approvalEntry.DocumentTypeId);
            ViewData["statusId"] = new SelectList(_context.SystemCodeDetails, "Id", "Id", approvalEntry.statusId);
            return View(approvalEntry);
        }

        // GET: ApprovalEntries/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var approvalEntry = await _context.approvalEntries
                .Include(a => a.Approver)
                .Include(a => a.DocumentType)
                .Include(a => a.Status)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (approvalEntry == null)
            {
                return NotFound();
            }

            return View(approvalEntry);
        }

        // POST: ApprovalEntries/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var approvalEntry = await _context.approvalEntries.FindAsync(id);
            if (approvalEntry != null)
            {
                _context.approvalEntries.Remove(approvalEntry);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ApprovalEntryExists(int id)
        {
            return _context.approvalEntries.Any(e => e.Id == id);
        }
    }
}
