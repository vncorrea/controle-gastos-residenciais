namespace ControleGastos.API.Models.DTOs;

public class TotaisCategoriaDto
{
    public int CategoriaId { get; set; }
    public string Descricao { get; set; } = string.Empty;
    public decimal TotalReceitas { get; set; }
    public decimal TotalDespesas { get; set; }
    public decimal Saldo { get; set; }
}

public class TotaisGeraisCategoriasDto
{
    public List<TotaisCategoriaDto> Categorias { get; set; } = new();
    public decimal TotalReceitasGeral { get; set; }
    public decimal TotalDespesasGeral { get; set; }
    public decimal SaldoLiquidoGeral { get; set; }
}
