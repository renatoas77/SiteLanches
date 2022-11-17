using LanchesMac.Areas.Admin.Services;
using Microsoft.AspNetCore.Mvc;

namespace LanchesMac.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class AdminGraficoController : Controller
    {
        private readonly GraficoVendaService _graficoVendas;

        public AdminGraficoController(GraficoVendaService graficoVendas)
        {
            _graficoVendas = graficoVendas ?? throw new ArgumentNullException(nameof(graficoVendas));
        }

        public JsonResult VendasLanches(int dias)
        {
            return Json(_graficoVendas.GetVendasLanches(dias));
        }

        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public IActionResult VendasMensais()
        {
            return View();
        }

        [HttpGet]
        public IActionResult VendasSemanais()
        {
            return View();
        }
    }
}
