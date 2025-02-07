using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SharedLibrary.DTOs.Auth
{
    public class LoginModel
    {
        [Required, EmailAddress]
        public string Email { get; set; } = string.Empty;

        [Required]
        public string Password { get; set; } = string.Empty;

        public bool RememberMe { get; set; }
    }

    public class LoginResponse
    {
        public bool IsSuccessfull { get; set; } = false;
        public string? Token { get; set; } = null;

        // Il costruttore non è necessario per la deserializzazione, ma lo manteniamo per utilità.
        public LoginResponse() { }

        public LoginResponse(bool isSuccessfull, string? token = null)
        {
            IsSuccessfull = isSuccessfull;
            Token = token;
        }
    }

    public class RegisterResponse
    {
        private bool IdentityResult { get; set; }
        private List<string>? messages = null;

        public RegisterResponse(bool IdentityResult, List<string>? messages = null)
        {
            this.IdentityResult = IdentityResult;
            if(messages is not  null) this.messages = messages;
        }
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
