# 🛒 Projeto Vion

> **Demonstração do Projeto**
>
> [![Assista ao vídeo](https://img.youtube.com/vi/VIDEO_ID_AQUI/maxresdefault.jpg)](https://youtu.be/SEU_VIDEO_LINK_AQUI)
>
> *(Cole o link do seu vídeo acima para exibir a demonstração)*

## 📄 Sobre o Projeto

O **Projeto Vion** é uma solução completa de gerenciamento comercial (Full Stack), integrando uma plataforma Web moderna com um sistema Desktop robusto. O objetivo é oferecer controle total sobre produtos, usuários e operações, mantendo os dados sincronizados em tempo real através de uma arquitetura compartilhada.

O sistema permite que administradores gerenciem o catálogo e usuários via Desktop ou Web, enquanto clientes podem visualizar produtos e interagir pela interface Web.

### Principais Funcionalidades
*   **Gestão de Produtos:** Cadastro, edição e visualização de produtos com imagens.
*   **Gestão de Usuários:** Controle de acesso e perfis (Admin, Gerente, Cliente).
*   **Sincronização:** Dados integrados entre Web e Desktop usando a mesma base de dados.
*   **Autenticação Segura:** Login e proteção de rotas.

---

## 🛠️ Ferramentas e Tecnologias

Este projeto foi desenvolvido utilizando as tecnologias mais recentes do ecossistema .NET.

### Backend & Core
*   **Linguagem:** C#
*   **Framework:** .NET 10.0 (Preview/Latest)
*   **ORM:** Entity Framework Core 9.0.9
*   **Banco de Dados:** SQL Server

### Web (Frontend & MVC)
*   **Framework:** ASP.NET Core MVC
*   **Frontend:** Razor Views (.cshtml), HTML5, CSS3
*   **Estilização:** Bootstrap 5, Custom CSS
*   **Scripts:** JavaScript, jQuery

### Desktop (Windows Client)
*   **Framework:** Windows Forms (WinForms)
*   **UI/UX:** Guna.UI2 Framework (para interface moderna e responsiva)
*   **Integração:** Consumo de API/Serviços compartilhados

---

## 🚀 Como Rodar o Projeto

Siga os passos abaixo para executar a aplicação em sua máquina local.

### Pré-requisitos
*   [.NET SDK 10.0](https://dotnet.microsoft.com/download) instalado.
*   SQL Server (LocalDB ou instância dedicada).
*   Git instalado.

### Passo a Passo

1.  **Clone o repositório:**
    ```bash
    git clone https://github.com/RiquelmeRafael/Vion_Project.git
    cd Vion_Project
    ```

2.  **Configure o Banco de Dados:**
    Abra o terminal na pasta raiz e execute as migrações para criar o banco:
    ```bash
    dotnet tool install --global dotnet-ef
    dotnet ef database update --project Vion.Infrastructure --startup-project Vion.Web
    ```

3.  **Rodar a Aplicação Web:**
    ```bash
    cd Vion.Web
    dotnet run
    ```
    *O site estará acessível em `https://localhost:7153` (ou porta similar).*

4.  **Rodar a Aplicação Desktop:**
    Abra um novo terminal:
    ```bash
    cd Vion_Desktop
    dotnet run
    ```

---

## 📸 Galeria de Imagens

Espaço reservado para prints das telas do sistema.

### Web
| Home Page | Detalhes do Produto |
|:---:|:---:|
| ![Home](https://via.placeholder.com/400x200?text=Home+Page) | ![Detalhes](https://via.placeholder.com/400x200?text=Detalhes) |

### Desktop
| Dashboard | Cadastro de Usuários |
|:---:|:---:|
| ![Dashboard](https://via.placeholder.com/400x200?text=Dashboard+Desktop) | ![Usuarios](https://via.placeholder.com/400x200?text=Cadastro+Usuarios) |

---

Desenvolvido por **Riquelme Rafael**.
