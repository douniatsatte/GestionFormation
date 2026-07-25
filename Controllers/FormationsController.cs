using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using GestionFormation.Data;
using GestionFormation.Models;

public class FormationsController : Controller
{
    private readonly MyContext _context;

    public FormationsController(MyContext context)
    {
        _context = context;
    }

    // GET: FORMATIONS
    public async Task<IActionResult> Index()
    {
        var formations = await _context.Formations
            .Include(f => f.Categorie)
            .Include(f => f.Formateur)
            .ToListAsync();

        return View(formations);
    }

    // GET: FORMATIONS/Details/5
    public async Task<IActionResult> Details(int? id)
    {
        if (id == null) return NotFound();

        var formation = await _context.Formations
            .Include(f => f.Categorie)
            .Include(f => f.Formateur)
            .FirstOrDefaultAsync(m => m.Id == id);

        if (formation == null) return NotFound();

        return View(formation);
    }

    // GET: FORMATIONS/Create
    public IActionResult Create()
    {
        ViewData["CategorieId"] = new SelectList(_context.Categories, "Id", "Nom");
        ViewData["FormateurId"] = new SelectList(_context.Formateurs, "Id", "Nom");
        return View();
    }

    // POST: FORMATIONS/Create
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create([Bind("Id,Titre,Description,DateDebut,DateFin,Prix,CategorieId,FormateurId")] Formation formation)
    {
        if (ModelState.IsValid)
        {
            _context.Add(formation);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        ViewData["CategorieId"] = new SelectList(_context.Categories, "Id", "Nom", formation.CategorieId);
        ViewData["FormateurId"] = new SelectList(_context.Formateurs, "Id", "Nom", formation.FormateurId);
        return View(formation);
    }

    // GET: FORMATIONS/Edit/5
    public async Task<IActionResult> Edit(int? id)
    {
        if (id == null) return NotFound();

        var formation = await _context.Formations.FindAsync(id);
        if (formation == null) return NotFound();

        ViewData["CategorieId"] = new SelectList(_context.Categories, "Id", "Nom", formation.CategorieId);
        ViewData["FormateurId"] = new SelectList(_context.Formateurs, "Id", "Nom", formation.FormateurId);
        return View(formation);
    }

    // POST: FORMATIONS/Edit/5
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(int? id, [Bind("Id,Titre,Description,DateDebut,DateFin,Prix,CategorieId,FormateurId")] Formation formation)
    {
        if (id != formation.Id) return NotFound();

        if (ModelState.IsValid)
        {
            try
            {
                _context.Update(formation);
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!FormationExists(formation.Id)) return NotFound();
                else throw;
            }
            return RedirectToAction(nameof(Index));
        }

        ViewData["CategorieId"] = new SelectList(_context.Categories, "Id", "Nom", formation.CategorieId);
        ViewData["FormateurId"] = new SelectList(_context.Formateurs, "Id", "Nom", formation.FormateurId);
        return View(formation);
    }

    // GET: FORMATIONS/Delete/5
    public async Task<IActionResult> Delete(int? id)
    {
        if (id == null) return NotFound();

        var formation = await _context.Formations
            .Include(f => f.Categorie)
            .Include(f => f.Formateur)
            .FirstOrDefaultAsync(m => m.Id == id);

        if (formation == null) return NotFound();

        return View(formation);
    }

    // POST: FORMATIONS/Delete/5
    [HttpPost, ActionName("Delete")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> DeleteConfirmed(int? id)
    {
        var formation = await _context.Formations.FindAsync(id);
        if (formation != null)
        {
            _context.Formations.Remove(formation);
            await _context.SaveChangesAsync();
        }

        return RedirectToAction(nameof(Index));
    }

    private bool FormationExists(int? id)
    {
        return _context.Formations.Any(e => e.Id == id);
    }
}