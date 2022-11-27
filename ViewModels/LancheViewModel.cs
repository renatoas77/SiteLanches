using LanchesMac.Models;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace LanchesMac.ViewModels
{
    public class LancheViewModel
    {
        public int LancheId { get; set; }

        [Required(ErrorMessage = "Este campo não pode ficar em branco")]
        [Display(Name = "Informe o nome do lanche")]
        [MaxLength(100, ErrorMessage = "Maximo de {1} caracteres")]
        public string Nome { get; set; }

        [Required(ErrorMessage = "Este campo não pode ficar em branco")]
        [Display(Name = "Breve descrição do lanche")]
        [MaxLength(300, ErrorMessage = "Maximo de {1} caracteres")]
        public string DescricaoCurta { get; set; }

        [Required(ErrorMessage = "Este campo não pode ficar em branco")]
        [Display(Name = "Descrição completa do lanche")]
        [MaxLength(700, ErrorMessage = "Maximo de {1} caracteres")]
        public string DescricaoDetalhada { get; set; }

        [Required(ErrorMessage = "Este campo não pode ficar em branco")]
        [Display(Name = "Preço")]
        [Column(TypeName = "decimal(10,2)")]
        [Range(1, 999.99, ErrorMessage = "O preço deve estar entre R$1 e R$999,99 ")]
        public decimal Preco { get; set; }

        [Required]
        [Display(Name = "Foto")]
        public IFormFile Imagem { get; set; }

        [Required]
        [Display(Name = "Thumbnail")]
        public IFormFile ImagemThumbnail { get; set; }

       

        [Display(Name = "É um lanche preferido?")]
        public bool IsLanchePreferido { get; set; }

        [Display(Name = "Tem em estoque?")]
        public bool EmEstoque { get; set; }
        public int CategoriaId { get; set; }

        public string ImagemExistente { get; set; }

        public string ThumbnailExistente { get; set; }
        public virtual Categoria Categoria { get; set; }

        public LancheViewModel()
        {

        }

        public LancheViewModel(Lanche lanche)
        {
            LancheId = lanche.LancheId;
            Nome = lanche.Nome;
            DescricaoCurta = lanche.DescricaoCurta;
            DescricaoDetalhada = lanche.DescricaoDetalhada;
            Preco = lanche.Preco;
            ImagemExistente = lanche.ImagemUrl;
            ThumbnailExistente = lanche.ImagemThumbnailUrl;
            IsLanchePreferido = lanche.IsLanchePreferido;
            EmEstoque = lanche.EmEstoque;
            CategoriaId = lanche.CategoriaId;
            Categoria = lanche.Categoria;
        }
    }
}