using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using pimfo.Models;
using pimfo.data;
using Microsoft.AspNetCore.Authorization;


using System.IO;
using iTextSharp.text;
using iTextSharp.text.pdf;


namespace pimfo.Controllers
{
    [Authorize]
    public class Folha_pagamentoController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly ApplicationDbContext _contextRelatorio;

        public Folha_pagamentoController(ApplicationDbContext context, ApplicationDbContext contextRelatorio)
        {
            _context = context;
            _contextRelatorio = contextRelatorio;
        }

        // GET: Folha_pagamento
        public async Task<IActionResult> Index()
        {
            return _context.Folha_pagamento != null ? 
                          View(await _context.Folha_pagamento.ToListAsync()) :
                          Problem("Entity set 'ApplicationDbContext.Folha_pagamento'  is null.");
        }

        // GET: Folha_pagamento/Details/5
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

        // GET: Folha_pagamento/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Folha_pagamento/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("id_folha,id_func,salario_base,cargo,data,vale_alimentacao,vale_transporte,valor_ferias,imposto_renda,valor_fgts")] Folha_pagamento folha_pagamento)
        {
            if (ModelState.IsValid)
            {
                _context.Add(folha_pagamento);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(folha_pagamento);
        }

        // GET: Folha_pagamento/Edit/5
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

        // POST: Folha_pagamento/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("id_folha,id_func,salario_base,cargo,data,vale_alimentacao,vale_transporte,valor_ferias,imposto_renda,valor_fgts")] Folha_pagamento folha_pagamento)
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

        [HttpPost]
        public async Task< IActionResult> GerarFolha()
        {
            var funcionarios = _context.Funcionarios.ToList();
            var descontos = _context.Desconto.ToList();
            DateTime currentTime = DateTime.Now;
            var salGeral = 0.0;


            foreach (var funcionario in funcionarios)
            {
                foreach(var desconto in descontos)
                {

                    var descTotal = desconto.vale_alimentacao + desconto.valor_ferias + desconto.imposto_renda + desconto.vale_transporte + desconto.valor_fgts;
                    var salaatt = funcionario.salario_bruto - descTotal;
                    salGeral += funcionario.salario_bruto;
                    var registroFolha = new Folha_pagamento
                    {
                        id_func = funcionario.id,//asas
                        nome_func = funcionario.nome,
                        salario_base = funcionario.salario_bruto,
                        cargo = funcionario.cargo,
                        data = currentTime.ToString("dd/MM/yyyy HH:mm:ss"),
                        vale_alimentacao = desconto.vale_alimentacao,
                        vale_transporte = desconto.vale_transporte,
                        valor_ferias = desconto.valor_ferias,
                        valor_fgts = desconto.valor_fgts,
                        imposto_renda = desconto.imposto_renda,
                        salario_liquido = salaatt
                   
                    };
                    _context.Add(registroFolha);
                    await _context.SaveChangesAsync();
                }
            }
            var relatorio = new Relatorio
            {
                data_relatorio = currentTime.ToString("dd/MM/yyyy HH:mm:ss"),
                valor_total = salGeral
            };

            _contextRelatorio.Add(relatorio);
            await _contextRelatorio.SaveChangesAsync();
            


            TempData["Message"] = "Salários reduzidos com sucesso!";
            return RedirectToAction("Index"); // Redirecionar para a página principal ou outra página desejada.
        }

        public async Task<IActionResult> Download(int id)
        {
            //Pegar valores
            var folha_pagamento = await _context.Folha_pagamento
                .FirstOrDefaultAsync(m => m.id_folha == id);


            var SalarioTotal = folha_pagamento.vale_alimentacao + folha_pagamento.valor_ferias + folha_pagamento.imposto_renda + folha_pagamento.vale_transporte + folha_pagamento.valor_fgts;


            //Gerar PDF
            Document doc = new Document();
            MemoryStream ms = new MemoryStream();
            PdfWriter writer = PdfWriter.GetInstance(doc, ms);
            doc.Open();

            doc.Add(new Paragraph($"Nome: {folha_pagamento.nome_func}"));
            doc.Add(new Paragraph($"Salario Base: {folha_pagamento.salario_base}"));
            doc.Add(new Paragraph($"Cargo: {folha_pagamento.cargo}"));
            doc.Add(new Paragraph($"Data: {folha_pagamento.data}"));
            doc.Add(new Paragraph($"Total de descontos: {SalarioTotal}"));
            doc.Add(new Paragraph($"Salario Liquido: {folha_pagamento.salario_liquido}"));
            doc.Close();
            Response.Headers.Add("content-disposition", "inline; filename=exemplo.pdf");
            return File(ms.ToArray(), "application/pdf");
        }

        // GET: Folha_pagamento/Delete/5
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

        // POST: Folha_pagamento/Delete/5
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
