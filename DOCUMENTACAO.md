# 📘 Documentação do Projeto Vion

## 🚀 Visão Geral

O **Projeto Vion** é uma solução completa de E-commerce desenvolvida em **.NET**, seguindo os princípios da **Clean Architecture** (Arquitetura Limpa). O sistema é composto por uma API centralizada, uma interface Web (MVC) para os clientes e administração, e uma aplicação Desktop (Windows Forms) para gestão interna.

O objetivo do projeto é fornecer uma plataforma robusta, escalável e de fácil manutenção para vendas online, gerenciamento de estoque, pedidos e usuários.

---

## 🛠 Tecnologias Utilizadas

### Backend & Core
*   **Linguagem**: C# (.NET 9.0 / .NET 10.0 Preview)
*   **Framework**: ASP.NET Core Web API
*   **ORM**: Entity Framework Core (Code-First)
*   **Banco de Dados**: SQL Server
*   **Autenticação**: JWT (JSON Web Tokens) e ASP.NET Core Identity (Cookie Auth no MVC)

### Frontend Web (Vion.Web)
*   **Framework**: ASP.NET Core MVC
*   **Motor de Renderização**: Razor Views (.cshtml)
*   **Estilização**: CSS, Bootstrap, JavaScript
*   **Features**: Carrinho de compras, Checkout, Área do Cliente, Painel Administrativo Web

### Desktop (Vion_Desktop)
*   **Framework**: Windows Forms (.NET)
*   **UI Library**: Guna.UI2 (Interface Moderna e Responsiva)
*   **Comunicação**: Consumo de API via `HttpClient`
*   **Features**: Gestão de Produtos, Categorias, Cupons, Usuários e Pedidos.

---

## 🏗 Arquitetura da Solução

A solução está dividida em camadas para garantir a separação de responsabilidades:

### 1. **Vion.Domain** (Núcleo)
Contém as entidades principais do negócio e as regras fundamentais. Não possui dependências externas.
*   **Principais Entidades**: `Produto`, `Categoria`, `Usuario`, `Pedido`, `ItemPedido`, `CarrinhoItem`, `Cupom`, `Tamanho`, `Avaliacao`.

### 2. **Vion.Application** (Aplicação)
Responsável pela lógica de aplicação, DTOs (Data Transfer Objects) e interfaces de serviço.
*   **DTOs**: Objetos otimizados para transferência de dados (ex: `ProdutoCreateDto`, `PedidoDto`).
*   **Interfaces**: Contratos para os serviços que serão implementados na camada de Infraestrutura ou API.

### 3. **Vion.Infrastructure** (Infraestrutura)
Implementa o acesso a dados e serviços externos.
*   **Persistence**: `AppDbContext` (Contexto do EF Core), Migrations.
*   **Repositories**: Implementação do acesso ao banco de dados.

### 4. **Vion.Api** (Interface de Serviço)
API RESTful que expõe os dados e funcionalidades do sistema para os clientes (Web e Desktop).
*   **Controllers**: `ProdutosController`, `PedidosController`, `AuthController`, etc.
*   **Endpoints**: CRUDs completos e operações de negócio (Upload, Checkout).

### 5. **Vion.Web** (Interface Web)
Aplicação MVC para o usuário final e administradores.
*   **Área Pública**: Catálogo, Detalhes do Produto, Carrinho, Checkout.
*   **Área Admin**: Painel de controle web para gerenciamento rápido.

### 6. **Vion_Desktop** (Interface Desktop)
Aplicação Windows Forms para gestão administrativa robusta.
*   **Views**: Formulários para cadastro e listagem.
*   **Services**: Camada de serviço client-side para comunicação com a API.

---

## 📦 Funcionalidades Detalhadas

### 🌐 Vion.Web (Loja Virtual)
1.  **Autenticação e Usuários**:
    *   Login, Cadastro, Recuperação de Senha.
    *   Perfil do Usuário com histórico de pedidos e favoritos.
2.  **Catálogo de Produtos**:
    *   Listagem com filtros por categoria e busca.
    *   Detalhes do produto com seleção de tamanho e cor.
    *   Sistema de Avaliações e Comentários.
