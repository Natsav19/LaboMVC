using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using MVCPlat.Data;
using MVCPlat.Models;

namespace MVCPlat.Controllers
{
    public class CosplaysController : Controller
    {
        private readonly MVCPlatContext _context;

        public CosplaysController(MVCPlatContext context)
        {
            _context = context;
        }

        // GET: Cosplays
        public async Task<IActionResult> Index(string cosplayGenre, string searchString)
        {
            // Use LINQ to get list of genres.
            IQueryable<string> genreQuery = from m in _context.Cosplay
                                            orderby m.Genre
                                            select m.Genre;
            var cosplays = from m in _context.Cosplay
                         select m;

            if (!string.IsNullOrEmpty(searchString))
            {
                cosplays = cosplays.Where(s => s.Title!.Contains(searchString));
            }

            if (!string.IsNullOrEmpty(cosplayGenre))
            {
                cosplays = cosplays.Where(x => x.Genre == cosplayGenre);
            }

            var CosplayGenreVM = new CosplayGenreViewModel
            {
                Genres = new SelectList(await genreQuery.Distinct().ToListAsync()),
                CosplaysList = await cosplays.ToListAsync()
            };

            return View(CosplayGenreVM);
        }

        // GET: Cosplays/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Cosplay == null)
            {
                return NotFound();
            }

            var cosplay = await _context.Cosplay
                .FirstOrDefaultAsync(m => m.Id == id);
            if (cosplay == null)
            {
                return NotFound();
            }

            return View(cosplay);
        }

        // GET: Cosplays/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Cosplays/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Title,ReleaseDate,Genre,Price,Rating")] Cosplays cosplay)
        {
            if (ModelState.IsValid)
            {
                _context.Add(cosplay);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(cosplay);
        }

        // GET: Cosplays/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Cosplay == null)
            {
                return NotFound();
            }

            var cosplay = await _context.Cosplay.FindAsync(id);
            if (cosplay == null)
            {
                return NotFound();
            }
            return View(cosplay);
        }

        // POST: Cosplays/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Title,ReleaseDate,Genre,Price")] Cosplays cosplay)
        {
            if (id != cosplay.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(cosplay);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CosplayExists(cosplay.Id))
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
            return View(cosplay);
        }

        // GET: Cosplays/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Cosplay == null)
            {
                return NotFound();
            }

            var cosplay = await _context.Cosplay
                .FirstOrDefaultAsync(m => m.Id == id);
            if (cosplay == null)
            {
                return NotFound();
            }

            return View(cosplay);
        }

        // POST: Cosplays/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Cosplay == null)
            {
                return Problem("Entity set 'MVCPlatContext.Cosplay'  is null.");
            }
            var cosplay = await _context.Cosplay.FindAsync(id);
            if (cosplay != null)
            {
                _context.Cosplay.Remove(cosplay);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool CosplayExists(int id)
        {
          return (_context.Cosplay?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
