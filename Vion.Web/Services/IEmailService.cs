using System.Threading.Tasks;
using Vion.Domain.Entities;

namespace Vion.Web.Services
{
    public interface IEmailService
    {
        Task SendOrderConfirmationEmailAsync(Pedido pedido);
        Task SendOrderStatusUpdateEmailAsync(Pedido pedido, string novoStatus);
        Task SendPasswordResetEmailAsync(string email, string resetLink);
    }
}
