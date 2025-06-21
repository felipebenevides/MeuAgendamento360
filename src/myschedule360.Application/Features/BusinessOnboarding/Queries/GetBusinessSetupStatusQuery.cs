using MediatR;

namespace myschedule360.Application.Features.BusinessOnboarding.Queries;

public record GetBusinessSetupStatusQuery(Guid BusinessId) : IRequest<GetBusinessSetupStatusResponse>;

public record GetBusinessSetupStatusResponse(
    Guid BusinessId,
    string Name,
    string Slug,
    bool IsOnboardingComplete,
    int CompletionPercentage,
    string[] MissingSteps
);