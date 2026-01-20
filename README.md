# Simple Order System

A production-ready ASP.NET Core 8 web application built using Clean Architecture.
The project demonstrates secure authentication, role-based authorization, and
well-structured business logic suitable for real-world applications.

---

## 🏗 Architecture

The application follows Clean Architecture principles:

- **Domain** – Core entities and business rules
- **Application** – Use cases and business logic
- **Infrastructure** – EF Core, SQL Server, Identity
- **Web** – MVC UI, controllers, authentication

All dependencies flow inward, ensuring low coupling and high maintainability.

---

## 🧩 Technologies Used

- ASP.NET Core 8 (MVC)
- Entity Framework Core
- SQL Server
- ASP.NET Core Identity
- Bootstrap
- xUnit + Moq (Unit Testing)

---

## 🔐 Security

- Authentication with ASP.NET Core Identity
- Role-based authorization (Admin, User)
- Secure cookies and HTTPS enforcement
- Centralized exception handling
- Environment-based configuration and secrets

---

## 🎯 Features

- User registration and login
- Product browsing
- Order creation with quantity selection
- Order history with details
- Admin dashboard for viewing all orders
- Role-protected admin functionality

---

## 🧠 Design Patterns & Principles

- Clean Architecture
- Repository Pattern
- Singleton Pattern (Order Number Generator)
- Dependency Injection
- Separation of Concerns

---

## 🚀 Getting Started

1. Clone the repository
2. Configure the database connection string
3. Run database migrations
4. Start the application

---

## 📌 Learning Outcome

This project demonstrates how to design, build, and harden a real-world ASP.NET Core
application using professional architectural and security best practices.
