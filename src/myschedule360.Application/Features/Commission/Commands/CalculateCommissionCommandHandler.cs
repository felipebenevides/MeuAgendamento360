using MediatR;
using Microsoft.EntityFrameworkCore;
using myschedule360.Domain.Interfaces;

namespace myschedule360.Application.Features.Commission.Commands;

public class CalculateCommissionCommandHandler : IRequestHandler<CalculateCommissionCommand, CalculateCommissionResponse>
{
    private readonly IApplicationDbContext _context;

    public CalculateCommissionCommandHandler(IApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<CalculateCommissionResponse> Handle(CalculateCommissionCommand request, CancellationToken cancellationToken)
    {
        var appointment = await _context.Appointments
            .Include(a => a.Staff)
            .FirstOrDefaultAsync(a => a.Id == request.AppointmentId, cancellationToken);

        if (appointment == null)
            throw new InvalidOperationException("Appointment not found");

        var commissionRate = appointment.Staff.CommissionRate ?? 0;
        var commissionAmount = appointment.Price * (commissionRate / 100);

        return new CalculateCommissionResponse(
            appointment.Id,
            appointment.StaffId,
            appointment.Price,
            commissionRate,
            commissionAmount
        );
    }
}