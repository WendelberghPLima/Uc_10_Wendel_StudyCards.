using System.ComponentModel.DataAnnotations;

namespace Uc_10_Wendel_StudyCards.Models
{
    public class Card
    {
        [Key]
        public int CardId { get; set; }

        [Required]
        [StringLength(255)]
        public string? Pergunta { get; set; }

        [Required]
        [StringLength(1000)]
        public string? Resposta { get; set; }

        [Display(Name = "Data de Criação")]
        public DateTime DataCriacao { get; set; } = DateTime.Now;

        [Required]
        [Display(Name = "Baralho")]
        public int BaralhoId { get; set; }

        public Baralho? Baralho { get; set; }

        [Required]
        [Display(Name = "Dificuldade")]
        public int DificuldadeId { get; set; }

        public Dificuldade? Dificuldade { get; set; }
    }
}