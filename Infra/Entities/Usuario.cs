using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infra.Entities
{
    public class Usuario
    {
        public Usuario()
        {
            UsuarioId = Guid.NewGuid();
        }

        public Guid UsuarioId { get; set; }

        public string Nome { get; set; }

        public string Senha { get; set; }

        public bool Ativo { get; set; }
    }
}
