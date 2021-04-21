using CustomFormCreator.Data;
using CustomFormCreator.Models.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;

namespace CustomFormCreator.Controllers
{
    public class FormsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public FormsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Forms
        public async Task<IActionResult> Index()
        {
            return View(await _context.Forms.ToListAsync());
        }

        // GET: Forms/Details/5
        public IActionResult Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Form form = GetFormAndSetOrder(id);

            if (form == null)
            {
                return NotFound();
            }

            return View(form);
        }

        // GET: Forms/Create
        public IActionResult Create()
        {
            var form = new Form()
            {
                FormSections = _context.FormSections
                    .Where(c => c.FormId == 1)
                        .Select(c => new FormSection
                        {
                            Order = c.Order,
                            Name = c.Name,
                            IsActive = c.IsActive,
                            FormColumns = _context.FormColumns.Include(x => x.FormColumnTypeNavigation).Where(gc => gc.SectionId == c.Id).OrderBy(gc => gc.Order).ToList()
                        }).OrderBy(c => c.Order).ToList(),
            };

            return View(form);
        }

        // POST: Forms/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([FromForm] Form form)
        {
            if (ModelState.IsValid)
            {
                foreach (var secton in form.FormSections)
                {
                    secton.Id = 0;
                    secton.Rowversion = null;
                    foreach (var column in secton.FormColumns)
                    {
                        column.Id = 0;
                        column.Rowversion = null;
                    }
                }
                _context.Add(form);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(form);
        }

        // GET: Forms/Edit/5
        public IActionResult Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Form model = GetFormAndSetOrder(id);

            if (model == null)
            {
                return NotFound();
            }
            return View(model);
        }

        // POST: Forms/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [FromForm] Form form)
        {
            if (id != form.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(form);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!FormExists(form.Id))
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
            return View(form);
        }

        // GET: Forms/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var form = await _context.Forms
                .FirstOrDefaultAsync(m => m.Id == id);
            if (form == null)
            {
                return NotFound();
            }

            return View(form);
        }

        // POST: Forms/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var form = await _context.Forms.FindAsync(id);
            _context.Forms.Remove(form);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool FormExists(int id)
        {
            return _context.Forms.Any(e => e.Id == id);
        }

        private Form GetFormAndSetOrder(int? id)
        {
            return _context.Forms
                .Where(x => x.Id == id)
                .Select(x => new Form
                {
                    Id = x.Id,
                    Name = x.Name,
                    Created = x.Created,
                    CreatedBy = x.CreatedBy,
                    Modified = x.Modified,
                    ModifiedBy = x.ModifiedBy,
                    IsActive = x.IsActive,
                    Rowversion = x.Rowversion,
                    FormSections = _context.FormSections
                    .Where(c => c.FormId == x.Id)
                        .Select(c => new FormSection
                        {
                            Id = c.Id,
                            Order = c.Order,
                            FormId = c.FormId,
                            Name = c.Name,
                            Created = c.Created,
                            CreatedBy = c.CreatedBy,
                            Modified = c.Modified,
                            ModifiedBy = c.ModifiedBy,
                            IsActive = c.IsActive,
                            Rowversion = c.Rowversion,
                            FormColumns = _context.FormColumns.Include(x => x.FormColumnTypeNavigation).Where(gc => gc.SectionId == c.Id).OrderBy(gc => gc.Order).ToList()
                        }).OrderBy(c => c.Order).ToList()
                }).FirstOrDefault();
        }
    }
}
