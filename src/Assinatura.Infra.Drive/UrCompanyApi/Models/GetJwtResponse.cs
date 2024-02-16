using System.Text.Json.Serialization;

namespace Assinatura.Infra.Drive.UrCompanyApi.Models;

public class GetJwtResponse
{
    [JsonPropertyName("success")]
    public bool Success { get; set; }

    [JsonPropertyName("message")]
    public List<object> Message { get; set; }

    [JsonPropertyName("data")]
    public DataJwtResponse Data { get; set; }
}

public class DataJwtResponse
{
    [JsonPropertyName("token")]
    public string Token { get; set; }

    [JsonPropertyName("issuedAt")]
    public DateTime IssuedAt { get; set; }

    [JsonPropertyName("expireIn")]
    public DateTime ExpireIn { get; set; }
}
