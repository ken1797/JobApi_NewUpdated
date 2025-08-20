namespace Teknorix.JobsApi.Domain.Entities;

public class Location : BaseEntity
{
    public string Title { get; set; } = default!;
    public string City { get; set; } = default!;
    public string State { get; set; } = default!;
    public string Country { get; set; } = default!;
    public string Zip { get; set; } = default!;
    public ICollection<Job>? Jobs { get; set; }
}
