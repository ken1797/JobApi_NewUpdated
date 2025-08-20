using MediatR;
using Microsoft.EntityFrameworkCore;
using Teknorix.JobsApi.Application.Jobs;
using Teknorix.JobsApi.Application.Common;

namespace Teknorix.JobsApi.Application.Jobs.Queries;

public record GetJobDetailsQuery(int Id) : IRequest<JobDetailsDto?>;

public class GetJobDetailsHandler : IRequestHandler<GetJobDetailsQuery, JobDetailsDto?>
{
    private readonly IUnitOfWork _uow;
    public GetJobDetailsHandler(IUnitOfWork uow) => _uow = uow;

    public async Task<JobDetailsDto?> Handle(GetJobDetailsQuery request, CancellationToken cancellationToken)
    {
        var q = _uow.Jobs.Query()
            .Where(j => j.Id == request.Id && !j.IsDeleted)
            .Include(j => j.Department)
            .Include(j => j.Location);

        var job = await q.FirstOrDefaultAsync(cancellationToken);
        if (job is null) return null;

        var location = new { id = job.LocationId, title = job.Location!.Title, city = job.Location!.City, state = job.Location!.State, country = job.Location!.Country, zip = job.Location!.Zip };
        var department = new { id = job.DepartmentId, title = job.Department!.Title };

        return new JobDetailsDto(job.Id, job.Code, job.Title, job.Description, location, department, job.PostedDate, job.ClosingDate);
    }
}
