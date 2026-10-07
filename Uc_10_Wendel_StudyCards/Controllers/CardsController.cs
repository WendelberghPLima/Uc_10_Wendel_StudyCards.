using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Uc_10_Wendel_StudyCards.Data;
using Uc_10_Wendel_StudyCards.Models;

namespace Uc_10_Wendel_StudyCards.Controllers
{
    public class CardsController : Controller
    {
        private readonly Uc_10_Wendel_StudyCardsContext _context;

        public CardsController(Uc_10_Wendel_StudyCardsContext context)
        {
            _context = context;
        }

        // GET: Cards
        public async Task<IActionResult> Index(string searchString)
        {
            var cards = _context.Card.AsQueryable();

            if (!string.IsNullOrEmpty(searchString))
            {
                cards = cards.Where(m =>
                    m.Pergunta.Contains(searchString));
            }

            ViewData["CurrentFilter"] = searchString;

            return View(await cards.ToListAsync());
        }

        // GET: Cards/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var card = await _context.Card
                .Include(c => c.Baralho)
                .Include(c => c.Dificuldade)
                .FirstOrDefaultAsync(m => m.CardId == id);
            if (card == null)
            {
                return NotFound();
            }

            return View(card);
        }

        // GET: Cards/Create
        public IActionResult Create()
        {
            ViewData["BaralhoId"] = new SelectList(_context.Baralho, "BaralhoId", "Nome");
            ViewData["DificuldadeId"] = new SelectList(_context.Dificuldade, "DificuldadeId", "Nome");
            return View();
        }

        // POST: Cards/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("CardId,Pergunta,Resposta,DataCriacao,BaralhoId,DificuldadeId")] Card card)
        {
            if (ModelState.IsValid)
            {
                _context.Add(card);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["BaralhoId"] = new SelectList(_context.Baralho, "BaralhoId", "Nome", card.BaralhoId);
            ViewData["DificuldadeId"] = new SelectList(_context.Dificuldade, "DificuldadeId", "Nome", card.DificuldadeId);
            return View(card);
        }

        // GET: Cards/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var card = await _context.Card.FindAsync(id);
            if (card == null)
            {
                return NotFound();
            }
            ViewData["BaralhoId"] = new SelectList(_context.Baralho, "BaralhoId", "Nome", card.BaralhoId);
            ViewData["DificuldadeId"] = new SelectList(_context.Dificuldade, "DificuldadeId", "Nome", card.DificuldadeId);
            return View(card);
        }

        // POST: Cards/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("CardId,Pergunta,Resposta,DataCriacao,BaralhoId,DificuldadeId")] Card card)
        {
            if (id != card.CardId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(card);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CardExists(card.CardId))
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
            ViewData["BaralhoId"] = new SelectList(_context.Baralho, "BaralhoId", "Nome", card.BaralhoId);
            ViewData["DificuldadeId"] = new SelectList(_context.Dificuldade, "DificuldadeId", "Nome", card.DificuldadeId);
            return View(card);
        }

        // GET: Cards/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var card = await _context.Card
                .Include(c => c.Baralho)
                .Include(c => c.Dificuldade)
                .FirstOrDefaultAsync(m => m.CardId == id);
            if (card == null)
            {
                return NotFound();
            }

            return View(card);
        }

        // POST: Cards/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var card = await _context.Card.FindAsync(id);
            if (card != null)
            {
                _context.Card.Remove(card);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool CardExists(int id)
        {
            return _context.Card.Any(e => e.CardId == id);
        }

        public async Task<IActionResult> Visualizar(int id)
        {
            var card = await _context.Card
                .Include(c => c.Baralho)
                .Include(c => c.Dificuldade)
                .FirstOrDefaultAsync(c => c.CardId == id);

            if (card == null)
                return NotFound();

            return View(card);
        }

        public async Task<IActionResult> Praticar(int id)
        {
            var card = await _context.Card
                .Include(c => c.Baralho)
                .Include(c => c.Dificuldade)
                .FirstOrDefaultAsync(c => c.CardId == id);

            if (card == null)
                return NotFound();

            return View(card);
        }
    }
}
