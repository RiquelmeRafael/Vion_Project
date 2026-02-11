using System.Collections.Generic;
using System.Threading.Tasks;
using Vion_Desktop.Models;

namespace Vion_Desktop.Services
{
    public class CategoriaService
    {
        private const string Endpoint = "api/categorias";

        public async Task<List<CategoriaDto>> GetAllAsync()
        {
            return await ApiClient.GetAsync<List<CategoriaDto>>(Endpoint);
        }

        public async Task<CategoriaDto> GetByIdAsync(int id)
        {
            return await ApiClient.GetAsync<CategoriaDto>($"{Endpoint}/{id}");
        }

        public async Task CreateAsync(CategoriaCreateDto dto)
        {
            await ApiClient.PostAsync<CategoriaCreateDto, CategoriaDto>(Endpoint, dto);
        }

        public async Task UpdateAsync(int id, CategoriaUpdateDto dto)
        {
            await ApiClient.PutAsync($"{Endpoint}/{id}", dto);
        }

        public async Task DeleteAsync(int id)
        {
            await ApiClient.DeleteAsync($"{Endpoint}/{id}");
        }
    }
}
