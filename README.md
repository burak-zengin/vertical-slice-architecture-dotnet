# Vertical Slice Architecture with .NET

A practical **Vertical Slice Architecture** sample built with **ASP.NET Core**, **Carter**, **MediatR**, **FluentValidation**, **Entity Framework Core**, and **SQL Server**.

This repository demonstrates how to organize a .NET application around **features** instead of traditional technical layers.

## Purpose

The goal of this project is to show a clean and pragmatic way of building APIs where each feature owns its own:

- Endpoint
- Request / response models
- Validation
- Command or query
- Handler
- Business logic

Instead of spreading a single use case across Controllers, Services, DTO folders, and Repository layers, each vertical slice keeps related code close together.

## Tech Stack

- ASP.NET Core
- Minimal API style endpoints with Carter
- MediatR
- FluentValidation
- Entity Framework Core
- SQL Server
- Docker Compose
- Swagger / OpenAPI

## Architecture Overview

```text
src/
└── VerticalSlice/
    ├── Common/
    │   └── Results/
    │
    ├── Features/
    │   └── Products/
    │       ├── CreateProduct/
    │       │   ├── CreateProduct.cs
    │       │   └── CreateProductRequest.cs
    │       │
    │       ├── GetProduct/
    │       │   ├── GetProduct.cs
    │       │   └── GetProductResponse.cs
    │       │
    │       └── Product.cs
    │
    ├── Infrastructure/
    │   └── Persistence/
    │
    └── Program.cs
