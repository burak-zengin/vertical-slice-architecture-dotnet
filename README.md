# Vertical Slice Architecture with .NET

A practical **Vertical Slice Architecture** sample built with **ASP.NET Core**, **Carter**, **MediatR**, **FluentValidation**, **Entity Framework Core**, and **SQL Server**.

This repository demonstrates how to organize a .NET application around **features** instead of traditional technical layers.

---

# Purpose

The goal of this project is to show a clean and pragmatic way of building APIs where each feature owns its own:

- Endpoint
- Request / response models
- Validation
- Command or query
- Handler
- Business logic

Instead of spreading a single use case across Controllers, Services, DTO folders, and Repository layers, each vertical slice keeps related code close together.

---

# Tech Stack

- ASP.NET Core
- Minimal API
- Carter
- MediatR
- FluentValidation
- Entity Framework Core
- SQL Server
- Docker Compose
- Swagger / OpenAPI

---

# Architecture Overview

```text
src/
└── VerticalSlice/
    ├── Common/
    │
    ├── Features/
    │   └── Products/
    │       ├── CreateProduct/
    │       ├── GetProduct/
    │       └── Product.cs
    │
    ├── Infrastructure/
    │
    └── Program.cs
```

---

# Why Vertical Slice Architecture?

Traditional layered architecture usually groups code by technical responsibility:

```text
Controllers/
Services/
Repositories/
DTOs/
Validators/
```

This works, but as the project grows, a single feature often becomes scattered across many folders.

Vertical Slice Architecture takes a different approach:

```text
Features/
└── Products/
    └── CreateProduct/
        ├── Endpoint
        ├── Request
        ├── Validator
        ├── Command
        └── Handler
```

Each feature becomes an independent slice of the system.

---

# Benefits

- Feature-focused structure
- Less jumping between folders
- Better maintainability
- Clear command/query boundaries
- Easier refactoring
- Better scalability for growing teams
- Simple and pragmatic API organization

---

# Implemented Features

## Products

- Create product
- Get product by id

Each product feature is organized as an independent vertical slice.

---

# Running the Project

## Prerequisites

Make sure you have installed:

- .NET SDK
- Docker
- Docker Compose

---

## Run with Docker Compose

```bash
docker compose up --build
```

This will start:

- ASP.NET Core API
- SQL Server container

---

## Run locally

```bash
dotnet restore
dotnet build
dotnet run --project src/VerticalSlice/VerticalSlice.csproj
```

Then open Swagger:

```text
https://localhost:<port>/swagger
```

---

# Database

The application uses SQL Server through Entity Framework Core.

The connection string is configured in the application settings and can be overridden through Docker Compose or environment variables.

---

# Design Notes

This repository intentionally keeps the structure simple.

The focus is not on adding every enterprise pattern, but on demonstrating how a real-world .NET API can be organized around business use cases.

This project is a good starting point for:

- Learning Vertical Slice Architecture
- Comparing layered architecture with feature-based architecture
- Building small and medium-sized APIs
- Teaching clean API organization in .NET

---

# Possible Improvements

Future improvements may include:

- Unit tests
- Integration tests with Testcontainers
- Pipeline behaviors
- Global exception handling
- Pagination examples
- Update and delete product slices
- Domain events
- Outbox pattern
- CI/CD pipeline
