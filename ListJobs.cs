using MediatR;
using Microsoft.EntityFrameworkCore;
using Teknorix.JobsApi.Application.Jobs;
using Teknorix.JobsApi.Application.Common;

namespace Teknorix.JobsApi.Application.Jobs.Queries;

public record ListJobsQuery(string? Q, int PageNo, int PageSize, int? LocationId, int? DepartmentId) : IRequest<JobListResponse>;

public class ListJobsHandler : IRequestHandler<ListJobsQuery, JobListResponse>
{
    private readonly IUnitOfWork _uow;
    public ListJobsHandler(IUnitOfWork uow) => _uow = uow;

    public async Task<JobListResponse> Handle(ListJobsQuery request, CancellationToken cancellationToken)
    {
        var q = _uow.Jobs.Query()
            .Where(j => !j.IsDeleted)
            .Include(j => j.Department)
            .Include(j => j.Location);

        if (!string.IsNullOrWhiteSpace(request.Q))
        {
            var s = request.Q.Trim().ToLower();
            q = q.Where(j => j.Title.ToLower().Contains(s) || j.Description.ToLower().Contains(s));
        }
        if (request.LocationId.HasValue) q = q.Where(j => j.LocationId == request.LocationId.Value);
        if (request.DepartmentId.HasValue) q = q.Where(j => j.DepartmentId == request.DepartmentId.Value);

        var total = await q.CountAsync(cancellationToken);
        var skip = (request.PageNo - 1) * request.PageSize;
        var data = await q.OrderByDescending(j => j.PostedDate)
            .Skip(skip).Take(request.PageSize)
            .Select(j => new JobListItemDto(j.Id, j.Code, j.Title, j.Location!.Title, j.Department!.Title, j.PostedDate, j.ClosingDate))
            .ToListAsync(cancellationToken);

        return new JobListResponse(total, data);
    }
}
