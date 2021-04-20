using CustomFormCreator.Data;
using CustomFormCreator.Models.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;

namespace CustomFormCreator.Controllers
{
    public class SectionsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public SectionsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Sections
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.Sections.Include(s => s.FormNavigation);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: Sections/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var section = await _context.Sections
                .Include(s => s.FormNavigation)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (section == null)
            {
                return NotFound();
            }

            return View(section);
        }

        // GET: Sections/Create
        public IActionResult Create()
        {
            ViewData["FormId"] = new SelectList(_context.Forms, "Id", "Id");
            return View();
        }

        // POST: Sections/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("FormId,Name,Order,Id,Rowversion,CreatedBy,Created,ModifiedBy,Modified")] Section section)
        {
            if (ModelState.IsValid)
            {
                _context.Add(section);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["FormId"] = new SelectList(_context.Forms, "Id", "Id", section.FormId);
            return View(section);
        }

        // GET: Sections/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var section = await _context.Sections.FindAsync(id);
            if (section == null)
            {
                return NotFound();
            }
            ViewData["FormId"] = new SelectList(_context.Forms, "Id", "Id", section.FormId);
            return View(section);
        }

        // POST: Sections/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("FormId,Name,Order,Id,Rowversion,CreatedBy,Created,ModifiedBy,Modified")] Section section)
        {
            if (id != section.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(section);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!SectionExists(section.Id))
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
            ViewData["FormId"] = new SelectList(_context.Forms, "Id", "Id", section.FormId);
            return View(section);
        }

        // GET: Sections/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var section = await _context.Sections
                .Include(s => s.FormNavigation)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (section == null)
            {
                return NotFound();
            }

            return View(section);
        }

        // POST: Sections/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var section = await _context.Sections.FindAsync(id);
            _context.Sections.Remove(section);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool SectionExists(int id)
        {
            return _context.Sections.Any(e => e.Id == id);
        }
    }
}
