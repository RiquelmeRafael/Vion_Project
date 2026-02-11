# 👟 Vion Store

**Vion Store** é uma plataforma de e-commerce moderna e completa desenvolvida para a venda de calçados e acessórios esportivos. O projeto oferece uma experiência de compra fluida para os clientes e um painel administrativo robusto para gerenciamento da loja.

---

### 🎥 Demonstração

[Insira aqui o vídeo ou GIF de demonstração do projeto]

---

## 🚀 Funcionalidades

### 🛍️ Para o Cliente
*   🔍 **Catálogo Inteligente**: Pesquisa de produtos por nome, filtros por categoria e tamanho.
*   📄 **Detalhes do Produto**: Visualização detalhada com galeria de imagens, seleção de tamanho e cálculo de frete.
*   🛒 **Carrinho de Compras**: Gestão completa de itens, cálculo de subtotal e frete.
*   💳 **Checkout Otimizado**: Preenchimento automático de endereço via CEP (Integração ViaCEP) e validação de CPF.
*   ❤️ **Favoritos**: Salve seus produtos preferidos para comprar depois.
*   👤 **Área do Cliente**: Histórico de pedidos ("Meus Pedidos"), edição de perfil e alteração de senha.
*   💬 **Chat de Suporte**: Canal direto de comunicação com a loja para tirar dúvidas.

### ⚙️ Painel Administrativo
*   📊 **Dashboard**: Visão geral das vendas e métricas da loja.
*   📦 **Gestão de Produtos**: CRUD completo de produtos, categorias e tamanhos.
*   🏷️ **Cupons de Desconto**: Criação e gerenciamento de códigos promocionais.
*   💬 **Central de Atendimento**: Gerenciamento e resposta aos chamados de suporte dos clientes.
*   🖼️ **Gestão de Conteúdo**: Configuração da Home Page (Banners e Destaques) via painel.

---

## 🛠️ Tecnologias Utilizadas

**Backend:**
*   🟣 C# .NET 8
*   🌐 ASP.NET Core MVC
*   🗄️ Entity Framework Core (ORM)
*   🔒 ASP.NET Core Identity (Autenticação)

**Frontend:**
*   🎨 HTML5 & CSS3
*   🅱️ Bootstrap 5
*   📜 JavaScript (jQuery & Vanilla)

**Banco de Dados:**
*   🛢️ SQL Server

---

## 💾 Banco de Dados

O projeto utiliza o **Entity Framework Core** com a abordagem **Code-First**. As tabelas são geradas automaticamente através de Migrations.

**Como configurar:**
1.  Certifique-se de ter o **SQL Server** instalado e rodando.
2.  A string de conexão já está configurada no `appsettings.json` (padrão LocalDB ou SQL Server local).
3.  Execute o comando de atualização para criar o banco:
    ```bash
    dotnet ef database update --project Vion.Infrastructure --startup-project Vion.Web
    ```

---

## 💻 Como rodar o projeto

### Pré-requisitos
*   [.NET 8 SDK](https://dotnet.microsoft.com/download/dotnet/8.0)
*   [SQL Server](https://www.microsoft.com/sql-server/sql-server-downloads) (ou LocalDB)

### Passo a passo

1.  **Clone o repositório**
    ```bash
    git clone https://github.com/RiquelmeRafael/Vion_Project.git
    cd Vion_Project
    ```

2.  **Restaure as dependências**
    ```bash
    dotnet restore
    ```

3.  **Atualize o Banco de Dados**
    ```bash
    dotnet ef database update --project Vion.Infrastructure --startup-project Vion.Web
    ```

4.  **Execute a aplicação**
    ```bash
    dotnet run --project Vion.Web
    ```

5.  **Acesse no navegador**
    *   O projeto rodará em: `http://localhost:5100`

---

## 📸 Fotos da Aplicação

[Insira aqui as capturas de tela (screenshots) das principais telas do sistema]
