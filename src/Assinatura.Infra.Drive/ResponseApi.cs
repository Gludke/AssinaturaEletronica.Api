using System.Dynamic;
using System.Net;

namespace Assinatura.Infra.Drive;

public class ResponseApi<T> where T : class
{
    public HttpStatusCode StatusHttp { get; set; }
    public T? Response { get; set; }
    public ExpandoObject? Error { get; set; }
}
