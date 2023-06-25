using CalculoDeSalario.Repository.IRepository;
using DataAccess.Data;
using Domain.Entities;
using Domain.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace CalculoDeSalario.Controllers
{
    public class SalariesController : Controller
    {
        private readonly IUnitOfWork unitOfWork;
        private ApplicationDbContext context;

        public SalariesController(ApplicationDbContext context, IUnitOfWork unitOfWork)
        {
            this.context = context;
            this.unitOfWork = unitOfWork;
        }

        // GET: Salaries
        public async Task<IActionResult> Index()
        {
            var buscarSalarios = unitOfWork.Salary.GetSalaries();
            return View(buscarSalarios);
        }

        //GET: Salaries/Details/5
        public async Task<IActionResult> Details(Guid id)
        {
            if (unitOfWork.Salary == null)
            {
                return NotFound();
            }

            var salary = unitOfWork.Salary.Find(s => s.Id == id);
            if (salary == null)
            {
                return NotFound();
            }

            return View(salary);
        }

        // GET: Salaries/Create
        public IActionResult Create()
        {
            PopulateAvaliado();

            return View();
        }

        // POST: Salaries/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Salary salary)
        {
            salary.Id = Guid.NewGuid();
            salary.TotalTimeWorked = salary.TimeWorkEnd - salary.TimeWorkStart;
            var salaryPerson = unitOfWork.People.GetPeopleWtithCargo().Where(s => s.Id.Equals(salary.PeopleId));

            foreach (var item in salaryPerson)
            {
                salary.Total = salary.TotalTimeWorked.TotalHours * Convert.ToDouble(item.Cargo.ValueHour);
            }

            unitOfWork.Salary.Add(salary);
            unitOfWork.Save();
            return RedirectToAction(nameof(Index));

        }

        //// GET: Salaries/Edit/5
        //public async Task<IActionResult> Edit(int? id)
        //{
        //    if (id == null || _context.Salary == null)
        //    {
        //        return NotFound();
        //    }

        //    var salary = await _context.Salary.FindAsync(id);
        //    if (salary == null)
        //    {
        //        return NotFound();
        //    }
        //    return View(salary);
        //}

        //// POST: Salaries/Edit/5
        //// To protect from overposting attacks, enable the specific properties you want to bind to.
        //// For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public async Task<IActionResult> Edit(int id, [Bind("Id,ValueHour")] Salary salary)
        //{
        //    if (id != salary.Id)
        //    {
        //        return NotFound();
        //    }

        //    if (ModelState.IsValid)
        //    {
        //        try
        //        {
        //            _context.Update(salary);
        //            await _context.SaveChangesAsync();
        //        }
        //        catch (DbUpdateConcurrencyException)
        //        {
        //            if (!SalaryExists(salary.Id))
        //            {
        //                return NotFound();
        //            }
        //            else
        //            {
        //                throw;
        //            }
        //        }
        //        return RedirectToAction(nameof(Index));
        //    }
        //    return View(salary);
        //}

        //// GET: Salaries/Delete/5
        //public async Task<IActionResult> Delete(int? id)
        //{
        //    if (id == null || _context.Salary == null)
        //    {
        //        return NotFound();
        //    }

        //    var salary = await _context.Salary
        //        .FirstOrDefaultAsync(m => m.Id == id);
        //    if (salary == null)
        //    {
        //        return NotFound();
        //    }

        //    return View(salary);
        //}

        //// POST: Salaries/Delete/5
        //[HttpPost, ActionName("Delete")]
        //[ValidateAntiForgeryToken]
        //public async Task<IActionResult> DeleteConfirmed(int id)
        //{
        //    if (_context.Salary == null)
        //    {
        //        return Problem("Entity set 'ApplicationDbContext.Salary'  is null.");
        //    }
        //    var salary = await _context.Salary.FindAsync(id);
        //    if (salary != null)
        //    {
        //        _context.Salary.Remove(salary);
        //    }

        //    await _context.SaveChangesAsync();
        //    return RedirectToAction(nameof(Index));
        //}

        //private bool SalaryExists(int id)
        //{
        //    return (_context.Salary?.Any(e => e.Id == id)).GetValueOrDefault();
        //}

        private void PopulateAvaliado(object PessoaSelecionada = null)
        {
            var pessoaQuery = unitOfWork.People.GetAll();

            ViewBag.PessoaAvaliadoId = new SelectList(pessoaQuery, "Id", "Name", PessoaSelecionada);
            return;
        }
    }
}
