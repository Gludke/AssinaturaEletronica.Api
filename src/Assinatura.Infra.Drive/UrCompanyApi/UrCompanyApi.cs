﻿using Assinatura.Infra.Data.Interfaces;
using Assinatura.Infra.Drive.UrCompanyApi.Models;
using Microsoft.Extensions.Configuration;
using System.Dynamic;
using System.Net;
using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;

namespace Assinatura.Infra.Drive.UrCompanyApi;

public class UrCompanyApi : IUrCompanyApi
{
    private ITokenCacheService _tokenCacheService;
    public readonly IConfiguration _config;

    private readonly string _urlApi;
    private readonly string _userId;
    private readonly string _apiId;
    private readonly string _privateKeyBase64;


    public UrCompanyApi(ITokenCacheService tokenCacheService, IConfiguration config)
    {
        _tokenCacheService = tokenCacheService;
        _config = config;
        //criar essas seções em 'appsettings.json'
        _urlApi = _config.GetSection("APIs:UrCompany.urlApi").Value!;
        _userId = _config.GetSection("APIs:UrCompany.userId").Value!;
        _apiId = _config.GetSection("APIs:UrCompany.apiId").Value!;
        _privateKeyBase64 = _config.GetSection("APIs:UrCompany.privateKeyBase64").Value!;
    }

    /// <summary>
    /// Realiza o login na API de assinatura eletrônica
    /// </summary>
    public async Task GetJwtToken()
    {
        const string urlRota = "/auth/signin";

        var jsonBody = CriarBodyJwtToken();

        var request = new HttpRequestMessage(HttpMethod.Post, _urlApi + urlRota)
        {
            Content = new StringContent(jsonBody, Encoding.UTF8, "application/json")
        };

        using (var client = new HttpClient())
        {
            var responseApi = await client.SendAsync(request);
            var content = await responseApi.Content.ReadAsStringAsync();

            var jsonResp = JsonSerializer.Deserialize<GetJwtResponse>(content);

            if (responseApi.IsSuccessStatusCode && jsonResp!.Success)
            {
                //salva o token no cache da API
                _tokenCacheService.SetTokenAssEletronica(jsonResp.Data.Token, jsonResp.Data.ExpireIn);
            }
        }
    }

    public async Task<ResponseApi<object>> ConsultarDocumento(string pacoteDocId)
    {
        if (_tokenCacheService.TokenAssEletronicaEstaValido() == false)
            await GetJwtToken();

        var bearerToken = _tokenCacheService.GetTokenAssEletronica();

        const string urlRota = "/doc/getDocumentPackage";

        throw new NotImplementedException();
    }

    public async Task<ResponseApi<PackageDocResponse>> EnviarPacoteDocumento()
    {
        if (_tokenCacheService.TokenAssEletronicaEstaValido() == false)
            await GetJwtToken();

        var bearerToken = _tokenCacheService.GetTokenAssEletronica();

        const string urlRota = "/doc/createDocumentPackage";

        var response = new ResponseApi<PackageDocResponse>();

        var jsonBody = CriarBodyEnviarPacoteDocumento(new List<Signer> { new Signer("Guilherme", "guiludke@gmail.com.br") });

        var request = new HttpRequestMessage(HttpMethod.Post, _urlApi + urlRota)
        {
            Content = new StringContent(jsonBody, Encoding.UTF8, "application/json"),
            Headers = {
                Authorization = new AuthenticationHeaderValue("Bearer", bearerToken),
            },
        };

        using (var client = new HttpClient())
        {
            var responseApi = await client.SendAsync(request);
            var content = await responseApi.Content.ReadAsStringAsync();

            var jsonContent = JsonSerializer.Deserialize<PackageDocResponse>(content);

            if (responseApi.IsSuccessStatusCode && jsonContent!.Success)
            {
                response.CodigoHttp = responseApi.StatusCode;
                response.Dados = jsonContent;
            }
            else
            {
                response.CodigoHttp = HttpStatusCode.BadRequest;
                response.Error = JsonSerializer.Deserialize<ExpandoObject>(content);
            }
        }

        return response;
    }



