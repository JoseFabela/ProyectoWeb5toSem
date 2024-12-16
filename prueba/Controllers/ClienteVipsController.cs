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
    public class ClienteVipsController : Controller
    {
        private readonly DatabaseContext db;

        public ClienteVipsController(DatabaseContext context)
        {
            db = context;
        }
        public IActionResult Index()
        {
            ViewBag.listaClienteVip = db.ClienteVip.Include(cv => cv.jugador).ToList();
            ViewBag.jugadorId = new SelectList(db.Jugador.Where(j => j.Estado == "activo"), "Id", "Nombre"); // Solo jugadores activos
            return View(new ClienteVip());
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Index(ClienteVip clienteVip)
        {
            if (clienteVip.id == 0)
            {
                db.ClienteVip.Add(clienteVip);
            }
            else
            {
                db.Entry(clienteVip).State = EntityState.Modified;
                ViewBag.Mensaje = "Cliente VIP actualizado correctamente";
            }

            db.SaveChanges();
            ViewBag.listaClienteVip = db.ClienteVip.Include(a => a.jugador).ToList();
            ViewBag.jugadorId = new SelectList(db.Jugador, "Id", "Nombre");
            return View(new ClienteVip());
        }

        // Acción GET para mostrar el formulario de edición
        public IActionResult Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var clienteVip = db.ClienteVip.Include(cv => cv.jugador).FirstOrDefault(cv => cv.id == id);
            if (clienteVip == null)
            {
                return NotFound();
            }

            ViewBag.jugadorId = new SelectList(db.Jugador.Where(j => j.Estado == "activo"), "Id", "Nombre", clienteVip.jugadorId);
            return View(clienteVip);
        }



        // Acción POST para procesar la edición de un descuento
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(int id, [Bind("id,jugadorId,nivel,estado")] ClienteVip clienteVip)
        {
            if (id != clienteVip.id)
            {
                return NotFound();
            }

           
                try
                {
                    db.Update(clienteVip);
                    db.SaveChanges();
                    TempData["Mensaje"] = "Cliente VIP actualizado correctamente.";
                    return RedirectToAction(nameof(Index));
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ClienteVipExists(clienteVip.id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
        
        }



        private bool ClienteVipExists(int id)
        {
            return db.ClienteVip.Any(e => e.id == id);
        }


     
    }
}
