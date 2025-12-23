using System.ComponentModel.DataAnnotations;
using ControleGastos.API.Models;

namespace ControleGastos.API.Models.DTOs;

public class TransacaoCreateDto
{
    [Required(ErrorMessage = "A descrição é obrigatória")]
    [StringLength(500, MinimumLength = 1, ErrorMessage = "A descrição deve ter entre 1 e 500 caracteres")]
    public string Descricao { get; set; } = string.Empty;

    [Required(ErrorMessage = "O valor é obrigatório")]
    [Range(0.01, double.MaxValue, ErrorMessage = "O valor deve ser maior que zero")]
    public decimal Valor { get; set; }

    [Required(ErrorMessage = "O tipo é obrigatório")]
    [Range(1, 2, ErrorMessage = "O tipo deve ser: 1 (Despesa) ou 2 (Receita)")]
    public TipoTransacao Tipo { get; set; }

    [Required(ErrorMessage = "A categoria é obrigatória")]
    public int CategoriaId { get; set; }

    [Required(ErrorMessage = "A pessoa é obrigatória")]
    public int PessoaId { get; set; }
}
