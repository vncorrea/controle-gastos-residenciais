namespace ControleGastos.API.Models.DTOs;

public class TotaisPessoaDto
{
    public int PessoaId { get; set; }
    public string Nome { get; set; } = string.Empty;
    public decimal TotalReceitas { get; set; }
    public decimal TotalDespesas { get; set; }
    public decimal Saldo { get; set; }
}

public class TotaisGeraisPessoasDto
{
    public List<TotaisPessoaDto> Pessoas { get; set; } = new();
    public decimal TotalReceitasGeral { get; set; }
    public decimal TotalDespesasGeral { get; set; }
    public decimal SaldoLiquidoGeral { get; set; }
}
