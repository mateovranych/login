using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

using PrograTF3.Models;

namespace PrograTF3.Controllers
{
    [Authorize(Policy = "RequiereAutenticacion")]
    public class Alumnos : Controller
    {
        private readonly DblogContext _context;

        public Alumnos(DblogContext context)
        {
            _context = context;
        }


        public async Task<IActionResult> Index()
        {
               
                var alumno = await _context.Alumnos.FirstOrDefaultAsync();                
                TempData["indice"] = 0;
                return View(alumno);

           
        }

        public async Task<IActionResult> Next()
        {
            
                ViewBag.nombre = TempData["nombreUsuario"];
                ViewBag.inicioSesion = true;
                ViewBag.session = true;

                var alumnos = await _context.Alumnos.ToListAsync();

                var indice = (int)TempData["indice"]! + 1;

                if (indice > alumnos.Count - 1)
                {
                    indice = alumnos.Count - 1;
                }

                TempData["sesion"] = true;
                TempData["indice"] = indice;
                return View("Index", alumnos[indice]);

        }
        public async Task<IActionResult> Previous()
        {
          
            
                ViewBag.nombre = TempData["nombreUsuario"];
                ViewBag.inicioSesion = true;
                ViewBag.session = true;

                var alumnos = await _context.Alumnos.ToListAsync();

                var indice = (int)TempData["indice"]! - 1;

                if (indice < 0)
                {
                    indice = 0;
                }

                TempData["sesion"] = true;
                TempData["indice"] = indice;
                return View("Index", alumnos[indice]);

            
         

        }
        public async Task<IActionResult> FirstStudent()
        {
            
                ViewBag.nombre = TempData["nombreUsuario"];
                ViewBag.inicioSesion = true;
                ViewBag.session = true;

                var alumno = await _context.Alumnos.FirstOrDefaultAsync();
                TempData["sesion"] = true;
                TempData["indice"] = 0;
                return View("Index", alumno);


        }

        public async Task<IActionResult> LastStudent()
        {
            
                ViewBag.nombre = TempData["nombreUsuario"];
                ViewBag.inicioSesion = true;
                ViewBag.session = true;

                var alumnos = await _context.Alumnos.ToListAsync();
                TempData["sesion"] = true;
                TempData["indice"] = alumnos.Count - 1;
                return View("Index", alumnos.Last());

        

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
