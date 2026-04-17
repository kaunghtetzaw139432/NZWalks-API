# 🚀 NZ Walks API

NZ Walks API is a **RESTful Web API** built with **ASP.NET Core 8** to manage walking tracks across New Zealand. This is my **first backend project**, focused on mastering CRUD operations and database relationships.

## 📂 Project Overview
The primary goal of this project was to learn the fundamentals of backend development, database connectivity, and API design patterns by building a real-world service.

## 🛠️ Tech Stack
- **Framework:** ASP.NET Core 10 Web API
- **ORM:** Entity Framework Core (EF Core)
- **Database:** SQL Server
- **Architecture:** Standard API Design (Domain Models & DTOs)

  ## 📊 Database Architecture

To manage the walking tracks effectively, I designed a relational database schema that ensures data integrity and supports complex queries. Below is the **Entity Relationship Diagram (ERD)** based on the C# Domain Models:

<p align="center">
  <img src="https://github.com/user-attachments/assets/3848fe89-c571-4e65-b80c-eac8aca9fc31" width="90%" alt="NZ Walks Database ER Diagram" style="border-radius: 10px; border: 1px solid #ddd;">
</p>

### 🔗 Relationship Logic
* **Walk & Region:** Each `Walk` belongs to exactly one `Region`, but a `Region` can contain multiple `Walks` (Many-to-One).
* **Walk & Difficulty:** Each `Walk` is assigned a specific `Difficulty` level (Many-to-One).
* **Guids:** All primary keys use `Guid` (uniqueidentifier) for global uniqueness, as shown in the C# classes.

---

## ✨ Key Features
- **Comprehensive CRUD:** Full Create, Read, Update, and Delete operations for **Regions**, **Difficulties**, and **Walks**.
- **Complex Relationships:** Implemented **Many-to-One** relationships between Walks and their respective Regions/Difficulties.
- **Eager Loading:** Used the `.Include()` method to fetch and display detailed object data (e.g., Category names) instead of just raw IDs.
- **Data Encapsulation:** Segregated Domain Models from DTOs (Data Transfer Objects) to ensure better security and maintainability.

## 🎯 What I Learned (First Project Milestone)
Building this project provided hands-on experience with:
- Understanding **RESTful principles** and HTTP Methods (GET, POST, PUT, DELETE).
- Using **Code-First Migrations** in Entity Framework Core to manage database schemas.
- Handling Data Mapping manually to understand the flow between the Database and the Client.
- Troubleshooting JSON serialization and Guid format validations.

## 🚀 How to Run
1. **Clone the Repository:** `git clone  https://github.com/kaunghtetzaw139432/NZWalks-API.git`
2. **Configure Database:** Update the connection string in `appsettings.json` to point to your local SQL Server instance.
3. **Apply Migrations:** Run `update-database` in the Package Manager Console.
4. **Launch:** Run the application and use **Swagger** or **Firecamp** to test the endpoints.

---
*Created by Kaung Htet Zaw as part of my Backend Engineering journey.*
