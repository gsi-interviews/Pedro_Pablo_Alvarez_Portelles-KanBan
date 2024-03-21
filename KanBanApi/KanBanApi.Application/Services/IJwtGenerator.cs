using KanBanApi.Domain.Entities;
using Microsoft.AspNetCore.Identity;

namespace KanBanApi.Application.Services;

public interface IJwtGenerator
{
    public string GetToken(IdentityUser user);
}