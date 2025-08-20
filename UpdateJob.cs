using MediatR;
using Microsoft.EntityFrameworkCore;
using Teknorix.JobsApi.Application.Common;
using Teknorix.JobsApi.Domain.Entities;

namespace Teknorix.JobsApi.Application.Jobs.Commands;

public record UpdateJobCommand(int Id, string Title, string Description, int LocationId, int DepartmentId, DateTime ClosingDate) : IRequest<bool>;

public class UpdateJobHandler : IRequestHandler<UpdateJobCommand, bool>
{
    private readonly IUnitOfWork _uow;
    public UpdateJobHandler(IUnitOfWork uow) => _uow = uow;

    public async Task<bool> Handle(UpdateJobCommand request, CancellationToken cancellationToken)
    {
        var job = await _uow.Jobs.Query().FirstOrDefaultAsync(x => x.Id == request.Id && !x.IsDeleted, cancellationToken);
        if (job is null) return false;
        job.Title = request.Title;
        job.Description = request.Description;
        job.LocationId = request.LocationId;
        job.DepartmentId = request.DepartmentId;
        job.ClosingDate = request.ClosingDate;
        job.UpdatedAt = DateTime.UtcNow;
        _uow.Jobs.Update(job);
        await _uow.SaveChangesAsync(cancellationToken);
        return true;
    }
}
