using CustomFormCreator.Data;
using CustomFormCreator.Models.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace CustomFormCreator.Controllers
{
    public class ColumnsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public ColumnsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Columns
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.FormColumns.Include(c => c.SectionNavigation);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: Columns/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var column = await _context.FormColumns
                .Include(c => c.SectionNavigation)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (column == null)
            {
                return NotFound();
            }

            return View(column);
        }

        // GET: Columns/Create
        public IActionResult Create()
        {
            ViewData["SectionId"] = new SelectList(_context.FormSections.Include(x => x.FormNavigation), "Id", "FormSectionName");
            ViewData["FormColumnTypeId"] = new SelectList(_context.FormColumnTypes, "Id", "Name");
            return View();
        }

        // POST: Columns/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([FromForm] FormColumn column)
        {
            if (ModelState.IsValid)
            {
                column.Modified = DateTime.Now;
                column.ModifiedBy = "Brandon Roberts";
                column.CreatedBy = "Brandon Roberts";
                column.IsActive = true;
                _context.Add(column);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["SectionId"] = new SelectList(_context.FormSections.Include(x => x.FormNavigation), "Id", "FormSectionName", column.SectionId);
            ViewData["FormColumnTypeId"] = new SelectList(_context.FormColumnTypes, "Id", "Name", column.FormColumnTypeId);
            return View(column);
        }

        // GET: Columns/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var column = await _context.FormColumns.FindAsync(id);
            if (column == null)
            {
                return NotFound();
            }
            ViewData["SectionId"] = new SelectList(_context.FormSections.Include(x => x.FormNavigation), "Id", "FormSectionName", column.SectionId);
            ViewData["FormColumnTypeId"] = new SelectList(_context.FormColumnTypes, "Id", "Name", column.FormColumnTypeId);
            return View(column);
        }

        // POST: Columns/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [FromForm] FormColumn column)
        {
            if (id != column.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    column.Modified = DateTime.Now;
                    column.ModifiedBy = "Brandon Roberts";
                    _context.Update(column);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ColumnExists(column.Id))
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
            ViewData["SectionId"] = new SelectList(_context.FormSections.Include(x => x.FormNavigation), "Id", "FormSectionName", column.SectionId);
            ViewData["FormColumnTypeId"] = new SelectList(_context.FormColumnTypes, "Id", "Name", column.FormColumnTypeId);

            return View(column);
        }

        // GET: Columns/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var column = await _context.FormColumns
                .Include(c => c.SectionNavigation)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (column == null)
            {
                return NotFound();
            }

            return View(column);
        }

        // POST: Columns/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var column = await _context.FormColumns.FindAsync(id);
            _context.FormColumns.Remove(column);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ColumnExists(int id)
        {
            return _context.FormColumns.Any(e => e.Id == id);
        }
    }
}
