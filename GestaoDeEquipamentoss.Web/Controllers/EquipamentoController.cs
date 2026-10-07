using GestaoDeEquipamentosWeb.ConsoleApp.Compartilhado;
using GestaoDeEquipamentosWeb.ConsoleApp.Compartilhado.Arquivos;
using GestaoDeEquipamentosWeb.ConsoleApp.ModuloEquipamento;
using Microsoft.AspNetCore.Mvc;

namespace GestaoDeEquipamentoss.Web.Controllers;

public class EquipamentoController : Controller
{
    IRepositorio<Equipamento> repositorioEquipamento;
    public EquipamentoController()
    {
        ContextoJson contexto = new ContextoJson();
        contexto.Carregar();

        repositorioEquipamento = new RepositorioEquipamentoEmArquivo(contexto);
    }

    [HttpGet]
    public ActionResult Listar()
    {
        var equipamentos = repositorioEquipamento.SelecionarTodos();

        List<ListarEquipamentosViewModel> viewModel = new List<ListarEquipamentosViewModel>();

        foreach (var equipamento in equipamentos)
        {
            viewModel.Add(new ListarEquipamentosViewModel(
                Id: equipamento.Id,
                Nome: equipamento.Nome,
                PrecoAquisicao: equipamento.PrecoAquisicao,
                DataFabricacao: equipamento.DataFabricacao,
                NomeFabricante: equipamento.Fabricante.Nome
            ));
        }

        return View(viewModel);
    }
}