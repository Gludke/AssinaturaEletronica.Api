namespace Assinatura.Infra.Drive.UrCompanyApi;

public interface IUrCompanyApi
{
    Task<ResponseApi<object>> GetJwtToken();
    Task<ResponseApi<object>> EnviarDocumento();
    Task<ResponseApi<object>> ConsultarDocumento();
}
