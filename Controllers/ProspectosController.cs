using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using AgricolaProspectos.Models;

namespace AgricolaProspectos.Controllers
{
    public class ProspectosController : Controller
    {
        private readonly agricolaContext _context;

        public ProspectosController(agricolaContext context)
        {
            _context = context;
        }

        private string GuardarArchivo(IFormFile archivo, string nombreDocumento)
        {
            try
            {
                if (archivo != null && archivo.Length > 0)
                {
                    string rutaDirectorio = "NewFolder";
                    string rutaArchivo = Path.Combine(rutaDirectorio, nombreDocumento);

                    using (var fileStream = new FileStream(rutaArchivo, FileMode.Create))
                    {
                        archivo.CopyTo(fileStream);
                    }

                    return rutaArchivo;
                }
            }
            catch (Exception ex)
            {
                // Manejar la excepción según tus necesidades, por ejemplo, registrándola
                Console.WriteLine($"Error en el método GuardarArchivo: {ex.Message}");
            }

            return null;
        }

        [HttpGet]
        public IActionResult Download(int documentoId)
        {
            try
            {
                var documento = _context.Documentos.FirstOrDefault(d => d.DocumentoId == documentoId);

                if (documento != null && !string.IsNullOrEmpty(documento.RutaDocumento))
                {
                    var rutaCompleta = documento.RutaDocumento.TrimEnd('.');
                    var fileBytes = System.IO.File.ReadAllBytes(rutaCompleta);

                    return File(fileBytes, "application/pdf", documento.NombreDocumento);
                }
                else
                {
                    return NotFound();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error en el método Download: {ex.Message}");
                return StatusCode(500, "Error interno del servidor");
            }
        }


        // GET: Prospectos
        public async Task<IActionResult> Index()
        {
            return _context.Prospectos != null ?
                        View(await _context.Prospectos.ToListAsync()) :
                        Problem("Entity set 'agricolaContext.Prospectos'  is null.");
        }

        // GET: Prospectos
        public async Task<IActionResult> Evaluacion()
        {
            return _context.Prospectos != null ?
                        View(await _context.Prospectos.ToListAsync()) :
                        Problem("Entity set 'agricolaContext.Prospectos'  is null.");
        }

        // GET: Prospectos/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Prospectos == null)
            {
                return NotFound();
            }

            var prospecto = await _context.Prospectos
                .Include(p => p.ObservacionesRechazos)
                .Include(p => p.Documentos)
                .FirstOrDefaultAsync(m => m.ProspectoId == id);

            if (prospecto == null)
            {
                return NotFound();
            }

            Console.WriteLine($"documentos cargados: {prospecto?.Documentos?.Count ?? 0}");

            return View(prospecto);
        }

        public async Task<IActionResult> DetallesEvaluacion(int? id)
        {
            if (id == null || _context.Prospectos == null)
            {
                return NotFound();
            }

            var prospecto = await _context.Prospectos
                .FirstOrDefaultAsync(m => m.ProspectoId == id);
            if (prospecto == null)
            {
                return NotFound();
            }

            return View(prospecto);
        }

        // GET: Prospectos/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Prospectos/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        public async Task<IActionResult> Create([Bind("Nombre,PrimerApellido,SegundoApellido,Calle,Numero,Colonia,CodigoPostal,Telefono,Rfc,Estatus,Documentos")] Prospecto prospecto)
        {
            try
            {
                Console.WriteLine("Prueba 1");

                if (ModelState.IsValid)
                {
                    prospecto.Documentos ??= new HashSet<Documento>();

                    foreach (var documento in prospecto.Documentos)
                    {
                        if (documento.Archivo != null && documento.Archivo.Length > 0)
                        {
                            Console.WriteLine("Prueba 5");
                            documento.RutaDocumento = GuardarArchivo(documento.Archivo, documento.NombreDocumento);
                            _context.Documentos.Add(documento);
                        }
                    }

                    // Guarda prospecto
                    _context.Prospectos.Add(prospecto);
                    await _context.SaveChangesAsync();

                    return RedirectToAction(nameof(Index));
                }
                return View(prospecto);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error en el método Create: {ex.Message}");
                return RedirectToAction("Error");
            }
        }

        // GET: Prospectos/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Prospectos == null)
            {
                return NotFound();
            }

            var prospecto = await _context.Prospectos.FindAsync(id);
            if (prospecto == null)
            {
                return NotFound();
            }
            return View(prospecto);
        }

        public async Task<IActionResult> Evaluar(int ProspectoId, string dictamen, string observaciones)
        {
            if (_context.Prospectos == null)
            {
                return Problem("Entity set 'agricolaContext.Prospectos' is null.");
            }

            Console.WriteLine($"ProspectoId: {ProspectoId}");
            Console.WriteLine($"dictamen: {dictamen}");
            Console.WriteLine($"observaciones: {observaciones}");

            // Llamada al procedimiento almacenado
            await _context.EvaluaProspecto(ProspectoId, dictamen, observaciones);

            return RedirectToAction(nameof(Index));
        }

        // GET: Prospectos/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Prospectos == null)
            {
                return NotFound();
            }

            var prospecto = await _context.Prospectos
                .FirstOrDefaultAsync(m => m.ProspectoId == id);
            if (prospecto == null)
            {
                return NotFound();
            }

            return View(prospecto);
        }

        // POST: Prospectos/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Prospectos == null)
            {
                return Problem("Entity set 'agricolaContext.Prospectos'  is null.");
            }
            var prospecto = await _context.Prospectos.FindAsync(id);
            if (prospecto != null)
            {
                _context.Prospectos.Remove(prospecto);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ProspectoExists(int id)
        {
            return (_context.Prospectos?.Any(e => e.ProspectoId == id)).GetValueOrDefault();
        }
    }
}
