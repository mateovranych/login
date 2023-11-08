using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TrabajoFinalProgramacion.Models;

namespace TrabajoFinalProgramacion.Controllers
{
    public class Profesores : Controller
    {
        private readonly DblogContext _context;
        public Profesores(DblogContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            var listaProfesores = _context.Profesores.ToList();
            return View(listaProfesores);
        }

        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Profesores == null)
            {
                return NotFound();
            }

            var profesore = await _context.Profesores
                .FirstOrDefaultAsync(m => m.Id == id);
            if (profesore == null)
            {
                return NotFound();
            }

            return View(profesore);
        }


        public IActionResult Create()
        {
            return View();
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Nombre,Apellido")] Profesore profesores)
        {
            if (ModelState.IsValid)
            {
                _context.Add(profesores);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(profesores);
        }


        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Profesores == null)
            {
                return NotFound();
            }

            var profesores = await _context.Profesores.FindAsync(id);
            if (profesores == null)
            {
                return NotFound();
            }
            return View(profesores);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Nombre,Apellido")] Profesore profesores)
        {
            if (id != profesores.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(profesores);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ProfesoresExists(profesores.Id))
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
            return View(profesores);
        }

        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Profesores == null)
            {
                return NotFound();
            }

            var profesores = await _context.Profesores
                .FirstOrDefaultAsync(m => m.Id == id);
            if (profesores == null)
            {
                return NotFound();
            }

            return View(profesores);
        }


        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Profesores == null)
            {
                return Problem("Entity set 'DblogContext.Profesores'  is null.");
            }
            var profesores = await _context.Profesores.FindAsync(id);

            if (profesores != null)
            {
                _context.Profesores.Remove(profesores);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ProfesoresExists(int id)
        {
            return (_context.Profesores?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}

