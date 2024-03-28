using KanBanApi.Domain.Entities;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace KanBanApi.Infraestructure.DbContexts;

public sealed class DefaultDbContext : IdentityDbContext
{
    public DefaultDbContext(DbContextOptions<DefaultDbContext> options) : base(options) { }

    public DbSet<Todo> Todos { get; set; }
    public DbSet<TodoHistory> TodoHistories { get; set; }
    public DbSet<TodoList> TodoLists { get; set; }
}