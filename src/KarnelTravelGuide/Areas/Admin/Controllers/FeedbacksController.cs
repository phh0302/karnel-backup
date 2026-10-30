using KarnelTravelGuide.Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace KarnelTravelGuide.Areas.Admin.Controllers;

/// <summary>/Admin/Feedbacks: read customer feedback, unread first (CRS FR-63).</summary>
[Area("Admin")]
[Authorize]
public class FeedbacksController : Controller
{
    private readonly KarnelDbContext _db;

    public FeedbacksController(KarnelDbContext db) => _db = db;

    // GET /Admin/Feedbacks
    public async Task<IActionResult> Index()
    {
        var list = await _db.Feedbacks.Include(f => f.Customer)
            .OrderBy(f => f.IsRead).ThenByDescending(f => f.CreatedAt).ToListAsync();
        return View(list);
    }

    // POST /Admin/Feedbacks/MarkRead/5
    [HttpPost, ValidateAntiForgeryToken]
    public async Task<IActionResult> MarkRead(int id)
    {
        var f = await _db.Feedbacks.FindAsync(id);
        if (f == null) return NotFound();
        f.IsRead = true;
        await _db.SaveChangesAsync();
        return RedirectToAction(nameof(Index));
    }
}
