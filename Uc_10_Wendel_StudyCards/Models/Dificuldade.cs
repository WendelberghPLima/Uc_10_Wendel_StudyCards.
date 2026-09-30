using System.ComponentModel.DataAnnotations;

namespace Uc_10_Wendel_StudyCards.Models
{
    public class Dificuldade
    {
        [Key]
        public int DificuldadeId { get; set; }

        [Required]
        [StringLength(50)]
        public string? Nome { get; set; }

        public ICollection<Card>? Cards { get; set; }
    }
}