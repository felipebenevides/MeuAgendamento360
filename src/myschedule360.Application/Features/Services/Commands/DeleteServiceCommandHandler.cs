using MediatR;
using Microsoft.EntityFrameworkCore;
using myschedule360.Domain.Interfaces;

namespace myschedule360.Application.Features.Services.Commands;

public class DeleteServiceCommandHandler : IRequestHandler<DeleteServiceCommand, DeleteServiceResponse>
{
    private readonly IApplicationDbContext _context;

    public DeleteServiceCommandHandler(IApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<DeleteServiceResponse> Handle(DeleteServiceCommand request, CancellationToken cancellationToken)
    {
        var service = await _context.Services
            .FirstOrDefaultAsync(s => s.Id == request.ServiceId, cancellationToken);

        if (service == null)
            return new DeleteServiceResponse(false, "Service not found");

        // Check if service has appointments
        var hasAppointments = await _context.Appointments
            .AnyAsync(a => a.ServiceId == request.ServiceId, cancellationToken);

        if (hasAppointments)
            return new DeleteServiceResponse(false, "Cannot delete service with existing appointments");

        _context.Services.Remove(service);
        await _context.SaveChangesAsync(cancellationToken);

        return new DeleteServiceResponse(true, "Service deleted successfully");
    }
}