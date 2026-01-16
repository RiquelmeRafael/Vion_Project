using System.ComponentModel.DataAnnotations;

namespace Vion.Web.Models
{
    public class RegisterVm
    {
        [Required(ErrorMessage = "O Nome é obrigatório")]
        public string Nome { get; set; } = "";

        [Required(ErrorMessage = "O Email é obrigatório")]
        [EmailAddress(ErrorMessage = "Email inválido")]
        public string Email { get; set; } = "";

        [Required(ErrorMessage = "A Senha é obrigatória")]
        [DataType(DataType.Password)]
        [MinLength(6, ErrorMessage = "A senha deve ter no mínimo 6 caracteres")]
        public string Senha { get; set; } = "";

        [DataType(DataType.Password)]
        [Compare("Senha", ErrorMessage = "As senhas não conferem")]
        public string ConfirmarSenha { get; set; } = "";
    }
}
