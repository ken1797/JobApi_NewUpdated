using MediatR;
using Teknorix.JobsApi.Application.Common;
using Teknorix.JobsApi.Domain.Entities;

namespace Teknorix.JobsApi.Application.Jobs.Commands;

public record CreateJobCommand(string Title, string Description, int LocationId, int DepartmentId, DateTime ClosingDate) : IRequest<int>;

public class CreateJobHandler : IRequestHandler<CreateJobCommand, int>
{
    private readonly IUnitOfWork _uow;
    public CreateJobHandler(IUnitOfWork uow) => _uow = uow;

    public async Task<int> Handle(CreateJobCommand request, CancellationToken cancellationToken)
    {
        var job = new Job
        {
            Title = request.Title,
            Description = request.Description,
            LocationId = request.LocationId,
            DepartmentId = request.DepartmentId,
            ClosingDate = request.ClosingDate,
            Code = $"JOB-{Guid.NewGuid().ToString()[..8].ToUpper()}",
            PostedDate = DateTime.UtcNow
        };
        await _uow.Jobs.AddAsync(job, cancellationToken);
        await _uow.SaveChangesAsync(cancellationToken);
        return job.Id;
    }
}
