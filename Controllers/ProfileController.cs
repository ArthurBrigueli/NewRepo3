using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using pimfo.Models;
using pimfo.data;
using System.Security.Claims;

namespace pimfo.Controllers
{
    public class ProfileController : Controller
    {
        private readonly ApplicationDbContext _context;

        public ProfileController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Profile
        public async Task<IActionResult> Index()
        {
            string id = User.FindFirst(ClaimTypes.Name)?.Value;

            var folhaPagamento = await _context.Folha_pagamento
            .Where(fp => fp.id_func == int.Parse(id))
            .ToListAsync();

                if (folhaPagamento != null)
                {
                    return View(folhaPagamento);
                }
                else
                {
                    return Problem("Entity set 'ApplicationDbContext.Folha_pagamento' is null.");
                }
        }

        // GET: Profile/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Folha_pagamento == null)
            {
                return NotFound();
            }

            var folha_pagamento = await _context.Folha_pagamento
                .FirstOrDefaultAsync(m => m.id_folha == id);
            if (folha_pagamento == null)
            {
                return NotFound();
            }

            return View(folha_pagamento);
        }

        // GET: Profile/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Profile/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("id_folha,id_func,nome_func,salario_base,salario_liquido,cargo,data,vale_alimentacao,vale_transporte,valor_ferias,imposto_renda,valor_fgts")] Folha_pagamento folha_pagamento)
        {
            if (ModelState.IsValid)
            {
                _context.Add(folha_pagamento);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(folha_pagamento);
        }

        // GET: Profile/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Folha_pagamento == null)
            {
                return NotFound();
            }

            var folha_pagamento = await _context.Folha_pagamento.FindAsync(id);
            if (folha_pagamento == null)
            {
                return NotFound();
            }
            return View(folha_pagamento);
        }

        // POST: Profile/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("id_folha,id_func,nome_func,salario_base,salario_liquido,cargo,data,vale_alimentacao,vale_transporte,valor_ferias,imposto_renda,valor_fgts")] Folha_pagamento folha_pagamento)
        {
            if (id != folha_pagamento.id_folha)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(folha_pagamento);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!Folha_pagamentoExists(folha_pagamento.id_folha))
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
            return View(folha_pagamento);
        }

        // GET: Profile/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Folha_pagamento == null)
            {
                return NotFound();
            }

            var folha_pagamento = await _context.Folha_pagamento
                .FirstOrDefaultAsync(m => m.id_folha == id);
            if (folha_pagamento == null)
            {
                return NotFound();
            }

            return View(folha_pagamento);
        }

        // POST: Profile/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Folha_pagamento == null)
            {
                return Problem("Entity set 'ApplicationDbContext.Folha_pagamento'  is null.");
            }
            var folha_pagamento = await _context.Folha_pagamento.FindAsync(id);
            if (folha_pagamento != null)
            {
                _context.Folha_pagamento.Remove(folha_pagamento);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool Folha_pagamentoExists(int id)
        {
          return (_context.Folha_pagamento?.Any(e => e.id_folha == id)).GetValueOrDefault();
        }
    }
}
