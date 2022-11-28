using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infra.Entities
{
    public class MiniCarrossel
    {
        public MiniCarrossel()
        {
            MiniCarrosselId = Guid.NewGuid();
            Ordenacao = 0;
        }
        public Guid MiniCarrosselId { get; set; }

        public string? Descricao { get; set; }

        public string Caminho { get; set; }

        public int Ordenacao { get; set; }
    }
}
