using Microsoft.AspNetCore.Identity;

namespace KanBanApi.Domain.Entities;

public class AppUser : IdentityUser, IEntity<Guid>
{
    Guid IEntity<Guid>.Id { get; set; }
}