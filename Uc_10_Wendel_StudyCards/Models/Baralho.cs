using System.ComponentModel.DataAnnotations;

namespace Uc_10_Wendel_StudyCards.Models
{
    public class Baralho
    {
        [Key]
        public int BaralhoId { get; set; }

        [Required]
        [StringLength(150)]
        public string? Nome { get; set; }

        [StringLength(500)]
        public string? Descricao { get; set; }

        [Display(Name = "Imagem")]
        public string? ImagemUrl { get; set; }

        [Required]
        [Display(Name = "Matéria")]
        public int MateriaId { get; set; }

        public Materia? Materia { get; set; }

        [Required]
        [Display(Name = "Categoria")]
        public int CategoriaId { get; set; }

        public Categoria? Categoria { get; set; }

        public ICollection<Card>? Cards { get; set; }
    }
}