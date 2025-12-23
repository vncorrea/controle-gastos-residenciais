using Microsoft.AspNetCore.Mvc;
using ControleGastos.API.Models;
using ControleGastos.API.Models.DTOs;
using ControleGastos.API.Services;

namespace ControleGastos.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class CategoriasController : ControllerBase
{
    private readonly ICategoriaService _categoriaService;

    public CategoriasController(ICategoriaService categoriaService)
    {
        _categoriaService = categoriaService;
    }

    [HttpGet]
    public async Task<ActionResult<List<Categoria>>> Listar()
    {
        var categorias = await _categoriaService.ListarAsync();
        return Ok(categorias);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<Categoria>> ObterPorId(int id)
    {
        var categoria = await _categoriaService.ObterPorIdAsync(id);
        if (categoria == null)
        {
            return NotFound($"Categoria com ID {id} não encontrada.");
        }

        return Ok(categoria);
    }

    [HttpPost]
    public async Task<ActionResult<Categoria>> Criar([FromBody] CategoriaCreateDto? dto)
    {
        if (dto == null)
        {
            return BadRequest(new { message = "O corpo da requisição não pode estar vazio. Envie um JSON com 'descricao' e 'finalidade' (1=Despesa, 2=Receita, 3=Ambas)." });
        }

        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        try
        {
            var categoria = await _categoriaService.CriarAsync(dto);
            return CreatedAtAction(nameof(ObterPorId), new { id = categoria.Id }, categoria);
        }
        catch (Exception ex)
        {
            return BadRequest(new { message = ex.Message });
        }
    }
}
