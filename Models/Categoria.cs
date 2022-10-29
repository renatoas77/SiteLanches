using LanchesMac.Context;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace LanchesMac.Models
{
    public class Categoria
    {
        public int CategoriaId { get; set; }

        [Required(ErrorMessage = "Este campo não pode ficar em branco")]
        [Display(Name = "Informe o nome da categoria")]
        [StringLength(50, ErrorMessage = "Maximo de {1} caracteres")]
        public string CategoriaNome { get; set; }

        [Required(ErrorMessage = "Este campo não pode ficar em branco")]
        [Display(Name = "Informe a descrição da categoria")]
        [MinLength(20, ErrorMessage = "Minimo de {1} caracteres")]
        [MaxLength(200, ErrorMessage = "Maximo de {1} caracteres")]
        public string Descricao { get; set; }

        public List<Lanche> Lanches { get; set; }
    }
}
