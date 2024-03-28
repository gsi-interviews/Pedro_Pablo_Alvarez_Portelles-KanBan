using FastEndpoints;
using KanBanApi.Application.Commands.ChangePassword;
using KanBanApi.Application.Dtos;

namespace KanBanApi.WebApi.Endpoints;

public class ChangePasswordEndpoint : Endpoint<ChangePasswordCommand, UserResponse>
{
    public override void Configure()
    {
        Post("/auth/change-password");
    }

    public override async Task<UserResponse> ExecuteAsync(ChangePasswordCommand req, CancellationToken ct)
    {
        return await req.ExecuteAsync(ct);
    }
}