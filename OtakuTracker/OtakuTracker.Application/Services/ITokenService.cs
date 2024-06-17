using System.Security.Claims;
using OtakuTracker.Domain.Models.Auth;

namespace OtakuTracker.Application.Services;

public interface ITokenService
{
    string GenerateAccessToken(IEnumerable<Claim> claims);
    RefreshToken GenerateRefreshToken();
    ClaimsPrincipal GetPrincipalFromExpiredToken(string token);
}
