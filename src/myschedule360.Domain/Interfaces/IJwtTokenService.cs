using myschedule360.Domain.Entities;

namespace myschedule360.Domain.Interfaces;

public interface IJwtTokenService
{
    string GenerateToken(User user);
    string GenerateRefreshToken();
}