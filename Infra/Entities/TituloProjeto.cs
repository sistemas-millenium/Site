using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infra.Entities
{
    public class TituloProjeto
    {
        public TituloProjeto()
        {
            TituloProjetoId = Guid.NewGuid();
        }

        public Guid TituloProjetoId { get; set; }
        public string Nome { get; set; }
    }
}
