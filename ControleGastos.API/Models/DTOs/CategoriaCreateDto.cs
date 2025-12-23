using System.ComponentModel.DataAnnotations;
using ControleGastos.API.Models;

namespace ControleGastos.API.Models.DTOs;

public class CategoriaCreateDto
{
    [Required(ErrorMessage = "A descrição é obrigatória")]
    [StringLength(200, MinimumLength = 1, ErrorMessage = "A descrição deve ter entre 1 e 200 caracteres")]
    public string Descricao { get; set; } = string.Empty;

    [Required(ErrorMessage = "A finalidade é obrigatória")]
    [Range(1, 3, ErrorMessage = "A finalidade deve ser: 1 (Despesa), 2 (Receita) ou 3 (Ambas)")]
    public FinalidadeCategoria Finalidade { get; set; }
}
