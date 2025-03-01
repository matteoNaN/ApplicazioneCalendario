using SharedLibrary.Models.IdentityOverrides;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SharedLibrary.Models
{
    public class Calendario
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        [MaxLength(100)]
        public string Name { get; set; }
        [MaxLength(1000)]
        public string Description { get; set; }

        public Guid GruppoId { get; set; }
        public Gruppi Gruppo { get; set; }


        
    }
}
