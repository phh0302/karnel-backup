using KarnelTravelGuide.Areas.Admin.Models;
using KarnelTravelGuide.Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace KarnelTravelGuide.Areas.Admin.Controllers;

/// <summary>
/// Shared add / edit / delete logic for the admin pages (CRS FR-61).
/// Each table has a small controller that inherits this one and says how to list,
/// name and check its rows. Views: Index.cshtml and Form.cshtml per table,
/// plus the shared Shared/Delete.cshtml.
/// </summary>
[Area("Admin")]
[Authorize]
public abstract class CrudController<T> : Controller where T : class, new()
{
    protected readonly KarnelDbContext Db;

    protected CrudController(KarnelDbContext db) => Db = db;

    /// <summary>Rows for the list page, with any Include and ordering.</summary>
    protected abstract IQueryable<T> ListQuery();

    /// <summary>Readable name of a row, used in messages.</summary>
    protected abstract string NameOf(T item);

    /// <summary>Word used in messages, e.g. "hotel".</summary>
    protected abstract string Kind { get; }

    /// <summary>Extra rules the attributes cannot express (e.g. rooms available ≤ total rooms).</summary>
    protected virtual void ValidateExtra(T item) { }

    /// <summary>Fills drop-down lists used by the form. By default: the list of locations.</summary>
    protected virtual async Task LoadLists()
    {
        ViewBag.Locations = new SelectList(await Db.Locations.OrderBy(l => l.City).ToListAsync(), "LocationId", "City");
    }

    // GET /Admin/{Table}
    public async Task<IActionResult> Index() => View(await ListQuery().ToListAsync());

    // GET /Admin/{Table}/Create
    public async Task<IActionResult> Create()
    {
        await LoadLists();
        ViewBag.IsNew = true;
        return View("Form", new T());
    }

    // POST /Admin/{Table}/Create
    [HttpPost, ValidateAntiForgeryToken]
    public async Task<IActionResult> Create(T item)
    {
        ValidateExtra(item);
        if (!ModelState.IsValid)
        {
            await LoadLists();
            ViewBag.IsNew = true;
            return View("Form", item);
        }
        Db.Set<T>().Add(item);
        await Db.SaveChangesAsync();
        TempData["Ok"] = $"Added {Kind} \"{NameOf(item)}\".";
        return RedirectToAction(nameof(Index));
    }

    // GET /Admin/{Table}/Edit/5
    public async Task<IActionResult> Edit(int id)
    {
        var item = await Db.Set<T>().FindAsync(id);
        if (item == null) return NotFound();
        await LoadLists();
        ViewBag.IsNew = false;
        return View("Form", item);
    }

    // POST /Admin/{Table}/Edit/5
    [HttpPost, ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(int id, T item)
    {
        // The key in the form must match the key in the URL
        var keyName = Db.Model.FindEntityType(typeof(T))!.FindPrimaryKey()!.Properties[0].Name;
        if ((int)Db.Entry(item).Property(keyName).CurrentValue! != id) return BadRequest();

        ValidateExtra(item);
        if (!ModelState.IsValid)
        {
            await LoadLists();
            ViewBag.IsNew = false;
            return View("Form", item);
        }

        Db.Set<T>().Update(item);
        // CreatedAt is set once by the database; never overwrite it from the form
        var created = Db.Entry(item).Metadata.FindProperty("CreatedAt");
        if (created != null) Db.Entry(item).Property("CreatedAt").IsModified = false;

        try
        {
            await Db.SaveChangesAsync();
        }
        catch (DbUpdateConcurrencyException)
        {
            return NotFound();   // the row was deleted by someone else meanwhile
        }
        TempData["Ok"] = $"Saved {Kind} \"{NameOf(item)}\".";
        return RedirectToAction(nameof(Index));
    }

    // GET /Admin/{Table}/Delete/5  - confirmation page (GUI Standards: delete always asks first)
    public async Task<IActionResult> Delete(int id)
    {
        var item = await Db.Set<T>().FindAsync(id);
        if (item == null) return NotFound();
        return View("Delete", new DeleteViewModel { Id = id, Kind = Kind, Name = NameOf(item) });
    }

    // POST /Admin/{Table}/Delete/5
    [HttpPost, ActionName("Delete"), ValidateAntiForgeryToken]
    public async Task<IActionResult> DeleteConfirmed(int id)
    {
        var item = await Db.Set<T>().FindAsync(id);
        if (item == null) return NotFound();
        var name = NameOf(item);
        try
        {
            Db.Set<T>().Remove(item);
            await Db.SaveChangesAsync();
            TempData["Ok"] = $"Deleted {Kind} \"{name}\".";
        }
        catch (DbUpdateException)
        {
            // Another table still refers to this row (database foreign key)
            TempData["Error"] = $"\"{name}\" is still used elsewhere, so it cannot be deleted.";
        }
        return RedirectToAction(nameof(Index));
    }
}
