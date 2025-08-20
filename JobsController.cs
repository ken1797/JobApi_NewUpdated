using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace JobsApi;

[ApiController]
[Route("api/[controller]")]
public class JobsController : ControllerBase
{
    private readonly AppDbContext _db;
    public JobsController(AppDbContext db) => _db = db;


    [HttpGet]
    [ProducesResponseType(typeof(IEnumerable<Job>), 200)]
    public async Task<ActionResult<IEnumerable<Job>>> GetAll()
    {
        var items = await _db.Jobs.AsNoTracking().ToListAsync();
        return Ok(items);
    }
    [HttpGet("{id:int}")]
    [ProducesResponseType(typeof(Job), 200)]
    [ProducesResponseType(404)]
    public async Task<ActionResult<Job>> GetById(int id)
    {
        var job = await _db.Jobs.FindAsync(id);
        if (job is null) return NotFound();
        return Ok(job);
    }

        [HttpPost]
    [ProducesResponseType(typeof(Job), 201)]
    [ProducesResponseType(400)]
    public async Task<ActionResult<Job>> Create([FromBody] Job job)
    {
        if (!ModelState.IsValid) return BadRequest(ModelState);
        _db.Jobs.Add(job);
        await _db.SaveChangesAsync();
        return CreatedAtAction(nameof(GetById), new { id = job.Id }, job);
    }

    [HttpPut("{id:int}")]
    [ProducesResponseType(204)]
    [ProducesResponseType(400)]
    [ProducesResponseType(404)]
    public async Task<IActionResult> Update(int id, [FromBody] Job job)
    {
        if (id != job.Id) return BadRequest("Id mismatch.");
        var exists = await _db.Jobs.AnyAsync(j => j.Id == id);
        if (!exists) return NotFound();
        _db.Entry(job).State = EntityState.Modified;
        await _db.SaveChangesAsync();
        return NoContent();
    }

    [HttpDelete("{id:int}")]
    [ProducesResponseType(204)]
    [ProducesResponseType(404)]
    public async Task<IActionResult> Delete(int id)
    {
        var job = await _db.Jobs.FindAsync(id);
        if (job is null) return NotFound();
        _db.Remove(job);
        await _db.SaveChangesAsync();
        return NoContent();
    }

}
