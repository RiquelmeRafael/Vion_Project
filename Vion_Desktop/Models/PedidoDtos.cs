using System;
using System.Collections.Generic;

namespace Vion_Desktop.Models
{
    public class PedidoDto
    {
        public int Id { get; set; }
        public DateTime DataPedido { get; set; }
        public string Status { get; set; } = string.Empty;
        public decimal Total { get; set; }
        public string FormaPagamento { get; set; } = string.Empty;
        public string EnderecoEntrega { get; set; } = string.Empty;
        public string Cidade { get; set; } = string.Empty;
        public string Estado { get; set; } = string.Empty;
        public string Cep { get; set; } = string.Empty;
        
        public UsuarioResumoDto? Usuario { get; set; }
        public List<PedidoItemDto> Itens { get; set; } = new();
    }

    public class UsuarioResumoDto
    {
        public string Nome { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
    }

    public class PedidoItemDto
    {
        public int ProdutoId { get; set; }
        public string ProdutoNome { get; set; } = string.Empty;
        public int Quantidade { get; set; }
        public decimal PrecoUnitario { get; set; }
        public string? ImagemUrl { get; set; }
    }
}
