using System.ComponentModel.DataAnnotations;
using GestaoDeEquipamentoss.Web.ModuloFabricante;

public record ListarEquipamentosViewModel(
    string Id,
    string Nome,
    decimal PrecoAquisicao,
    DateTime DataFabricacao,
    string NomeFabricante
);

public record CadastrarEquipamentoViewModel(
    [Required(ErrorMessage = "O campo \"Nome\" deve ser preenchido.")]
    [StringLength(50, MinimumLength = 2, ErrorMessage = "O campo \"Nome\" deve conter entre 2 e 50 caracteres.")]
    string Nome,

    [Range(typeof(decimal), "0.01", "79228162514264337593543950335", ErrorMessage = "O campo \"Preço de Aquisição\" deve conter um valor positivo.")]
    decimal PrecoAquisicao,

    [DataFabricacaoNaoFutura]
    DateTime DataFabricacao,

    [Required(ErrorMessage = "O campo \"Fabricante\" deve ser preenchido.")]
    string FabricanteId
);

public record EditarEquipamentoViewModel(
    string Id,
    string Nome,
    decimal PrecoAquisicao,
    DateTime DataFabricacao,
    string FabricanteId
);

public record ExcluirEquipamentoViewModel(
    string Id,
    string Nome,
    decimal PrecoAquisicao,
    DateTime DataFabricacao,
    string Fabricante    
);


    // public string Nome { get; set; } = string.Empty;
    // public decimal PrecoAquisicao { get; set; }
    // public DateTime DataFabricacao { get; set; }
    // public Fabricante Fabricante { get; set; } = null!;

public sealed class DataFabricacaoNaoFuturaAttribute : ValidationAttribute
{
    public DataFabricacaoNaoFuturaAttribute()
        : base("O campo \"Data de Fabricação\" deve conter uma data válida que não seja futura.")
    {
    }

    public override bool IsValid(object? value)
    {
        return value is DateTime dataFabricacao
            && dataFabricacao != default
            && dataFabricacao.Date <= DateTime.Today;
    }
}