using MediatR;

namespace myschedule360.Application.Features.Authentication.Commands;

public record ResetPasswordCommand(
    string Email,
    string ResetToken,
    string NewPassword
) : IRequest<ResetPasswordResponse>;

public record ResetPasswordResponse(
    bool Success,
    string Message
);