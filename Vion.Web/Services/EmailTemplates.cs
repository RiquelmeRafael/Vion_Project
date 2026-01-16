using Vion.Domain.Entities;
using System.Text;
using System;

namespace Vion.Web.Services
{
    public static class EmailTemplates
    {
        public static string GetOrderConfirmationTemplate(Pedido pedido, string pedidoUrl)
        {
            var sb = new StringBuilder();
            sb.Append($@"
<div style='font-family: Arial, sans-serif; max-width: 600px; margin: 0 auto; border: 1px solid #e0e0e0; border-radius: 8px; overflow: hidden;'>
    <div style='background-color: #000; color: #fff; padding: 20px; text-align: center;'>
        <h1 style='margin: 0;'>Pedido Confirmado!</h1>
    </div>
    <div style='padding: 20px; background-color: #fff; color: #333;'>
        <p>Olá, <strong>{pedido.NomeCliente}</strong>!</p>
        <p>Recebemos seu pedido <strong>#{pedido.Id}</strong> com sucesso.</p>
        <p>Status atual: <strong style='color: #28a745;'>{pedido.Status}</strong></p>
        
        <hr style='border: 0; border-top: 1px solid #eee; margin: 20px 0;'>
        
        <h3 style='color: #000;'>Resumo do Pedido</h3>
        <table style='width: 100%; border-collapse: collapse;'>
");
            
            if (pedido.Itens != null)
            {
                foreach(var item in pedido.Itens)
                {
                    // Note: item.Produto might be null if not included, so we handle that safely
                    string nomeProduto = item.Produto?.Nome ?? "Produto";
                    sb.Append($@"
                <tr style='border-bottom: 1px solid #f0f0f0;'>
                    <td style='padding: 10px 0;'>{nomeProduto} <small>(x{item.Quantidade})</small></td>
                    <td style='padding: 10px 0; text-align: right;'>{item.PrecoUnitario:C}</td>
                </tr>
");
                }
            }

            sb.Append($@"
        </table>
        
        <div style='margin-top: 20px; text-align: right;'>
            <p style='margin: 5px 0;'>Frete: {pedido.ValorFrete:C}</p>
            <p style='margin: 5px 0; font-size: 1.2em; font-weight: bold;'>Total: {pedido.ValorTotal:C}</p>
        </div>

        <div style='background-color: #f9f9f9; padding: 15px; border-radius: 5px; margin-top: 20px;'>
            <p style='margin: 0; font-size: 0.9em; color: #666;'>
                <strong>Endereço de Entrega:</strong><br>
                {pedido.Endereco}, {pedido.Cidade} - {pedido.Estado}<br>
                CEP: {pedido.Cep}
            </p>
        </div>

        <div style='margin-top: 30px; text-align: center;'>
            <a href='{pedidoUrl}' style='background-color: #000; color: #fff; padding: 12px 25px; text-decoration: none; border-radius: 4px; font-weight: bold;'>Ver Pedido</a>
        </div>
    </div>
    <div style='background-color: #f0f0f0; padding: 15px; text-align: center; font-size: 0.8em; color: #666;'>
        <p>&copy; {DateTime.Now.Year} Vion. Todos os direitos reservados.</p>
    </div>
</div>
");
            return sb.ToString();
        }

        public static string GetStatusUpdateTemplate(Pedido pedido, string novoStatus, string pedidoUrl)
        {
            return $@"
<div style='font-family: Arial, sans-serif; max-width: 600px; margin: 0 auto; border: 1px solid #e0e0e0; border-radius: 8px; overflow: hidden;'>
    <div style='background-color: #000; color: #fff; padding: 20px; text-align: center;'>
        <h1 style='margin: 0;'>Atualização de Status</h1>
    </div>
    <div style='padding: 20px; background-color: #fff; color: #333;'>
        <p>Olá, <strong>{pedido.NomeCliente}</strong>!</p>
        <p>O seu pedido <strong>#{pedido.Id}</strong> teve uma atualização.</p>
        
        <div style='text-align: center; margin: 30px 0;'>
            <p style='font-size: 1.1em; margin-bottom: 10px;'>Novo Status:</p>
            <span style='background-color: #007bff; color: white; padding: 10px 20px; border-radius: 20px; font-weight: bold; font-size: 1.2em;'>
                {novoStatus}
            </span>
        </div>

        <p>Você pode acompanhar os detalhes do seu pedido acessando sua conta em nosso site.</p>
        
        <div style='margin-top: 30px; text-align: center;'>
            <a href='{pedidoUrl}' style='background-color: #000; color: #fff; padding: 12px 25px; text-decoration: none; border-radius: 4px; font-weight: bold;'>Ver Pedido</a>
        </div>
    </div>
    <div style='background-color: #f0f0f0; padding: 15px; text-align: center; font-size: 0.8em; color: #666;'>
        <p>&copy; {DateTime.Now.Year} Vion. Todos os direitos reservados.</p>
    </div>
</div>
";
        }
    }
}