3.  **Carrinho e Checkout**:
    *   Adicionar/Remover itens.
    *   Cálculo de frete e total.
    *   Finalização de pedido com seleção de endereço e pagamento.
4.  **Chat em Tempo Real**:
    *   Suporte ao cliente integrado.

### 🖥️ Vion_Desktop (Gestão)
1.  **Dashboard**:
    *   Visão geral de vendas e métricas.
2.  **Gestão de Produtos**:
    *   Cadastro completo (Nome, Preço, Estoque, Imagens).
    *   **Filtros Avançados**: Busca por nome e categoria.
    *   **Upload de Imagens**: Integração para envio de fotos.
    *   **Cupons**: Associação de cupons promocionais a produtos.
3.  **Gestão de Pedidos**:
    *   Visualização de pedidos e alteração de status (Pendente, Aprovado, Cancelado).
4.  **Cadastros Auxiliares**:
    *   Categorias, Tamanhos e Usuários.

---

## 🗄️ Modelo de Dados (Principais Entidades)

*   **Usuario**: Clientes e Administradores.
*   **Produto**: Item vendável, com relacionamento para `Categoria`, `Tamanho` e `Cupom`.
*   **Pedido**: Registro de compra, contém `ItensPedido`, dados de entrega e status.
*   **CarrinhoItem**: Itens temporários do carrinho de compras do usuário.
*   **Cupom**: Códigos promocionais aplicáveis a produtos ou pedidos.

---

## 🚀 Como Executar o Projeto

### Pré-requisitos
*   Visual Studio 2022+
*   .NET SDK 9.0 ou superior
*   SQL Server

### Passos
1.  **Configurar Banco de Dados**:
    *   No projeto `Vion.Api` ou `Vion.Web`, verifique a string de conexão no `appsettings.json`.
    *   Execute as migrations: `Update-Database` (via Package Manager Console).
2.  **Executar a API**:
    *   Defina `Vion.Api` como projeto de inicialização e inicie.
    *   A API rodará (ex: `http://localhost:5000`).
3.  **Executar o Desktop ou Web**:
    *   Para o Desktop: Inicie o projeto `Vion_Desktop`.
    *   Para o Web: Inicie o projeto `Vion.Web`.

---

## 📝 Considerações Finais
Este projeto demonstra a aplicação prática de conceitos modernos de desenvolvimento de software, incluindo separação de camadas, injeção de dependência, padrões de repositório e consumo de APIs RESTful. A interface Desktop utiliza a biblioteca **Guna.UI2** para oferecer uma experiência de usuário moderna e fluida.

---

## ✅ Checklist de Conformidade com Requisitos

O projeto atende e excede os requisitos propostos para a entrega, conforme detalhado abaixo:

### Requisitos Obrigatórios
*   **[X] Consistência de Dados**: A aplicação utiliza o mesmo banco de dados (`VionDb`) tanto para o módulo Web (MVC) quanto para a API consumida pelo Desktop, garantindo integridade e unicidade das informações.
*   **[X] Operações CRUD Completas**: Implementadas operações de Cadastro, Leitura, Atualização e Exclusão para as principais entidades (Produtos, Usuários, Pedidos).
*   **[X] Interface Funcional e Organizada**: Utilização da biblioteca **Guna.UI2** no Desktop para uma experiência de usuário (UX) moderna, limpa e intuitiva.
*   **[X] Integridade dos Dados**: Validações aplicadas tanto no frontend quanto no backend (API/Domain) via Data Annotations e regras de negócio.

### Evolução Arquitetural (Diferencial)
O projeto adotou a abordagem de **Evolução Arquitetural** sugerida, implementando:
*   **[X] API com Entity Framework**: Criação do projeto `Vion.Api` utilizando EF Core para abstração e persistência de dados.
*   **[X] Consumo de API no Desktop**: A aplicação `Vion_Desktop` não conecta diretamente ao banco, mas consome a API RESTful, simulando um cenário real de sistemas distribuídos.
*   **[X] Separação de Responsabilidades**: Adoção da **Clean Architecture** (Domain, Application, Infrastructure, Presentation), garantindo um código desacoplado, testável e de fácil manutenção.

