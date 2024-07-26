using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using DynamicInputTest.Data;
using DynamicInputTest.Models;

namespace DynamicInputTest.Controllers
{
    public class AcondicionadorsController : Controller
    {
        private readonly MyDbContext _context;

        public AcondicionadorsController(MyDbContext context)
        {
            _context = context;
        }

        // GET: Acondicionadors
        public async Task<IActionResult> Index()
        {
              return _context.Acondicionadores != null ? 
                          View(await _context.Acondicionadores.ToListAsync()) :
                          Problem("Entity set 'MyDbContext.Acondicionadores'  is null.");
        }

        // GET: Acondicionadors/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Acondicionadores == null)
            {
                return NotFound();
            }

            var acondicionador = await _context.Acondicionadores
                .FirstOrDefaultAsync(m => m.Id == id);
            if (acondicionador == null)
            {
                return NotFound();
            }

            return View(acondicionador);
        }

        // GET: Acondicionadors/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Acondicionadors/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id")] Acondicionador acondicionador)
        {
            if (ModelState.IsValid)
            {
                _context.Add(acondicionador);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(acondicionador);
        }

        // GET: Acondicionadors/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Acondicionadores == null)
            {
                return NotFound();
            }

            var acondicionador = await _context.Acondicionadores.FindAsync(id);
            if (acondicionador == null)
            {
                return NotFound();
            }
            return View(acondicionador);
        }

        // POST: Acondicionadors/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id")] Acondicionador acondicionador)
        {
            if (id != acondicionador.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(acondicionador);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!AcondicionadorExists(acondicionador.Id))
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
            return View(acondicionador);
        }

        // GET: Acondicionadors/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Acondicionadores == null)
            {
                return NotFound();
            }

            var acondicionador = await _context.Acondicionadores
                .FirstOrDefaultAsync(m => m.Id == id);
            if (acondicionador == null)
            {
                return NotFound();
            }

            return View(acondicionador);
        }

        // POST: Acondicionadors/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Acondicionadores == null)
            {
                return Problem("Entity set 'MyDbContext.Acondicionadores'  is null.");
            }
            var acondicionador = await _context.Acondicionadores.FindAsync(id);
            if (acondicionador != null)
            {
                _context.Acondicionadores.Remove(acondicionador);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool AcondicionadorExists(int id)
        {
          return (_context.Acondicionadores?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
