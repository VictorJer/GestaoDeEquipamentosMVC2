using Microsoft.AspNetCore.Mvc;

namespace GestaoDeEquipamentoss.Web.Controllers;

public class HomeController : Controller
{
    public ActionResult Index()
    {
        return View();
    }
}