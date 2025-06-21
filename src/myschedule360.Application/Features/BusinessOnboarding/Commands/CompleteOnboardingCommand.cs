using MediatR;

namespace myschedule360.Application.Features.BusinessOnboarding.Commands;

public record CompleteOnboardingCommand(Guid BusinessId) : IRequest<CompleteOnboardingResponse>;

public record CompleteOnboardingResponse(
    Guid BusinessId,
    bool IsOnboardingComplete,
    DateTime CompletedAt
);