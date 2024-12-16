using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using prueba.DAL;
using prueba.Models;

namespace prueba.Controllers
{
    public class descuentoController : Controller
    {
        private readonly DatabaseContext db;

        public descuentoController(DatabaseContext context)
        {
            db = context;
        }
        public IActionResult Index()
        {
            ViewBag.listaDescuentos = db.descuentos.ToList();
            return View();
        }
		[HttpPost]
		[ValidateAntiForgeryToken]
		public ActionResult Index(descuento descuento1)
		{
			
			if (descuento1.id == 0)
			{
				db.descuentos.Add(descuento1);
			}
			else
			{
				db.Entry(descuento1).State = EntityState.Modified;
				ViewBag.Mensaje = "Descuento actualizado correctamente";
			}
			db.SaveChanges();
			ViewBag.listaDescuentos = db.descuentos.ToList();
			return View();
		}
        // Acción GET para mostrar el formulario de edición
        public IActionResult Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var descuento = db.descuentos.Find(id);
            if (descuento == null)
            {
                return NotFound();
            }

            return View(descuento);
        }

        // Acción POST para procesar la edición de un descuento
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(int id, [Bind("id,porcentaje,descripcion,fecha_inicio,fecha_fin,estado")] descuento descuento)
        {
            if (id != descuento.id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    db.Update(descuento);
                    db.SaveChanges();
                    TempData["Mensaje"] = "Descuento actualizado correctamente.";
                    return RedirectToAction(nameof(Index));
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!DescuentoExists(descuento.id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
            }
            return View(descuento);
        }

        // Método auxiliar para verificar si un descuento existe
        private bool DescuentoExists(int id)
        {
            return db.descuentos.Any(e => e.id == id);
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
