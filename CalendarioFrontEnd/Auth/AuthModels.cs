using System.ComponentModel.DataAnnotations;

namespace CalendarioFrontEnd.Auth
{
    public class AuthModels
    {

    }

    public class RegisterModel
    {
        [Required(ErrorMessage = "L'email è obbligatoria")]
        [EmailAddress(ErrorMessage = "Indirizzo email non valido")]
        public string Email { get; set; } = string.Empty;

        [Required(ErrorMessage = "La password è obbligatoria")]
        [StringLength(100, MinimumLength = 6, ErrorMessage = "La password deve essere di almeno {2} caratteri")]
        [DataType(DataType.Password)]
        public string Password { get; set; } = string.Empty;

        [Required(ErrorMessage = "Conferma la password")]
        [Compare("Password", ErrorMessage = "Le password non coincidono")]
        [DataType(DataType.Password)]
        public string ConfirmPassword { get; set; } = string.Empty;
    }
}
