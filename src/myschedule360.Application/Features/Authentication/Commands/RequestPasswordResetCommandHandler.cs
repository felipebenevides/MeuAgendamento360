using MediatR;
using Microsoft.EntityFrameworkCore;
using myschedule360.Domain.Interfaces;

namespace myschedule360.Application.Features.Authentication.Commands;

public class RequestPasswordResetCommandHandler : IRequestHandler<RequestPasswordResetCommand, RequestPasswordResetResponse>
{
    private readonly IApplicationDbContext _context;

    public RequestPasswordResetCommandHandler(IApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<RequestPasswordResetResponse> Handle(RequestPasswordResetCommand request, CancellationToken cancellationToken)
    {
        var user = await _context.Users
            .FirstOrDefaultAsync(u => u.Email == request.Email && u.IsActive, cancellationToken);

        if (user == null)
        {
            // Return success even if user not found for security
            return new RequestPasswordResetResponse(true, "If the email exists, a reset link has been sent");
        }

        // TODO: Generate reset token and send email
        var resetToken = Guid.NewGuid().ToString();
        
        // In a real implementation, you would:
        // 1. Store the reset token with expiration
        // 2. Send email with reset link
        
        return new RequestPasswordResetResponse(true, "Password reset link has been sent to your email");
    }
}