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
    public class HorarioAtencionsController : Controller
    {
        private readonly DatabaseContext db;

        public HorarioAtencionsController(DatabaseContext context)
        {
            db = context;
        }
        public IActionResult Index()
        {
            ViewBag.listaHorarios = db.HorarioAtencion.ToList();
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Index(HorarioAtencion horarioAtencion)
        {

            if (horarioAtencion.id == 0)
            {
                db.HorarioAtencion.Add(horarioAtencion);
            }
            else
            {
                db.Entry(horarioAtencion).State = EntityState.Modified;
                ViewBag.Mensaje = "Horario Atencion actualizado correctamente";
            }
            db.SaveChanges();
            ViewBag.listaHorarios = db.HorarioAtencion.ToList();
            return View();
        }
        // Acción GET para mostrar el formulario de edición
        public IActionResult Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var horarioAtencion = db.HorarioAtencion.Find(id);
            if (horarioAtencion == null)
            {
                return NotFound();
            }

            return View(horarioAtencion);
        }

        // Acción POST para procesar la edición de un descuento
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(int id, [Bind("id,dia,apertura,cierre,estado")] HorarioAtencion horarioAtencion)
        {
            if (id != horarioAtencion.id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    db.Update(horarioAtencion);
                    db.SaveChanges();
                    TempData["Mensaje"] = "Horario Atencion actualizado correctamente.";
                    return RedirectToAction(nameof(Index));
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!HorarioExists(horarioAtencion.id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
            }
            return View(horarioAtencion);
        }

        // Método auxiliar para verificar si un descuento existe
        private bool HorarioExists(int id)
        {
            return db.HorarioAtencion.Any(e => e.id == id);
        }
    }
}
