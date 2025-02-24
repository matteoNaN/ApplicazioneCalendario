using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SharedLibrary.DTOs
{
    public class CalendarioDTO
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public List<ImpegnoDTO> Impegni { get; set; }
    }

    public class ModificaCalendarioDTO
    {
        public string Name { get; set; }
        public string Description { get; set; }
    }
}
