using System.ComponentModel.DataAnnotations;

namespace Vion.Web.Models.ViewModels
{
    public class AlterarSenhaViewModel
    {
        [Required(ErrorMessage = "A senha atual é obrigatória")]
        [DataType(DataType.Password)]
        [Display(Name = "Senha Atual")]
        public string SenhaAtual { get; set; } = string.Empty;

        [Required(ErrorMessage = "A nova senha é obrigatória")]
        [DataType(DataType.Password)]
        [Display(Name = "Nova Senha")]
        [MinLength(4, ErrorMessage = "A senha deve ter pelo menos 4 caracteres")]
        public string NovaSenha { get; set; } = string.Empty;

        [DataType(DataType.Password)]
        [Display(Name = "Confirmar Nova Senha")]
        [Compare("NovaSenha", ErrorMessage = "As senhas não conferem")]
        public string ConfirmarNovaSenha { get; set; } = string.Empty;
    }
}
