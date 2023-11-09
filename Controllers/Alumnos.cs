using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

using TrabajoFinalProgramacion.Models;

namespace TrabajoFinalProgramacion.Controllers
{
    public class Alumnos : Controller
    {
        private readonly DblogContext _context;

        public Alumnos(DblogContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            var listaAlumnos = _context.Alumnos.ToList();
            return View(listaAlumnos);
        }

        

        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Alumnos == null)
            {
                return NotFound();
            }

            var alumnos = await _context.Alumnos
                .FirstOrDefaultAsync(m => m.Id == id);
            if (alumnos == null)
            {
                return NotFound();
            }

            return View(alumnos);
        }


        public IActionResult Create()
        {
            return View();
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Nombre,Dni")] Alumno alumnos)
        {
            if (ModelState.IsValid)
            {
                _context.Add(alumnos);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(alumnos);
        }


        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Alumnos == null)
            {
                return NotFound();
            }

            var alumnos = await _context.Alumnos.FindAsync(id);
            if (alumnos == null)
            {
                return NotFound();
            }
            return View(alumnos);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Nombre,Dni")] Alumno alumnos)
        {
            if (id != alumnos.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(alumnos);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!AlumnosExists(alumnos.Id))
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
            return View(alumnos);
        }

        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Alumnos == null)
            {
                return NotFound();
            }

            var alumnos = await _context.Alumnos
                .FirstOrDefaultAsync(m => m.Id == id);
            if (alumnos == null)
            {
                return NotFound();
            }

            return View(alumnos);
        }


        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Alumnos == null)
            {
                return Problem("Entity set 'DblogContext.Alumnos'  is null.");
            }
            var alumnos = await _context.Alumnos.FindAsync(id);
            if (alumnos != null)
            {
                _context.Alumnos.Remove(alumnos);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool AlumnosExists(int id)
        {
            return (_context.Alumnos?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
