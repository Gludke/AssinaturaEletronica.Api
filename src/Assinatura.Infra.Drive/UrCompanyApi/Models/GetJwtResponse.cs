using System.Text.Json.Serialization;

namespace Assinatura.Infra.Drive.UrCompanyApi.Models;

public class GetJwtResponse
{
    [JsonPropertyName("token")]
    public string Token { get; set; }

    [JsonPropertyName("issuedAt")]
    public DateTimeOffset IssuedAt { get; set; }

    [JsonPropertyName("expireIn")]
    public DateTimeOffset ExpireIn { get; set; }
}
