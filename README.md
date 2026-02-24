# 🛍️ Vion Project - E-commerce Solution (.NET 10)

>**Uma solução de comércio eletrônico moderna e completa, construída com Arquitetura Limpa, ASP.NET Core e Windows Forms.**

---

## 📺 Video do Site




---

## 📖 Sobre o Projeto

**Vion** é uma plataforma de comércio eletrônico abrangente, desenvolvida como projeto final de curso. Ela demonstra conceitos avançados de engenharia de software utilizando as mais recentes tecnologias da Microsoft (**C# / .NET 10**).

A solução segue os princípios da **Arquitetura Limpa** para garantir escalabilidade, manutenibilidade e testabilidade. Ela consiste em três componentes principais:
1. **Vion.Api**: Uma API RESTful robusta que serve como núcleo do backend.

2. **Vion.Web**: Um aplicativo web MVC responsivo para que os clientes naveguem e comprem produtos.

3. **Vion_Desktop**: Um painel administrativo moderno em Windows Forms (utilizando Guna.UI2) para gerenciar a loja.

---

## 🚀Principais Recursos

### 🌐 Vion.Web (Loja do Cliente)
* **Autenticação**: Login seguro, cadastro e recuperação de senha (identidade).

* **Catálogo de Produtos**: Navegue pelos produtos com categorias, busca e detalhes.

* **Experiência de Compra**: Carrinho de compras completo e processo de finalização da compra.

* **Área do Usuário**: Histórico de pedidos, gerenciamento de perfil e favoritos.

* **Suporte em Tempo Real**: Chat integrado para atendimento ao cliente.

### 🖥️ Vion_Desktop (Painel de Administração)
* **Interface Moderna**: Desenvolvido com **Guna.UI2** para um design elegante e responsivo.

* **Painel**: Visão geral em tempo real das vendas e métricas.

* **Gerenciamento de Produtos**: Operações CRUD para produtos com upload de imagens.

* **Gerenciamento de Pedidos**: Visualize e atualize o status dos pedidos (Pendente, Aprovado, Enviado).

* **Ferramentas Administrativas**: Gerencie usuários, categorias, cupons e tamanhos.

---

## 🛠️ Tecnologias Utilizadas

Este projeto foi desenvolvido com o ecossistema de ponta **.NET 10**.

### Backend e Núcleo
* **Framework**: .NET 10 (Prévia) / ASP.NET Core Web API
* **Linguagem**: C#
* **Acesso a Dados**: Entity Framework Core (Code-First)
* **Banco de Dados**: SQL Server
* **Arquitetura**: Arquitetura Limpa (Domínio, Aplicação, Infraestrutura, Apresentação)

### Frontend (Web)
* **Framework**: ASP.NET Core MVC
* **Estilização**: Bootstrap 5, CSS personalizado
* **Scripting**: JavaScript, jQuery

### Desktop (Windows)
* **Framework**: Windows Forms (.NET 10)
* **Biblioteca de UI**: Guna.UI2
* **Comunicação**: HttpClient (Consumindo Vion.Api)

---

## 🏗️ Arquitetura

A solução é estruturada em camadas para garantir a separação de responsabilidades:

* **Vion.Domain**: Lógica e entidades corporativas (Produto, Pedido, Usuário). Sem dependências externas.

* **Vion.Application**: Casos de uso, DTOs e interfaces de serviço.

* **Vion.Infrastructure**: Implementação de acesso a dados (Repositórios EF Core) e serviços externos.

* **Vion.Api**: Expõe a lógica de negócios por meio de endpoints REST.

* **Vion.Web e Vion_Desktop**: Camadas de apresentação que consomem a lógica principal.

---

## ⚡Começando

### Pre requisitos
*   [Visual Studio 2022](https://visualstudio.microsoft.com/) (Latest Preview recommended for .NET 10 support)
*   [.NET 10 SDK](https://dotnet.microsoft.com/download/dotnet/10.0) (or .NET 9 if compatible)
*   SQL Server (Express or LocalDB)

### Instalação e execução

1.  **Clone o repositório**
    ```bash
    git clone https://github.com/RiquelmeRafael/Vion_Project.git
    cd Vion_Project
    ```

2.  **Configurar Banco de Dados**
    *   Open `Vion.Web/appsettings.json` (or `Vion.Api/appsettings.json`) and check the `ConnectionStrings`.
    *   Default is usually `Server=localhost\\SQLEXPRESS;Database=VionDb;...`

3.  **Aplicar Migrações**

Abra o terminal na raiz da solução e execute:
    ```bash
    dotnet tool install --global dotnet-ef
    dotnet ef database update --project Vion.Infrastructure --startup-project Vion.Web
    ```

4. **Executar os Aplicativos**

* **Loja Virtual**: Defina `Vion.Web` como o projeto de inicialização e execute.

* **Administração da Área de Trabalho**: Defina `Vion_Desktop` como o projeto de inicialização e execute.
---

## 📸 Imagens

### Página Home
<img width="1912" height="911" alt="image" src="https://github.com/user-attachments/assets/81709c7c-0e30-4e73-93d8-e8bd746d4c47" />

## catálogo
<img width="1896" height="932" alt="image" src="https://github.com/user-attachments/assets/b7e7d10c-b055-4e49-a225-58384ed80b64" />

## Produto
<img width="1888" height="937" alt="Captura de tela 2026-02-24 140702" src="https://github.com/user-attachments/assets/7633c626-d26c-452f-a149-8dacee2a186b" />


## Dashboard
<img width="1862" height="950" alt="image" src="https://github.com/user-attachments/assets/379a048d-8d2b-4806-b974-5ca3c466a447" />

## Gerenciamento
<img width="1881" height="933" alt="image" src="https://github.com/user-attachments/assets/14e8aace-7d4a-4dbc-83c7-44eb8e32c8b5" />



### Desktop Admin

## Login
<img width="677" height="393" alt="image" src="https://github.com/user-attachments/assets/f5743f27-b322-4d97-a40d-1ce613861f0c" />


##  Dashboard
<img width="979" height="585" alt="image" src="https://github.com/user-attachments/assets/60eba626-5cc4-4164-a1ed-f4b135c227c9" />

## Crud
<img width="981" height="583" alt="image" src="https://github.com/user-attachments/assets/15cafc18-76da-4c8f-8bb2-b1872f5c5dee" />

<img width="986" height="595" alt="image" src="https://github.com/user-attachments/assets/b16f79dd-9a38-476d-b951-11efe3dc3246" />

<img width="987" height="583" alt="image" src="https://github.com/user-attachments/assets/438759b8-7eda-4aa7-b47c-812136494b3c" />



---

<h3>Vion feito Para Você 🚀 </h3> 
