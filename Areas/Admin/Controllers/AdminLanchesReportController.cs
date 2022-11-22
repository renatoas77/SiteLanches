using FastReport.Data;
using FastReport.Web;
using LanchesMac.Areas.Admin.Services;
using LanchesMac.Areas.Admin.FastReportUtils;
using Microsoft.AspNetCore.Mvc;
using FastReport.Export.PdfSimple;

namespace LanchesMac.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class AdminLanchesReportController : Controller
    {
        private readonly IWebHostEnvironment _webHostEnvironment;
        private readonly RelatorioLanchesService _relatorioLanchesService;

        public AdminLanchesReportController(IWebHostEnvironment webHostEnvironment, RelatorioLanchesService relatorioLanchesService)
        {
            _webHostEnvironment = webHostEnvironment;
            _relatorioLanchesService = relatorioLanchesService;
        }

        public async Task<IActionResult> LanchesCategoriaReport()
        {
            var webReport = new WebReport();
            var mssqlDataConnection = new MsSqlDataConnection();

            webReport.Report.Dictionary.AddChild(mssqlDataConnection);

            webReport.Report.Load(Path.Combine(_webHostEnvironment.ContentRootPath, "wwwroot/reports", "CategoriaLanches.frx"));

            var lanches = HelperFastReport.GetTable(await _relatorioLanchesService.GetLanchesReport(), "LanchesReport");
            var categorias = HelperFastReport.GetTable(await _relatorioLanchesService.GetLanchesReport(), "CategoriasReport");

            webReport.Report.RegisterData(lanches, "LancheReport");
            webReport.Report.RegisterData(categorias, "CategoriasReport");
            return View(webReport);
        }

        [Route("LanchesCategoriaPDF")]
        public async Task<IActionResult> LanchesCategoriaPDF()
        {
            var webReport = new WebReport();
            var mssqlDataConnection = new MsSqlDataConnection();

            webReport.Report.Dictionary.AddChild(mssqlDataConnection);

            webReport.Report.Load(Path.Combine(_webHostEnvironment.ContentRootPath, "wwwroot/reports", "CategoriaLanches.frx"));

            var lanches = HelperFastReport.GetTable(await _relatorioLanchesService.GetLanchesReport(), "LanchesReport");
            var categorias = HelperFastReport.GetTable(await _relatorioLanchesService.GetLanchesReport(), "CategoriasReport");

            webReport.Report.RegisterData(lanches, "LancheReport");
            webReport.Report.RegisterData(categorias, "CategoriasReport");

            webReport.Report.Prepare();

            Stream stream = new MemoryStream();

            webReport.Report.Export(new PDFSimpleExport(), stream);
            stream.Position = 0;

            return new FileStreamResult(stream, "application/pdf");

        }
    }
}
