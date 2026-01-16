using System;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;

namespace Vion.Web.Services
{
    public class FreteResult
    {
        public decimal Valor { get; set; }
        public int PrazoDias { get; set; }
        public string? Tipo { get; set; } // "PAC", "SEDEX", "Transportadora"
        public string? Erro { get; set; }
    }

    public class ViaCepResult
    {
        public string? Cep { get; set; }
        public string? Logradouro { get; set; }
        public string? Complemento { get; set; }
        public string? Bairro { get; set; }
        public string? Localidade { get; set; }
        public string? Uf { get; set; }
        public bool Erro { get; set; }
    }

    public interface IFreteService
    {
        Task<FreteResult> CalcularFreteAsync(string cep, decimal valorCarrinho);
        Task<ViaCepResult> ConsultarCepAsync(string cep);
    }

    public class FreteService : IFreteService
    {
        private readonly HttpClient _httpClient;

        public FreteService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<ViaCepResult> ConsultarCepAsync(string cep)
        {
            try
            {
                var cleanCep = cep.Replace("-", "").Replace(".", "").Trim();
                if (cleanCep.Length != 8) return new ViaCepResult { Erro = true };

                var response = await _httpClient.GetFromJsonAsync<ViaCepResult>($"https://viacep.com.br/ws/{cleanCep}/json/");
                return response ?? new ViaCepResult { Erro = true };
            }
            catch
            {
                return new ViaCepResult { Erro = true };
            }
        }

        public async Task<FreteResult> CalcularFreteAsync(string cep, decimal valorCarrinho)
        {
            // Simulação de cálculo de frete
            // Regra de Negócio (Admin simulation):
            // 1. Frete Grátis para compras acima de R$ 300,00
            // 2. Sudeste (SP, RJ, MG, ES) tem frete mais barato
            // 3. Outros estados um pouco mais caro

            if (valorCarrinho >= 300)
            {
                return new FreteResult
                {
                    Valor = 0,
                    PrazoDias = 5,
                    Tipo = "Frete Grátis"
                };
            }

            var endereco = await ConsultarCepAsync(cep);
            if (endereco.Erro)
            {
                return new FreteResult { Erro = "CEP não encontrado" };
            }

            decimal valorFrete;
            int prazo;

            var sudeste = new[] { "SP", "RJ", "MG", "ES" };
            
            if (Array.Exists(sudeste, uf => uf == endereco.Uf))
            {
                valorFrete = 15.00m;
                prazo = 3;
            }
            else if (endereco.Uf == "DF" || endereco.Uf == "GO")
            {
                valorFrete = 25.00m;
                prazo = 5;
            }
            else
            {
                valorFrete = 45.00m;
                prazo = 10;
            }

            return new FreteResult
            {
                Valor = valorFrete,
                PrazoDias = prazo,
                Tipo = "Normal"
            };
        }
    }
}
