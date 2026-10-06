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

    public ActionResult Listar()
    {
        List<Fabricante> fabricantes = repositorioFabricante.SelecionarTodos();

        return View(fabricantes);
    }
}