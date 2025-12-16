namespace Vion.Domain.Entities
{
    public class Produto
    {
        public int Id { get; set; }
        public string Nome { get; set; }
        public string Descricao { get; set; }
        public decimal Preco { get; set; }

        public int CategoriaId { get; set; }
        public Categoria Categoria { get; set; }

        public string Cor { get; set; }
        public string Tamanho { get; set; }
        public int Estoque { get; set; }

        public string ImagemUrl { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}
