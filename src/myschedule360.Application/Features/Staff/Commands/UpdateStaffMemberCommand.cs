using MediatR;

namespace myschedule360.Application.Features.Staff.Commands;

public record UpdateStaffMemberCommand(
    Guid StaffId,
    string Role,
    decimal? CommissionRate
) : IRequest<UpdateStaffMemberResponse>;

public record UpdateStaffMemberResponse(
    Guid StaffId,
    string Role,
    decimal? CommissionRate,
    DateTime UpdatedAt
);