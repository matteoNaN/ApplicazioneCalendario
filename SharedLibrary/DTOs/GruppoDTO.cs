using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SharedLibrary.DTOs
{
    public class GruppoDTO
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public ApplicationUserDTO CreatorUser { get; set; }
        public List<ApplicationUserDTO> JoinedUser { get; set; }

    }
}
