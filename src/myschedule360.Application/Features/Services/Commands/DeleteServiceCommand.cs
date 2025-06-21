using MediatR;

namespace myschedule360.Application.Features.Services.Commands;

public record DeleteServiceCommand(Guid ServiceId) : IRequest<DeleteServiceResponse>;

public record DeleteServiceResponse(
    bool Success,
    string Message
);