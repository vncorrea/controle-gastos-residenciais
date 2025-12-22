using Microsoft.EntityFrameworkCore;
using ControleGastos.API.Data;
using ControleGastos.API.Models;
using ControleGastos.API.Models.DTOs;

namespace ControleGastos.API.Services;

public class TransacaoService : ITransacaoService
{
    private readonly ApplicationDbContext _context;

    public TransacaoService(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<List<Transacao>> ListarAsync()
    {
        return await _context.Transacoes
            .Include(t => t.Pessoa)
            .Include(t => t.Categoria)
            .OrderByDescending(t => t.Id)
            .ToListAsync();
    }

    public async Task<Transacao?> ObterPorIdAsync(int id)
    {
        return await _context.Transacoes
            .Include(t => t.Pessoa)
            .Include(t => t.Categoria)
            .FirstOrDefaultAsync(t => t.Id == id);
    }

    public async Task<Transacao> CriarAsync(TransacaoCreateDto dto)
    {
        var pessoa = await _context.Pessoas.FirstOrDefaultAsync(p => p.Id == dto.PessoaId);
        if (pessoa == null)
        {
            throw new InvalidOperationException($"Pessoa com ID {dto.PessoaId} não encontrada.");
        }

        var categoria = await _context.Categorias.FirstOrDefaultAsync(c => c.Id == dto.CategoriaId);
        if (categoria == null)
        {
            throw new InvalidOperationException($"Categoria com ID {dto.CategoriaId} não encontrada.");
        }

        if (pessoa.Idade < 18 && dto.Tipo == TipoTransacao.Receita)
        {
            throw new InvalidOperationException(
                "Menores de 18 anos não podem criar transações do tipo Receita. Apenas despesas são permitidas.");
        }

        bool categoriaCompativel = dto.Tipo switch
        {
            TipoTransacao.Despesa => categoria.Finalidade == FinalidadeCategoria.Despesa || 
                                     categoria.Finalidade == FinalidadeCategoria.Ambas,
            TipoTransacao.Receita => categoria.Finalidade == FinalidadeCategoria.Receita || 
                                     categoria.Finalidade == FinalidadeCategoria.Ambas,
            _ => false
        };

        if (!categoriaCompativel)
        {
            var tipoTransacaoStr = dto.Tipo == TipoTransacao.Despesa ? "Despesa" : "Receita";
            var finalidadeStr = categoria.Finalidade switch
            {
                FinalidadeCategoria.Despesa => "Despesa",
                FinalidadeCategoria.Receita => "Receita",
                FinalidadeCategoria.Ambas => "Ambas",
                _ => "Desconhecida"
            };

            throw new InvalidOperationException(
                $"A categoria '{categoria.Descricao}' tem finalidade '{finalidadeStr}' e não pode ser usada " +
                $"em transações do tipo '{tipoTransacaoStr}'.");
        }

        var transacao = new Transacao
        {
            Descricao = dto.Descricao.Trim(),
            Valor = dto.Valor,
            Tipo = dto.Tipo,
            CategoriaId = dto.CategoriaId,
            PessoaId = dto.PessoaId
        };

        _context.Transacoes.Add(transacao);
        await _context.SaveChangesAsync();

        await _context.Entry(transacao).Reference(t => t.Pessoa).LoadAsync();
        await _context.Entry(transacao).Reference(t => t.Categoria).LoadAsync();

        return transacao;
    }
}
