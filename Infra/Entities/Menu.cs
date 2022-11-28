using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infra.Entities
{
    public class Menu
    {
        public Menu()
        {
            MenuId = Guid.NewGuid();
            Ordenacao = 0;
        }

        public Guid MenuId { get; set; }

        public string Nome { get; set; }

        public string Url { get; set; }

        public string? Descriacao { get; set; }

        public int Ordenacao { get; set; }
    }
}
