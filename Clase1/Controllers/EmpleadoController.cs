using Clase1.Data;
using Clase1.DTOs;
using Clase1.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace Clase1.Controllers
{
    public class EmpleadoController : Controller
    {
        private readonly ILogger<EmpleadoController> _logger;
        private readonly ApplicationDbContext _context;

        public EmpleadoController(ILogger<EmpleadoController> logger,
            ApplicationDbContext context)
        {
            _logger = logger;
            _context = context;
        }

        public IActionResult Index()
        {
           
             ViewBag.Empleados = _context.Empleados.ToList(); //Convierte en una lista
             return View();
     
        }
        // Método para obtener un empleado por ID (para editar)
        [HttpGet]
        public IActionResult GetEmpleado(int id)
        {
            var empleado = _context.Empleados.Find(id);
            if (empleado == null)
            {
                return NotFound();
            }
            return Json(empleado);
        }

        // Método para crear un nuevo empleado (con AJAX)
        [HttpPost]
        public IActionResult Create([FromForm] EmpleadoDTO empleado)
        {
            if (ModelState.IsValid)
            {
                
                Empleado empleadoNuevo = new Empleado();
                empleadoNuevo.TipoEmpleadoId = empleado.TipoEmpleadoId;
                empleadoNuevo.NumeroTelefonico = empleado.NumeroTelefonico;
                empleadoNuevo.Apellido = empleado.Apellido;
                empleadoNuevo.Nombre = empleado.Nombre;
                empleadoNuevo.Dui = empleado.Dui;
                empleadoNuevo.Email = empleado.Email;
                empleadoNuevo.EsActivo = true; // podria ponerse en el BaseEntity como true por defecto
                // FechaCreacion y FechaModificacion se llenan solas

                _context.Empleados.Add(empleadoNuevo);
                _context.SaveChanges();
                return Ok(new  { message = "Empleado creado correctamente" });
            }
            return BadRequest();
        }

        // Método para editar un empleado (con AJAX)
        [HttpPost]
        public IActionResult Edit([FromForm] EmpleadoDTO empleado)
        {
            if (ModelState.IsValid)
            {
                Empleado empleadoModificado = new Empleado();
                empleadoModificado.TipoEmpleadoId = empleado.TipoEmpleadoId;
                empleadoModificado.NumeroTelefonico = empleado.NumeroTelefonico;
                empleadoModificado.Apellido = empleado.Apellido;
                empleadoModificado.Nombre = empleado.Nombre;
                empleadoModificado.Dui = empleado.Dui;
                empleadoModificado.Email = empleado.Email;
                _context.Empleados.Update(empleadoModificado);
                _context.SaveChanges();
                return Ok(new { message = "Empleado actualizado correctamente" });
            }
            return BadRequest();
        
        }

        // Método para eliminar un empleado (con AJAX)
        [HttpPost]
        public IActionResult Delete(int id)
        {
            var empleado = _context.Empleados.Find(id);
            if (empleado == null)
            {
                return NotFound();
            }

            _context.Empleados.Remove(empleado);
            _context.SaveChanges();

            return Ok(new { message = "Empleado eliminado correctamente" });
        }
    }
}

