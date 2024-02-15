using Assinatura.Domain.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Assinatura.Api.Controllers;

[ApiController]
public class DocController : ControllerBase
{
    private readonly IDocService _docService;

    public DocController(IDocService docService)
    {
        _docService = docService;
    }

}
