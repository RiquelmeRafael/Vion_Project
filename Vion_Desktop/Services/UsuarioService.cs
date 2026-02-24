using Vion_Desktop.Models;

namespace Vion_Desktop.Services
{
    public class UsuarioService
    {
        private const string Endpoint = "api/usuarios";

        public async Task<List<UsuarioDto>> GetAllAsync()
        {
            return await ApiClient.GetAsync<List<UsuarioDto>>(Endpoint);
        }

        public async Task<UsuarioDto> GetByIdAsync(int id)
        {
            return await ApiClient.GetAsync<UsuarioDto>($"{Endpoint}/{id}");
        }

        public async Task CreateAsync(UsuarioCreateDto usuario)
        {
            await ApiClient.PostAsync<UsuarioCreateDto, UsuarioDto>(Endpoint, usuario);
        }

        public async Task UpdateAsync(int id, UsuarioUpdateDto usuario)
        {
            await ApiClient.PutAsync($"{Endpoint}/{id}", usuario);
        }

        public async Task DeleteAsync(int id)
        {
            await ApiClient.DeleteAsync($"{Endpoint}/{id}");
        }

        public async Task<List<TipoUsuarioDto>> GetTiposAsync()
        {
            return await ApiClient.GetAsync<List<TipoUsuarioDto>>($"{Endpoint}/tipos");
        }

        public async Task<List<TipoUsuarioDto>> GetTiposPublicoAsync()
        {
            return await ApiClient.GetAsync<List<TipoUsuarioDto>>($"{Endpoint}/tipos-publico");
        }

        public async Task RegisterAdminAsync(UsuarioCreateDto usuario)
        {
            await ApiClient.PostAsync<UsuarioCreateDto, object>("api/auth/register-admin", usuario);
        }
    }
}
