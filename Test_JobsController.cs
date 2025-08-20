using NUnit.Framework;
using JobsApi;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using System.Collections.Generic;

namespace JobsApi.Tests
{
    public class Test_JobsController
    {
        private AppDbContext NewInMemory()
        {
            var options = new DbContextOptionsBuilder<AppDbContext>()
                .UseInMemoryDatabase(databaseName: "JobsTests_" + System.Guid.NewGuid())
                .Options;
            return new AppDbContext(options);
        }

        [Test]
        public async Task Create_Then_GetAll_ShouldContainItem()
        {
            using var db = NewInMemory();
            var ctrl = new JobsController(db);
            var created = await ctrl.Create(new Job { Title = "Test Job", Description = "Desc" });
            var all = await ctrl.GetAll();
            var ok = all.Result as OkObjectResult;
            Assert.IsNotNull(ok, "Expected OkObjectResult");
            var list = ok.Value as IEnumerable<Job>;
            Assert.IsNotNull(list, "Expected a list of jobs");
            Assert.IsTrue(list.Any(j => j.Title == "Test Job"));
        }
    }
}