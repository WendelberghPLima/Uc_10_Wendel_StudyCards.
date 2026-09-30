using System.ComponentModel.DataAnnotations;

namespace Uc_10_Wendel_StudyCards.Models
{
    public class Categoria
    {
        [Key]
        public int CategoriaId { get; set; }

        [Required]
        [StringLength(150)]
        public string? Nome { get; set; }

        [StringLength(500)]
        public string? Descricao { get; set; }

        public ICollection<Baralho>? Baralhos { get; set; }
    }
}