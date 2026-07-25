using GestionFormation.Data;
using GestionFormation.Models;
using GestionFormation.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

public class FormateursController : Controller
{
    private readonly MyContext _context;

    public FormateursController(MyContext context)
    {
        _context = context;
    }

    // GET: Formateurs
    public async Task<IActionResult> Index()
    {
        return View(await _context.Formateurs.ToListAsync());
    }

    // GET: Formateurs/Details/5
    public async Task<IActionResult> Details(int? id)
    {
        if (id == null) return NotFound();

        var formateur = await _context.Formateurs.FirstOrDefaultAsync(m => m.Id == id);
        if (formateur == null) return NotFound();

        return View(formateur);
    }

    // GET: Formateurs/Create
    public IActionResult Create()
    {
        return View();
    }

    // POST: Formateurs/Create
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create([Bind("Id,Nom,Prenom,Email,Bio,PhotoUrl")] Formateur formateur)
    {
        if (ModelState.IsValid)
        {
            _context.Add(formateur);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
        return View(formateur);
    }

    // GET: Formateurs/Edit/5
    public async Task<IActionResult> Edit(int? id)
    {
        if (id == null) return NotFound();

        var formateur = await _context.Formateurs.FindAsync(id);
        if (formateur == null) return NotFound();

        return View(formateur);
    }

    // POST: Formateurs/Edit/5
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(int id, [Bind("Id,Nom,Prenom,Email,Bio,PhotoUrl")] Formateur formateur)
    {
        if (id != formateur.Id) return NotFound();

        if (ModelState.IsValid)
        {
            try
            {
                _context.Update(formateur);
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!FormateurExists(formateur.Id)) return NotFound();
                else throw;
            }
            return RedirectToAction(nameof(Index));
        }
        return View(formateur);
    }

    // GET: Formateurs/Delete/5
    public async Task<IActionResult> Delete(int? id)
    {
        if (id == null) return NotFound();

        var formateur = await _context.Formateurs.FirstOrDefaultAsync(m => m.Id == id);
        if (formateur == null) return NotFound();

        return View(formateur);
    }

    // POST: Formateurs/Delete/5
    [HttpPost, ActionName("Delete")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> DeleteConfirmed(int id)
    {
        var formateur = await _context.Formateurs.FindAsync(id);
        if (formateur != null)
        {
            _context.Formateurs.Remove(formateur);
            await _context.SaveChangesAsync();
        }
        return RedirectToAction(nameof(Index));
    }

    private bool FormateurExists(int id)
    {
        return _context.Formateurs.Any(e => e.Id == id);
    }
}