using Infra.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infra.Context
{
    public class Context : DbContext
    {
        public Context(DbContextOptions<Context> options) : base(options)
        {           
        }

        public DbSet<Carrossel> Carrossel { get; set; }
        public DbSet<Dado> Dado { get; set; }
        public DbSet<Menu> Menu { get; set; }
        public DbSet<MiniCarrossel> MiniCarrossel { get; set; }
        public DbSet<Pergunta> Pergunta { get; set; }
        public DbSet<Projeto> Projeto { get; set; }
        public DbSet<SobreNos> SobreNos { get; set; }
        public DbSet<TituloProjeto> TituloProjeto { get; set; }
        public DbSet<Usuario> Usuario { get; set; }
        public DbSet<ImagemProjeto> ImagemProjeto { get; set; }
    }
}
