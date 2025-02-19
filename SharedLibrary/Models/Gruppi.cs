using SharedLibrary.Models.IdentityOverrides;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
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

        public ICollection<GruppoUser> JoinedUsers { get; set; } = new List<GruppoUser>();
    }

}
