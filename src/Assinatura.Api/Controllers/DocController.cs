using Assinatura.Api.ViewModels;
using Assinatura.Domain.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Assinatura.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class DocController : ControllerBase
{
    private readonly IDocService _docService;

    public DocController(IDocService docService)
    {
        _docService = docService;
    }

    [HttpPost("criarPacoteDocs")]
    public async Task<IActionResult> CriarPacoteDocs()//[FromBody] CriarPacoteDocsViewModel model
    {
        if (!ModelState.IsValid) return BadRequest(new { sucess = false });

        await _docService.CriarPacoteDocs();

        return Ok(new { sucess = true });
    }
}
