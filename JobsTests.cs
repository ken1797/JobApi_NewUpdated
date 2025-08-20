using Microsoft.EntityFrameworkCore;
using Teknorix.JobsApi.Infrastructure.Persistence;
using Teknorix.JobsApi.Infrastructure.Repositories;
using Teknorix.JobsApi.Application.Jobs.Commands;
using Teknorix.JobsApi.Application.Jobs.Queries;

namespace Teknorix.JobsApi.Tests;

public class JobsTests
{
    private ApplicationDbContext CreateDb()
    {
        var opt = new DbContextOptionsBuilder<ApplicationDbContext>()
            .UseInMemoryDatabase(Guid.NewGuid().ToString())
            .Options;
        var db = new ApplicationDbContext(opt);
        db.Departments.Add(new Teknorix.JobsApi.Domain.Entities.Department { Id = 1, Title = "Dev" });
        db.Locations.Add(new Teknorix.JobsApi.Domain.Entities.Location { Id = 1, Title = "India", City = "Pune", State = "MH", Country = "India", Zip = "411001" });
        db.SaveChanges();
        return db;
    }

    [Fact]
    public async Task Create_And_List_Job()
    {
        var db = CreateDb();
        var uow = new UnitOfWork(db);

        var create = new CreateJobHandler(uow);
        var id = await create.Handle(new CreateJobCommand("Software Developer", "Build things", 1, 1, DateTime.UtcNow.AddDays(30)), default);
        Assert.True(id > 0);

        var list = new ListJobsHandler(uow);
        var resp = await list.Handle(new ListJobsQuery(null, 1, 10, null, null), default);
        Assert.True(resp.Total == 1);
        Assert.Equal("Software Developer", resp.Data.First().Title);
    }
}
