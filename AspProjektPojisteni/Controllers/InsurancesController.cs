using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using AspProjektPojisteni.Data;
using AspProjektPojisteni.Models;
using Microsoft.AspNetCore.Authorization;
using System.Data;

namespace AspProjektPojisteni.Controllers
{
    [Authorize(Roles = "admin")]
    public class InsurancesController : Controller
    {
        private readonly ApplicationDbContext _context;

        public InsurancesController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Insurances
        [AllowAnonymous]
        public async Task<IActionResult> Index(int? searchId)
        {
            var applicationDbContext = from ins in _context.Insurance.Include(i => i.Policyholder)
                                       select ins;
            if (searchId != null)
            {
                applicationDbContext = applicationDbContext.Where(a => a.ID == searchId);
            }
                return View(await applicationDbContext.ToListAsync());
        }

        // GET: Insurances/Details/5
        [AllowAnonymous]
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Insurance == null)
            {
                return NotFound();
            }

            var insurance = await _context.Insurance
                .Include(i => i.Policyholder).Include(e => e.InsuranceEvents)
                .FirstOrDefaultAsync(m => m.ID == id);
            if (insurance == null)
            {
                return NotFound();
            }

            return View(insurance);
        }

        // GET: Insurances/Create
        [AllowAnonymous]
        public IActionResult Create()
        {
            ViewData["PolicyholderID"] = new SelectList(_context.Policyholder, "ID", "ID");
            return View();
        }

        // POST: Insurances/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [AllowAnonymous]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ID,Name,InsuranceRate,InsuranceStart,InsuranceEnd,PolicyholderID")] Insurance insurance)
        {
            if (ModelState.IsValid)
            {
                _context.Add(insurance);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["PolicyholderID"] = new SelectList(_context.Policyholder, "ID", "ID", insurance.PolicyholderID);
            return View(insurance);
        }

        // GET: Insurances/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Insurance == null)
            {
                return NotFound();
            }

            var insurance = await _context.Insurance.FindAsync(id);
            if (insurance == null)
            {
                return NotFound();
            }
            ViewData["PolicyholderID"] = new SelectList(_context.Policyholder, "ID", "ID", insurance.PolicyholderID);
            return View(insurance);
        }

        // POST: Insurances/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ID,Name,InsuranceRate,InsuranceStart,InsuranceEnd,PolicyholderID")] Insurance insurance)
        {
            if (id != insurance.ID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(insurance);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!InsuranceExists(insurance.ID))
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
            ViewData["PolicyholderID"] = new SelectList(_context.Policyholder, "ID", "ID", insurance.PolicyholderID);
            return View(insurance);
        }

        // GET: Insurances/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Insurance == null)
            {
                return NotFound();
            }

            var insurance = await _context.Insurance
                .Include(i => i.Policyholder)
                .FirstOrDefaultAsync(m => m.ID == id);
            if (insurance == null)
            {
                return NotFound();
            }

            return View(insurance);
        }

        // POST: Insurances/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Insurance == null)
            {
                return Problem("Entity set 'ApplicationDbContext.Insurance'  is null.");
            }
            var insurance = await _context.Insurance.FindAsync(id);
            if (insurance != null)
            {
                _context.Insurance.Remove(insurance);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool InsuranceExists(int id)
        {
          return _context.Insurance.Any(e => e.ID == id);
        }
    }
}
