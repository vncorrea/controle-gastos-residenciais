using ControleGastos.API.Models;
using ControleGastos.API.Models.DTOs;

namespace ControleGastos.API.Services;

public interface ITransacaoService
{
    Task<List<Transacao>> ListarAsync();
    Task<Transacao?> ObterPorIdAsync(int id);
    Task<Transacao> CriarAsync(TransacaoCreateDto dto);
}
