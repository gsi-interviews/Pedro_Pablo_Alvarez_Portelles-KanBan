namespace KanBanApi.Application.Dtos;

public sealed class GoogleConfigDto
{
    public string ClientId { get; private set; } = null!;
    public string ClientSecret { get; private set; } = null!;

    public GoogleConfigDto(string clientId, string clientSecret)
    {
        ClientId = clientId;
        ClientSecret = clientSecret;
    }
}