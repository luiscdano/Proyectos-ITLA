using Microsoft.AspNetCore.Mvc;
using PredictorActivos.Business.Models;
using PredictorActivos.Web.ViewModels;

namespace PredictorActivos.Web.Controllers;

public class ModesController : Controller
{
    [HttpGet]
    public IActionResult Index()
    {
        return View(new ModeViewModel
        {
            SelectedMode = PredictionModeStore.Instance.CurrentMode
        });
    }

    [HttpPost]
    public IActionResult Index(ModeViewModel vm)
    {
        if (!ModelState.IsValid)
            return View(vm);

        PredictionModeStore.Instance.SetMode(vm.SelectedMode);
        TempData["msg"] = "Modo guardado correctamente.";

        return RedirectToAction("Index");
    }
}