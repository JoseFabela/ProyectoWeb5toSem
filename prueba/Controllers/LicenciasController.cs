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
    public class LicenciasController : Controller
    {
        private readonly DatabaseContext db;

        public LicenciasController(DatabaseContext context)
        {
            db = context;
        }
        public IActionResult Index()
        {
            ViewBag.listaLicencias = db.Licencias.ToList();
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Index(Licencias licencias)
        {

            if (licencias.id == 0)
            {
                db.Licencias.Add(licencias);
            }
            else
            {
                db.Entry(licencias).State = EntityState.Modified;
                ViewBag.Mensaje = "Licencias actualizadas correctamente";
            }
            db.SaveChanges();
            ViewBag.listaLicencias = db.Licencias.ToList();
            return View();
        }
        // Acción GET para mostrar el formulario de edición
        public IActionResult Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var licencias = db.Licencias.Find(id);
            if (licencias == null)
            {
                return NotFound();
            }

            return View(licencias);
        }

        // Acción POST para procesar la edición de un descuento
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(int id, [Bind("id,tipo,fecha_emision,fecha_vencimiento,estado")] Licencias licencias)
        {
            if (id != licencias.id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    db.Update(licencias);
                    db.SaveChanges();
                    TempData["Mensaje"] = "Licencias actualizadas correctamente.";
                    return RedirectToAction(nameof(Index));
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!licenciasExists(licencias.id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
            }
            return View(licencias);
        }

        // Método auxiliar para verificar si un descuento existe
        private bool licenciasExists(int id)
        {
            return db.Licencias.Any(e => e.id == id);
        }
    }
}
