using SharedLibrary.Models.IdentityOverrides;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SharedLibrary.Models
{
    public class Gruppi
    {
        public Guid Id { get; set; } = Guid.NewGuid();

        [MaxLength(100)]
        public string Name { get; set; }

        [MaxLength(10000)]
        public string Description { get; set; }

        // La chiave primaria di ApplicationUser è string, non int
        public string CreatorUserId { get; set; }
        public ApplicationUser CreatorUser { get; set; }


        public Guid? CalendarioId { get; set; }
        public Calendario? Calendario { get; set; }


        public ICollection<Impegno> Impegni { get; set; } = new List<Impegno>();
        public ICollection<GruppoUser> JoinedUsers { get; set; } = new List<GruppoUser>();
    }

}
