using Microsoft.EntityFrameworkCore;
using Teknorix.JobsApi.Application.Common;
using Teknorix.JobsApi.Domain.Entities;
using Teknorix.JobsApi.Infrastructure.Persistence;

namespace Teknorix.JobsApi.Infrastructure.Repositories;

public class GenericRepository<T> : IGenericRepository<T> where T : BaseEntity
{
    private readonly ApplicationDbContext _db;
    private readonly DbSet<T> _set;
    public GenericRepository(ApplicationDbContext db)
    {
        _db = db;
        _set = db.Set<T>();
    }

    public async Task AddAsync(T entity, CancellationToken ct = default) => await _set.AddAsync(entity, ct);
    public void Delete(T entity) { entity.IsDeleted = true; _set.Update(entity); }
    public async Task<T?> GetByIdAsync(int id) => await _set.FirstOrDefaultAsync(e => e.Id == id && !e.IsDeleted);
    public IQueryable<T> Query() => _set.AsQueryable();
    public void Update(T entity) => _set.Update(entity);
}
