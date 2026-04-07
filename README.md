# 🏠 RealEstatePortal

> A modern, high-performance ASP.NET Core MVC platform designed for efficient real estate management and seamless connection between agents and clients. Built on the principles of Clean Architecture and the Service-Repository Pattern.

![.NET Version](https://img.shields.io/badge/.NET-8.0-purple?style=flat&logo=dotnet)
![ASP.NET Core](https://img.shields.io/badge/ASP.NET_Core-MVC-blue?style=flat&logo=microsoft)
![Entity Framework Core](https://img.shields.io/badge/EF_Core-8.0-red?style=flat&logo=dotnet)
![SQL Server](https://img.shields.io/badge/SQL_Server-2022-red?style=flat&logo=microsoftsqlserver)
![Bootstrap](https://img.shields.io/badge/Bootstrap-5.3-purple?style=flat&logo=bootstrap)

![RealEstatePortal Banner](/screenshots/hero-preview.png)

---

## 📋 Table of Contents

- [About the Project](#-about-the-project)
- [Visual Preview](#-visual-preview)
- [Key Features](#-key-features)
- [Technical Highlights & Challenges](#-technical-highlights--challenges-solved)
- [Technologies Used](#-technologies-used)
- [Project Architecture](#-project-architecture)
- [Getting Started](#-getting-started)
- [Contact](#-contact)

---

## 📖 About the Project

**RealEstatePortal** is a comprehensive web application for the real estate sector. It allows users to browse, filter, and save properties, while agents can manage their listings and professional profiles. The design is inspired by modern minimalist interfaces, utilizing the **Inter** font family and glassmorphism elements for a premium user experience.

The project goes beyond standard CRUD operations, focusing on **Scalability**, **Security**, and **Separation of Concerns**.

---

## 📸 Visual Preview

<table border="0">
  <tr>
    <td width="50%" align="center">
      <img src="/screenshots/home-page.png" alt="Home Page" width="100%"/>
      <br>
      <em>Home page with advanced search</em>
    </td>
    <td width="50%" align="center">
      <img src="/screenshots/search-results.png" alt="Search Results" width="100%"/>
      <br>
      <em>Detailed filters and property listings</em>
    </td>
  </tr>
  <tr>
    <td width="50%" align="center">
      <img src="/screenshots/property-details.png" alt="Property Details" width="100%"/>
      <br>
      <em>Full information and property specifications</em>
    </td>
    <td width="50%" align="center">
      <img src="/screenshots/agent-profile.png" alt="Agent Profile" width="100%"/>
      <br>
      <em>Agent business card with their active listings</em>
    </td>
  </tr>
</table>

---

## ✨ Key Features

- [x] **Property Management:** Full CRUD cycle for listings with detailed attributes (floor, exposure, construction type).
- [x] **Advanced Search:** Dynamic filtering by price, location, transaction type (Sale/Rent), and sorting.
- [x] **Favorites System:** Users can save properties to personalized "My Favorites" lists.
- [x] **Agent Ecosystem:** Dedicated agent profiles including working hours, spoken languages, and auto-linked listings.
- [x] **Image Gallery:** Support for multiple images per property with designated "Main Image" functionality.
- [x] **Smart Authorization:** Role-based security ensuring only listing owners can modify their data.
- [x] **Responsive UI:** Fully adaptive design for mobile, tablet, and desktop devices.

---

## 💡 Technical Highlights & Challenges Solved

### 1. Generic Repository Pattern
**The Challenge:** Avoiding code duplication when accessing data for multiple entities (RealEstates, Agents, Cities, Categories).
**The Solution:** I implemented `IBaseRepository` and `BaseRepository` to encapsulate common operations. This allowed the Service Layer to work with abstractions, significantly improving maintainability and Unit Testing.

### 2. Complex "Favorites" Mechanism (Toggle Logic)
**The Challenge:** Creating a seamless user experience for adding/removing properties from favorites that works correctly across different views (Home, Search, Details).
**The Solution:** Developed an asynchronous `ToggleFavoriteAsync` method that intelligently manages relations in the `UserFavoriteRealEstate` table and maintains UI state in real-time.

### 3. Identity Customization
**The Challenge:** The default `IdentityUser` was insufficient for the portal's domain requirements.
**The Solution:** Extended the system with `ApplicationUser`, adding specific fields (First/Last Name) and integrating it with `Agent` entities via Fluent API configurations.

---

## 🛠️ Technologies Used

| Technology | Version | Purpose |
| :--- | :--- | :--- |
| **ASP.NET Core** | 8.0 | Main Web Framework (MVC Pattern) |
| **Entity Framework Core** | 8.0 | ORM for Database Management |
| **SQL Server** | 2022 | Relational Database |
| **NUnit / Moq** | - | Unit Testing of Business Logic |
| **Bootstrap** | 5.3 | Responsive Frontend Framework |
| **Inter Font** | - | Primary Typography for Modern UI |

---

## 📁 Project Architecture

The solution follows an **N-Tier Architecture** to ensure a clean separation of concerns.

```text
RealEstatePortal.sln
│
├── 🗄️ Data Layer (RealEstatePortal.Data)
│   ├── Configurations/      # Fluent API (Seeding, Relationships)
│   ├── Models/              # Database Entities
│   └── Repository/          # IBaseRepository & BaseRepository
│
├── 🧠 Service Layer (RealEstatePortal.Services.Core)
│   └── Implementations/     # Business Logic (RealEstateService, AgentService)
│
├── 🌐 Presentation Layer (RealEstatePortal.Web)
│   ├── Controllers/         # MVC Controllers
│   ├── Views/               # Razor Views
│   └── wwwroot/             # Static Assets (CSS, JS, Images)
│
├── 📝 ViewModels Layer (RealEstatePortal.Web.ViewModels)
│   └── Agent/RealEstate/    # DTOs (Decoupled from DB Models)
│
└── 🧪 Tests (RealEstatePortal.Services.Tests)
    └── Unit Tests           # Service Layer Testing
