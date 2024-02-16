using Assinatura.Domain.Services.Interfaces;
using Assinatura.Infra.Drive.UrCompanyApi;

namespace Assinatura.Domain.Services;

public class DocService : IDocService
{
	public IUrCompanyApi _urCompanyApi;

	public DocService(IUrCompanyApi urCompanyApi)
	{
		_urCompanyApi = urCompanyApi;
	}

	public async Task CriarPacoteDocs()
	{
		var response = await _urCompanyApi.GetJwtToken();


    }
}
