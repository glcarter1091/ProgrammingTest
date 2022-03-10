#nullable disable
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ProgrammingTest.Data;

namespace ProgrammingTest.Controllers
{
    public class TitleauthorsController : Controller
    {
        private readonly pubsContext _context;

        public TitleauthorsController(pubsContext context)
        {
            _context = context;
        }

        // GET: Titleauthors
        public async Task<IActionResult> Index()
        {
            var pubsContext = _context.Titleauthors.Include(t => t.Au).Include(t => t.Title);
            return View(await pubsContext.ToListAsync());
        }

        // GET: Titleauthors/Details/5
        public async Task<IActionResult> Details(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var titleauthor = await _context.Titleauthors
                .Include(t => t.Au)
                .Include(t => t.Title)
                .FirstOrDefaultAsync(m => m.AuId == id);
            if (titleauthor == null)
            {
                return NotFound();
            }

            return View(titleauthor);
        }

        // GET: Titleauthors/Create
        public IActionResult Create()
        {
            ViewData["AuId"] = new SelectList(_context.Authors, "AuId", "AuId");
            ViewData["TitleId"] = new SelectList(_context.Titles, "TitleId", "TitleId");
            return View();
        }

        // POST: Titleauthors/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("AuId,TitleId,AuOrd,Royaltyper")] Titleauthor titleauthor)
        {
            if (ModelState.IsValid)
            {
                _context.Add(titleauthor);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["AuId"] = new SelectList(_context.Authors, "AuId", "AuId", titleauthor.AuId);
            ViewData["TitleId"] = new SelectList(_context.Titles, "TitleId", "TitleId", titleauthor.TitleId);
            return View(titleauthor);
        }

        // GET: Titleauthors/Edit/5
        public async Task<IActionResult> Edit(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var titleauthor = await _context.Titleauthors.FindAsync(id);
            if (titleauthor == null)
            {
                return NotFound();
            }
            ViewData["AuId"] = new SelectList(_context.Authors, "AuId", "AuId", titleauthor.AuId);
            ViewData["TitleId"] = new SelectList(_context.Titles, "TitleId", "TitleId", titleauthor.TitleId);
            return View(titleauthor);
        }

        // POST: Titleauthors/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string id, [Bind("AuId,TitleId,AuOrd,Royaltyper")] Titleauthor titleauthor)
        {
            if (id != titleauthor.AuId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(titleauthor);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!TitleauthorExists(titleauthor.AuId))
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
            ViewData["AuId"] = new SelectList(_context.Authors, "AuId", "AuId", titleauthor.AuId);
            ViewData["TitleId"] = new SelectList(_context.Titles, "TitleId", "TitleId", titleauthor.TitleId);
            return View(titleauthor);
        }

        // GET: Titleauthors/Delete/5
        public async Task<IActionResult> Delete(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var titleauthor = await _context.Titleauthors
                .Include(t => t.Au)
                .Include(t => t.Title)
                .FirstOrDefaultAsync(m => m.AuId == id);
            if (titleauthor == null)
            {
                return NotFound();
            }

            return View(titleauthor);
        }

        // POST: Titleauthors/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            var titleauthor = await _context.Titleauthors.FindAsync(id);
            _context.Titleauthors.Remove(titleauthor);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool TitleauthorExists(string id)
        {
            return _context.Titleauthors.Any(e => e.AuId == id);
        }
    }
}
