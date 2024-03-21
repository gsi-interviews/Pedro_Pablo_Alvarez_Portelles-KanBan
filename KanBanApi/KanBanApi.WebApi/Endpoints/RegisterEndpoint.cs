using FastEndpoints;
using KanBanApi.Application.Commands.Register;
using KanBanApi.Application.Dtos;

namespace KanBanApi.WebApi.Endpoints;


public class RegisterEndpoint : Endpoint<RegisterCommand, UserReponse>
{
    public override void Configure()
    {
        Post("/auth/register");
        AllowAnonymous();
    }

    public override async Task<UserReponse> ExecuteAsync(RegisterCommand req, CancellationToken ct)
    {
        return await req.ExecuteAsync(ct);
    }
}