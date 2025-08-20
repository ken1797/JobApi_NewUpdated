using Teknorix.JobsApi.Domain.Entities;

namespace Teknorix.JobsApi.Application.Common;

public interface IGenericRepository<T> where T : BaseEntity
{
    Task<T?> GetByIdAsync(int id);
    IQueryable<T> Query();
    Task AddAsync(T entity, CancellationToken ct = default);
    void Update(T entity);
    void Delete(T entity);
}

public interface IUnitOfWork
{
    IGenericRepository<Job> Jobs { get; }
    IGenericRepository<Department> Departments { get; }
    IGenericRepository<Location> Locations { get; }
    Task<int> SaveChangesAsync(CancellationToken ct = default);
}
