using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using GestionFormation.Data;
using GestionFormation.Services;

public class AvatarController : Controller
{
    private readonly MyContext _context;
    private readonly AvatarService _avatarService;

    public AvatarController(MyContext context, AvatarService avatarService)
    {
        _context = context;
        _avatarService = avatarService;
    }

    // GET: Avatar/Presenter/5  (5 = Id du Module)
    public async Task<IActionResult> Presenter(int id)
    {
        var module = await _context.Modules.FirstOrDefaultAsync(m => m.Id == id);
        if (module == null) return NotFound();

        ViewBag.ModuleTitre = module.Titre;
        ViewBag.ModuleId = module.Id;
        return View();
    }

    // POST: Avatar/GetToken/5 (appelé en JS pour récupérer le token de session)
    [HttpPost]
    public async Task<IActionResult> GetToken(int id)
    {
        var module = await _context.Modules.FirstOrDefaultAsync(m => m.Id == id);
        if (module == null) return NotFound();

        var token = await _avatarService.GenerateSessionTokenAsync(module.Contenu ?? module.Titre);
        return Json(new { token });
    }
}