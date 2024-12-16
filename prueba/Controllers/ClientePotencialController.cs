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
    public class ClientePotencialController : Controller
    {
        private readonly DatabaseContext db;

        public ClientePotencialController(DatabaseContext context)
        {
            db = context;
        }
        public IActionResult Index()
        {
            ViewBag.listaClientePotencial = db.ClientePotencial.ToList();
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Index(ClientePotencial clientePotencial)
        {

            if (clientePotencial.id == 0)
            {
                db.ClientePotencial.Add(clientePotencial);
            }
            else
            {
                db.Entry(clientePotencial).State = EntityState.Modified;
                ViewBag.Mensaje = "Descuento actualizado correctamente";
            }
            db.SaveChanges();
            ViewBag.listaClientePotencial = db.ClientePotencial.ToList();
            return View();
        }
        // Acción GET para mostrar el formulario de edición
        public IActionResult Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var clientePotencial = db.ClientePotencial.Find(id);
            if (clientePotencial == null)
            {
                return NotFound();
            }

            return View(clientePotencial);
        }

        // Acción POST para procesar la edición de un descuento
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(int id, [Bind("id,nombre,email,estado")] ClientePotencial clientePotencial)
        {
            if (id != clientePotencial.id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    db.Update(clientePotencial);
                    db.SaveChanges();
                    TempData["Mensaje"] = "Descuento actualizado correctamente.";
                    return RedirectToAction(nameof(Index));
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ClientePotencialExist(clientePotencial.id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
            }
            return View(clientePotencial);
        }

        // Método auxiliar para verificar si un descuento existe
        private bool ClientePotencialExist(int id)
        {
            return db.ClientePotencial.Any(e => e.id == id);
        }

    }
}