    #region OTHER METHODS

    private string CriarBodyJwtToken()
    {
        var body = new
        {
            userId = _userId,
            apiId = _apiId,
            expirationSeconds = 86400,//24h
            privateKeyBase64 = _privateKeyBase64,
        };

        return JsonSerializer.Serialize(body);
    }

    private string CriarBodyEnviarPacoteDocumento(IList<Signer> signers)
    {
        var listSigners = signers.Select(s => new
        {
            name = s.Name,
            email = s.Email,
            flMonitoringEventSign = false
        });

        var body = new
        {
            listDocument = new[]
            {
                new
                {
                    name = "Documento Exemplo 9",
                    description = "Documento para demonstração da API",
                    signatureType = "DIGITAL",
                    flSignSequentialOrder = false,
                    flSendOriginalDocumentEmail = false,
                    flSignatureTimestamp = false,
                    flPublicDocument = false,
                    flMonitoringEventSign = false,
                    listSigner = listSigners,
                    originalFile = "JVBERi0xLjQKJeLjz9MKMSAwIG9iago8PC9UaXRsZShNaWNyb3NvZnQgV29yZCAtIERvY3VtZW50bzEpL0F1dGhvcihMZW9uYXJkbykvQ3JlYXRpb25EYXRlKEQ6MjAxOTA1MTQxMTE3NDMpL0NyZWF0b3IoUFNjcmlwdDUuZGxsIFZlcnNpb24gNS4yLjIpL1Byb2R1Y2VyKEZSRUUgUERGaWxsIFBERiBhbmQgSW1hZ2UgV3JpdGVyKS9Nb2REYXRlKEQ6MjAxOTA1MTQxMTE3NTAtMDMnMDAnKT4+CmVuZG9iagoyIDAgb2JqCjw8L1BhZ2VzIDMgMCBSL1R5cGUvQ2F0YWxvZz4+CmVuZG9iago0IDAgb2JqCjw8L1RpdGxlKE1pY3Jvc29mdCBXb3JkIC0gRG9jdW1lbnRvMSkvQXV0aG9yKExlb25hcmRvKS9Qcm9kdWNlcihHUEwgR2hvc3RzY3JpcHQgOC4xNSkvQ3JlYXRvcihQU2NyaXB0NS5kbGwgVmVyc2lvbiA1LjIuMikvTW9kRGF0ZShEOjIwMTkwNTE0MTExNzQzKS9DcmVhdGlvbkRhdGUoRDoyMDE5MDUxNDExMTc0Myk+PgplbmRvYmoKMyAwIG9iago8PC9Sb3RhdGUgMC9JVFhUKDQuMS42KS9LaWRzWzUgMCBSXS9Db3VudCAxL1R5cGUvUGFnZXM+PgplbmRvYmoKNSAwIG9iago8PC9SZXNvdXJjZXM8PC9Gb250IDYgMCBSL0V4dEdTdGF0ZSA3IDAgUi9Qcm9jU2V0Wy9QREYvVGV4dF0+Pi9Sb3RhdGUgMC9QYXJlbnQgMyAwIFIvTWVkaWFCb3hbMCAwIDU5NSA4NDJdL0NvbnRlbnRzIDggMCBSL1R5cGUvUGFnZT4+CmVuZG9iago4IDAgb2JqCjw8L0ZpbHRlci9GbGF0ZURlY29kZS9MZW5ndGggNjQ3Nz4+c3RyZWFtCnicvV3djt04ckZu+ynOZTdgK6IoUSJytZmZBAmym5lZJzdBLo67e2wP7G7P2D3YN9wnyPuEklisIvlRP8fdgmFArcM/kaxi1Vc//O1UV017qsd/9HD76eoff+5P775cTa9PP/+rf/j93dVvV22ljDH99EI+3346/fMbV9GeVFvV7enNL1d1Za1ttZl+V6dGmapX9tR3Q6WNOr35dPU/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cP/B9T/cvPl1JpKfrv4fX/3ezQplbmRzdHJlYW0KZW5kb2JqCjkgMCBvYmoKPDwvVHlwZS9FeHRHU3RhdGUvT1BNIDE+PgplbmRvYmoKMTAgMCBvYmoKPDwvRm9udE5hbWUvVFBMR1JZK0hlbHZldGljYS1Cb2xkL1N0ZW1WIDExNi9Gb250RmlsZTMgMTEgMCBSL0Rlc2NlbnQgLTIyMC9Bc2NlbnQgOTI4L0ZsYWdzIDQvTWlzc2luZ1dpZHRoIDI3OC9JdGFsaWNBbmdsZSAwL0NoYXJTZXQoL0NjZWRpbGxhL00vTi9DL08vRC9PdGlsZGUvRS9SL1MvVC9JL3NwYWNlKS9DYXBIZWlnaHQgOTI4L0ZvbnRCQm94WzAgLTIyMCA3NzYgOTI4XS9UeXBlL0ZvbnREZXNjcmlwdG9yPj4KZW5kb2JqCjEyIDAgb2JqCjw8L0ZvbnREZXNjcmlwdG9yIDEwIDAgUi9CYXNlRm9udC9UUExHUlkrSGVsdmV0aWNhLUJvbGQvRmlyc3RDaGFyIDMyL0VuY29kaW5nL1dpbkFuc2lFbmNvZGluZy9TdWJ0eXBlL1R5cGUxL1dpZHRoc1syNzggMCAwIDAgMCAwIDAgMCAwIDAgMCAwIDAgMCAwIDAgMCAwIDAgMCAwIDAgMCAwIDAgMCAwIDAgMCAwIDAgMCAwIDAgMCA3MjIgNzIyIDY2NyAwIDAgMCAyNzggMCAwIDAgODMzIDcyMiA3NzggMCAwIDcyMiA2NjcgNjExIDAgMCAwIDAgMCAwIDAgMCAwIDAgMCAwIDAgMCAwIDAgMCAwIDAgMCAwIDAgMCAwIDAgMCAwIDAgMCAwIDAgMCAwIDAgMCAwIDAgMCAwIDAgMCAwIDAgMCAwIDAgMCAwIDAgMCAwIDAgMCAwIDAgMCAwIDAgMCAwIDAgMCAwIDAgMCAwIDAgMCAwIDAgMCAwIDAgMCAwIDAgMCAwIDAgMCAwIDAgMCAwIDAgMCAwIDAgMCAwIDAgMCAwIDAgMCAwIDAgMCAwIDAgMCAwIDAgMCAwIDAgMCAwIDAgMCAwIDAgMCAwIDcyMiAwIDAgMCAwIDAgMCAwIDAgMCAwIDAgMCAwIDc3OF0vTGFzdENoYXIgMjEzL1R5cGUvRm9udD4+CmVuZG9iagoxMyAwIG9iago8PC9Gb250TmFtZS9WSUxTUFIrSGVsdmV0aWNhL1N0ZW1WIDExNC9Gb250RmlsZTMgMTQgMCBSL0Rlc2NlbnQgLTIxOC9Bc2NlbnQgNzQxL0ZsYWdzIDQvTWlzc2luZ1dpZHRoIDI3OC9JdGFsaWNBbmdsZSAwL0NoYXJTZXQoL0wvQS9uL2MvTS9vL2QvTi9DL3AvZS9EL3EvZi9QL0Uvci9nL1EvRi9zL2gvdC9pL1MvdS9qL0kvdi9VL2wvYS9WL3gvbS9iL3NlbWljb2xvbi9zcGFjZS9jb21tYS9wZXJpb2QpL0NhcEhlaWdodCA3NDEvRm9udEJCb3hbLTE4IC0yMTggNzYyIDc0MV0vVHlwZS9Gb250RGVzY3JpcHRvcj4+CmVuZG9iagoxNSAwIG9iago8PC9Gb250RGVzY3JpcHRvciAxMyAwIFIvQmFzZUZvbnQvVklMU1BSK0hlbHZldGljYS9GaXJzdENoYXIgMzIvRW5jb2RpbmcvV2luQW5zaUVuY29kaW5nL1N1YnR5cGUvVHlwZTEvV2lkdGhzWzI3OCAwIDAgMCAwIDAgMCAwIDAgMCAwIDAgMjc4IDAgMjc4IDAgMCAwIDAgMCAwIDAgMCAwIDAgMCAwIDI3OCAwIDAgMCAwIDAgNjY3IDAgNzIyIDcyMiA2NjcgNjExIDAgMCAyNzggMCAwIDU1NiA4MzMgNzIyIDAgNjY3IDc3OCAwIDY2NyAwIDcyMiA2NjcgMCAwIDAgMCAwIDAgMCAwIDAgMCA1NTYgNTU2IDUwMCA1NTYgNTU2IDI3OCA1NTYgNTU2IDIyMiAyMjIgMCAyMjIgODMzIDU1NiA1NTYgNTU2IDU1NiAzMzMgNTAwIDI3OCA1NTYgNTAwIDAgNTAwXS9MYXN0Q2hhciAxMjAvVHlwZS9Gb250Pj4KZW5kb2JqCjE2IDAgb2JqCjw8L0ZvbnROYW1lL09ETk1ERytUVDE1Q3QwMC9TdGVtViA3Mi9EZXNjZW50IDAvQXNjZW50IDYzMS9GbGFncyA0L01pc3NpbmdXaWR0aCA1MDYvSXRhbGljQW5nbGUgMC9Gb250RmlsZTIgMTcgMCBSL0NhcEhlaWdodCA2MzEvRm9udEJCb3hbMCAwIDQ4NSA2MzFdL1R5cGUvRm9udERlc2NyaXB0b3I+PgplbmRvYmoKMTggMCBvYmoKPDwvQmFz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MDAgbiAKMDAwMDAwMDIzMCAwMDAwMCBuIAowMDAwMDAwNDcxIDAwMDAwIG4gCjAwMDAwMDAyNzUgMDAwMDAgbiAKMDAwMDAwMDU0MyAwMDAwMCBuIAowMDAwMDA5MTc3IDAwMDAwIG4gCjAwMDAwMDkxNDggMDAwMDAgbiAKMDAwMDAwMDY5MiAwMDAwMCBuIAowMDAwMDA3MjM3IDAwMDAwIG4gCjAwMDAwMDcyNzggMDAwMDAgbiAKMDAwMDAxODIxMCAwMDAwMCBuIAowMDAwMDA3NTMxIDAwMDAwIG4gCjAwMDAwMDgwNzggMDAwMDAgbiAKMDAwMDAwOTIyOSAwMDAwMCBuIAowMDAwMDA4Mzg3IDAwMDAwIG4gCjAwMDAwMDg3OTcgMDAwMDAgbiAKMDAwMDAxMjAyOCAwMDAwMCBuIAowMDAwMDA4OTg1IDAwMDAwIG4gCjAwMDAwMTk2NzQgMDAwMDAgbiAKMDAwMDAxOTQ1NCAwMDAwMCBuIAp0cmFpbGVyCjw8L0luZm8gMSAwIFIvUm9vdCAyIDAgUi9TaXplIDIxL0lEIFs8YmI2Yzg4ZmYwMDg5YTg1ZmViYzNiZmZmZTJlYTk0YWM+PDdhMjUxMDVkZDQ4YWNlNDMzYWEyMmQyODRlZjcwMTIyPl0+PgpzdGFydHhyZWYKMTk3NTYKJSVFT0YK",
                },
            },
        };

        return JsonSerializer.Serialize(body);
    }

    #endregion
}
