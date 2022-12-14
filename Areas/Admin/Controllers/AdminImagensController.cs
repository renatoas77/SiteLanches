using LanchesMac.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;

namespace LanchesMac.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "Admin")]
    public class AdminImagensController : Controller
    {
        private readonly ConfigurationImages _myConfig;
        private readonly IWebHostEnvironment _webHostEnvironment;

        public AdminImagensController(IOptions<ConfigurationImages> myConfig, IWebHostEnvironment webHostEnvironment)
        {
            _myConfig = myConfig.Value;
            _webHostEnvironment = webHostEnvironment;
        }

        public IActionResult Index()
        {
            return View();
        }

        public async Task<IActionResult> UploadFiles(List<IFormFile> files)
        {
            if (files == null || files.Count <= 0)
            {
                ViewData["Erro"] = "Erro: Arquivo(s) não selecionado(s)";
            }

            if (files.Count > 10)
            {
                ViewData["Erro"] = "Você excedeu o limite de arquivos permitidos";
                return View(ViewData);
            }

            List<IFormFile> FilteredList = new List<IFormFile>();

            foreach (var file in files)
            {
                if (file.FileName.Contains(".jpg") || file.FileName.Contains(".png") || file.FileName.Contains(".gif"))
                {
                    FilteredList.Add(file);
                }
            }

            files = FilteredList;

            long size = files.Sum(f => f.Length);

            var filePathsName = new List<string>();

            var filePath = Path.Combine(_webHostEnvironment.WebRootPath, _myConfig.PastaImagensProdutos);

            foreach (var file in files)
            {
                if(file.FileName.Contains(".jpg") || file.FileName.Contains(".png") || file.FileName.Contains(".gif"))
                {
                    var fileNameWithPath = string.Concat(filePath, "\\", file.FileName);

                    filePathsName.Add(fileNameWithPath);

                    using (var stream = new FileStream(fileNameWithPath, FileMode.Create))
                    {
                        await file.CopyToAsync(stream);
                    }
                }
            }

            ViewData["Resultado"] = files.Count + " arquivos foram enviados ao servidor, com o tamanho total de: " + size + " bytes";
            ViewData["Caminho"] = filePath;

            ViewBag.Arquivos = filePathsName;

            return View(ViewData);
        }

        public IActionResult GetImagens()
        {
            FileManagerModel model = new FileManagerModel();

            var userImagesPath =  Path.Combine(_webHostEnvironment.WebRootPath, _myConfig.PastaImagensProdutos);

            DirectoryInfo dir = new DirectoryInfo(userImagesPath);

            FileInfo[] files = dir.GetFiles();

            model.PathImagesProdutos = _myConfig.PastaImagensProdutos;

            if(files.Length == 0)
            {
                ViewData["Erro"] = "Nenhum arquivo encontrado na pasta " + userImagesPath;
            }

            model.Files = files;

            return View(model);
        }

        public IActionResult DeleteFile(string fname)
        {
            string _targetImage = Path.Combine(_webHostEnvironment.WebRootPath,_myConfig.PastaImagensProdutos + "\\",fname);

            if(System.IO.File.Exists(_targetImage))
            {
                System.IO.File.Delete(_targetImage);

                ViewData["Deletado"] = "Arquivo(s): " + _targetImage + " deletado com sucesso";
            }

            return View("Index");
        }
    }
}
