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
    public class clienteController : Controller
    {
        private readonly DatabaseContext db;

        public clienteController(DatabaseContext context)
        {
            db = context;
        }
        public IActionResult Index()
        {
            ViewBag.listaClientes = db.clientes.ToList();
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Index(cliente cliente)
        {

            if (cliente.id == 0)
            {
                db.clientes.Add(cliente);
            }
            else
            {
                db.Entry(cliente).State = EntityState.Modified;
                ViewBag.Mensaje = "Cliente actualizado correctamente";
            }
            db.SaveChanges();
            ViewBag.listaClientes = db.clientes.ToList();
            return View();
        }
        // Acción GET para mostrar el formulario de edición
        public IActionResult Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var cliente = db.clientes.Find(id);
            if (cliente == null)
            {
                return NotFound();
            }

            return View(cliente);
        }

        // Acción POST para procesar la edición de un descuento
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(int id, [Bind("id,nombre,descripcion,email,telefono,fecha_registro,estado")] cliente cliente)
        {
            if (id != cliente.id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    db.Update(cliente);
                    db.SaveChanges();
                    TempData["Mensaje"] = "Descuento actualizado correctamente.";
                    return RedirectToAction(nameof(Index));
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!DescuentoExists(cliente.id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
            }
            return View(cliente);
        }

        // Método auxiliar para verificar si un descuento existe
        private bool DescuentoExists(int id)
        {
            return db.clientes.Any(e => e.id == id);
        }

        // Acción GET para mostrar el formulario de eliminación (Opcional)
        public IActionResult Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var descuento = db.descuentos
                .FirstOrDefault(m => m.id == id);
            if (descuento == null)
            {
                return NotFound();
            }

            return View(descuento);
        }

        // Acción POST para procesar la eliminación de un descuento (Opcional)
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(int id)
        {
            var descuento = db.descuentos.Find(id);
            db.descuentos.Remove(descuento);
            db.SaveChanges();
            TempData["Mensaje"] = "Descuento eliminado correctamente.";
            return RedirectToAction(nameof(Index));
        }
    }
}
