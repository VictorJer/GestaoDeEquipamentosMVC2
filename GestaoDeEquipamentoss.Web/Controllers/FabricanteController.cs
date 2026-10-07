using GestaoDeEquipamentoss.Web.Models;
using GestaoDeEquipamentoss.Web.ModuloFabricante;
using GestaoDeEquipamentosWeb.ConsoleApp.Compartilhado;
using GestaoDeEquipamentosWeb.ConsoleApp.Compartilhado.Arquivos;
using GestaoDeEquipamentosWeb.ConsoleApp.ModuloFabricante;
using Microsoft.AspNetCore.Mvc;

namespace GestaoDeEquipamentoss.Web.Controllers;

public class FabricanteController : Controller
{
    private readonly IRepositorio<Fabricante> repositorioFabricante;

    public FabricanteController()
    {
        ContextoJson contexto = new ContextoJson();
        contexto.Carregar();


        repositorioFabricante = new RepositorioFabricanteEmArquivo(contexto);
    }

    [HttpGet]
    public ActionResult Listar()
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

        return View(listarVms);

    }

    [HttpGet]
    public ActionResult Cadastrar()
    {
        return View();
    }

    [HttpPost]
    public ActionResult Cadastrar(CadastrarFabricanteViewModel cadastrarVm)
    {
        Fabricante fabricante = new Fabricante(cadastrarVm.Nome, cadastrarVm.Email, cadastrarVm.Telefone);

        repositorioFabricante.Cadastrar(fabricante);

        return RedirectToAction(nameof(Listar));
    }

    [HttpGet]
    public ActionResult Editar(string id)
    {
        Fabricante? fabricante = repositorioFabricante.SelecionarPorId(id);

        if (fabricante == null)
            return RedirectToAction(nameof(Listar));

        EditarFabricanteViewModel vm = new EditarFabricanteViewModel(
            fabricante.Id,
            fabricante.Nome,
            fabricante.Email,
            fabricante.Telefone
        );


        return View(vm);
    }

    [HttpPost]
    public ActionResult Editar(EditarFabricanteViewModel vm)
    {
        Fabricante? fabricante = new Fabricante(
            vm.Nome, 
            vm.Email, 
            vm.Telefone
            );

        if (fabricante == null)
            return RedirectToAction(nameof(Listar));

        repositorioFabricante.Editar(vm.Id, fabricante);

        return RedirectToAction(nameof(Listar));
    }

    [HttpGet]
    public ActionResult Excluir(string id)
    {
        Fabricante? fabricante = repositorioFabricante.SelecionarPorId(id);

        if (fabricante == null)
            return RedirectToAction(nameof(Listar));

        ExcluirFabricanteViewModel vm = new ExcluirFabricanteViewModel(
            fabricante.Id,
            fabricante.Nome,
            fabricante.Email,
            fabricante.Telefone
        );

        return View(vm);
    }

    [HttpPost]
    public ActionResult Excluir(ExcluirFabricanteViewModel vm)
    {
        Fabricante? fabricante = repositorioFabricante.SelecionarPorId(vm.Id);

        if (fabricante == null)
            return RedirectToAction(nameof(Listar));

        repositorioFabricante.Excluir(fabricante);

        return RedirectToAction(nameof(Listar));
    }

}

