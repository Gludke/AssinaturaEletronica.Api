using Assinatura.Domain.Services.Interfaces;
using Assinatura.Infra.Data.Interfaces;
using Assinatura.Infra.Drive.UrCompanyApi;

namespace Assinatura.Domain.Services;

public class DocService : IDocService
{
	public IUrCompanyApi _urCompanyApi;
    private ITokenCacheService _tokenCacheService;

	public DocService(IUrCompanyApi urCompanyApi, ITokenCacheService tokenCacheService)
	{
		_urCompanyApi = urCompanyApi;
		_tokenCacheService = tokenCacheService;
	}

	public async Task CriarPacoteDocs()
	{
		await _urCompanyApi.GetJwtToken();
        await _urCompanyApi.ConsultarDocumento("id");


    }
}
