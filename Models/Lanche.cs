using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace LanchesMac.Models
{
    [Table("Lanches")]
    public class Lanche
    {
        [Key]
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

        [Display(Name ="Imagem do lanche")]
        [StringLength(300,ErrorMessage = "Maximo de {1} caracteres")]
        public string ImagemUrl { get; set; }

        [Display(Name = "Imagem miniatura do lanche")]
        [StringLength(300, ErrorMessage = "Maximo de {1} caracteres")]
        public string ImagemThumbnailUrl { get; set; }

        [Display(Name = "É um lanche preferido?")]
        public bool IsLanchePreferido { get; set; }

        [Display(Name = "Tem em estoque?")]
        public bool EmEstoque { get; set; }


        public int CategoriaId { get; set; }


        public virtual Categoria Categoria { get; set; }
    }
}
