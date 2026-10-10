using GestaoDeEquipamentosWeb.ConsoleApp.Compartilhado;
using GestaoDeEquipamentosWeb.ConsoleApp.Compartilhado.Arquivos;
using GestaoDeEquipamentosWeb.ConsoleApp.ModuloChamado;

namespace GestaoDeEquipamentoss.Web.ModuloChamado;

public class RepositorioChamadoEmArquivo : RepositorioBaseEmArquivo<Chamado>, IRepositorioChamado
{
    public RepositorioChamadoEmArquivo(ContextoJson contexto) : base(contexto)
    {
    }

    // public List<Chamado> SelecionarTodosConcluido()
    // {
    //     List<Chamado> chamadosConcluidos = new List<Chamado>();

    //     foreach(Chamado c in chamadosConcluidos)
    //     {
    //         if (c.EstaConcluido)
    //             chamadosConcluidos.Add(c);
    //     }

    //     return chamadosConcluidos;
    // }

    // public List<Chamado> SelecionarTodosEmAberto()
    // {
    //     List<Chamado> chamadosAbertos = new List<Chamado>();

    //     foreach(Chamado c in chamadosAbertos)
    //     {
    //         if (!c.EstaConcluido)
    //             chamadosAbertos.Add(c);
    //     }

    //     return chamadosAbertos;
    // }

    public List<Chamado> FiltrarChamados(FiltroChamado filtro)
    {
        List<Chamado> chamadosFiltrados = new List<Chamado>();

        foreach(Chamado c in registros)
        {
            if (filtro(c))
                chamadosFiltrados.Add(c);
        }

        return chamadosFiltrados;
    }

    protected override List<Chamado> CarregarRegistros()
    {
        return contexto.Chamados;
    }
}