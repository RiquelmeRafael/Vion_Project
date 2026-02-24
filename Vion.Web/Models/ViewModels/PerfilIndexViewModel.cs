namespace Vion.Web.Models.ViewModels
{
    public class PerfilIndexViewModel
    {
        public PerfilViewModel Dados { get; set; } = new();
        public AlterarSenhaViewModel Senha { get; set; } = new();
    }
}
