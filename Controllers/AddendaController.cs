using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Certifac.Models;

namespace Certifac.Controllers
{
    public class AddendaController : Controller
    {
        private readonly Cer_AddendasContext _context;

        public AddendaController(Cer_AddendasContext context)
        {
            _context = context;
        }

        // GET: Addenda
        public async Task<IActionResult> Index()
        {
              return _context.Addendas != null ? 
                          View(await _context.Addendas.Where(a=>a.Estado==true).ToListAsync()):
                          Problem("Entity set 'Cer_AddendasContext.Addendas'  is null.");
        }

        // GET: Addenda/Details/5
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null || _context.Addendas == null)
            {
                return NotFound();
            }

            var addenda = await _context.Addendas
                .FirstOrDefaultAsync(m => m.IdAddenda == id);
            if (addenda == null)
            {
                return NotFound();
            }

            return View(addenda);
        }

        // GET: Addenda/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Addenda/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("NombreAddenda,Xml,Usuario")] Addenda addenda)
        {
            if (ModelState.IsValid)
            {
                addenda.IdAddenda = Guid.NewGuid();
                addenda.FechaModificacion = DateTime.Now;
                _context.Add(addenda);
                addenda.Estado = true;
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(addenda);
        }

        // GET: Addenda/Edit/5
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null || _context.Addendas == null)
            {
                return NotFound();
            }

            var addenda = await _context.Addendas.FindAsync(id);
            if (addenda == null)
            {
                return NotFound();
            }
            return View(addenda);
        }

        // POST: Addenda/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, [Bind("NombreAddenda,Xml,Usuario")] Addenda addenda)
        {
            if (id != addenda.IdAddenda)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    addenda.Estado = true;
                    addenda.FechaModificacion=DateTime.Now; 
                    addenda.IdAddenda= id;
                    _context.Add(addenda);
                    _context.Update(addenda);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!AddendaExists(addenda.IdAddenda))
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
            return View(addenda);
        }

        // GET: Addenda/Delete/5
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null || _context.Addendas == null)
            {
                return NotFound();
            }

            var addenda = await _context.Addendas
                .FirstOrDefaultAsync(m => m.IdAddenda == id && m.Estado==true);
            if (addenda == null)
            {
                return NotFound();
            }

            return View(addenda);
        }

        // POST: Addenda/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            if (_context.Addendas == null)
            {
                return Problem("Entity set 'Cer_AddendasContext.Addendas'  is null.");
            }
            var addenda = await _context.Addendas.FindAsync(id);
            if (addenda != null)
            {
                addenda.Estado = false;
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool AddendaExists(Guid id)
        {
          return (_context.Addendas?.Any(e => e.IdAddenda == id)).GetValueOrDefault();
        }
    }
}
