using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DataAccess.Data;
using Domain.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace CalculoDeSalario.Controllers
{
    public class VagasController : Controller
    {
        private readonly ApplicationDbContext _context;

        public VagasController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Vagas
        public async Task<IActionResult> Index()
        {
              return _context.Vagas != null ? 
                          View(await _context.Vagas.ToListAsync()) :
                          Problem("Entity set 'ApplicationDbContext.Vagas'  is null.");
        }

        // GET: Vagas/Details/5
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null || _context.Vagas == null)
            {
                return NotFound();
            }

            var vagas = await _context.Vagas
                .FirstOrDefaultAsync(m => m.Id == id);
            if (vagas == null)
            {
                return NotFound();
            }

            return View(vagas);
        }

        // GET: Vagas/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Vagas/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,NomeVaga,DescricaoVaga,Salario,CardImgUrl")] Vagas vagas)
        {
            if (ModelState.IsValid)
            {
                vagas.Id = Guid.NewGuid();
                _context.Add(vagas);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(vagas);
        }

        // GET: Vagas/Edit/5
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null || _context.Vagas == null)
            {
                return NotFound();
            }

            var vagas = await _context.Vagas.FindAsync(id);
            if (vagas == null)
            {
                return NotFound();
            }
            return View(vagas);
        }

        // POST: Vagas/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, [Bind("Id,NomeVaga,DescricaoVaga,Salario,CardImgUrl")] Vagas vagas)
        {
            if (id != vagas.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(vagas);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!VagasExists(vagas.Id))
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
            return View(vagas);
        }

        // GET: Vagas/Delete/5
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null || _context.Vagas == null)
            {
                return NotFound();
            }

            var vagas = await _context.Vagas
                .FirstOrDefaultAsync(m => m.Id == id);
            if (vagas == null)
            {
                return NotFound();
            }

            return View(vagas);
        }

        // POST: Vagas/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            if (_context.Vagas == null)
            {
                return Problem("Entity set 'ApplicationDbContext.Vagas'  is null.");
            }
            var vagas = await _context.Vagas.FindAsync(id);
            if (vagas != null)
            {
                _context.Vagas.Remove(vagas);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool VagasExists(Guid id)
        {
          return (_context.Vagas?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
