using MediatR;
using Microsoft.EntityFrameworkCore;
using myschedule360.Domain.Interfaces;

namespace myschedule360.Application.Features.Public.Queries;

public class GetBusinessBySlugQueryHandler : IRequestHandler<GetBusinessBySlugQuery, GetBusinessBySlugResponse>
{
    private readonly IApplicationDbContext _context;

    public GetBusinessBySlugQueryHandler(IApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<GetBusinessBySlugResponse> Handle(GetBusinessBySlugQuery request, CancellationToken cancellationToken)
    {
        var business = await _context.Businesses
            .Include(b => b.Address)
            .Include(b => b.Services.Where(s => s.IsActive))
            .FirstOrDefaultAsync(b => b.Slug == request.Slug, cancellationToken);

        if (business == null)
            throw new InvalidOperationException("Business not found");

        var addressDto = business.Address != null ? new AddressDto(
            business.Address.Street,
            business.Address.Number,
            business.Address.Complement,
            business.Address.Neighborhood,
            business.Address.City,
            business.Address.State,
            business.Address.CEP.Formatted,
            business.Address.Latitude,
            business.Address.Longitude
        ) : null;

        var servicesDto = business.Services
            .Select(s => new ServiceDto(
                s.Id,
                s.Name,
                s.Description,
                s.Price,
                s.DurationMinutes
            ))
            .ToArray();

        return new GetBusinessBySlugResponse(
            business.Id,
            business.Name,
            business.Slug,
            business.Type.ToString(),
            business.Description,
            business.Website,
            addressDto,
            servicesDto
        );
    }
}