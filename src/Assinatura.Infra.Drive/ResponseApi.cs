using System.Dynamic;
using System.Net;

namespace Assinatura.Infra.Drive;

public class ResponseApi<T> where T : class
{
    public HttpStatusCode CodigoHttp { get; set; }
    public T? Dados { get; set; }
    public ExpandoObject? Error { get; set; }
}
