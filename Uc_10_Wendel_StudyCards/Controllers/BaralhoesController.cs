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
    public class BaralhoesController : Controller
    {
        private readonly Uc_10_Wendel_StudyCardsContext _context;

        public BaralhoesController(Uc_10_Wendel_StudyCardsContext context)
        {
            _context = context;
        }

        // GET: Baralhoes
        public async Task<IActionResult> Index(string searchString)
        {
            var baralhos = _context.Baralho.AsQueryable();

            if (!string.IsNullOrEmpty(searchString))
            {
                baralhos = baralhos.Where(m =>
                    m.Nome.Contains(searchString));
            }

            ViewData["CurrentFilter"] = searchString;

            return View(await baralhos.ToListAsync());
        }

        // GET: Baralhoes/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var baralho = await _context.Baralho
                .Include(b => b.Categoria)
                .Include(b => b.Materia)
                .FirstOrDefaultAsync(m => m.BaralhoId == id);
            if (baralho == null)
            {
                return NotFound();
            }

            return View(baralho);
        }

        // GET: Baralhoes/Create
        public IActionResult Create()
        {
            ViewData["CategoriaId"] = new SelectList(_context.Categoria, "CategoriaId", "Nome");
            ViewData["MateriaId"] = new SelectList(_context.Materia, "MateriaId", "Nome");
            return View();
        }

        // POST: Baralhoes/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("BaralhoId,Nome,Descricao,ImagemUrl,MateriaId,CategoriaId")] Baralho baralho)
        {
            if (ModelState.IsValid)
            {
                _context.Add(baralho);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["CategoriaId"] = new SelectList(_context.Categoria, "CategoriaId", "Nome", baralho.CategoriaId);
            ViewData["MateriaId"] = new SelectList(_context.Materia, "MateriaId", "Nome", baralho.MateriaId);
            return View(baralho);
        }

        // GET: Baralhoes/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var baralho = await _context.Baralho.FindAsync(id);
            if (baralho == null)
            {
                return NotFound();
            }
            ViewData["CategoriaId"] = new SelectList(_context.Categoria, "CategoriaId", "Nome", baralho.CategoriaId);
            ViewData["MateriaId"] = new SelectList(_context.Materia, "MateriaId", "Nome", baralho.MateriaId);
            return View(baralho);
        }

        // POST: Baralhoes/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("BaralhoId,Nome,Descricao,ImagemUrl,MateriaId,CategoriaId")] Baralho baralho)
        {
            if (id != baralho.BaralhoId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(baralho);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!BaralhoExists(baralho.BaralhoId))
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
            ViewData["CategoriaId"] = new SelectList(_context.Categoria, "CategoriaId", "Nome", baralho.CategoriaId);
            ViewData["MateriaId"] = new SelectList(_context.Materia, "MateriaId", "Nome", baralho.MateriaId);
            return View(baralho);
        }

        // GET: Baralhoes/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var baralho = await _context.Baralho
                .Include(b => b.Categoria)
                .Include(b => b.Materia)
                .FirstOrDefaultAsync(m => m.BaralhoId == id);
            if (baralho == null)
            {
                return NotFound();
            }

            return View(baralho);
        }

        // POST: Baralhoes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var baralho = await _context.Baralho.FindAsync(id);
            if (baralho != null)
            {
                _context.Baralho.Remove(baralho);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool BaralhoExists(int id)
        {
            return _context.Baralho.Any(e => e.BaralhoId == id);
        }
        public async Task<IActionResult> Cards(int id)
        {
            var baralho = await _context.Baralho
                .Include(b => b.Cards)
                .ThenInclude(c => c.Dificuldade)
                .FirstOrDefaultAsync(b => b.BaralhoId == id);

            if (baralho == null)
                return NotFound();

            return View(baralho);
        }

        public async Task<IActionResult> Estudar(int id)
        {
            var card = await _context.Card
                .Where(c => c.BaralhoId == id)
                .FirstOrDefaultAsync();

            if (card == null)
                return NotFound();

            return View(card);
        }

    }
}
