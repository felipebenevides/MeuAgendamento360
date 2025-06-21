using MediatR;

namespace myschedule360.Application.Features.Authentication.Commands;

public record RequestPasswordResetCommand(string Email) : IRequest<RequestPasswordResetResponse>;

public record RequestPasswordResetResponse(
    bool Success,
    string Message
);