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
    public class PolicyholdersController : Controller
    {
        private readonly ApplicationDbContext _context;

        public PolicyholdersController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Policyholders
        [AllowAnonymous]
        public async Task<IActionResult> Index(string searchName)
        {
            var policyholder = from p in _context.Policyholder
                               select p;
            if (!string.IsNullOrEmpty(searchName) || !string.IsNullOrEmpty(searchName))
            {
                policyholder = policyholder.Where(p => p.FirstName!.Contains(searchName) || p.LastName!.Contains(searchName));
            }
              return View(await policyholder.ToListAsync());
        }

        // GET: Policyholders/Details/5
        [AllowAnonymous]
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Policyholder == null)
            {
                return NotFound();
            }

            var policyholder = await _context.Policyholder.Include(i => i.Insurances)
                .FirstOrDefaultAsync(m => m.ID == id);
            if (policyholder == null)
            {
                return NotFound();
            }

            return View(policyholder);
        }

        // GET: Policyholders/Create
        [AllowAnonymous]
        public IActionResult Create()
        {
            return View();
        }

        // POST: Policyholders/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [AllowAnonymous]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ID,FirstName,LastName,Email,PhoneNumber,Address,City,ZIP")] Policyholder policyholder)
        {
            if (ModelState.IsValid)
            {
                _context.Add(policyholder);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(policyholder);
        }

        // GET: Policyholders/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Policyholder == null)
            {
                return NotFound();
            }

            var policyholder = await _context.Policyholder.FindAsync(id);
            if (policyholder == null)
            {
                return NotFound();
            }
            return View(policyholder);
        }

        // POST: Policyholders/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ID,FirstName,LastName,Email,PhoneNumber,Address,City,ZIP")] Policyholder policyholder)
        {
            if (id != policyholder.ID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(policyholder);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PolicyholderExists(policyholder.ID))
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
            return View(policyholder);
        }

        // GET: Policyholders/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Policyholder == null)
            {
                return NotFound();
            }

            var policyholder = await _context.Policyholder
                .FirstOrDefaultAsync(m => m.ID == id);
            if (policyholder == null)
            {
                return NotFound();
            }

            return View(policyholder);
        }

        // POST: Policyholders/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Policyholder == null)
            {
                return Problem("Entity set 'ApplicationDbContext.Policyholder'  is null.");
            }
            var policyholder = await _context.Policyholder.FindAsync(id);
            if (policyholder != null)
            {
                _context.Policyholder.Remove(policyholder);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool PolicyholderExists(int id)
        {
          return _context.Policyholder.Any(e => e.ID == id);
        }
    }
}
