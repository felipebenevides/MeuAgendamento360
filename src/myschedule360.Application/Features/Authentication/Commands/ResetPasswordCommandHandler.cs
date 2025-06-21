using MediatR;
using Microsoft.EntityFrameworkCore;
using myschedule360.Domain.Interfaces;

namespace myschedule360.Application.Features.Authentication.Commands;

public class ResetPasswordCommandHandler : IRequestHandler<ResetPasswordCommand, ResetPasswordResponse>
{
    private readonly IApplicationDbContext _context;

    public ResetPasswordCommandHandler(IApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<ResetPasswordResponse> Handle(ResetPasswordCommand request, CancellationToken cancellationToken)
    {
        var user = await _context.Users
            .FirstOrDefaultAsync(u => u.Email == request.Email && u.IsActive, cancellationToken);

        if (user == null)
            return new ResetPasswordResponse(false, "Invalid reset request");

        // TODO: Validate reset token and expiration
        // In a real implementation, you would:
        // 1. Check if token exists and is not expired
        // 2. Validate token matches the user
        
        // For now, accept any token for demo purposes
        if (string.IsNullOrEmpty(request.ResetToken))
            return new ResetPasswordResponse(false, "Invalid reset token");

        user.PasswordHash = BCrypt.Net.BCrypt.HashPassword(request.NewPassword);
        user.UpdatedAt = DateTime.UtcNow;

        await _context.SaveChangesAsync(cancellationToken);

        return new ResetPasswordResponse(true, "Password has been reset successfully");
    }
}