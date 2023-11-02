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
    public class HomeController : Controller
    {
        private readonly ApplicationDbContext _context;

        public HomeController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Home
        public async Task<IActionResult> Index()
        {
            if (!Request.Cookies.ContainsKey("TokenCookie"))
            {
                return RedirectToAction("Login", "Login");
            }
            return _context.Relatorio != null ? 
                          View(await _context.Relatorio.ToListAsync()) :
                          Problem("Entity set 'ApplicationDbContext.Relatorio'  is null.");
        }

        // GET: Home/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Relatorio == null)
            {
                return NotFound();
            }

            var relatorio = await _context.Relatorio
                .FirstOrDefaultAsync(m => m.id_relatorio == id);
            if (relatorio == null)
            {
                return NotFound();
            }

            return View(relatorio);
        }

        // GET: Home/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Home/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("id,data,valor")] Relatorio relatorio)
        {
            if (ModelState.IsValid)
            {
                _context.Add(relatorio);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(relatorio);
        }

        // GET: Home/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Relatorio == null)
            {
                return NotFound();
            }

            var relatorio = await _context.Relatorio.FindAsync(id);
            if (relatorio == null)
            {
                return NotFound();
            }
            return View(relatorio);
        }

        // POST: Home/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("id,data,valor")] Relatorio relatorio)
        {
            if (id != relatorio.id_relatorio)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(relatorio);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!RelatorioExists(relatorio.id_relatorio))
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
            return View(relatorio);
        }

        // GET: Home/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Relatorio == null)
            {
                return NotFound();
            }

            var relatorio = await _context.Relatorio
                .FirstOrDefaultAsync(m => m.id_relatorio == id);
            if (relatorio == null)
            {
                return NotFound();
            }

            return View(relatorio);
        }

        // POST: Home/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Relatorio == null)
            {
                return Problem("Entity set 'ApplicationDbContext.Relatorio'  is null.");
            }
            var relatorio = await _context.Relatorio.FindAsync(id);
            if (relatorio != null)
            {
                _context.Relatorio.Remove(relatorio);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool RelatorioExists(int id)
        {
          return (_context.Relatorio?.Any(e => e.id_relatorio == id)).GetValueOrDefault();
        }
    }
}
