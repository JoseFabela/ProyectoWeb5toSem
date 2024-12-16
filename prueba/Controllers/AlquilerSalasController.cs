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
    public class AlquilerSalasController : Controller
    {
        private readonly DatabaseContext db;

        public AlquilerSalasController(DatabaseContext context)
        {
            db = context;
        }

        // Acción para mostrar la lista de Apertura de Cajas
        public IActionResult Index()
        {
            // Carga los datos relacionados con AlquilerSala
            ViewBag.listaAlquilerSalas = db.AlquilerSala
                .Include(a => a.SalaVip)
                .Include(a => a.Empleado)
                .Include(a => a.Jugador)
                .ToList();

            // Inicializa las listas para los desplegables
            ViewBag.SalaVipId = new SelectList(db.SalaVip, "id", "nombre");
            ViewBag.EmpleadoId = new SelectList(db.Empleados, "Id", "Nombre");
            ViewBag.JugadorId = new SelectList(db.Jugador, "Id", "Nombre");

            return View(new AlquilerSala());
        }


        // Acción POST para crear o actualizar una Apertura de Caja
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Index(AlquilerSala alquilerSala)
        {
            
                db.AlquilerSala.Add(alquilerSala);
                db.SaveChanges();
                TempData["Mensaje"] = "Registro creado correctamente.";
                return RedirectToAction(nameof(Index));
            
        }


        // Acción GET: Mostrar el formulario de edición
        // Acción GET para mostrar el formulario de edición
        public IActionResult Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var alquilerSala = db.AlquilerSala
                .Include(a => a.SalaVip)
                .Include(a => a.Empleado)
                .Include(a => a.Jugador)
                .FirstOrDefault(a => a.id == id);

            if (alquilerSala == null)
            {
                return NotFound();
            }

            // Prepara las listas para los desplegables
            ViewBag.SalaVipId = new SelectList(db.SalaVip, "id", "nombre", alquilerSala.SalaVipId);
            ViewBag.EmpleadoId = new SelectList(db.Empleados, "Id", "Nombre", alquilerSala.EmpleadoId);
            ViewBag.JugadorId = new SelectList(db.Jugador, "Id", "Nombre", alquilerSala.JugadorId);

            return View(alquilerSala);
        }



        // Acción POST: Procesar el formulario de edición
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(int id, [Bind("id,SalaVipId,EmpleadoId,JugadorId,fechaInicio,fechaFin,estado")] AlquilerSala alquilerSala)
        {
            if (id != alquilerSala.id)
            {
                return NotFound();
            }

            
                try
                {
                    db.Update(alquilerSala);
                    db.SaveChanges();
                    TempData["Mensaje"] = "Registro actualizado correctamente.";
                    return RedirectToAction(nameof(Index));
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!AlquilerSalaExists(alquilerSala.id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                
                }

           
        }

        // Método auxiliar para verificar si el registro existe
        private bool AlquilerSalaExists(int id)
        {
            return db.AlquilerSala.Any(e => e.id == id);
        }

    }
}