using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Teknorix.JobsApi.Domain.Entities;
using Teknorix.JobsApi.Infrastructure.Persistence;

namespace Teknorix.JobsApi.WebApi.Controllers;

[ApiController]
[Route(""/api/v1/departments"")]
public class DepartmentsController : ControllerBase
{
    private readonly ApplicationDbContext _db;
    public DepartmentsController(ApplicationDbContext db) => _db = db;

    [HttpGet]
    [AllowAnonymous]
    public async Task<IActionResult> GetAll() =>
        Ok(await _db.Departments.Where(d => !d.IsDeleted).ToListAsync());

    [HttpPost]
    [Authorize(Roles = ""Admin"")]
    public async Task<IActionResult> Create([FromBody] Department d)
    {
        _db.Departments.Add(d);
        await _db.SaveChangesAsync();
        return CreatedAtAction(nameof(GetAll), new { id = d.Id }, d);
    }

    [HttpPut(""{id:int}"")]
    [Authorize(Roles = ""Admin"")]
    public async Task<IActionResult> Update(int id, [FromBody] Department d)
    {
        var existing = await _db.Departments.FindAsync(id);
        if (existing is null) return NotFound();
        existing.Title = d.Title;
        existing.UpdatedAt = DateTime.UtcNow;
        await _db.SaveChangesAsync();
        return Ok();
    }
}
