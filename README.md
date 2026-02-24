# 🛍️ Vion Project - E-commerce Solution (.NET 10)

> **A modern, full-stack E-commerce solution built with Clean Architecture, ASP.NET Core, and Windows Forms.**

---

## 📺 Demo Video

<!-- Place your video link or embed code here -->
[![Watch the Demo](https://img.youtube.com/vi/YOUR_VIDEO_ID/maxresdefault.jpg)](https://youtu.be/YOUR_VIDEO_ID)

*(Click the image above to watch the project demonstration)*

---

## 📖 About the Project

**Vion** is a comprehensive E-commerce platform developed as a final course project. It demonstrates advanced software engineering concepts using the latest Microsoft technologies (**C# / .NET 10**).

The solution follows the **Clean Architecture** principles to ensure scalability, maintainability, and testability. It consists of three main components:
1.  **Vion.Api**: A robust RESTful API serving as the backend core.
2.  **Vion.Web**: A responsive MVC web application for customers to browse and purchase products.
3.  **Vion_Desktop**: A modern Windows Forms administrative dashboard (using Guna.UI2) for managing the store.

---

## 🚀 Key Features

### 🌐 Vion.Web (Customer Store)
*   **Authentication**: Secure Login, Registration, and Password Recovery (Identity).
*   **Product Catalog**: Browse products with categories, search, and details.
*   **Shopping Experience**: Full Shopping Cart and Checkout process.
*   **User Area**: Order history, profile management, and favorites.
*   **Real-time Support**: Integrated Chat for customer service.

### 🖥️ Vion_Desktop (Admin Dashboard)
*   **Modern UI**: Built with **Guna.UI2** for a sleek, responsive design.
*   **Dashboard**: Real-time overview of sales and metrics.
*   **Product Management**: CRUD operations for products with image upload.
*   **Order Management**: View and update order statuses (Pending, Approved, Shipped).
*   **Administrative Tools**: Manage Users, Categories, Coupons, and Sizes.

---

## 🛠️ Tech Stack

This project is built with the cutting-edge **.NET 10** ecosystem.

### Backend & Core
*   **Framework**: .NET 10 (Preview) / ASP.NET Core Web API
*   **Language**: C#
*   **Data Access**: Entity Framework Core (Code-First)
*   **Database**: SQL Server
*   **Architecture**: Clean Architecture (Domain, Application, Infrastructure, Presentation)

### Frontend (Web)
*   **Framework**: ASP.NET Core MVC
*   **Styling**: Bootstrap 5, Custom CSS
*   **Scripting**: JavaScript, jQuery

### Desktop (Windows)
*   **Framework**: Windows Forms (.NET 10)
*   **UI Library**: Guna.UI2
*   **Communication**: HttpClient (Consuming Vion.Api)

---

## 🏗️ Architecture

The solution is structured into layers to enforce separation of concerns:

*   **Vion.Domain**: Enterprise logic and entities (Product, Order, User). No external dependencies.
*   **Vion.Application**: Use cases, DTOs, and Service interfaces.
*   **Vion.Infrastructure**: Implementation of data access (EF Core Repositories) and external services.
*   **Vion.Api**: Exposes business logic via REST endpoints.
*   **Vion.Web & Vion_Desktop**: Presentation layers consuming the core logic.

---

## ⚡ Getting Started

### Prerequisites
*   [Visual Studio 2022](https://visualstudio.microsoft.com/) (Latest Preview recommended for .NET 10 support)
*   [.NET 10 SDK](https://dotnet.microsoft.com/download/dotnet/10.0) (or .NET 9 if compatible)
*   SQL Server (Express or LocalDB)

### Installation & Run

1.  **Clone the repository**
    ```bash
    git clone https://github.com/RiquelmeRafael/Vion_Project.git
    cd Vion_Project
    ```

2.  **Configure Database**
    *   Open `Vion.Web/appsettings.json` (or `Vion.Api/appsettings.json`) and check the `ConnectionStrings`.
    *   Default is usually `Server=localhost\\SQLEXPRESS;Database=VionDb;...`

3.  **Apply Migrations**
    Open the terminal in the solution root and run:
    ```bash
    dotnet tool install --global dotnet-ef
    dotnet ef database update --project Vion.Infrastructure --startup-project Vion.Web
    ```

4.  **Run the Applications**
    *   **Web Store**: Set `Vion.Web` as the startup project and run.
    *   **Desktop Admin**: Set `Vion_Desktop` as the startup project and run.

---

## 📸 Screenshots

### Web Store
<!-- Add your Web screenshots here -->
<div align="center">
  <img src="path/to/web-home.png" alt="Web Home" width="45%" />
  <img src="path/to/web-product.png" alt="Web Product" width="45%" />
</div>

### Desktop Admin
<!-- Add your Desktop screenshots here -->
<div align="center">
  <img src="path/to/desktop-dashboard.png" alt="Desktop Dashboard" width="45%" />
  <img src="path/to/desktop-products.png" alt="Desktop Products" width="45%" />
</div>

---

Made with 💜 by **Riquelme Rafael** & **Karlos**
