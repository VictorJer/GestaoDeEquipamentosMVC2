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
            equipamentoSelecionado.Fabricante.Id
        );

        return View(editarVm);
    }

    [HttpPost]
    public ActionResult Editar(EditarEquipamentoViewModel vm)
    {
        Fabricante? fabricante = repositorioFabricante.SelecionarPorId(vm.FabricanteId);

        if (fabricante == null)
            return RedirectToAction(nameof(Listar));

        Equipamento? equipamento = new Equipamento(
            vm.Nome, 
            vm.PrecoAquisicao, 
            vm.DataFabricacao, 
            fabricante
            );
        
        var result = repositorioEquipamento.Editar(vm.Id, equipamento);

        if (!result)
            return RedirectToAction(nameof(Listar));

        
        return RedirectToAction(nameof(Listar));
    }

    [HttpGet]
    public ActionResult Excluir(string id)
    {
        Equipamento? equipamentoSelecionado = repositorioEquipamento.SelecionarPorId(id);

        ViewBag.fabricantes = CarregarFabricantes();

        if (equipamentoSelecionado == null)
            return RedirectToAction(nameof(Listar));       

        ExcluirEquipamentoViewModel excluirVm = new ExcluirEquipamentoViewModel(
            id,
            equipamentoSelecionado.Nome,
            equipamentoSelecionado.PrecoAquisicao,
            equipamentoSelecionado.DataFabricacao,
            equipamentoSelecionado.Fabricante.Nome
        );

        return View(excluirVm);
    }

    [HttpPost]
    [ActionName("Excluir")]
    public ActionResult ExcluirConfirmado(ExcluirEquipamentoViewModel excluirVm)
    {
        Equipamento? equipamentoSelecionado = repositorioEquipamento.SelecionarPorId(excluirVm.Id);

        if (equipamentoSelecionado == null)
            return RedirectToAction(nameof(Listar));
        
        repositorioEquipamento.Excluir(equipamentoSelecionado);

        return RedirectToAction(nameof(Listar));
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