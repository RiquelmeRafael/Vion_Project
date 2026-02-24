using System.Collections.Generic;
using System.Threading.Tasks;
using Vion_Desktop.Models;

namespace Vion_Desktop.Services
{
    public class PedidoService
    {
        private const string Endpoint = "api/pedidos";

        public async Task<List<PedidoDto>> GetAllAsync(string? status = null)
        {
            var url = Endpoint;
            if (!string.IsNullOrEmpty(status))
                url += $"?status={status}";
            
            return await ApiClient.GetAsync<List<PedidoDto>>(url);
        }

        public async Task<PedidoDto> GetByIdAsync(int id)
        {
            return await ApiClient.GetAsync<PedidoDto>($"{Endpoint}/{id}");
        }

        public async Task UpdateStatusAsync(int id, string novoStatus)
        {
            // O ApiClient serializa o dado em JSON.
            // Para enviar uma string crua como JSON, basta passá-la.
            // A API espera [FromBody] string, então o corpo deve ser "NovoStatus" (com aspas).
            await ApiClient.PutAsync($"{Endpoint}/{id}/status", novoStatus);
        }
    }
}
