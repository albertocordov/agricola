﻿using System;
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

        // GET: Prospectos
        public async Task<IActionResult> Index()
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
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ProspectoId,Nombre,PrimerApellido,SegundoApellido,Calle,Numero,Colonia,CodigoPostal,Telefono,Rfc,Estatus")] Prospecto prospecto)
        {
            if (ModelState.IsValid)
            {
                _context.Add(prospecto);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(prospecto);
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

        // POST: Prospectos/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ProspectoId,Nombre,PrimerApellido,SegundoApellido,Calle,Numero,Colonia,CodigoPostal,Telefono,Rfc,Estatus")] Prospecto prospecto)
        {
            if (id != prospecto.ProspectoId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(prospecto);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ProspectoExists(prospecto.ProspectoId))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(prospecto);
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