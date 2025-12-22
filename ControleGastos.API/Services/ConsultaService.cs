using Microsoft.EntityFrameworkCore;
using ControleGastos.API.Data;
using ControleGastos.API.Models;
using ControleGastos.API.Models.DTOs;

namespace ControleGastos.API.Services;

public class ConsultaService : IConsultaService
{
    private readonly ApplicationDbContext _context;

    public ConsultaService(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<TotaisGeraisPessoasDto> ObterTotaisPorPessoaAsync()
    {
        var pessoas = await _context.Pessoas
            .Include(p => p.Transacoes)
            .OrderBy(p => p.Nome)
            .ToListAsync();

        var totaisPessoas = new List<TotaisPessoaDto>();

        foreach (var pessoa in pessoas)
        {
            var totalReceitas = pessoa.Transacoes
                .Where(t => t.Tipo == TipoTransacao.Receita)
                .Sum(t => t.Valor);

            var totalDespesas = pessoa.Transacoes
                .Where(t => t.Tipo == TipoTransacao.Despesa)
                .Sum(t => t.Valor);

            var saldo = totalReceitas - totalDespesas;

            totaisPessoas.Add(new TotaisPessoaDto
            {
                PessoaId = pessoa.Id,
                Nome = pessoa.Nome,
                TotalReceitas = totalReceitas,
                TotalDespesas = totalDespesas,
                Saldo = saldo
            });
        }

        var totalReceitasGeral = totaisPessoas.Sum(p => p.TotalReceitas);
        var totalDespesasGeral = totaisPessoas.Sum(p => p.TotalDespesas);
        var saldoLiquidoGeral = totalReceitasGeral - totalDespesasGeral;

        return new TotaisGeraisPessoasDto
        {
            Pessoas = totaisPessoas,
            TotalReceitasGeral = totalReceitasGeral,
            TotalDespesasGeral = totalDespesasGeral,
            SaldoLiquidoGeral = saldoLiquidoGeral
        };
    }

    public async Task<TotaisGeraisCategoriasDto> ObterTotaisPorCategoriaAsync()
    {
        var categorias = await _context.Categorias
            .Include(c => c.Transacoes)
            .OrderBy(c => c.Descricao)
            .ToListAsync();

        var totaisCategorias = new List<TotaisCategoriaDto>();

        foreach (var categoria in categorias)
        {
            var totalReceitas = categoria.Transacoes
                .Where(t => t.Tipo == TipoTransacao.Receita)
                .Sum(t => t.Valor);

            var totalDespesas = categoria.Transacoes
                .Where(t => t.Tipo == TipoTransacao.Despesa)
                .Sum(t => t.Valor);

            var saldo = totalReceitas - totalDespesas;

            totaisCategorias.Add(new TotaisCategoriaDto
            {
                CategoriaId = categoria.Id,
                Descricao = categoria.Descricao,
                TotalReceitas = totalReceitas,
                TotalDespesas = totalDespesas,
                Saldo = saldo
            });
        }

        var totalReceitasGeral = totaisCategorias.Sum(c => c.TotalReceitas);
        var totalDespesasGeral = totaisCategorias.Sum(c => c.TotalDespesas);
        var saldoLiquidoGeral = totalReceitasGeral - totalDespesasGeral;

        return new TotaisGeraisCategoriasDto
        {
            Categorias = totaisCategorias,
            TotalReceitasGeral = totalReceitasGeral,
            TotalDespesasGeral = totalDespesasGeral,
            SaldoLiquidoGeral = saldoLiquidoGeral
        };
    }
}
