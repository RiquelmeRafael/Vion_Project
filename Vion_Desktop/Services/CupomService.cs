using System.Collections.Generic;
using System.Threading.Tasks;
using Vion_Desktop.Models;

namespace Vion_Desktop.Services
{
    public class CupomService
    {
        private const string Endpoint = "api/cupons";

        public async Task<List<CupomDto>> GetAllAsync()
        {
            return await ApiClient.GetAsync<List<CupomDto>>(Endpoint);
        }

        public async Task<CupomDto> GetByIdAsync(int id)
        {
            return await ApiClient.GetAsync<CupomDto>($"{Endpoint}/{id}");
        }

        public async Task CreateAsync(CupomCreateDto dto)
        {
            await ApiClient.PostAsync<CupomCreateDto, CupomDto>(Endpoint, dto);
        }

        public async Task UpdateAsync(int id, CupomCreateDto dto) // Reusing CreateDto as UpdateDto is similar
        {
            var updateDto = new CupomDto 
            { 
                Id = id, 
                Codigo = dto.Codigo, 
                PercentualDesconto = dto.PercentualDesconto, 
                Ativo = dto.Ativo 
            };
            await ApiClient.PutAsync($"{Endpoint}/{id}", updateDto);
        }

        public async Task DeleteAsync(int id)
        {
            await ApiClient.DeleteAsync($"{Endpoint}/{id}");
        }
    }
}
