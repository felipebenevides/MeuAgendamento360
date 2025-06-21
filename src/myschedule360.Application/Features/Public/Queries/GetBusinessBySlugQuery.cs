using MediatR;

namespace myschedule360.Application.Features.Public.Queries;

public record GetBusinessBySlugQuery(string Slug) : IRequest<GetBusinessBySlugResponse>;

public record GetBusinessBySlugResponse(
    Guid Id,
    string Name,
    string Slug,
    string Type,
    string? Description,
    string? Website,
    AddressDto? Address,
    ServiceDto[] Services
);

public record AddressDto(
    string Street,
    string Number,
    string? Complement,
    string Neighborhood,
    string City,
    string State,
    string CEP,
    decimal? Latitude,
    decimal? Longitude
);

public record ServiceDto(
    Guid Id,
    string Name,
    string? Description,
    decimal Price,
    int DurationMinutes
);