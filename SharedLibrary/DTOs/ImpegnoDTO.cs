using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SharedLibrary.DTOs
{


    public class AggiungiImpegnoDTO : Heron.MudCalendar.CalendarItem
    {
        public string Id { get; set; }

        [Required(ErrorMessage = "Il nome è obbligatorio.")]
        [MaxLength(100, ErrorMessage = "Il nome non può superare i 100 caratteri.")]
        public string Name { get; set; }


        [Required(ErrorMessage = "La descrizione è obbligatoria.")]
        [MaxLength(1000, ErrorMessage = "La descrizione non può superare i 1000 caratteri.")]
        public string Description { get; set; }


        public DateTime CreationDate { get; set; } = DateTime.UtcNow;

        public string UserId { get; set; } // Deve essere string

        public ApplicationUserDTO CreationUser { get; set; }
        public Guid GruppoId { get; set; }
    }
}
