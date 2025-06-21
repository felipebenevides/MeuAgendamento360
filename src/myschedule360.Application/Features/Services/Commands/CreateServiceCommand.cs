using MediatR;

namespace myschedule360.Application.Features.Services.Commands;

public record CreateServiceCommand(
    string Name,
    string? Description,
    decimal Price,
    int DurationMinutes,
    Guid BusinessId,
    bool IsActive = true
) : IRequest<CreateServiceResponse>;

public record CreateServiceResponse(
    Guid ServiceId,
    string Name,
    decimal Price,
    int DurationMinutes,
    bool IsActive
);