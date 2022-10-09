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
    public class InsuranceEventsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public InsuranceEventsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: InsuranceEvents
        [AllowAnonymous]
        public async Task<IActionResult> Index(int? searchId)
        {
            var applicationDbContext = from ie in _context.InsuranceEvent.Include(i => i.Insurance).Include(i => i.Policyholder)
                                       select ie;
            if (searchId != null)
            {
                applicationDbContext = applicationDbContext.Where(i => i.ID == searchId);
            }
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: InsuranceEvents/Details/5
        [AllowAnonymous]
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.InsuranceEvent == null)
            {
                return NotFound();
            }

            var insuranceEvent = await _context.InsuranceEvent
                .Include(i => i.Insurance)
                .Include(i => i.Policyholder)
                .FirstOrDefaultAsync(m => m.ID == id);
            if (insuranceEvent == null)
            {
                return NotFound();
            }

            return View(insuranceEvent);
        }

        // GET: InsuranceEvents/Create
        [AllowAnonymous]
        public IActionResult Create()
        {
            ViewData["InsuranceID"] = new SelectList(_context.Insurance, "ID", "ID");
            ViewData["PolicyholderID"] = new SelectList(_context.Policyholder, "ID", "ID");
            return View();
        }

        // POST: InsuranceEvents/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [AllowAnonymous]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ID,Description,DateOfEvent,Payout,PolicyholderID,InsuranceID")] InsuranceEvent insuranceEvent)
        {
            if (ModelState.IsValid)
            {
                _context.Add(insuranceEvent);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["InsuranceID"] = new SelectList(_context.Insurance, "ID", "ID", insuranceEvent.InsuranceID);
            ViewData["PolicyholderID"] = new SelectList(_context.Policyholder, "ID", "ID", insuranceEvent.PolicyholderID);
            return View(insuranceEvent);
        }

        // GET: InsuranceEvents/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.InsuranceEvent == null)
            {
                return NotFound();
            }

            var insuranceEvent = await _context.InsuranceEvent.FindAsync(id);
            if (insuranceEvent == null)
            {
                return NotFound();
            }
            ViewData["InsuranceID"] = new SelectList(_context.Insurance, "ID", "ID", insuranceEvent.InsuranceID);
            ViewData["PolicyholderID"] = new SelectList(_context.Policyholder, "ID", "ID", insuranceEvent.PolicyholderID);
            return View(insuranceEvent);
        }

        // POST: InsuranceEvents/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ID,Description,DateOfEvent,Payout,PolicyholderID,InsuranceID")] InsuranceEvent insuranceEvent)
        {
            if (id != insuranceEvent.ID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(insuranceEvent);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!InsuranceEventExists(insuranceEvent.ID))
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
            ViewData["InsuranceID"] = new SelectList(_context.Insurance, "ID", "ID", insuranceEvent.InsuranceID);
            ViewData["PolicyholderID"] = new SelectList(_context.Policyholder, "ID", "ID", insuranceEvent.PolicyholderID);
            return View(insuranceEvent);
        }

        // GET: InsuranceEvents/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.InsuranceEvent == null)
            {
                return NotFound();
            }

            var insuranceEvent = await _context.InsuranceEvent
                .Include(i => i.Insurance)
                .Include(i => i.Policyholder)
                .FirstOrDefaultAsync(m => m.ID == id);
            if (insuranceEvent == null)
            {
                return NotFound();
            }

            return View(insuranceEvent);
        }

        // POST: InsuranceEvents/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.InsuranceEvent == null)
            {
                return Problem("Entity set 'ApplicationDbContext.InsuranceEvent'  is null.");
            }
            var insuranceEvent = await _context.InsuranceEvent.FindAsync(id);
            if (insuranceEvent != null)
            {
                _context.InsuranceEvent.Remove(insuranceEvent);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool InsuranceEventExists(int id)
        {
          return _context.InsuranceEvent.Any(e => e.ID == id);
        }
    }
}
