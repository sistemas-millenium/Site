using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infra.Entities
{
    public class ImagemProjeto
    {
        public ImagemProjeto()
        {
            ImagemProjetoId = Guid.NewGuid();
        }
        public Guid ImagemProjetoId { get; set; }

        public int Ordenacao { get; set; }

        public string? Descricao { get; set; }

        public string Caminho { get; set; }

        public Guid ProjetoId { get; set; }
        public virtual Projeto Projeto { get; set; }
    }
}
