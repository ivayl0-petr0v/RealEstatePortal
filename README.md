# 🏠 RealEstateApp - ASP.NET Core MVC Project

Welcome to **RealEstateApp** – a comprehensive platform for managing real estate listings, built with **ASP.NET Core 8**. This project was developed as part of the SoftUni ASP.NET Advanced course (February 2026) to bridge the gap between real estate agents and potential buyers or tenants.

---

## 🛠️ Tech Stack

This project leverages the modern Microsoft ecosystem:

* **Backend:** ASP.NET Core 8.0 MVC
* **Database:** Microsoft SQL Server
* **ORM:** Entity Framework Core
* **Identity:** ASP.NET Core Identity (Roles: Admin, Agent, User)
* **Frontend:** Razor Views, Bootstrap 5, JavaScript
* **Architecture:** Clean Architecture / Layered Architecture (Repository Pattern & Services)

---

## 🌟 Key Features

### 🔍 For Users (Clients)
* **Advanced Filtering:** Search for properties by type (Apartment, House, Office), price range, location, and number of rooms.
* **Property Details:** Detailed views featuring image galleries, technical specifications, and amenities.

### 💼 For Real Estate Agents
* **Listing Management:** Full CRUD (Create, Read, Update, Delete) functionality for their own property listings.
* **Favorites:** A personal "Wishlist" where users can save properties they are interested in.

### 🛡️ Administrative Panel
* **Category Management:** Manage property types, amenities, and cities.
* **Global Overview:** Monitor all active listings and platform activity.

---

## ✅ Default Seeded Accounts

To make testing easier, the application comes with pre-seeded accounts for each role. You can use these credentials to explore different access levels:

| Role | Email | Password |
| :--- | :--- | :--- |
| **Admin** | `admin@test.com` | `Admin123!` |
| **Agent** | `testuser@example.com` | `test123` |

---

## ⚙️ Installation & Setup

1.  **Clone the repository:**
    ```bash
    git clone https://github.com/YourUsername/RealEstateApp.git
    ```
2.  **Configure the Database:**
    Update the connection string in `appsettings.json` to match your local SQL Server instance:
    ```json
    "ConnectionStrings": {
        "DefaultConnection": "Server=(localdb)\\mssqllocaldb;Database=RealEstatePortal;Trusted_Connection=True;MultipleActiveResultSets=true"
    }
    ```
3.  **Open in Visual Studio**
    * Open the .sln file
    * Restore NuGet packages
    
5.  **Apply Migrations:**
    Run the following command in the Package Manager Console:
    ```bash
    Update-Database
    ```
6.  **Run the Application:**
    Press `F5` in Visual Studio or use the terminal:
    ```bash
    dotnet run
    ```

---

## 📂 Project Structure

* **RealEstate.Data:** Database models, DbContext, and data seeding.
* **RealEstate.Services:** Business logic, DTOs, and service layers.
* **RealEstate.Tests:** Unit testing using NUnit
* **RealEstate.Web:** Controllers, ViewModels, Razor Views, and Static Assets.

---
⭐ *If you find this project useful, please give it a star!*
