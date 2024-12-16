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
    public class ProductosBarsController : Controller
    {
        private readonly DatabaseContext db;

        public ProductosBarsController(DatabaseContext context)
        {
            db = context;
        }
        public IActionResult Index()
        {
            ViewBag.listaProductosBar = db.ProductosBar.ToList();
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Index(ProductosBar productosBar)
        {

            if (productosBar.id == 0)
            {
                db.ProductosBar.Add(productosBar);
            }
            else
            {
                db.Entry(productosBar).State = EntityState.Modified;
                ViewBag.Mensaje = "Producto Bar actualizado correctamente";
            }
            db.SaveChanges();
            ViewBag.listaProductosBar = db.ProductosBar.ToList();
            return View();
        }
        // Acción GET para mostrar el formulario de edición
        public IActionResult Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var productoBar = db.ProductosBar.Find(id);
            if (productoBar == null)
            {
                return NotFound();
            }

            return View(productoBar);
        }

        // Acción POST para procesar la edición de un descuento
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(int id, [Bind("id,nombre,descripcion,precio,estado")] ProductosBar productoBar)
        {
            if (id != productoBar.id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    db.Update(productoBar);
                    db.SaveChanges();
                    TempData["Mensaje"] = "Producto Bar actualizado correctamente.";
                    return RedirectToAction(nameof(Index));
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ProductoBarExists(productoBar.id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
            }
            return View(productoBar);
        }

        // Método auxiliar para verificar si un descuento existe
        private bool ProductoBarExists(int id)
        {
            return db.ProductosBar.Any(e => e.id == id);
        }

        
    }
}
