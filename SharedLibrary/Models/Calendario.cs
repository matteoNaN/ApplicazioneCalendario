using SharedLibrary.Models.IdentityOverrides;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SharedLibrary.Models
{
    public class Calendario
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public string Name { get; set; }
        public string Description { get; set; }

        public Guid GruppoId { get; set; }
        public Gruppi Gruppo { get; set; }


        public ICollection<Impegno> Impegni { get; set; } = new List<Impegno>();
    }
}
