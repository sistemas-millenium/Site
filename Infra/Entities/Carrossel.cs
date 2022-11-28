using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infra.Entities
{
    public class Carrossel
    {
        public Carrossel()
        {
            CarrosselId = Guid.NewGuid();
            Ordenacao = 0;
        }

        public Guid CarrosselId { get; set; }

        public string Link { get; set; } = "";

        public string? Descricao { get; set; }

        public string Caminho { get; set; } = "";

        public int Ordenacao { get; set; }
    }
}
