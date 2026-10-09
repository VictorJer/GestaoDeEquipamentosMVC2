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
    private readonly IRepositorioChamado repositorioChamado;
    private readonly IRepositorio<Equipamento> repositorioEquipamento;

    public ChamadoController()
    {
        ContextoJson contexto = new ContextoJson();
        contexto.Carregar();

        repositorioChamado = new RepositorioChamadoEmArquivo(contexto);
        repositorioEquipamento = new RepositorioEquipamentoEmArquivo(contexto);
    }

    [HttpGet]
    public ActionResult Listar(string? status)
    {
        string? statusSelecionados = status?.ToLower();

        List<Chamado> chamados;

        if (statusSelecionados == "em-aberto")
            chamados = repositorioChamado.SelecionarTodosEmAberto();

        else if (statusSelecionados == "concluidos")
            chamados = repositorioChamado.SelecionarTodosConcluido();
        
        else
            chamados = repositorioChamado.SelecionarTodos();

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

        ViewBag.StatusSelecionado = statusSelecionados;

        return View(listarVms);
    }

    [HttpGet]
    public ActionResult Cadastrar()
    {
        ViewBag.Equipamentos = CarregarEquipamentos();

        CadastrarChamadoViewModel cadastrarChamadoViewModel = new CadastrarChamadoViewModel(string.Empty, null, string.Empty);

        return View(cadastrarChamadoViewModel);
    }

    [HttpPost]
    public ActionResult Cadastrar(CadastrarChamadoViewModel cadastrarVm)
    {
        if (!ModelState.IsValid)
        {
            ViewBag.Equipamentos = CarregarEquipamentos();
            return View(cadastrarVm);
        }

        Equipamento? equipamento = repositorioEquipamento.SelecionarPorId(cadastrarVm.EquipamentoId);

        if (equipamento == null)
        {
            ModelState.AddModelError(nameof(cadastrarVm.EquipamentoId), "Selecione um equipamento válido.");
            ViewBag.Equipamentos = CarregarEquipamentos();
            return View(cadastrarVm);
        }

        Chamado novoChamado = new Chamado(
            cadastrarVm.Titulo,
            equipamento,
            cadastrarVm.Descricao
        );

        repositorioChamado.Cadastrar(novoChamado);

        return RedirectToAction(nameof(Listar));
    }

    [HttpGet]
    public ActionResult Editar(string id)
    {
        Chamado? chamado = repositorioChamado.SelecionarPorId(id);

        if (chamado == null)
            return RedirectToAction(nameof(Listar));

        ViewBag.Equipamentos = CarregarEquipamentos();

        EditarChamadoViewModel editarVm = new EditarChamadoViewModel(
            chamado.Id,
            chamado.Titulo,
            chamado.Descricao,
            chamado.Equipamento.Id,
            chamado.EstaConcluido
        );

        return View(editarVm);
    }

    [HttpPost]
    public ActionResult Editar(EditarChamadoViewModel editarVm)
    {
        if (!ModelState.IsValid)
        {
            ViewBag.Equipamentos = CarregarEquipamentos();
            return View(editarVm);
        }

        Equipamento? equipamento = repositorioEquipamento.SelecionarPorId(editarVm.EquipamentoId);

        if (equipamento == null)
        {
            ModelState.AddModelError(nameof(editarVm.EquipamentoId), "Selecione um equipamento válido.");
            ViewBag.Equipamentos = CarregarEquipamentos();
            return View(editarVm);
        }

        Chamado chamadoAtualizado = new Chamado(
            editarVm.Titulo,
            equipamento,
            editarVm.EstaConcluido,
            editarVm.Descricao
        );

        repositorioChamado.Editar(editarVm.Id, chamadoAtualizado);

        return RedirectToAction(nameof(Listar));
    }

    [HttpGet]
    public ActionResult Excluir(string id)
    {
        Chamado? chamado = repositorioChamado.SelecionarPorId(id);

        if (chamado == null)
            return RedirectToAction(nameof(Listar));

        ExcluirChamadoViewModel excluirVm = new ExcluirChamadoViewModel(
            chamado.Id,
            chamado.Titulo,
            chamado.Descricao,
            chamado.Equipamento.Nome,
            chamado.DataAbertura,
            chamado.TempoDecorrido,
            chamado.EstaConcluido
        );

        return View(excluirVm);
    }

    [HttpPost]
    [ActionName("Excluir")]
    public ActionResult ExcluirConfirmado(ExcluirChamadoViewModel excluirVm)
    {
        Chamado? chamado = repositorioChamado.SelecionarPorId(excluirVm.Id);

        if (chamado == null)
            return RedirectToAction(nameof(Listar));

        repositorioChamado.Excluir(chamado);

        return RedirectToAction(nameof(Listar));
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
