using Microsoft.AspNet.WebHooks;
using Newtonsoft.Json.Linq;

namespace Assinatura.Webhook.Api.Controllers;

public class WebhookController : WebHookHandler
{
    public WebhookController()
    {
        this.Receiver = GenericJsonWebHookReceiver.ReceiverName;
    }

    public override Task ExecuteAsync(string receiver, WebHookHandlerContext context)
    {
        try
        {
            // Get JSON from WebHook
            JObject data = context.GetDataOrDefault<JObject>();

            // Get the action for this WebHook coming from the action query parameter in the URI
            string action = context.Actions.FirstOrDefault();

        }
        catch (Exception)
        {

            throw;
        }

        return Task.FromResult(true);

    }
}
