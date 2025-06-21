using MediatR;

namespace myschedule360.Application.Features.Services.Commands;

public record ToggleServiceStatusCommand(Guid ServiceId) : IRequest<ToggleServiceStatusResponse>;

public record ToggleServiceStatusResponse(
    Guid ServiceId,
    bool IsActive,
    DateTime UpdatedAt
);