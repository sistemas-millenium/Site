using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infra.Entities
{
    public class SobreNos
    {
        public SobreNos()
        {
            SobreNosId = Guid.NewGuid();
        }

        public string Caminho { get; set; }

        public Guid SobreNosId { get; set; }

        public string TituloChamada { get; set; } = "";

        public string TituloPrincipal { get; set; } = "";

        public string TextoPrincipal { get; set; } = "";

        public string TituloSecundario { get; set; } = "";

        public string TextoSecundario { get; set; } = "";
    }
}
