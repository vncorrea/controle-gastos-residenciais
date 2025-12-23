using ControleGastos.API.Models.DTOs;

namespace ControleGastos.API.Services;

public interface IConsultaService
{
    Task<TotaisGeraisPessoasDto> ObterTotaisPorPessoaAsync();
    Task<TotaisGeraisCategoriasDto> ObterTotaisPorCategoriaAsync();
}
