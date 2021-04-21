using CustomFormCreator.Data;
using CustomFormCreator.Models.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;

namespace CustomFormCreator.Controllers
{
    public class FormColumnTypesController : Controller
    {
        private readonly ApplicationDbContext _context;

        public FormColumnTypesController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: FormColumnTypes
        public async Task<IActionResult> Index()
        {
            return View(await _context.FormColumnTypes.ToListAsync());
        }

        // GET: FormColumnTypes/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var formColumnType = await _context.FormColumnTypes
                .FirstOrDefaultAsync(m => m.Id == id);
            if (formColumnType == null)
            {
                return NotFound();
            }

            return View(formColumnType);
        }

        // GET: FormColumnTypes/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: FormColumnTypes/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Name,Id,Rowversion,CreatedBy,Created,ModifiedBy,Modified")] FormColumnType formColumnType)
        {
            if (ModelState.IsValid)
            {
                _context.Add(formColumnType);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(formColumnType);
        }

        // GET: FormColumnTypes/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var formColumnType = await _context.FormColumnTypes.FindAsync(id);
            if (formColumnType == null)
            {
                return NotFound();
            }
            return View(formColumnType);
        }

        // POST: FormColumnTypes/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Name,Id,Rowversion,CreatedBy,Created,ModifiedBy,Modified")] FormColumnType formColumnType)
        {
            if (id != formColumnType.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(formColumnType);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!FormColumnTypeExists(formColumnType.Id))
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
            return View(formColumnType);
        }

        // GET: FormColumnTypes/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var formColumnType = await _context.FormColumnTypes
                .FirstOrDefaultAsync(m => m.Id == id);
            if (formColumnType == null)
            {
                return NotFound();
            }

            return View(formColumnType);
        }

        // POST: FormColumnTypes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var formColumnType = await _context.FormColumnTypes.FindAsync(id);
            _context.FormColumnTypes.Remove(formColumnType);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool FormColumnTypeExists(int id)
        {
            return _context.FormColumnTypes.Any(e => e.Id == id);
        }
    }
}
