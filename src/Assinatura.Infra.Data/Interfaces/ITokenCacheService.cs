namespace Assinatura.Infra.Data.Interfaces;

public interface ITokenCacheService
{
    public void SetTokenAssEletronica(string token, DateTime expirationTime);
    public string? GetTokenAssEletronica();
    public DateTime? GetTokenExpirationTimeAssEletronica();
    bool TokenAssEletronicaEstaValido();
}
