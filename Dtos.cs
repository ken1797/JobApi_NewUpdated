namespace Teknorix.JobsApi.Application.Jobs;

public record JobCreateDto(string Title, string Description, int LocationId, int DepartmentId, DateTime ClosingDate);
public record JobUpdateDto(string Title, string Description, int LocationId, int DepartmentId, DateTime ClosingDate);
public record JobListRequest(string? Q, int PageNo, int PageSize, int? LocationId, int? DepartmentId);
public record JobListItemDto(int Id, string Code, string Title, string Location, string Department, DateTime PostedDate, DateTime ClosingDate);
public record JobListResponse(int Total, IEnumerable<JobListItemDto> Data);
public record JobDetailsDto(
    int Id,
    string Code,
    string Title,
    string Description,
    object Location,
    object Department,
    DateTime PostedDate,
    DateTime ClosingDate);
