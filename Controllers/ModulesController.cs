using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using GestionFormation.Data;
using GestionFormation.Models;

public class ModulesController : Controller
{
    private readonly MyContext _context;

    public ModulesController(MyContext context)
    {
        _context = context;
    }

    // GET: Modules
    public async Task<IActionResult> Index()
    {
        var modules = await _context.Modules
            .Include(m => m.Formation)
            .ToListAsync();

        return View(modules);
    }

    // GET: Modules/Details/5
    public async Task<IActionResult> Details(int? id)
    {
        if (id == null) return NotFound();

        var module = await _context.Modules
            .Include(m => m.Formation)
            .FirstOrDefaultAsync(m => m.Id == id);

        if (module == null) return NotFound();

        return View(module);
    }

    // GET: Modules/Create
    public IActionResult Create()
    {
        ViewData["FormationId"] = new SelectList(_context.Formations, "Id", "Titre");
        return View();
    }

    // POST: Modules/Create
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create([Bind("Id,Titre,Ordre,Contenu,FormationId")] Module module)
    {
        if (ModelState.IsValid)
        {
            _context.Add(module);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        ViewData["FormationId"] = new SelectList(_context.Formations, "Id", "Titre", module.FormationId);
        return View(module);
    }

    // GET: Modules/Edit/5
    public async Task<IActionResult> Edit(int? id)
    {
        if (id == null) return NotFound();

        var module = await _context.Modules.FindAsync(id);
        if (module == null) return NotFound();

        ViewData["FormationId"] = new SelectList(_context.Formations, "Id", "Titre", module.FormationId);
        return View(module);
    }

    // POST: Modules/Edit/5
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(int? id, [Bind("Id,Titre,Ordre,Contenu,FormationId")] Module module)
    {
        if (id != module.Id) return NotFound();

        if (ModelState.IsValid)
        {
            try
            {
                _context.Update(module);
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ModuleExists(module.Id)) return NotFound();
                else throw;
            }
            return RedirectToAction(nameof(Index));
        }

        ViewData["FormationId"] = new SelectList(_context.Formations, "Id", "Titre", module.FormationId);
        return View(module);
    }

    // GET: Modules/Delete/5
    public async Task<IActionResult> Delete(int? id)
    {
        if (id == null) return NotFound();

        var module = await _context.Modules
            .Include(m => m.Formation)
            .FirstOrDefaultAsync(m => m.Id == id);

        if (module == null) return NotFound();

        return View(module);
    }

    // POST: Modules/Delete/5
    [HttpPost, ActionName("Delete")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> DeleteConfirmed(int? id)
    {
        var module = await _context.Modules.FindAsync(id);
        if (module != null)
        {
            _context.Modules.Remove(module);
            await _context.SaveChangesAsync();
        }

        return RedirectToAction(nameof(Index));
    }

    private bool ModuleExists(int? id)
    {
        return _context.Modules.Any(e => e.Id == id);
    }
}