using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using DynamicInputTest.Data;
using DynamicInputTest.Models;
using DynamicInputTest.Models.ViewModels;
using Acondicionador = DynamicInputTest.Models.Acondicionador;

namespace DynamicInputTest.Controllers
{
    public class ProductosController : Controller
    {
        private readonly MyDbContext _context;

        public ProductosController(MyDbContext context)
        {
            _context = context;
        }

        // GET: Productos
        public async Task<IActionResult> Index()
        {
            var productos = await _context.Productos
                        .Include(x => x.Acondicionadores)
                            .ThenInclude(x => x.Pais)
                        .Include(x => x.Acondicionadores)
                            .ThenInclude(x => x.Fabricante)
                        .ToListAsync();

            return _context.Productos != null ?
                        View(productos) :
                        Problem("Entity set 'MyDbContext.Productos'  is null.");
        }

        // GET: Productos/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Productos == null)
            {
                return NotFound();
            }

            var producto = await _context.Productos
                .FirstOrDefaultAsync(m => m.Id == id);
            if (producto == null)
            {
                return NotFound();
            }

            return View(producto);
        }

        // GET: Productos/Create
        public IActionResult Create()
        {
            ViewData["paises"] = new SelectList(_context.Paises.OrderBy(x => x.Nombre), "Id", "Nombre");
            ViewData["fabricantes"] = new SelectList(_context.Fabricantes.OrderBy(x => x.Nombre), "Id", "Nombre");
            CrearProducto crearProducto = new CrearProducto();
            crearProducto.Acondicionadores.Add(new Models.ViewModels.AcondicionadorViewItem());
            return View(crearProducto);
        }

        // POST: Productos/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Nombre, Acondicionadores")] CrearProducto producto)
        {
            if (ModelState.IsValid)
            {
                // Definir lista de acondicionadores
                var acondicionadoresDb = new List<Acondicionador>();

                // Cargar lista de acondicionadores
                foreach (var acondicionador in producto.Acondicionadores)
                {
                    // Obtener fabricante de la BD
                    var fabricante = await _context.Fabricantes.Where(x => x.Id == acondicionador.IdFabricante).FirstOrDefaultAsync();

                    // Obgener país de la BD
                    var pais = await _context.Paises.Where(x => x.Id == acondicionador.IdPais).FirstOrDefaultAsync();

                    // Agregar a la lista de acondicionadores
                    acondicionadoresDb.Add(new Acondicionador
                    {
                        Fabricante = fabricante,
                        Pais = pais
                    });
                }

                // Crear producto y agregar acondicionadores
                var productoDb = new Producto
                {
                    Nombre = producto.Nombre,
                    Acondicionadores = acondicionadoresDb
                };

                // Guardar producto
                _context.Add(productoDb);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["paises"] = new SelectList(_context.Paises.AsNoTracking().OrderBy(x => x.Nombre), "Id", "Nombre");
            ViewData["fabricantes"] = new SelectList(_context.Fabricantes.AsNoTracking().OrderBy(x => x.Nombre), "Id", "Nombre");
            return View(producto);
        }

        [HttpPost]
        public async Task<ActionResult> AgregarAcondicionador([Bind("Acondicionadores")] CrearProducto producto)
        {
            ViewData["paises"] = new SelectList(_context.Paises.AsNoTracking().OrderBy(x => x.Nombre), "Id", "Nombre");
            ViewData["fabricantes"] = new SelectList(_context.Fabricantes.AsNoTracking().OrderBy(x => x.Nombre), "Id", "Nombre");

            // Agregar nuevo acondicionador
            producto.Acondicionadores.Add(new Models.ViewModels.AcondicionadorViewItem());

            // Retornar partial view
            return PartialView("~/Views/Productos/AcondicionadorViewItems.cshtml", producto);
        }

        // GET: Productos/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Productos == null)
            {
                return NotFound();
            }

            var producto = await _context.Productos.FindAsync(id);
            if (producto == null)
            {
                return NotFound();
            }
            return View(producto);
        }

        // POST: Productos/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Nombre")] Producto producto)
        {
            if (id != producto.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(producto);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ProductoExists(producto.Id))
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
            return View(producto);
        }

        // GET: Productos/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Productos == null)
            {
                return NotFound();
            }

            var producto = await _context.Productos
                .FirstOrDefaultAsync(m => m.Id == id);
            if (producto == null)
            {
                return NotFound();
            }

            return View(producto);
        }

        // POST: Productos/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Productos == null)
            {
                return Problem("Entity set 'MyDbContext.Productos'  is null.");
            }
            var producto = await _context.Productos.FindAsync(id);
            if (producto != null)
            {
                _context.Productos.Remove(producto);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ProductoExists(int id)
        {
            return (_context.Productos?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
