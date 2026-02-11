# 🛒 Projeto Vion

> **Demonstração do Projeto**
>
>![videoVion](https://github.com/user-attachments/assets/9b1967b9-85a2-43ee-97cf-d55134647463)

>


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

## 📸Imagns do Site


### Web
<h2>Home Page </h2> 
 <img width="1899" height="947" alt="image" src="https://github.com/user-attachments/assets/c6a54b38-8c0d-4aa2-864b-9f10c509a568" />

<h2>Catálogo</h2> 
 <img width="1893" height="947" alt="image" src="https://github.com/user-attachments/assets/52008039-fa12-4555-9f21-4dad5ab989bf" />

<h2>Produtos</h2>
<img width="1906" height="944" alt="image" src="https://github.com/user-attachments/assets/893fb59f-6b8d-4ee4-b8a6-2ccb52fa30ca" /> 
<img width="1890" height="848" alt="image" src="https://github.com/user-attachments/assets/3ad79a42-29f0-4d90-97ae-444ca9da75ad" />



<h2>Sobre</h2>
<img width="1895" height="948" alt="image" src="https://github.com/user-attachments/assets/b16df3ec-04a8-4e2d-8c78-7a385574e3df" />



<h2>Gerenciamento</h2>
<img width="1889" height="917" alt="image" src="https://github.com/user-attachments/assets/be00a907-78cd-4c2b-a07c-79c9b1c699d1" />


<h2>Dashboard</h2>
<img width="1890" height="944" alt="image" src="https://github.com/user-attachments/assets/129e8d2a-7f06-45cb-bcda-d8be7454c9c1" />

### Desktop

<h2>Login</h2>
<img width="675" height="353" alt="image" src="https://github.com/user-attachments/assets/994a5467-c596-4be2-91cc-cd474f1a5a89" />


<h2>Dashboard Desktop</h2>
<img width="981" height="576" alt="image" src="https://github.com/user-attachments/assets/23738d28-1b26-4900-9e43-b00b1d6669f2" />


