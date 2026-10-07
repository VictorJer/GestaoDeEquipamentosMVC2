using GestaoDeEquipamentoss.Web.ModuloFabricante;

public record ListarEquipamentosViewModel(
    string Id,
    string Nome,
    decimal PrecoAquisicao,
    DateTime DataFabricacao,
    string NomeFabricante
);

public record CadastrarEquipamentoViewModel(
    string Nome,
    decimal PrecoAquisicao,
    DateTime DataFabricacao,
    string FabricanteId
);

public record EditarEquipamentoViewModel(
    string Id,
    string Nome,
    decimal PrecoAquisicao,
    DateTime DataFabricacao,
    string FabricanteId
);


    // public string Nome { get; set; } = string.Empty;
    // public decimal PrecoAquisicao { get; set; }
    // public DateTime DataFabricacao { get; set; }
    // public Fabricante Fabricante { get; set; } = null!;