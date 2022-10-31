using LanchesMac.Areas.Admin.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace LanchesMac.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize("Admin")]
    public class AdminRelatorioVendasController : Controller
    {
        private readonly RelatorioVendasService _relatorioVendasService;

        public AdminRelatorioVendasController(RelatorioVendasService relatorioVendasService)
        {
            _relatorioVendasService = relatorioVendasService;
        }

        public IActionResult Index()
        {
            return View();
        }

        public async Task<IActionResult> RelatorioVendasSimples(DateTime? initialDate, DateTime? finalDate)
        {

            if (!initialDate.HasValue)
            {
                initialDate = new DateTime(DateTime.Now.Year, 1, 1);
            }
            if (!finalDate.HasValue)
            {
                finalDate = DateTime.Now;
            }

            ViewData["initialDate"] = initialDate.Value.ToString("yyyy-MM-dd");
            ViewData["finalDate"] = finalDate.Value.ToString("yyyy-MM-dd");

            var result = await _relatorioVendasService.FindByDateAsync(initialDate, finalDate);
            return View(result);
        }
    }
}
