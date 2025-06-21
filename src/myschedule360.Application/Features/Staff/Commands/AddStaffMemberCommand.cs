using MediatR;

namespace myschedule360.Application.Features.Staff.Commands;

public record AddStaffMemberCommand(
    Guid UserId,
    Guid BusinessId,
    string Role,
    decimal? CommissionRate,
    bool IsActive = true
) : IRequest<AddStaffMemberResponse>;

public record AddStaffMemberResponse(
    Guid StaffId,
    Guid UserId,
    string FirstName,
    string LastName,
    string Role,
    bool IsActive
);