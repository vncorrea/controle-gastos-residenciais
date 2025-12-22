using Microsoft.AspNetCore.Mvc;
using ControleGastos.API.Models;
using ControleGastos.API.Models.DTOs;
using ControleGastos.API.Services;

namespace ControleGastos.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class PessoasController : ControllerBase
{
    private readonly IPessoaService _pessoaService;

    public PessoasController(IPessoaService pessoaService)
    {
        _pessoaService = pessoaService;
    }

    [HttpGet]
    public async Task<ActionResult<List<Pessoa>>> Listar()
    {
        var pessoas = await _pessoaService.ListarAsync();
        return Ok(pessoas);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<Pessoa>> ObterPorId(int id)
    {
        var pessoa = await _pessoaService.ObterPorIdAsync(id);
        if (pessoa == null)
        {
            return NotFound($"Pessoa com ID {id} não encontrada.");
        }

        return Ok(pessoa);
    }

    [HttpPost]
    public async Task<ActionResult<Pessoa>> Criar([FromBody] PessoaCreateDto dto)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        try
        {
            var pessoa = await _pessoaService.CriarAsync(dto);
            return CreatedAtAction(nameof(ObterPorId), new { id = pessoa.Id }, pessoa);
        }
        catch (Exception ex)
        {
            return BadRequest(new { message = ex.Message });
        }
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Deletar(int id)
    {
        var deletado = await _pessoaService.DeletarAsync(id);
        if (!deletado)
        {
            return NotFound($"Pessoa com ID {id} não encontrada.");
        }

        return NoContent();
    }
}
