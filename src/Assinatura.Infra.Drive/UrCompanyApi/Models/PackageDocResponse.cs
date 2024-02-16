using System.Text.Json.Serialization;

namespace Assinatura.Infra.Drive.UrCompanyApi.Models;

public class PackageDocResponse
{
    [JsonPropertyName("success")]
    public bool Success { get; set; }

    [JsonPropertyName("message")]
    public List<object> Message { get; set; }

    [JsonPropertyName("data")]
    public DataDocResponse Data { get; set; }
}

public class DataDocResponse
{
    [JsonPropertyName("id")]
    public string Id { get; set; }

    [JsonPropertyName("listDocument")]
    public IEnumerable<DocumentResponse> ListDocument { get; set; }

    [JsonPropertyName("flOpenForNewDocuments")]
    public bool FlOpenForNewDocuments { get; set; }

    [JsonPropertyName("flAutoGenerateSummaryFile")]
    public bool FlAutoGenerateSummaryFile { get; set; }

    [JsonPropertyName("flSummaryFileComplete")]
    public bool FlSummaryFileComplete { get; set; }

    [JsonPropertyName("flUtilizaPkiExpressParaMerge")]
    public bool FlUtilizaPkiExpressParaMerge { get; set; }

    [JsonPropertyName("flMergePkiExpressCompleto")]
    public bool FlMergePkiExpressCompleto { get; set; }
}

public class DocumentResponse
{
    [JsonPropertyName("id")]
    public string Id { get; set; }

    [JsonPropertyName("name")]
    public string Name { get; set; }

    [JsonPropertyName("description")]
    public string Description { get; set; }

    [JsonPropertyName("flSignSequentialOrder")]
    public bool FlSignSequentialOrder { get; set; }

    [JsonPropertyName("flSendOriginalDocumentEmail")]
    public bool FlSendOriginalDocumentEmail { get; set; }

    [JsonPropertyName("flSignatureTimestamp")]
    public bool FlSignatureTimestamp { get; set; }

    [JsonPropertyName("flPublicDocument")]
    public bool FlPublicDocument { get; set; }

    [JsonPropertyName("flMonitoringEventSign")]
    public bool FlMonitoringEventSign { get; set; }

    [JsonPropertyName("listSigner")]
    public IEnumerable<SignerResponse> ListSigner { get; set; }

    [JsonPropertyName("signatureType")]
    public string SignatureType { get; set; }

    [JsonPropertyName("flNotSendEmails")]
    public bool FlNotSendEmails { get; set; }

    [JsonPropertyName("flNaoExibirEmails")]
    public bool FlNaoExibirEmails { get; set; }

    [JsonPropertyName("flCpfCnpjIsRequired")]
    public bool FlCpfCnpjIsRequired { get; set; }

    [JsonPropertyName("flNaoAssinarDashboardUr")]
    public bool FlNaoAssinarDashboardUr { get; set; }

    [JsonPropertyName("idDocumentPackage")]
    public string IdDocumentPackage { get; set; }

    [JsonPropertyName("status")]
    public string Status { get; set; }

    [JsonPropertyName("verificationCode")]
    public string VerificationCode { get; set; }

    [JsonPropertyName("linkVerificationDocument")]
    public Uri LinkVerificationDocument { get; set; }

    [JsonPropertyName("createdDate")]
    public DateTimeOffset CreatedDate { get; set; }

    [JsonPropertyName("lastModifiedDate")]
    public DateTimeOffset LastModifiedDate { get; set; }
}

public class SignerResponse
{
    [JsonPropertyName("id")]
    public string Id { get; set; }

    [JsonPropertyName("name")]
    public string Name { get; set; }

    [JsonPropertyName("email")]
    public string Email { get; set; }

    [JsonPropertyName("flSignerForAll")]
    public bool FlSignerForAll { get; set; }

    [JsonPropertyName("flMonitoringEventSign")]
    public bool FlMonitoringEventSign { get; set; }

    [JsonPropertyName("order")]
    public long Order { get; set; }

    [JsonPropertyName("flEndosso")]
    public bool FlEndosso { get; set; }

    [JsonPropertyName("flPermiteAlterarTipoAssinatura")]
    public bool FlPermiteAlterarTipoAssinatura { get; set; }

    [JsonPropertyName("idDocument")]
    public string IdDocument { get; set; }

    [JsonPropertyName("status")]
    public string Status { get; set; }

    [JsonPropertyName("linkToSign")]
    public Uri LinkToSign { get; set; }
}
