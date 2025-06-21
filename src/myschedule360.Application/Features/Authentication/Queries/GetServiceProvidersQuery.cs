using MediatR;

namespace myschedule360.Application.Features.Authentication.Queries;

public record GetServiceProvidersQuery(Guid BusinessId) : IRequest<GetServiceProvidersResponse>;

public record GetServiceProvidersResponse(
    ServiceProviderDto[] ServiceProviders
);

public record ServiceProviderDto(
    Guid UserId,
    Guid PersonId,
    string FirstName,
    string LastName,
    string Email,
    string Phone,
    bool IsActive,
    DateTime CreatedAt
);