using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using GestionFormation.Data;
using GestionFormation.Models;

public class InscriptionsController : Controller
{
    private readonly MyContext _context;

    public InscriptionsController(MyContext context)
    {
        _context = context;
    }

    // GET: Inscriptions
    public async Task<IActionResult> Index()
    {
        var inscriptions = await _context.Inscriptions
            .Include(i => i.Apprenant)
            .Include(i => i.Formation)
            .ToListAsync();

        return View(inscriptions);
    }

    // GET: Inscriptions/Details/5
    public async Task<IActionResult> Details(int? id)
    {
        if (id == null) return NotFound();

        var inscription = await _context.Inscriptions
            .Include(i => i.Apprenant)
            .Include(i => i.Formation)
            .FirstOrDefaultAsync(m => m.Id == id);

        if (inscription == null) return NotFound();

        return View(inscription);
    }

    // GET: Inscriptions/Create
    public IActionResult Create()
    {
        ViewData["ApprenantId"] = new SelectList(_context.Apprenants, "Id", "Nom");
        ViewData["FormationId"] = new SelectList(_context.Formations, "Id", "Titre");
        return View();
    }

    // POST: Inscriptions/Create
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create([Bind("Id,DateInscription,Statut,ApprenantId,FormationId")] Inscription inscription)
    {
        if (ModelState.IsValid)
        {
            _context.Add(inscription);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        ViewData["ApprenantId"] = new SelectList(_context.Apprenants, "Id", "Nom", inscription.ApprenantId);
        ViewData["FormationId"] = new SelectList(_context.Formations, "Id", "Titre", inscription.FormationId);
        return View(inscription);
    }

    // GET: Inscriptions/Edit/5
    public async Task<IActionResult> Edit(int? id)
    {
        if (id == null) return NotFound();

        var inscription = await _context.Inscriptions.FindAsync(id);
        if (inscription == null) return NotFound();

        ViewData["ApprenantId"] = new SelectList(_context.Apprenants, "Id", "Nom", inscription.ApprenantId);
        ViewData["FormationId"] = new SelectList(_context.Formations, "Id", "Titre", inscription.FormationId);
        return View(inscription);
    }

    // POST: Inscriptions/Edit/5
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(int? id, [Bind("Id,DateInscription,Statut,ApprenantId,FormationId")] Inscription inscription)
    {
        if (id != inscription.Id) return NotFound();

        if (ModelState.IsValid)
        {
            try
            {
                _context.Update(inscription);
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!InscriptionExists(inscription.Id)) return NotFound();
                else throw;
            }
            return RedirectToAction(nameof(Index));
        }

        ViewData["ApprenantId"] = new SelectList(_context.Apprenants, "Id", "Nom", inscription.ApprenantId);
        ViewData["FormationId"] = new SelectList(_context.Formations, "Id", "Titre", inscription.FormationId);
        return View(inscription);
    }

    // GET: Inscriptions/Delete/5
    public async Task<IActionResult> Delete(int? id)
    {
        if (id == null) return NotFound();

        var inscription = await _context.Inscriptions
            .Include(i => i.Apprenant)
            .Include(i => i.Formation)
            .FirstOrDefaultAsync(m => m.Id == id);

        if (inscription == null) return NotFound();

        return View(inscription);
    }

    // POST: Inscriptions/Delete/5
    [HttpPost, ActionName("Delete")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> DeleteConfirmed(int? id)
    {
        var inscription = await _context.Inscriptions.FindAsync(id);
        if (inscription != null)
        {
            _context.Inscriptions.Remove(inscription);
            await _context.SaveChangesAsync();
        }

        return RedirectToAction(nameof(Index));
    }

    private bool InscriptionExists(int? id)
    {
        return _context.Inscriptions.Any(e => e.Id == id);
    }
}