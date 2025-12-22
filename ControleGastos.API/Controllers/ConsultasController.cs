using Microsoft.AspNetCore.Mvc;
using ControleGastos.API.Models.DTOs;
using ControleGastos.API.Services;

namespace ControleGastos.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ConsultasController : ControllerBase
{
    private readonly IConsultaService _consultaService;

    public ConsultasController(IConsultaService consultaService)
    {
        _consultaService = consultaService;
    }

    [HttpGet("totais-por-pessoa")]
    public async Task<ActionResult<TotaisGeraisPessoasDto>> ObterTotaisPorPessoa()
    {
        var resultado = await _consultaService.ObterTotaisPorPessoaAsync();
        return Ok(resultado);
    }

    [HttpGet("totais-por-categoria")]
    public async Task<ActionResult<TotaisGeraisCategoriasDto>> ObterTotaisPorCategoria()
    {
        var resultado = await _consultaService.ObterTotaisPorCategoriaAsync();
        return Ok(resultado);
    }
}
