using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infra.Entities
{
    public class Pergunta
    {
        public Pergunta()
        {
            PerguntaId = Guid.NewGuid();
            Ordenacao = 0;
        }
        public Guid PerguntaId { get; set; }

        public string Nome { get; set; } = "";

        public string NomeLink { get; set; } = "";

        public string Link { get; set; } = "";

        public string Resposta { get; set; } = "";

        public int Ordenacao { get; set; }
    }
}
