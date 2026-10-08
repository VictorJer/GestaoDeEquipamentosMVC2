using GestaoDeEquipamentoss.Web.Models;
using GestaoDeEquipamentoss.Web.ModuloChamado;
using GestaoDeEquipamentosWeb.ConsoleApp.Compartilhado;
using GestaoDeEquipamentosWeb.ConsoleApp.Compartilhado.Arquivos;
using GestaoDeEquipamentosWeb.ConsoleApp.ModuloChamado;
using GestaoDeEquipamentosWeb.ConsoleApp.ModuloEquipamento;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace GestaoDeEquipamentoss.Web.Controllers;

public class ChamadoController : Controller
{
    private readonly IRepositorio<Chamado> repositorioChamado;
    private readonly IRepositorio<Equipamento> repositorioEquipamento;

    public ChamadoController()
    {
        ContextoJson contexto = new ContextoJson();
        contexto.Carregar();

        repositorioChamado = new RepositorioChamadoEmArquivo(contexto);
        repositorioEquipamento = new RepositorioEquipamentoEmArquivo(contexto);
    }

    [HttpGet]
    public ActionResult Listar()
    {
        List<Chamado> chamados = repositorioChamado.SelecionarTodos();

        List<ListarChamadoViewModel> listarVms = new List<ListarChamadoViewModel>();

        foreach (Chamado chamado in chamados)
        {
            listarVms.Add(new ListarChamadoViewModel(
                chamado.Id,
                chamado.Titulo,
                chamado.Equipamento.Nome,
                chamado.DataAbertura,
                chamado.TempoDecorrido,
                chamado.EstaConcluido
            ));
        }

        return View(listarVms);
    }

    [HttpGet]
    public ActionResult Cadastrar()
    {
        ViewBag.Equipamentos = CarregarEquipamentos();

        CadastrarChamadoViewModel cadastrarChamadoViewModel = new CadastrarChamadoViewModel(string.Empty, null, string.Empty);

        return View(cadastrarChamadoViewModel);
    }

    private List<SelectListItem> CarregarEquipamentos()
    {
        List<Equipamento> equipamentos = repositorioEquipamento.SelecionarTodos();

        List<SelectListItem> selecionarEquipamento = new List<SelectListItem>();

        foreach (Equipamento e in equipamentos)
        {
            SelectListItem selectListItem = new SelectListItem(
                e.Nome,
                e.Id
            );

            selecionarEquipamento.Add(selectListItem);
        }

        return selecionarEquipamento;
    }
}
