using ControleGastos.API.Models;
using ControleGastos.API.Models.DTOs;

namespace ControleGastos.API.Services;

public interface ICategoriaService
{
    Task<List<Categoria>> ListarAsync();
    Task<Categoria?> ObterPorIdAsync(int id);
    Task<Categoria> CriarAsync(CategoriaCreateDto dto);
}
