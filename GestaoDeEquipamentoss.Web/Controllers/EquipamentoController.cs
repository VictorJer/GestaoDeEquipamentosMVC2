using GestaoDeEquipamentoss.Web.Models;
using GestaoDeEquipamentoss.Web.ModuloFabricante;
using GestaoDeEquipamentosWeb.ConsoleApp.Compartilhado;
using GestaoDeEquipamentosWeb.ConsoleApp.Compartilhado.Arquivos;
using GestaoDeEquipamentosWeb.ConsoleApp.ModuloEquipamento;
using GestaoDeEquipamentosWeb.ConsoleApp.ModuloFabricante;
using Microsoft.AspNetCore.Mvc;

namespace GestaoDeEquipamentoss.Web.Controllers;

public class EquipamentoController : Controller
{
    IRepositorio<Equipamento> repositorioEquipamento;
    IRepositorio<Fabricante> repositorioFabricante;
    public EquipamentoController()
    {
        ContextoJson contexto = new ContextoJson();
        contexto.Carregar();

        repositorioEquipamento = new RepositorioEquipamentoEmArquivo(contexto);
        repositorioFabricante = new RepositorioFabricanteEmArquivo(contexto);
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

    [HttpGet]
    public ActionResult Cadastrar()
    {
        ViewBag.fabricantes = CarregarFabricantes();

        return View();
    }

    [HttpPost]
    public ActionResult Cadastrar(CadastrarEquipamentoViewModel cadastrarVm)
    {
        Fabricante? fabricanteSelecionado = repositorioFabricante.SelecionarPorId(cadastrarVm.FabricanteId);

        if (fabricanteSelecionado == null)
            return RedirectToAction(nameof(Listar));

        Equipamento novoEquipamento = new Equipamento(
            cadastrarVm.Nome,
            cadastrarVm.PrecoAquisicao,
            cadastrarVm.DataFabricacao,
            fabricanteSelecionado
        );

        repositorioEquipamento.Cadastrar(novoEquipamento);

        return RedirectToAction(nameof(Listar));
    }

    [HttpGet]
    public ActionResult Editar(string id)
    {
        ViewBag.fabricantes = CarregarFabricantes();

        Equipamento? equipamentoSelecionado = repositorioEquipamento.SelecionarPorId(id);

        if (equipamentoSelecionado == null)
            return RedirectToAction(nameof(Listar));

        EditarEquipamentoViewModel editarVm = new EditarEquipamentoViewModel(
            equipamentoSelecionado.Id,
            equipamentoSelecionado.Nome,
            equipamentoSelecionado.PrecoAquisicao,
            equipamentoSelecionado.DataFabricacao,
            equipamentoSelecionado.Fabricante
        );

        return View(editarVm);
    }

    private List<ListarFabricantesViewModel> CarregarFabricantes()
    {
        List<Fabricante> fabricantes = repositorioFabricante.SelecionarTodos();

        List<ListarFabricantesViewModel> listarVms = new List<ListarFabricantesViewModel>();

        foreach (Fabricante fabricante in fabricantes)
        {
            ListarFabricantesViewModel vm = new ListarFabricantesViewModel(
                fabricante.Id,
                fabricante.Nome,
                fabricante.Email,
                fabricante.Telefone
            );

            listarVms.Add(vm);
        }

        return listarVms;
    }
}