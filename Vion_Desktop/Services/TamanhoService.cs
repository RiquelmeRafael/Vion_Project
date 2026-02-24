using System.Collections.Generic;
using System.Threading.Tasks;
using Vion_Desktop.Models;

namespace Vion_Desktop.Services
{
    public class TamanhoService
    {
        private const string Endpoint = "api/tamanhos";

        public async Task<List<TamanhoDto>> GetAllAsync()
        {
            return await ApiClient.GetAsync<List<TamanhoDto>>(Endpoint);
        }

        public async Task<TamanhoDto> GetByIdAsync(int id)
        {
            return await ApiClient.GetAsync<TamanhoDto>($"{Endpoint}/{id}");
        }

        public async Task CreateAsync(TamanhoCreateDto dto)
        {
            await ApiClient.PostAsync<TamanhoCreateDto, TamanhoDto>(Endpoint, dto);
        }

        public async Task UpdateAsync(int id, TamanhoUpdateDto dto)
        {
            await ApiClient.PutAsync($"{Endpoint}/{id}", dto);
        }

        public async Task DeleteAsync(int id)
        {
            await ApiClient.DeleteAsync($"{Endpoint}/{id}");
        }
    }
}
