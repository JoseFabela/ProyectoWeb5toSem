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
    public class PagoesController : Controller
    {
        private readonly DatabaseContext db;

        public PagoesController(DatabaseContext context)
        {
            db = context;
        }
        public IActionResult Index()
        {
            ViewBag.listaPago = db.Pago.Include(cv => cv.jugador).ToList();
            ViewBag.jugadorId = new SelectList(db.Jugador.Where(j => j.Estado == "activo"), "Id", "Nombre"); // Solo jugadores activos
            return View(new Pago());
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Index(Pago pago)
        {
            if (pago.id == 0)
            {
                db.Pago.Add(pago);
            }
            else
            {
                db.Entry(pago).State = EntityState.Modified;
                ViewBag.Mensaje = "Cliente VIP actualizado correctamente";
            }

            db.SaveChanges();
            ViewBag.listaPago = db.Pago.Include(a => a.jugador).ToList();
            ViewBag.jugadorId = new SelectList(db.Jugador, "Id", "Nombre");
            return View(new Pago());
        }

        // Acción GET para mostrar el formulario de edición
        public IActionResult Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var pago = db.Pago.Include(cv => cv.jugador).FirstOrDefault(cv => cv.id == id);
            if (pago == null)
            {
                return NotFound();
            }

            ViewBag.jugadorId = new SelectList(db.Jugador.Where(j => j.Estado == "activo"), "Id", "Nombre", pago.jugadorId);
            return View(pago);
        }



        // Acción POST para procesar la edición de un descuento
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(int id, [Bind("id,jugadorId,fecha,monto,tipo_pago,estado")] Pago pago)
        {
            if (id != pago.id)
            {
                return NotFound();
            }


            try
            {
                db.Update(pago);
                db.SaveChanges();
                TempData["Mensaje"] = "Cliente VIP actualizado correctamente.";
                return RedirectToAction(nameof(Index));
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!PagoExists(pago.id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

        }



        private bool PagoExists(int id)
        {
            return db.Pago.Any(e => e.id == id);
        }
    }
}
