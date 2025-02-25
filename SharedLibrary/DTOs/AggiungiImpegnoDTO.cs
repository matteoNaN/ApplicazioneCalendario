using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SharedLibrary.DTOs
{
    public class AggiungiImpegnoDTO
    {

        [MaxLength(100)]
        public string Name { get; set; }

        [MaxLength(1000)]
        public string Description { get; set; }

        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }

        public DateTime CreationDate { get; set; } = DateTime.UtcNow;

        public string UserId { get; set; } // Deve essere string
        public Guid GruppoId { get; set; }
    }
}
