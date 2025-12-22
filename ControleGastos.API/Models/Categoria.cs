namespace ControleGastos.API.Models;

public enum FinalidadeCategoria
{
    Despesa = 1,
    Receita = 2,
    Ambas = 3
}

public class Categoria
{
    public int Id { get; set; }
    public string Descricao { get; set; } = string.Empty;
    public FinalidadeCategoria Finalidade { get; set; }
    public ICollection<Transacao> Transacoes { get; set; } = new List<Transacao>();
}
