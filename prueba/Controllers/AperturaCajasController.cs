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
    public class AperturaCajasController : Controller
    {
        private readonly DatabaseContext db;

        public AperturaCajasController(DatabaseContext context)
        {
            db = context;
        }

        // Acción para mostrar la lista de Apertura de Cajas
        public IActionResult Index()
        {
            ViewBag.listaAperturaCajas = db.AperturaCaja.Include(a => a.Empleado).ToList();
            ViewBag.EmpleadoId = new SelectList(db.Empleados, "Id", "Nombre");
            return View(new AperturaCaja());
        }

        // Acción POST para crear o actualizar una Apertura de Caja
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Index(AperturaCaja aperturaCaja)
        {
            if (aperturaCaja.Id == 0)
            {
                db.AperturaCaja.Add(aperturaCaja);
            }
            else
            {
                db.Entry(aperturaCaja).State = EntityState.Modified;
                ViewBag.Mensaje = "Apertura de Caja actualizada correctamente.";
            }

            db.SaveChanges();
            ViewBag.listaAperturaCajas = db.AperturaCaja.Include(a => a.Empleado).ToList();
            ViewBag.EmpleadoId = new SelectList(db.Empleados, "Id", "Nombre");
            return View(new AperturaCaja());
        }

        // Acción GET: Mostrar el formulario de edición
        // Acción GET para mostrar el formulario de edición
        public IActionResult Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var aperturaCaja = db.AperturaCaja.Find(id);
            if (aperturaCaja == null)
            {
                return NotFound();
            }

            ViewBag.EmpleadoId = new SelectList(db.Empleados, "Id", "Nombre", aperturaCaja.EmpleadoId);
            return View(aperturaCaja);
        }



        // Acción POST: Procesar el formulario de edición
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(int id, AperturaCaja aperturaCaja)
        {
            if (id != aperturaCaja.Id)
            {
                return NotFound();
            }

           
                try
                {
                    db.Update(aperturaCaja);
                    db.SaveChanges();
                    TempData["Mensaje"] = "Apertura de Caja actualizada correctamente.";
                    return RedirectToAction(nameof(Index));
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!AperturaCajaExists(aperturaCaja.Id))
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
        private bool AperturaCajaExists(int id)
        {
            return db.AperturaCaja.Any(a => a.Id == id);
        }











    }
}
