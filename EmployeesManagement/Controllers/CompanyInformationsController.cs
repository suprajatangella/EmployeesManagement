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

namespace EmployeesManagement.Controllers
{
    public class CompanyInformationsController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly IConfiguration _configuration;
        public CompanyInformationsController(ApplicationDbContext context, IConfiguration configuration)
        {
            _context = context;
            _configuration = configuration;
        }

        // GET: CompanyInformations
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.CompanyInformation.Include(c => c.City).Include(c => c.Country);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: CompanyInformations/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var companyInformation = await _context.CompanyInformation
                .Include(c => c.City)
                .Include(c => c.Country)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (companyInformation == null)
            {
                return NotFound();
            }

            return View(companyInformation);
        }

        // GET: CompanyInformations/Create
        public IActionResult Create()
        {
            ViewData["CityId"] = new SelectList(_context.Cities, "Id", "Name");
            ViewData["CountryId"] = new SelectList(_context.Countries, "Id", "Name");
            return View();
        }

        // POST: CompanyInformations/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(CompanyInformation companyInformation, IFormFile logo)
        {
            if (logo.Length > 0)
            {
                var filename = "CompanyLogo_" + DateTime.Now.ToString("yyyymmddhhmmss") + "_" + logo.FileName;
                var path = _configuration["Filesettings:UploadFolder"];
                var filepath = Path.Combine(path, filename);
                var stream = new FileStream(filepath, FileMode.Create);
                await logo.CopyToAsync(stream);
                companyInformation.Logo = filename;
            }

            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            companyInformation.CreatedById = userId;
            companyInformation.CreatedOn = DateTime.Now;

            _context.Add(companyInformation);
            await _context.SaveChangesAsync(userId);
            ViewData["CityId"] = new SelectList(_context.Cities, "Id", "Name", companyInformation.CityId);
            ViewData["CountryId"] = new SelectList(_context.Countries, "Id", "Name", companyInformation.CountryId);
            return RedirectToAction(nameof(Index));
            
           // return View(companyInformation);
        }

        // GET: CompanyInformations/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var companyInformation = await _context.CompanyInformation.FindAsync(id);
            if (companyInformation == null)
            {
                return NotFound();
            }
            ViewData["CityId"] = new SelectList(_context.Cities, "Id", "Name", companyInformation.CityId);
            ViewData["CountryId"] = new SelectList(_context.Countries, "Id", "Name", companyInformation.CountryId);
            return View(companyInformation);
        }

        // POST: CompanyInformations/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, CompanyInformation companyInformation)
        {
            if (id != companyInformation.Id)
            {
                return NotFound();
            }

            //if (ModelState.IsValid)
            {
                try
                {
                    var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
                    companyInformation.ModifiedById = userId;
                    companyInformation.ModifiedOn = DateTime.Now;
                    _context.Update(companyInformation);
                    await _context.SaveChangesAsync(userId);
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CompanyInformationExists(companyInformation.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                ViewData["CityId"] = new SelectList(_context.Cities, "Id", "Name", companyInformation.CityId);
                ViewData["CountryId"] = new SelectList(_context.Countries, "Id", "Name", companyInformation.CountryId);
                return RedirectToAction(nameof(Index));
            }
            
           // return View(companyInformation);
        }

        // GET: CompanyInformations/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var companyInformation = await _context.CompanyInformation
                .Include(c => c.City)
                .Include(c => c.Country)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (companyInformation == null)
            {
                return NotFound();
            }

            return View(companyInformation);
        }

        // POST: CompanyInformations/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var companyInformation = await _context.CompanyInformation.FindAsync(id);
            if (companyInformation != null)
            {
                _context.CompanyInformation.Remove(companyInformation);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool CompanyInformationExists(int id)
        {
            return _context.CompanyInformation.Any(e => e.Id == id);
        }
    }
}
