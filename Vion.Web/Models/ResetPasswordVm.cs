using System.ComponentModel.DataAnnotations;

namespace Vion.Web.Models
{
    public class ResetPasswordVm
    {
        [Required]
        [EmailAddress]
        public string Email { get; set; } = "";

        [Required]
        [DataType(DataType.Password)]
        [MinLength(6)]
        public string NovaSenha { get; set; } = "";

        [Required]
        [DataType(DataType.Password)]
        [Compare("NovaSenha", ErrorMessage = "As senhas não conferem.")]
        public string ConfirmarSenha { get; set; } = "";

        [Required]
        public string Token { get; set; } = "";
    }
}

