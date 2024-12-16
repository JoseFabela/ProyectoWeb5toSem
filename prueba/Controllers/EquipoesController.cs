using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using prueba.DAL;
using prueba.Models;

namespace prueba.Controllers
{
    public class EquipoesController : Controller
    {
        private readonly DatabaseContext db;

        public EquipoesController(DatabaseContext context)
        {
            db = context;
        }
        public IActionResult Index()
        {
            ViewBag.listaEquipos = db.Equipo.ToList();
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Index(Equipo equipo)
        {

            if (equipo.id == 0)
            {
                db.Equipo.Add(equipo);
            }
            else
            {
                db.Entry(equipo).State = EntityState.Modified;
                ViewBag.Mensaje = "Equipo actualizado correctamente";
            }
            db.SaveChanges();
            ViewBag.listaEquipos = db.Equipo.ToList();
            return View();
        }
        // Acción GET para mostrar el formulario de edición
        public IActionResult Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var equipo = db.Equipo.Find(id);
            if (equipo == null)
            {
                return NotFound();
            }

            return View(equipo);
        }

        // Acción POST para procesar la edición de un descuento
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(int id, [Bind("id,nombre,categoria,estado")] Equipo equipo)
        {
            if (id != equipo.id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    db.Update(equipo);
                    db.SaveChanges();
                    TempData["Mensaje"] = "Equipo actualizado correctamente.";
                    return RedirectToAction(nameof(Index));
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!EquipoExists(equipo.id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
            }
            return View(equipo);
        }

        // Método auxiliar para verificar si un descuento existe
        private bool EquipoExists(int id)
        {
            return db.Equipo.Any(e => e.id == id);
        }
    }
}
