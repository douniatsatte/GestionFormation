using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using GestionFormation.Data;
using GestionFormation.Models;

public class ApprenantsController : Controller
{
    private readonly MyContext _context;

    public ApprenantsController(MyContext context)
    {
        _context = context;
    }

    // GET: Apprenants
    public async Task<IActionResult> Index()
    {
        return View(await _context.Apprenants.ToListAsync());
    }

    // GET: Apprenants/Details/5
    public async Task<IActionResult> Details(int? id)
    {
        if (id == null) return NotFound();

        var apprenant = await _context.Apprenants.FirstOrDefaultAsync(m => m.Id == id);
        if (apprenant == null) return NotFound();

        return View(apprenant);
    }

    // GET: Apprenants/Create
    public IActionResult Create()
    {
        return View();
    }

    // POST: Apprenants/Create
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create([Bind("Id,Nom,Prenom,Email")] Apprenant apprenant)
    {
        if (ModelState.IsValid)
        {
            _context.Add(apprenant);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
        return View(apprenant);
    }

    // GET: Apprenants/Edit/5
    public async Task<IActionResult> Edit(int? id)
    {
        if (id == null) return NotFound();

        var apprenant = await _context.Apprenants.FindAsync(id);
        if (apprenant == null) return NotFound();

        return View(apprenant);
    }

    // POST: Apprenants/Edit/5
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(int? id, [Bind("Id,Nom,Prenom,Email")] Apprenant apprenant)
    {
        if (id != apprenant.Id) return NotFound();

        if (ModelState.IsValid)
        {
            try
            {
                _context.Update(apprenant);
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ApprenantExists(apprenant.Id)) return NotFound();
                else throw;
            }
            return RedirectToAction(nameof(Index));
        }
        return View(apprenant);
    }

    // GET: Apprenants/Delete/5
    public async Task<IActionResult> Delete(int? id)
    {
        if (id == null) return NotFound();

        var apprenant = await _context.Apprenants.FirstOrDefaultAsync(m => m.Id == id);
        if (apprenant == null) return NotFound();

        return View(apprenant);
    }

    // POST: Apprenants/Delete/5
    [HttpPost, ActionName("Delete")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> DeleteConfirmed(int? id)
    {
        var apprenant = await _context.Apprenants.FindAsync(id);
        if (apprenant != null)
        {
            _context.Apprenants.Remove(apprenant);
            await _context.SaveChangesAsync();
        }
        return RedirectToAction(nameof(Index));
    }

    private bool ApprenantExists(int? id)
    {
        return _context.Apprenants.Any(e => e.Id == id);
    }
}