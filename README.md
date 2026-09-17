OnlineShop

A full-stack online shopping application built with C# and .NET, developed as a portfolio/resume project with an emphasis on backend architecture, separation of concerns, persistence, testing, and maintainable application structure.

Note: The UI/design of the application is not my own. The primary focus of this project is the backend and application architecture.

Overview

This project implements the backend of an online shop and is structured as a multi-project .NET solution.

The application is separated into distinct layers and responsibilities rather than placing business logic, persistence, and presentation concerns in a single project.

Architecture

The solution is organized around the following projects:

Domain — Core domain models and business concepts
Application — Application logic and use cases
Common — Shared abstractions and common functionality
Infrastructure — Infrastructure-level implementations
Persistence — Database and data-access concerns
Endpoint.Site — Web/presentation layer
ApplicationTest — Automated tests for application logic
TestingData — Test and development data

The Visual Studio solution also organizes these projects into Core, Infrastructure, Presentation, and Test sections.

What Has Been Implemented

The project was built to demonstrate the development of a complete online-shop application rather than a collection of isolated examples.

Key areas covered include:

Domain modeling
Separation of application and domain logic
Layered application architecture
Database persistence
Application-level business logic
Web endpoints and presentation
Dependency separation between layers
Automated application testing
Test/development data management
A structured multi-project .NET solution
Technologies
C#
.NET
ASP.NET Core
Entity Framework Core / database persistence
SQL
HTML / CSS
Visual Studio
Git / GitHub
Project Structure
OnlineShop
│
├── Application
│   └── Application logic and use cases
│
├── Domain
│   └── Domain models and business rules
│
├── Common
│   └── Shared functionality and abstractions
│
├── Infrastructure
│   └── Infrastructure implementations
│
├── Persistence
│   └── Database and persistence layer
│
├── Endpoint.Site
│   └── Web / presentation layer
│
├── ApplicationTest
│   └── Application tests
│
├── UserData
│   └── TestingData
│       └── Test and development data
│
└── OnlineShop.sln

The repository currently contains 29 commits and maintains the application as a multi-project Visual Studio solution.

Purpose

This project was created primarily as a software engineering portfolio project.

Rather than focusing only on creating an online-shop interface, the project explores how a larger application can be divided into independent components with clear responsibilities.

Particular attention was given to:

Maintaining separation of concerns
Keeping domain logic independent from infrastructure
Structuring application functionality into use cases
Separating persistence from the rest of the application
Making application logic testable
Organizing a growing codebase into manageable projects
Status

This is a portfolio/resume project rather than a production e-commerce platform.

The project is intended to demonstrate familiarity with C#, .NET application development, backend architecture, persistence, testing, and software design principles.
