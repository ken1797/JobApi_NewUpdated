namespace Teknorix.JobsApi.Domain.Entities;

public class Job : BaseEntity
{
    public string Code { get; set; } = default!;
    public string Title { get; set; } = default!;
    public string Description { get; set; } = default!;
    public int LocationId { get; set; }
    public Location? Location { get; set; }
    public int DepartmentId { get; set; }
    public Department? Department { get; set; }
    public DateTime PostedDate { get; set; } = DateTime.UtcNow;
    public DateTime ClosingDate { get; set; }
}
