using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using CalculoDeSalario.Data;
using CalculoDeSalario.Models;

namespace CalculoDeSalario.Controllers
{
    public class TotalCostsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public TotalCostsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: TotalCosts
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.TotalCost.Include(t => t.People);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: TotalCosts/Details/5
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null || _context.TotalCost == null)
            {
                return NotFound();
            }

            var totalCost = await _context.TotalCost
                .Include(t => t.People)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (totalCost == null)
            {
                return NotFound();
            }

            return View(totalCost);
        }

        // GET: TotalCosts/Create
        public IActionResult Create()
        {
            ViewData["PeopleId"] = new SelectList(_context.People, "Id", "Id");
            return View();
        }

        // POST: TotalCosts/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,PeopleId,SalaryCostId,CostDescription")] TotalCost totalCost)
        {
            if (ModelState.IsValid)
            {
                totalCost.Id = Guid.NewGuid();
                _context.Add(totalCost);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["PeopleId"] = new SelectList(_context.People, "Id", "Id", totalCost.PeopleId);
            return View(totalCost);
        }

        // GET: TotalCosts/Edit/5
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null || _context.TotalCost == null)
            {
                return NotFound();
            }

            var totalCost = await _context.TotalCost.FindAsync(id);
            if (totalCost == null)
            {
                return NotFound();
            }
            ViewData["PeopleId"] = new SelectList(_context.People, "Id", "Id", totalCost.PeopleId);
            return View(totalCost);
        }

        // POST: TotalCosts/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, [Bind("Id,PeopleId,SalaryCostId,CostDescription")] TotalCost totalCost)
        {
            if (id != totalCost.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(totalCost);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!TotalCostExists(totalCost.Id))
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
            ViewData["PeopleId"] = new SelectList(_context.People, "Id", "Id", totalCost.PeopleId);
            return View(totalCost);
        }

        // GET: TotalCosts/Delete/5
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null || _context.TotalCost == null)
            {
                return NotFound();
            }

            var totalCost = await _context.TotalCost
                .Include(t => t.People)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (totalCost == null)
            {
                return NotFound();
            }

            return View(totalCost);
        }

        // POST: TotalCosts/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            if (_context.TotalCost == null)
            {
                return Problem("Entity set 'ApplicationDbContext.TotalCost'  is null.");
            }
            var totalCost = await _context.TotalCost.FindAsync(id);
            if (totalCost != null)
            {
                _context.TotalCost.Remove(totalCost);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool TotalCostExists(Guid id)
        {
          return (_context.TotalCost?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
