using Vion_Desktop.Models;

namespace Vion_Desktop.Services
{
    public class AuthService
    {
        public async Task<(bool Success, string Message)> LoginAsync(string email, string senha)
        {
            try
            {
                var loginData = new LoginModel { Email = email, Senha = senha };
                // Endpoint ajustado para incluir 'api/' pois o BaseUrl agora é a raiz
                var response = await ApiClient.PostAsync<LoginModel, LoginResponse>("api/auth/login", loginData);

                if (response != null && !string.IsNullOrEmpty(response.Token))
                {
                    // Valida se é Admin ou Gerente
                    if (response.Role == "Admin" || response.Role == "Gerente")
                    {
                        ApiClient.SetToken(response.Token, response.Role);
                        return (true, "Login realizado com sucesso.");
                    }
                    else
                    {
                        return (false, "Usuário sem permissão de acesso ao sistema Desktop.");
                    }
                }
                return (false, "Falha na autenticação.");
            }
            catch (Exception ex)
            {
                return (false, $"Erro: {ex.Message}");
            }
        }
    }
}
