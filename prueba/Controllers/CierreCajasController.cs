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
    public class CierreCajasController : Controller
    {
        private readonly DatabaseContext db;

        public CierreCajasController(DatabaseContext context)
        {
            db = context;
        }

        // Acción para mostrar la lista de Apertura de Cajas
        public IActionResult Index()
        {
            ViewBag.listaCierreCajas = db.CierreCaja.Include(a => a.Empleado).ToList();
            ViewBag.EmpleadoId = new SelectList(db.Empleados, "Id", "Nombre");
            return View(new CierreCaja());
        }

        // Acción POST para crear o actualizar una Apertura de Caja
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Index(CierreCaja cierreCaja)
        {
            if (cierreCaja.id == 0)
            {
                db.CierreCaja.Add(cierreCaja);
            }
            else
            {
                db.Entry(cierreCaja).State = EntityState.Modified;
                ViewBag.Mensaje = "Apertura de Caja actualizada correctamente.";
            }

            db.SaveChanges();
            ViewBag.listaCierreCajas = db.CierreCaja.Include(a => a.Empleado).ToList();
            ViewBag.EmpleadoId = new SelectList(db.Empleados, "Id", "Nombre");
            return View(new CierreCaja());
        }

        // Acción GET: Mostrar el formulario de edición
        // Acción GET para mostrar el formulario de edición
        public IActionResult Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var cierreCaja = db.CierreCaja.Find(id);
            if (cierreCaja == null)
            {
                return NotFound();
            }

            ViewBag.EmpleadoId = new SelectList(db.Empleados, "Id", "Nombre", cierreCaja.EmpleadoId);
            return View(cierreCaja);
        }



        // Acción POST: Procesar el formulario de edición
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(int id, CierreCaja cierreCaja)
        {
            if (id != cierreCaja.id)
            {
                return NotFound();
            }


            try
            {
                db.Update(cierreCaja);
                db.SaveChanges();
                TempData["Mensaje"] = "Apertura de Caja actualizada correctamente.";
                return RedirectToAction(nameof(Index));
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CIerreCajaExists(cierreCaja.id))
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
        private bool CIerreCajaExists(int id)
        {
            return db.CierreCaja.Any(a => a.id == id);
        }
    }
}
