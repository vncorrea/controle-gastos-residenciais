using Microsoft.AspNetCore.Mvc;
using ControleGastos.API.Models;
using ControleGastos.API.Models.DTOs;
using ControleGastos.API.Services;

namespace ControleGastos.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class TransacoesController : ControllerBase
{
    private readonly ITransacaoService _transacaoService;

    public TransacoesController(ITransacaoService transacaoService)
    {
        _transacaoService = transacaoService;
    }

    [HttpGet]
    public async Task<ActionResult<List<Transacao>>> Listar()
    {
        var transacoes = await _transacaoService.ListarAsync();
        return Ok(transacoes);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<Transacao>> ObterPorId(int id)
    {
        var transacao = await _transacaoService.ObterPorIdAsync(id);
        if (transacao == null)
        {
            return NotFound($"Transação com ID {id} não encontrada.");
        }

        return Ok(transacao);
    }

    [HttpPost]
    public async Task<ActionResult<Transacao>> Criar([FromBody] TransacaoCreateDto dto)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        try
        {
            var transacao = await _transacaoService.CriarAsync(dto);
            return CreatedAtAction(nameof(ObterPorId), new { id = transacao.Id }, transacao);
        }
        catch (InvalidOperationException ex)
        {
            return BadRequest(new { message = ex.Message });
        }
        catch (Exception ex)
        {
            return StatusCode(500, new { message = "Erro interno ao processar a solicitação.", details = ex.Message });
        }
    }
}
