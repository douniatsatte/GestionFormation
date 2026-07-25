using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using GestionFormation.Data;
using GestionFormation.Models;

public class ModalitesController : Controller
{
    private readonly MyContext _context;

    public ModalitesController(MyContext context)
    {
        _context = context;
    }

    // GET: Modalites
    public async Task<IActionResult> Index()
    {
        var modalites = await _context.Modalites
            .Include(m => m.Module)
            .ToListAsync();

        return View(modalites);
    }

    // GET: Modalites/Details/5
    public async Task<IActionResult> Details(int? id)
    {
        if (id == null) return NotFound();

        var modalite = await _context.Modalites
            .Include(m => m.Module)
            .FirstOrDefaultAsync(m => m.Id == id);

        if (modalite == null) return NotFound();

        return View(modalite);
    }

    // GET: Modalites/Create
    public IActionResult Create()
    {
        ViewData["ModuleId"] = new SelectList(_context.Modules, "Id", "Titre");
        return View();
    }

    // POST: Modalites/Create
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create([Bind("Id,Titre,Type,Contenu,ModuleId")] Modalite modalite)
    {
        if (ModelState.IsValid)
        {
            _context.Add(modalite);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        ViewData["ModuleId"] = new SelectList(_context.Modules, "Id", "Titre", modalite.ModuleId);
        return View(modalite);
    }

    // GET: Modalites/Edit/5
    public async Task<IActionResult> Edit(int? id)
    {
        if (id == null) return NotFound();

        var modalite = await _context.Modalites.FindAsync(id);
        if (modalite == null) return NotFound();

        ViewData["ModuleId"] = new SelectList(_context.Modules, "Id", "Titre", modalite.ModuleId);
        return View(modalite);
    }

    // POST: Modalites/Edit/5
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(int? id, [Bind("Id,Titre,Type,Contenu,ModuleId")] Modalite modalite)
    {
        if (id != modalite.Id) return NotFound();

        if (ModelState.IsValid)
        {
            try
            {
                _context.Update(modalite);
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ModaliteExists(modalite.Id)) return NotFound();
                else throw;
            }
            return RedirectToAction(nameof(Index));
        }

        ViewData["ModuleId"] = new SelectList(_context.Modules, "Id", "Titre", modalite.ModuleId);
        return View(modalite);
    }

    // GET: Modalites/Delete/5
    public async Task<IActionResult> Delete(int? id)
    {
        if (id == null) return NotFound();

        var modalite = await _context.Modalites
            .Include(m => m.Module)
            .FirstOrDefaultAsync(m => m.Id == id);

        if (modalite == null) return NotFound();

        return View(modalite);
    }

    // POST: Modalites/Delete/5
    [HttpPost, ActionName("Delete")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> DeleteConfirmed(int? id)
    {
        var modalite = await _context.Modalites.FindAsync(id);
        if (modalite != null)
        {
            _context.Modalites.Remove(modalite);
            await _context.SaveChangesAsync();
        }

        return RedirectToAction(nameof(Index));
    }

    private bool ModaliteExists(int? id)
    {
        return _context.Modalites.Any(e => e.Id == id);
    }
}