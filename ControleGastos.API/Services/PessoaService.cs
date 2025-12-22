using Microsoft.EntityFrameworkCore;
using ControleGastos.API.Data;
using ControleGastos.API.Models;
using ControleGastos.API.Models.DTOs;

namespace ControleGastos.API.Services;

public class PessoaService : IPessoaService
{
    private readonly ApplicationDbContext _context;

    public PessoaService(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<List<Pessoa>> ListarAsync()
    {
        return await _context.Pessoas
            .OrderBy(p => p.Nome)
            .ToListAsync();
    }

    public async Task<Pessoa?> ObterPorIdAsync(int id)
    {
        return await _context.Pessoas
            .FirstOrDefaultAsync(p => p.Id == id);
    }

    public async Task<Pessoa> CriarAsync(PessoaCreateDto dto)
    {
        var pessoa = new Pessoa
        {
            Nome = dto.Nome.Trim(),
            Idade = dto.Idade
        };

        _context.Pessoas.Add(pessoa);
        await _context.SaveChangesAsync();

        return pessoa;
    }

    public async Task<bool> DeletarAsync(int id)
    {
        var pessoa = await ObterPorIdAsync(id);
        if (pessoa == null)
        {
            return false;
        }

        _context.Pessoas.Remove(pessoa);
        await _context.SaveChangesAsync();

        return true;
    }
}
