using System.Security.Claims;

namespace KanBanApi.Application.Services;

public interface IActiveSession
{
    string? UserId();
    string BaseUrl();
    ClaimsPrincipal? GetClaimPrincipal();
}