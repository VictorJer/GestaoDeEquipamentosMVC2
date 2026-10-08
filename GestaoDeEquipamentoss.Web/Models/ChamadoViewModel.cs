namespace GestaoDeEquipamentoss.Web.Models;

public record ListarChamadoViewModel(
    string Id,
    string Titulo,
    string Equipamento,
    DateTime DataAbertura,
    int TempoDecorrido,
    bool EstaConcluido
);