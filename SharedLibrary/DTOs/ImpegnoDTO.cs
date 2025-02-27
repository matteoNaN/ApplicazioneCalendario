using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SharedLibrary.DTOs
{
    public class ImpegnoDTO : Heron.MudCalendar.CalendarItem
    {

        public Guid Id { get; set; }

        [MaxLength(100)]
        public string Name { get; set; }

        public string Description { get; set; }

        public DateTime Start { get; set; }

        public DateTime? End { get; set; }

        public DateTime CreationDate { get; set; } = DateTime.UtcNow;

        public ApplicationUserDTO CreationUser { get; set; }

    }

    public class AggiungiImpegnoDTO : Heron.MudCalendar.CalendarItem
    {

        [Required(ErrorMessage = "Il nome è obbligatorio.")]
        [MaxLength(100, ErrorMessage = "Il nome non può superare i 100 caratteri.")]
        public string Name { get; set; }


        [Required(ErrorMessage = "La descrizione è obbligatoria.")]
        [MaxLength(1000, ErrorMessage = "La descrizione non può superare i 1000 caratteri.")]
        public string Description { get; set; }


        [Required(ErrorMessage = "La Data di Inizio è obbligatoria.")]
        public DateTime? Start { get; set; } = null;
        public DateTime? End { get; set; } = null;

        public DateTime CreationDate { get; set; } = DateTime.UtcNow;

        public string UserId { get; set; } // Deve essere string
        public Guid GruppoId { get; set; }
    }
}
