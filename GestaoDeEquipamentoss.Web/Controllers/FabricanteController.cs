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
    public ActionResult Cadastrar(string nome, string email, string telefone)
    {
        Fabricante fabricante = new Fabricante(nome, email, telefone);

        repositorioFabricante.Cadastrar(fabricante);

        return RedirectToAction(nameof(Listar));
    }

    [HttpGet]
    public ActionResult Editar(string id)
    {
        Fabricante? fabricante = repositorioFabricante.SelecionarPorId(id);

        if (fabricante == null)
            return RedirectToAction(nameof(Listar));

        return View(fabricante);
    }

    [HttpPost]
    public ActionResult Editar(string id, string nome, string email, string telefone)
    {
        Fabricante? fabricante = repositorioFabricante.SelecionarPorId(id);

        if (fabricante == null)
            return RedirectToAction(nameof(Listar));

        fabricante.Nome = nome;
        fabricante.Email = email;
        fabricante.Telefone = telefone;

        repositorioFabricante.Editar(id, fabricante);

        return RedirectToAction(nameof(Listar));
    }

    [HttpGet]
    public ActionResult Excluir(string id)
    {
        Fabricante? fabricante = repositorioFabricante.SelecionarPorId(id);

        if (fabricante == null)
            return RedirectToAction(nameof(Listar));

        return View(fabricante);
    }

    [HttpPost]
    [ActionName("Excluir")]
    public ActionResult ExcluirConfirmacao(string id)
    {
        Fabricante? fabricante = repositorioFabricante.SelecionarPorId(id);

        if (fabricante == null)
            return RedirectToAction(nameof(Listar));

        repositorioFabricante.Excluir(fabricante);

        return RedirectToAction(nameof(Listar));
    }

}

