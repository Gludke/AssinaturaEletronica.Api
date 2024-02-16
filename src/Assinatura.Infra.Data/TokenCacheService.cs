using Assinatura.Infra.Data.Interfaces;
using Microsoft.Extensions.Caching.Memory;


namespace Assinatura.Infra.Data;

public class TokenCacheService : ITokenCacheService
{
    private readonly IMemoryCache _cache;

    public TokenCacheService(IMemoryCache memoryCache)
    {
        _cache = memoryCache;
    }

    public void SetTokenAssEletronica(string token, DateTime expirationTime)
    {
        _cache.Set("tokenAssEletronica", token);
        _cache.Set("tokenAssEletronica_Time", expirationTime);
    }

    public string? GetTokenAssEletronica()
    {
        // Obtém o token do cache de memória
        return _cache.Get<string?>("tokenAssEletronica");
    }

    public DateTime? GetTokenExpirationTimeAssEletronica()
    {
        // Obtém o tempo de expiração do token do cache de memória
        return _cache.Get<DateTime?>("tokenAssEletronica_Time");
    }

    public bool TokenAssEletronicaEstaValido()
    {
        var expire = GetTokenExpirationTimeAssEletronica();
        //token expirou ou não existe
        if(expire == null || expire < DateTime.Now)
            return false;

        return true;
    }

}
