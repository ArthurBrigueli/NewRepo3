using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using pimfo.Models;
using pimfo.data;


namespace pimfo.Controllers
{
    public class DescontoController : Controller
    {
        private readonly ApplicationDbContext _context;

        public DescontoController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Desconto
        public async Task<IActionResult> Index()
        {
              return _context.Desconto != null ? 
                          View(await _context.Desconto.ToListAsync()) :
                          Problem("Entity set 'ApplicationDbContext.Desconto'  is null.");
        }

        // GET: Desconto/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Desconto == null)
            {
                return NotFound();
            }

            var desconto = await _context.Desconto
                .FirstOrDefaultAsync(m => m.id == id);
            if (desconto == null)
            {
                return NotFound();
            }

            return View(desconto);
        }

        // GET: Desconto/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Desconto/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("id,vale_alimentacao,vale_transporte,valor_ferias,imposto_renda,valor_fgts")] Desconto desconto)
        {
            var quantidadeDesc = await _context.Desconto.ToListAsync();

            if(quantidadeDesc.Count < 1)
            {
                if (ModelState.IsValid)
                {
                    _context.Add(desconto);
                    await _context.SaveChangesAsync();
                    return RedirectToAction(nameof(Index));
                }
            }
            else
            {
                TempData["Erro"] = "Voce ja tem Descontos cadastrados.";
            }

            return View(desconto);
        }

        // GET: Desconto/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Desconto == null)
            {
                return NotFound();
            }

            var desconto = await _context.Desconto.FindAsync(id);
            if (desconto == null)
            {
                return NotFound();
            }
            return View(desconto);
        }

        // POST: Desconto/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("id,vale_alimentacao,vale_transporte,valor_ferias,imposto_renda,valor_fgts")] Desconto desconto)
        {
            if (id != desconto.id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(desconto);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!DescontoExists(desconto.id))
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
            return View(desconto);
        }

        // GET: Desconto/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Desconto == null)
            {
                return NotFound();
            }

            var desconto = await _context.Desconto
                .FirstOrDefaultAsync(m => m.id == id);
            if (desconto == null)
            {
                return NotFound();
            }

            return View(desconto);
        }

        // POST: Desconto/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Desconto == null)
            {
                return Problem("Entity set 'ApplicationDbContext.Desconto'  is null.");
            }
            var desconto = await _context.Desconto.FindAsync(id);
            if (desconto != null)
            {
                _context.Desconto.Remove(desconto);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool DescontoExists(int id)
        {
          return (_context.Desconto?.Any(e => e.id == id)).GetValueOrDefault();
        }
    }
}
