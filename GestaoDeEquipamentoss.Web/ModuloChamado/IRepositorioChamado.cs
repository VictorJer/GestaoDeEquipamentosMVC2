using GestaoDeEquipamentosWeb.ConsoleApp.Compartilhado;
using GestaoDeEquipamentosWeb.ConsoleApp.ModuloChamado;

public interface IRepositorioChamado : IRepositorio<Chamado>
{
    List<Chamado> FiltrarChamados(FiltroChamado filtro);
}