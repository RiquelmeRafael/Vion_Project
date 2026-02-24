using System.ComponentModel.DataAnnotations;

namespace Vion.Web.Models.ViewModels
{
    public class CheckoutViewModel
    {
        [Required(ErrorMessage = "O Nome Completo é obrigatório")]
        public string? NomeCompleto { get; set; }

        [Required(ErrorMessage = "O Email é obrigatório")]
        [EmailAddress(ErrorMessage = "Email inválido")]
        public string? Email { get; set; }

        [Required(ErrorMessage = "O CPF é obrigatório")]
        [StringLength(14, MinimumLength = 11, ErrorMessage = "O CPF deve ter 11 dígitos")]
        public string? Cpf { get; set; }

        [Required(ErrorMessage = "O CEP é obrigatório")]
        [RegularExpression(@"^\d{5}-?\d{3}$", ErrorMessage = "CEP inválido")]
        public string? Cep { get; set; }

        [Required(ErrorMessage = "O Endereço é obrigatório")]
        public string? Endereco { get; set; }

        [Required(ErrorMessage = "A Cidade é obrigatória")]
        public string? Cidade { get; set; }

        [Required(ErrorMessage = "O Estado é obrigatório")]
        [StringLength(2, MinimumLength = 2, ErrorMessage = "Use a sigla do estado (ex: SP)")]
        public string? Estado { get; set; }

        [Required(ErrorMessage = "Selecione uma forma de pagamento")]
        public string? FormaPagamento { get; set; } // Pix, CartaoCredito

        // Campos auxiliares para exibição
        public CarrinhoViewModel Carrinho { get; set; } = new();
    }
}
