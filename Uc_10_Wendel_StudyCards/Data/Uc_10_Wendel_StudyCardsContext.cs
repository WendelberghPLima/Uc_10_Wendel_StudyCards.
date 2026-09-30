using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Uc_10_Wendel_StudyCards.Models;

namespace Uc_10_Wendel_StudyCards.Data
{
    public class Uc_10_Wendel_StudyCardsContext : DbContext
    {
        public Uc_10_Wendel_StudyCardsContext (DbContextOptions<Uc_10_Wendel_StudyCardsContext> options)
            : base(options)
        {
        }

        public DbSet<Uc_10_Wendel_StudyCards.Models.Materia> Materia { get; set; } = default!;
        public DbSet<Uc_10_Wendel_StudyCards.Models.Categoria> Categoria { get; set; } = default!;
        public DbSet<Uc_10_Wendel_StudyCards.Models.Dificuldade> Dificuldade { get; set; } = default!;
        public DbSet<Uc_10_Wendel_StudyCards.Models.Baralho> Baralho { get; set; } = default!;
        public DbSet<Uc_10_Wendel_StudyCards.Models.Card> Card { get; set; } = default!;
    }
}
