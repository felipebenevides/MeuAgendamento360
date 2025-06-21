using MediatR;

namespace myschedule360.Application.Features.Services.Queries;

public record GetServicesQuery(
    Guid BusinessId,
    bool? IsActive = null,
    int Page = 1,
    int PageSize = 10
) : IRequest<GetServicesResponse>;

public record GetServicesResponse(
    ServiceDto[] Services,
    int TotalCount,
    int Page,
    int PageSize
);

public record ServiceDto(
    Guid Id,
    string Name,
    string? Description,
    decimal Price,
    int DurationMinutes,
    bool IsActive,
    DateTime CreatedAt,
    DateTime? UpdatedAt
);