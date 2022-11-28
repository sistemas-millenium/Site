using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infra.Entities
{
    public class Dado
    {
        public Dado()
        {
            DadoId = Guid.NewGuid();
            Ordenacao = 0;
        }

        public Guid DadoId { get; set; }
        public string Caminho { get; set; }

        public string Titulo { get; set; }

        public string Subtitulo { get; set; }

        public int Ordenacao { get; set; }
    }
}
