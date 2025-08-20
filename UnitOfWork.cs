using Teknorix.JobsApi.Application.Common;
using Teknorix.JobsApi.Domain.Entities;
using Teknorix.JobsApi.Infrastructure.Persistence;

namespace Teknorix.JobsApi.Infrastructure.Repositories;

public class UnitOfWork : IUnitOfWork
{
    private readonly ApplicationDbContext _db;
    public IGenericRepository<Job> Jobs { get; }
    public IGenericRepository<Department> Departments { get; }
    public IGenericRepository<Location> Locations { get; }

    public UnitOfWork(ApplicationDbContext db)
    {
        _db = db;
        Jobs = new GenericRepository<Job>(_db);
        Departments = new GenericRepository<Department>(_db);
        Locations = new GenericRepository<Location>(_db);
    }

    public Task<int> SaveChangesAsync(CancellationToken ct = default) => _db.SaveChangesAsync(ct);
}
