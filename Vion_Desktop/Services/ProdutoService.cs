using System.Collections.Generic;
using System.IO;
using System.Net.Http;
using System.Threading.Tasks;
using Vion_Desktop.Models;

namespace Vion_Desktop.Services
{
    public class ProdutoService
    {
        private const string Endpoint = "api/produtos";
        private const string UploadEndpoint = "api/upload/produto";

        public async Task<List<ProdutoDto>> GetAllAsync(string busca = "", int? categoriaId = null)
        {
            var query = Endpoint;
            var parameters = new List<string>();

            if (!string.IsNullOrWhiteSpace(busca))
                parameters.Add($"busca={Uri.EscapeDataString(busca)}");
            
            if (categoriaId.HasValue && categoriaId.Value > 0)
                parameters.Add($"categoriaId={categoriaId.Value}");

            if (parameters.Count > 0)
                query += "?" + string.Join("&", parameters);

            return await ApiClient.GetAsync<List<ProdutoDto>>(query);
        }

        public async Task<ProdutoDto> GetByIdAsync(int id)
        {
            return await ApiClient.GetAsync<ProdutoDto>($"{Endpoint}/{id}");
        }

        public async Task CreateAsync(ProdutoCreateDto dto)
        {
            await ApiClient.PostAsync<ProdutoCreateDto, object>(Endpoint, dto);
        }

        public async Task UpdateAsync(int id, ProdutoUpdateDto dto)
        {
            await ApiClient.PutAsync($"{Endpoint}/{id}", dto);
        }

        public async Task DeleteAsync(int id)
        {
            await ApiClient.DeleteAsync($"{Endpoint}/{id}");
        }

        public async Task<string> UploadImageAsync(string filePath)
        {
            return await ApiClient.UploadFileAsync(UploadEndpoint, filePath);
        }
    }
}
