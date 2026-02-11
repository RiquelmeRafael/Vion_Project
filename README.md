# 👟 Vion Store

**Vion Store** é uma plataforma de e-commerce moderna e completa desenvolvida para a venda de calçados e acessórios esportivos. O projeto oferece uma experiência de compra fluida para os clientes e um painel administrativo robusto para gerenciamento da loja.

---

### 🎥 Demonstração


https://github.com/user-attachments/assets/d29e2912-fad3-4e59-bd63-c8bc1bc7d7a5




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

<h2>Home</h2>
<img width="1888" height="903" alt="home" src="https://github.com/user-attachments/assets/b747006c-5fd4-4633-b3fc-60c95841716e" />

<h2>Catálogo </h2>
<img width="1908" height="912" alt="catalogo" src="https://github.com/user-attachments/assets/7e2aa851-89ee-44b6-93bf-1388a0950810" />

<h2>Produtos</h2>
<img width="1912" height="912" alt="produtos" src="https://github.com/user-attachments/assets/24a1b412-5b6d-42a2-b65c-3a3a183049c9" />

<h2>Gerenciamento</h2>
<img width="1907" height="907" alt="gerenciamento" src="https://github.com/user-attachments/assets/3f4515a7-8394-4033-ac0a-18de0ea828ba" />

<h2>Carrinho</h2>
<img width="1886" height="912" alt="carrinho" src="https://github.com/user-attachments/assets/4b60b627-268f-42db-bcb7-b3fbbef4d6c5" />

<h2>Checkout</h2>
<img width="1887" height="907" alt="checkout" src="https://github.com/user-attachments/assets/bd95e46f-0875-438f-ab78-9143ac2de0fd" />



