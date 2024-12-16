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
    public class OfertaPublicitariasController : Controller
    {
        private readonly DatabaseContext db;

        public OfertaPublicitariasController(DatabaseContext context)
        {
            db = context;
        }
        public IActionResult Index()
        {
            ViewBag.listaOfertaPublicitaria = db.OfertaPublicitaria.ToList();
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Index(OfertaPublicitaria ofertaPublicitaria)
        {

            if (ofertaPublicitaria.id == 0)
            {
                db.OfertaPublicitaria.Add(ofertaPublicitaria);
            }
            else
            {
                db.Entry(ofertaPublicitaria).State = EntityState.Modified;
                ViewBag.Mensaje = "Oferta actualizada correctamente";
            }
            db.SaveChanges();
            ViewBag.listaOfertaPublicitaria = db.OfertaPublicitaria.ToList();
            return View();
        }
        // Acción GET para mostrar el formulario de edición
        public IActionResult Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var ofertaPublicitaria = db.OfertaPublicitaria.Find(id);
            if (ofertaPublicitaria == null)
            {
                return NotFound();
            }

            return View(ofertaPublicitaria);
        }

        // Acción POST para procesar la edición de un descuento
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(int id, [Bind("id,Nombre,Descripcion,fecha_inicio,fecha_fin,estado")] OfertaPublicitaria ofertaPublicitaria)
        {
            if (id != ofertaPublicitaria.id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    db.Update(ofertaPublicitaria);
                    db.SaveChanges();
                    TempData["Mensaje"] = "Oferta actualizada correctamente.";
                    return RedirectToAction(nameof(Index));
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!OfertaPublicitariaExists(ofertaPublicitaria.id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
            }
            return View(ofertaPublicitaria);
        }

        // Método auxiliar para verificar si un descuento existe
        private bool OfertaPublicitariaExists(int id)
        {
            return db.OfertaPublicitaria.Any(e => e.id == id);
        }
    }
}
