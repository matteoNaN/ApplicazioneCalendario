using SharedLibrary.Models.IdentityOverrides;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SharedLibrary.Models
{
    public class Gruppi
    {
        public Guid Id { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public int CreatorUserId { get; set; }

        public ApplicationUser? CreatorUser { get; set; }

        public ICollection<ApplicationUser> JoinedUsers { get; set; }

    }
}
