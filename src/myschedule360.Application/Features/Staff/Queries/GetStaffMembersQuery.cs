using MediatR;

namespace myschedule360.Application.Features.Staff.Queries;

public record GetStaffMembersQuery(
    Guid BusinessId,
    bool? IsActive = null
) : IRequest<GetStaffMembersResponse>;

public record GetStaffMembersResponse(
    StaffMemberDto[] StaffMembers
);

public record StaffMemberDto(
    Guid StaffId,
    Guid UserId,
    string FirstName,
    string LastName,
    string Email,
    string Role,
    decimal? CommissionRate,
    bool IsActive,
    DateTime CreatedAt
);