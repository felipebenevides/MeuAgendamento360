using MediatR;

namespace myschedule360.Application.Features.Services.Commands;

public record UpdateServiceCommand(
    Guid ServiceId,
    string Name,
    string? Description,
    decimal Price,
    int DurationMinutes
) : IRequest<UpdateServiceResponse>;

public record UpdateServiceResponse(
    Guid ServiceId,
    string Name,
    decimal Price,
    int DurationMinutes,
    DateTime UpdatedAt
);