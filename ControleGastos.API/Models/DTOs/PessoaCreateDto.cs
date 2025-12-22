using System.ComponentModel.DataAnnotations;

namespace ControleGastos.API.Models.DTOs;

public class PessoaCreateDto
{
    [Required(ErrorMessage = "O nome é obrigatório")]
    [StringLength(200, MinimumLength = 1, ErrorMessage = "O nome deve ter entre 1 e 200 caracteres")]
    public string Nome { get; set; } = string.Empty;

    [Required(ErrorMessage = "A idade é obrigatória")]
    [Range(1, 150, ErrorMessage = "A idade deve ser um número positivo entre 1 e 150")]
    public int Idade { get; set; }
}
