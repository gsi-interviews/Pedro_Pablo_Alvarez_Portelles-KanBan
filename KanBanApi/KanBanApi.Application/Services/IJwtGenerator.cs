using KanBanApi.Domain.Entities;

namespace KanBanApi.Application.Services;

public interface IJwtGenerator
{
    public string GetToken(AppUser user);
}