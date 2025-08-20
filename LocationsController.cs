using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Teknorix.JobsApi.Domain.Entities;
using Teknorix.JobsApi.Infrastructure.Persistence;

namespace Teknorix.JobsApi.WebApi.Controllers;

[ApiController]
[Route(""/api/v1/locations"")]
public class LocationsController : ControllerBase
{
    private readonly ApplicationDbContext _db;
    public LocationsController(ApplicationDbContext db) => _db = db;

    [HttpGet]
    [AllowAnonymous]
    public async Task<IActionResult> GetAll() =>
        Ok(await _db.Locations.Where(l => !l.IsDeleted).ToListAsync());

    [HttpPost]
    [Authorize(Roles = ""Admin"")]
    public async Task<IActionResult> Create([FromBody] Location l)
    {
        _db.Locations.Add(l);
        await _db.SaveChangesAsync();
        return CreatedAtAction(nameof(GetAll), new { id = l.Id }, l);
    }

    [HttpPut(""{id:int}"")]
    [Authorize(Roles = ""Admin"")]
    public async Task<IActionResult> Update(int id, [FromBody] Location l)
    {
        var existing = await _db.Locations.FindAsync(id);
        if (existing is null) return NotFound();
        existing.Title = l.Title;
        existing.City = l.City;
        existing.State = l.State;
        existing.Country = l.Country;
        existing.Zip = l.Zip;
        existing.UpdatedAt = DateTime.UtcNow;
        await _db.SaveChangesAsync();
        return Ok();
    }
}
