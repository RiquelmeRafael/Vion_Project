using System.ComponentModel.DataAnnotations;

namespace Vion.Web.Models.ViewModels
{
    public class PerfilViewModel
    {
        [Required(ErrorMessage = "O nome é obrigatório")]
        [Display(Name = "Nome Completo")]
        public string Nome { get; set; } = string.Empty;

        [Required(ErrorMessage = "O e-mail é obrigatório")]
        [EmailAddress(ErrorMessage = "E-mail inválido")]
        [Display(Name = "E-mail")]
        public string Email { get; set; } = string.Empty;

        [Display(Name = "Telefone")]
        [Required(ErrorMessage = "O telefone é obrigatório")]
        [StringLength(15, ErrorMessage = "O telefone deve ter no máximo 15 caracteres")]
        [RegularExpression(@"^(\(?\d{2}\)?\s?)?\d{4,5}-?\d{4}$", ErrorMessage = "Formato inválido. Use (XX) XXXXX-XXXX ou apenas números.")]
        public string? Telefone { get; set; }
    }
}
