namespace Teknorix.JobsApi.Domain.Entities;

public class Department : BaseEntity
{
    public string Title { get; set; } = default!;
    public ICollection<Job>? Jobs { get; set; }
}
