using GestaoDeEquipamentosWeb.ConsoleApp.Compartilhado;
using GestaoDeEquipamentosWeb.ConsoleApp.Compartilhado.Arquivos;
using GestaoDeEquipamentosWeb.ConsoleApp.ModuloChamado;

namespace GestaoDeEquipamentoss.Web.ModuloChamado;

public class RepositorioChamadoEmArquivo : RepositorioBaseEmArquivo<Chamado>, IRepositorio<Chamado>
{
    public RepositorioChamadoEmArquivo(ContextoJson contexto) : base(contexto)
    {
    }

    protected override List<Chamado> CarregarRegistros()
    {
        return contexto.Chamados;
    }
}