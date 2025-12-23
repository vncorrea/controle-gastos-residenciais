using ControleGastos.API.Models;
using ControleGastos.API.Models.DTOs;

namespace ControleGastos.API.Services;

public interface IPessoaService
{
    Task<List<Pessoa>> ListarAsync();
    Task<Pessoa?> ObterPorIdAsync(int id);
    Task<Pessoa> CriarAsync(PessoaCreateDto dto);
    Task<bool> DeletarAsync(int id);
}
