using MediatR;

namespace myschedule360.Application.Features.Services.Queries;

public record GetServiceByIdQuery(Guid ServiceId) : IRequest<GetServiceByIdResponse>;

public record GetServiceByIdResponse(
    Guid Id,
    string Name,
    string? Description,
    decimal Price,
    int DurationMinutes,
    bool IsActive,
    Guid BusinessId,
    DateTime CreatedAt,
    DateTime? UpdatedAt
);