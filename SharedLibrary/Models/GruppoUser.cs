using SharedLibrary.Models.IdentityOverrides;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SharedLibrary.Models
{
    public class GruppoUser
    {
        public int Id { get; set; }

        public string UserId { get; set; } // Deve essere string
        public Guid GruppoId { get; set; }

        public DateTime CreationDate { get; set; } = DateTime.UtcNow;

        public ApplicationUser User { get; set; }
        public Gruppi Gruppo { get; set; }
    }

}

