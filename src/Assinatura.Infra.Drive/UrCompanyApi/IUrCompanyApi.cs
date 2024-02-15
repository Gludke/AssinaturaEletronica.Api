﻿using Assinatura.Infra.Drive.UrCompanyApi.Models;

namespace Assinatura.Infra.Drive.UrCompanyApi;

public interface IUrCompanyApi
{
    Task<ResponseApi<GetJwtResponse>> GetJwtToken();
    Task<ResponseApi<PackageDocResponse>> EnviarPacoteDocumento();
    Task<ResponseApi<object>> ConsultarDocumento(string pacoteDocId);
}
