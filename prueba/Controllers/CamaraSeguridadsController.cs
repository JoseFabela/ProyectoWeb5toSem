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
    public class CamaraSeguridadsController : Controller
    {
        private readonly DatabaseContext db;

        public CamaraSeguridadsController(DatabaseContext context)
        {
            db = context;
        }

        // Acción para mostrar la lista de Apertura de Cajas
        public IActionResult Index()
        {
            ViewBag.listaCamaraSeguridad = db.CamaraSeguridad.ToList();
            return View();
        }

        // Acción POST para crear o actualizar una Apertura de Caja
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Index(CamaraSeguridad camaraSeguridad)
        {
            if (camaraSeguridad.id == 0)
            {
                db.CamaraSeguridad.Add(camaraSeguridad);
            }
            else
            {
                db.Entry(camaraSeguridad).State = EntityState.Modified;
                ViewBag.Mensaje = "Camara de Seguridad actualizada correctamente.";
            }

            db.SaveChanges();
            ViewBag.listaCamaraSeguridad = db.CamaraSeguridad.ToList();
            return View(new CamaraSeguridad());
        }

        // Acción GET: Mostrar el formulario de edición
        // Acción GET para mostrar el formulario de edición
        public IActionResult Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var camaraSeguridad = db.CamaraSeguridad.Find(id);
            if (camaraSeguridad == null)
            {
                return NotFound();
            }

            return View(camaraSeguridad);
        }



        // Acción POST: Procesar el formulario de edición
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(int id,[Bind("id,ubicacion,estado")] CamaraSeguridad camaraSeguridad)
        {
            if (id != camaraSeguridad.id)
            {
                return NotFound();
            }


            try
            {
                db.Update(camaraSeguridad);
                db.SaveChanges();
                TempData["Mensaje"] = "Apertura de Caja actualizada correctamente.";
                return RedirectToAction(nameof(Index));
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CamaraSeguridadExist(camaraSeguridad.id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

        }

        // Método auxiliar para verificar si una AperturaCaja existe
        private bool CamaraSeguridadExist(int id)
        {
            return db.CamaraSeguridad.Any(a => a.id == id);
        }

    }
}
