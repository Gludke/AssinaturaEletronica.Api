﻿using Assinatura.Infra.Drive.UrCompanyApi.Models;
using System.Dynamic;
using System.Text;
using System.Text.Json;

namespace Assinatura.Infra.Drive.UrCompanyApi;

public class UrCompanyApi : IUrCompanyApi
{
    private readonly string _urlApi = "https://demo.urcompany.com.br/restapi";

    private readonly string _userId = "bDgN2SbCeJsAaBdH-cEkVhQ63uFlWjSaA";
    private readonly string _apiId = "q75Zr875zQ75zR86-3VgN2SaAbCeKvGp4";
    private readonly string _privateKeyBase64 = "MIICdgIBADANBgkqhkiG9w0BAQEFAASCAmAwggJcAgEAAoGBAKWkhUwpDOPHPZt4mr3lu5UdY2GsJjOCSXU60YrMA9cyNlaSbtYCR39mPkskFtUCzsIO5xZJ7Tf2hTTjR7klXTR9ITdM9uwhDPcw6fxFhC+dOzztg43FyY1wMI5mK6dCWWoRldLjoOPvmOgbT0OLG/ppZtkcTXeDDn4vB+BI4UHXAgMBAAECgYAhorLesEdJyZ+c/nSNsyTQNtODde2b8AzynSsHwD3XaP7XvYx8MKJMIHrtzzpDrvpFNbl/MSvWfVy3TJ+33Pp75dKr0yFHpub9U4deFGQ7RL+Y/IGg/bO23IECasLFG2r5PK5AU0C2eYgGB0p4DUUCvVOT/OkRGVNxMuImmnmygQJBAOlwj87uiiBa/i66Kd6m/i7+BrTej9eona3865QGcp1LSDarRLemUUNPYxQ3qZdiHFTw4U/cs2EK6omnL+1CeucCQQC1pp6b3drhtkb7yXPW7iC25evjpUVZYMqDL2y7bGInoaAhaKacm6pAl2XM/P2aN5jv8ZhgO92OWAPdo0uzQZORAkBlPcsw3OWM6MnKbDTSeqxMpyEzej76MgfIuKNW/IDi1Q6JnzfbSkd+IMUAtK9Zl1RgRmQBZd9qG/jiIF850BZLAkBX2wNhXXbsre09AB0fucJm02M4kgmthcvMkRZku7HpexloryXOHtfEL7VT5JR/jx5QBqhs+udYXidYfg8x3qiRAkEA0mcgYDiy5JUedveslAg5/S1BIUhix3EYfWxIeQwuOm1U4OZRT9AeKZFltR9tCgDyyMMCkHvJDHimXabAxQMKRg==";


    public async Task<ResponseApi<GetJwtResponse>> GetJwtToken()
    {
        const string urlRota = "/auth/signin";

        var response = new ResponseApi<GetJwtResponse>();

        var bodyJson = CriarBodyJsonJwtToken();

        var request = new HttpRequestMessage(HttpMethod.Post, _urlApi + urlRota);
        request.Content = new StringContent(bodyJson, Encoding.UTF8, "application/json");

        using (var client = new HttpClient())
        {
            var responseApi = await client.SendAsync(request);
            var content = await responseApi.Content.ReadAsStringAsync();

            var jsonResp = JsonSerializer.Deserialize<GetJwtResponse>(content);

            if (responseApi.IsSuccessStatusCode)
            {
                response.CodigoHttp = responseApi.StatusCode;
                response.Dados = jsonResp;
            }
            else
            {
                response.CodigoHttp = responseApi.StatusCode;
                response.Error = JsonSerializer.Deserialize<ExpandoObject>(content);
            }
        }

        return response;
    }

    public Task<ResponseApi<object>> ConsultarDocumento(string pacoteDocId)
    {
        const string urlRota = "/doc/getDocumentPackage";

        throw new NotImplementedException();
    }

    public Task<ResponseApi<PackageDocResponse>> EnviarPacoteDocumento()
    {
        const string urlRota = "/doc/createDocumentPackage";

        throw new NotImplementedException();
    }

    #region OTHER METHODS

    private string CriarBodyJsonJwtToken()
    {
        var body = new
        {
            userId = _userId,
            apiId = _apiId,
            expirationSeconds = 3600,
            privateKeyBase64 = _privateKeyBase64,
        };

        return JsonSerializer.Serialize(body);
    }

    #endregion
}
