using System.Security.Claims;
using KanBanApi.Application.Services;
using Microsoft.AspNetCore.Http;


namespace KanBanApi.Infraestructure.Services;

public sealed class ActiveSession : IActiveSession
{
    private readonly ClaimsPrincipal? _user;
    private string? _currentUserId;
    private string? _baseUrl;

    public ActiveSession(IHttpContextAccessor accessor)
    {
        _user = accessor?.HttpContext?.User;
        var request = accessor?.HttpContext?.Request;
        _baseUrl = $"{request?.Scheme}://{request?.Host}{request?.PathBase}";
    }

    public string BaseUrl() => _baseUrl ?? String.Empty;

    public ClaimsPrincipal? GetClaimPrincipal() => _user;

    public string? UserId() => _currentUserId ??= _user?.FindFirst(ClaimTypes.Name)?.Value;
}