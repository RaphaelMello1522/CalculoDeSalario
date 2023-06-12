using DataAccess.Data;
using Domain.Entities;
using Domain.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using NToastNotify;

namespace CalculoDeSalario.Controllers
{
    public class PeopleController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly IUnitOfWork unitOfWork;
        private readonly IWebHostEnvironment webHostEnvironment;
        private IToastNotification _toastNotification;


        public PeopleController(ApplicationDbContext context, IWebHostEnvironment webHostEnvironment, IToastNotification toastNotification, IUnitOfWork unitOfWork)
        {
            _context = context;
            this.webHostEnvironment = webHostEnvironment;
            _toastNotification = toastNotification;
            this.unitOfWork = unitOfWork;
        }

        // GET: People
        public async Task<IActionResult> Index()
        {
            var contextCount = unitOfWork.People.GetAll().Count();
            if (contextCount == 0)
            {
                _toastNotification.AddWarningToastMessage("Nenhum funcionário registrado");
                return View(unitOfWork.People.GetAll().AsEnumerable());
            }

            _toastNotification.AddWarningToastMessage(contextCount + " Funcionários registrados");

            return unitOfWork.People != null ?
                        View(unitOfWork.People.GetAll().AsEnumerable()) :
                        Problem("Entity set 'ApplicationDbContext.People'  is null.");
        }

        // GET: People/Details/5
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null || unitOfWork.People  == null && unitOfWork.Cargo == null)
            {
                return NotFound();
            }

            var people = await unitOfWork.People.GetPeopleDetails(id);
                

            if (people.Sexo == "Feminino")
            {
                _toastNotification.AddInfoToastMessage("Detalhes da funcionária " + people.Name);
            }
            else
            {
                _toastNotification.AddInfoToastMessage("Detalhes do funcionário " + people.Name);
            }

            return people == null ? NotFound() : View(people);
        }

        // GET: People/Create
        public IActionResult Create()
        {
            PopulateSelect();
            return View();
        }

        // POST: People/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(People people)
        {
            people.Id = Guid.NewGuid();

            var cargoPerson = _context.Cargo.ToList().Where(s => s.Id.Equals(people.CargoId));

            string uniqueFileName = UploadedFile(people);

            people.PictureSource = uniqueFileName;

            _context.Add(people);
            await _context.SaveChangesAsync();

            if(people.Sexo == "Feminino")
            {
                _toastNotification.AddSuccessToastMessage("Funcionária " + people.Name + " registrada com sucesso!");
            }
            else
            {
                _toastNotification.AddSuccessToastMessage("Funcionário " + people.Name + " registrado com sucesso!");
            }

            return RedirectToAction(nameof(Index));

        }

        // GET: People/Edit/5
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null || _context.People == null)
            {
                return NotFound();
            }

            var people = await _context.People.FindAsync(id);
            if (people == null)
            {
                return NotFound();
            }
            return View(people);
        }

        // POST: People/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, [Bind("Id,Name,ValueHour,PictureSource")] People people)
        {
            if (id != people.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(people);
                    await _context.SaveChangesAsync();
                }
                catch (Microsoft.EntityFrameworkCore.DbUpdateConcurrencyException)
                {
                    if (!PeopleExists(people.Id))
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
            return View(people);
        }

        // GET: People/Delete/5
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null || _context.People == null)
            {
                return NotFound();
            }

            var people = await _context.People
                .FirstOrDefaultAsync(m => m.Id == id);
            if (people == null)
            {
                return NotFound();
            }

            return View(people);
        }

        // POST: People/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            if (_context.People == null)
            {
                return Problem("Entity set 'ApplicationDbContext.People'  is null.");
            }
            var people = await _context.People.FindAsync(id);
            if (people != null)
            {
                _context.People.Remove(people);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool PeopleExists(Guid id)
        {
            return (_context.People?.Any(e => e.Id == id)).GetValueOrDefault();
        }

        private string UploadedFile(People model)
        {
            string uniqueFileName = null;

            if (model.ProfilePicture != null)
            {
                string uploadsFolder = Path.Combine(webHostEnvironment.WebRootPath, "images");
                uniqueFileName = model.ProfilePicture.FileName;
                string filePath = Path.Combine(uploadsFolder, uniqueFileName);
                using (var fileStream = new FileStream(filePath, FileMode.Create))
                {
                    model.ProfilePicture.CopyTo(fileStream);
                }
            }
            return uniqueFileName;
        }

        private void PopulateSelect(object? CargoSelecionado = null)
        {
            var cargoQuery = unitOfWork.Cargo.GetAll().AsEnumerable();

            ViewBag.CargoId = new SelectList(cargoQuery, "Id", "NomeCargo", CargoSelecionado);
            return;
        }
    }
}
