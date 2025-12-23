using Microsoft.EntityFrameworkCore;
using ControleGastos.API.Data;
using ControleGastos.API.Models;
using ControleGastos.API.Models.DTOs;

namespace ControleGastos.API.Services;

public class CategoriaService : ICategoriaService
{
    private readonly ApplicationDbContext _context;

    public CategoriaService(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<List<Categoria>> ListarAsync()
    {
        return await _context.Categorias
            .OrderBy(c => c.Descricao)
            .ToListAsync();
    }

    public async Task<Categoria?> ObterPorIdAsync(int id)
    {
        return await _context.Categorias
            .FirstOrDefaultAsync(c => c.Id == id);
    }

    public async Task<Categoria> CriarAsync(CategoriaCreateDto dto)
    {
        var categoria = new Categoria
        {
            Descricao = dto.Descricao.Trim(),
            Finalidade = dto.Finalidade
        };

        _context.Categorias.Add(categoria);
        await _context.SaveChangesAsync();

        return categoria;
    }
}
