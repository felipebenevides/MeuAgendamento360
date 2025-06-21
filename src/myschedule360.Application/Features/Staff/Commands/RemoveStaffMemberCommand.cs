using MediatR;

namespace myschedule360.Application.Features.Staff.Commands;

public record RemoveStaffMemberCommand(Guid StaffId) : IRequest<RemoveStaffMemberResponse>;

public record RemoveStaffMemberResponse(
    bool Success,
    string Message
);