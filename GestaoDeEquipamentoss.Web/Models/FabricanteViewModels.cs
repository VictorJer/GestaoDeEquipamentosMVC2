using System.ComponentModel.DataAnnotations;

namespace GestaoDeEquipamentoss.Web.Models;

public record ListarFabricantesViewModel(
    string Id,
    string Nome,
    string Email,
    string Telefone
);

public record CadastrarFabricanteViewModel(
    [Required(ErrorMessage = "O campo \"Nome\" deve ser preenchido.")]
    [StringLength(50, MinimumLength = 2, ErrorMessage = "O campo \"Nome\" deve conter entre 2 e 50 caracteres.")]
    string Nome,

    [Required(ErrorMessage = "O campo \"Email\" deve ser preenchido.")]
    [EmailAddress(ErrorMessage = "Informe um endereço de email válido.")]
    string Email,

    [Required(ErrorMessage = "O campo \"Telefone\" deve ser preenchido.")]
    string Telefone
);

public record EditarFabricanteViewModel(
    string Id,
    string Nome,
    string Email,
    string Telefone
);

public record ExcluirFabricanteViewModel(
    string Id,
    string Nome,
    string Email,
    string Telefone
);