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
    public class SalaVipsController : Controller
    {
        private readonly DatabaseContext db;

        public SalaVipsController(DatabaseContext context)
        {
            db = context;
        }
        public IActionResult Index()
        {
            ViewBag.listaSalaVip = db.SalaVip.ToList();
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Index(SalaVip salaVip)
        {

            if (salaVip.id == 0)
            {
                db.SalaVip.Add(salaVip);
            }
            else
            {
                db.Entry(salaVip).State = EntityState.Modified;
                ViewBag.Mensaje = "Descuento actualizado correctamente";
            }
            db.SaveChanges();
            ViewBag.listaSalaVip = db.SalaVip.ToList();
            return View();
        }
        // Acción GET para mostrar el formulario de edición
        public IActionResult Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var salavip = db.SalaVip.Find(id);
            if (salavip == null)
            {
                return NotFound();
            }

            return View(salavip);
        }

        // Acción POST para procesar la edición de un descuento
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(int id, [Bind("id,nombre,capacidad,estado")] SalaVip salaVip)
        {
            if (id != salaVip.id)
            {
                return NotFound();
            }

         
                try
                {
                    db.Update(salaVip);
                    db.SaveChanges();
                    TempData["Mensaje"] = "Sala Vip actualizado correctamente.";
                    return RedirectToAction(nameof(Index));
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!SalaVipExists(salaVip.id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                
            }
           
        }

        // Método auxiliar para verificar si un descuento existe
        private bool SalaVipExists(int id)
        {
            return db.SalaVip.Any(e => e.id == id);
        }


    }
}
