using FastEndpoints;
using KanBanApi.Application.Commands.Register;
using KanBanApi.Application.Dtos;

namespace KanBanApi.WebApi.Endpoints;


public class RegisterEndpoint : Endpoint<RegisterCommand, UserResponse>
{
    public override void Configure()
    {
        Post("/auth/register");
        AllowAnonymous();
    }

    public override async Task<UserResponse> ExecuteAsync(RegisterCommand req, CancellationToken ct)
    {
        return await req.ExecuteAsync(ct);
    }
}