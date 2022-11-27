using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using LanchesMac.Context;
using LanchesMac.Models;
using Microsoft.AspNetCore.Authorization;
using ReflectionIT.Mvc.Paging;
using LanchesMac.ViewModels;

namespace LanchesMac.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize("Admin")]
    public class AdminLanchesController : Controller
    {
        private readonly AppDbContext _context;
        private readonly IWebHostEnvironment _webHostEnvironment;

        public AdminLanchesController(AppDbContext context, IWebHostEnvironment webHostEnvironment)
        {
            _context = context;
            _webHostEnvironment = webHostEnvironment;
        }

        private string ProcessaUploadedFile(IFormFile image)
        {
            string nomeArquivoImagem = null;
            if (image != null)
            {
                string uploadsFolder = Path.Combine(_webHostEnvironment.WebRootPath, "produtos");
                nomeArquivoImagem = Guid.NewGuid().ToString() + "_" + image.FileName;
                string filePath = Path.Combine(uploadsFolder, nomeArquivoImagem);
                using (var fileStream = new FileStream(filePath, FileMode.Create))
                {
                    image.CopyTo(fileStream);
                }
            }
            return nomeArquivoImagem;
        }

        public async Task<IActionResult> Index(string filter, int pageindex = 1, string sort = "Nome")
        {
            var resultado = _context.Lanches.Include(c => c.Categoria).AsNoTracking().AsQueryable();

            if (!string.IsNullOrWhiteSpace(filter))
            {
                resultado = resultado.Where(l => l.Nome.Contains(filter));
            }

            var model = await PagingList.CreateAsync(resultado, 5, pageindex, sort, "Nome");
            model.RouteValue = new RouteValueDictionary() { { "filter", filter } };

            return View(model);
        }

        // GET: Admin/AdminLanches/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Lanches == null)
            {
                return NotFound();
            }

            var lanche = await _context.Lanches
                .Include(l => l.Categoria)
                .FirstOrDefaultAsync(m => m.LancheId == id);
            if (lanche == null)
            {
                return NotFound();
            }

            return View(lanche);
        }

        // GET: Admin/AdminLanches/Create
        public IActionResult Create()
        {
            ViewData["CategoriaId"] = new SelectList(_context.Categorias, "CategoriaId", "CategoriaNome");
            return View();
        }

        // POST: Admin/AdminLanches/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("LancheId,Nome,DescricaoCurta,DescricaoDetalhada,Preco,Imagem,ImagemThumbnail,IsLanchePreferido,EmEstoque,CategoriaId")] LancheViewModel model)
        {
            if (ModelState.IsValid)
            {
                model.ImagemExistente = ProcessaUploadedFile(model.Imagem);
                model.ThumbnailExistente = ProcessaUploadedFile(model.ImagemThumbnail);

                Lanche lanche = new Lanche(model);
                
                _context.Add(lanche);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["CategoriaId"] = new SelectList(_context.Categorias, "CategoriaId", "CategoriaNome", model.CategoriaId);
            return View(model);
        }

        // GET: Admin/AdminLanches/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Lanches == null)
            {
                return NotFound();
            }

            var lanche = await _context.Lanches.FindAsync(id);

            var model = new LancheViewModel(lanche);

            if (lanche == null)
            {
                return NotFound();
            }
            ViewData["CategoriaId"] = new SelectList(_context.Categorias, "CategoriaId", "CategoriaNome", lanche.CategoriaId);
            return View(model);
        }

        // POST: Admin/AdminLanches/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, LancheViewModel model)
        {
            if (id != model.LancheId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    var lanche = await _context.Lanches.FindAsync(model.LancheId);

                    lanche.Nome = model.Nome;
                    lanche.DescricaoCurta = model.DescricaoCurta;
                    lanche.DescricaoDetalhada = model.DescricaoDetalhada;
                    lanche.Preco = model.Preco;
                    lanche.IsLanchePreferido = model.IsLanchePreferido;
                    lanche.EmEstoque = model.EmEstoque;
                    lanche.CategoriaId = model.CategoriaId;

                    if (model.Imagem != null)
                    {
                        if (lanche.ImagemUrl != null)
                        {
                            string filePath = Path.Combine(_webHostEnvironment.WebRootPath, "produtos", lanche.ImagemUrl);
                            System.IO.File.Delete(filePath);
                        }
                        lanche.ImagemUrl = ProcessaUploadedFile(model.Imagem);
                    }

                    if (model.ImagemThumbnail != null)
                    {
                        if (lanche.ImagemThumbnailUrl != null)
                        {
                            string filePath = Path.Combine(_webHostEnvironment.WebRootPath, "produtos", lanche.ImagemThumbnailUrl);
                            System.IO.File.Delete(filePath);
                        }
                        lanche.ImagemThumbnailUrl = ProcessaUploadedFile(model.ImagemThumbnail);
                    }


                    _context.Update(lanche);
                    await _context.SaveChangesAsync();
                    model = new LancheViewModel(lanche);
                }
                catch (DbUpdateConcurrencyException)
                {
                    var lanche = await _context.Lanches.FindAsync(model.LancheId);
                    if (!LancheExists(lanche.LancheId))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            ViewData["CategoriaId"] = new SelectList(_context.Categorias, "CategoriaId", "CategoriaNome", model.CategoriaId);
            return View(model);
        }

        // GET: Admin/AdminLanches/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Lanches == null)
            {
                return NotFound();
            }

            var lanche = await _context.Lanches
                .Include(l => l.Categoria)
                .FirstOrDefaultAsync(m => m.LancheId == id);
            if (lanche == null)
            {
                return NotFound();
            }

            return View(lanche);
        }

        // POST: Admin/AdminLanches/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Lanches == null)
            {
                return Problem("Entity set 'AppDbContext.Lanches'  is null.");
            }
            var lanche = await _context.Lanches.FindAsync(id);
            if (lanche != null)
            {
                _context.Lanches.Remove(lanche);
            }

            if(await _context.SaveChangesAsync() > 0)
            {
                string filePath = Path.Combine(_webHostEnvironment.WebRootPath, "produtos", lanche.ImagemUrl);
                if(System.IO.File.Exists(filePath))
                {
                    System.IO.File.Delete(filePath);
                }

                filePath = Path.Combine(_webHostEnvironment.WebRootPath, "produtos", lanche.ImagemThumbnailUrl);
                if (System.IO.File.Exists(filePath))
                {
                    System.IO.File.Delete(filePath);
                }
            };

            return RedirectToAction(nameof(Index));
        }

        private bool LancheExists(int id)
        {
            return _context.Lanches.Any(e => e.LancheId == id);
        }
    }
}
