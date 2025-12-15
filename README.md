# ShoeShop

 A minimal, modern ASP.NET Web API for a shoe store — products, categories, and simple CRUD operations.

 Built with .NET 10, Entity Framework Core, and a small project-per-concern structure so it's easy to extend.

 ----

 ## Tech stack

 - .NET 10 (ASP.NET Core Web API)
 - Entity Framework Core (DbContext + Migrations)
 - C# 12

 ## Repository structure

 - `ShoeShop.sln` — solution
 - `ShoeShop.WebApi/` — Web API project (startup project)
 - `ShoeShop.DataContext/` — EF Core DbContext + Migrations
 - `ShoeShop.Entities/` — domain entities (`Product`, `Category`)
 - `ShoeShop.Dto/` — DTOs for API models

 ## Quick start

 Prerequisites:

 - .NET 10 SDK (install from https://dotnet.microsoft.com)
 - (optional) `dotnet-ef` tool for running migrations: `dotnet tool install --global dotnet-ef`

 Restore, build and run the API from the repo root:

 ```bash
 dotnet restore
 dotnet build
 dotnet run --project ShoeShop.WebApi
 ```

 The API will listen on the ports configured in `ShoeShop.WebApi/Properties/launchSettings.json` when run locally.

 ## Database / Migrations

 This solution keeps the EF Core context and migrations in `ShoeShop.DataContext`.

 To apply migrations to your database (example uses default connection from `appsettings.json` or your environment):

 ```bash
 # from repo root
 dotnet ef database update --project ShoeShop.DataContext --startup-project ShoeShop.WebApi
 ```

 If `dotnet ef` is not installed, install it with:

 ```bash
 dotnet tool install --global dotnet-ef
 ```

 ## API Endpoints (examples)

 Categories
 - GET `/api/category` — list categories
 - GET `/api/category/{id}` — get category by id
 - POST `/api/category` — create category
 - PUT `/api/category/{id}` — update category
 - DELETE `/api/category/{id}` — delete category

 Products
 - GET `/api/product` — list products (optional `?category=Shoes`)
 - GET `/api/product/{id}` — get product by id
 - POST `/api/product` — create product
 - PUT `/api/product/{id}` — update product
 - DELETE `/api/product/{id}` — delete product

 Example: create a category

 ```bash
 curl -X POST "http://localhost:5000/api/category" \
   -H "Content-Type: application/json" \
   -d '{"name":"Shoes"}'
 ```

 Example: list products

 ```bash
 curl http://localhost:5000/api/product
 ```

 ## Notes & gotchas

 - The projects target `.NET 10`. Make sure your SDK version matches.
 - `ShoeShop.DataContext` contains EF Core migrations — use the `--project` and `--startup-project` flags when using `dotnet ef`.
 - Controllers use repository interfaces (`ICategoryRepository`, `IProductRepository`). If you add implementations, wire them in `Program.cs`'s DI container.

 ## Contributing

 Contributions welcome. Open an issue or submit a PR — keep changes focused and add small commits.

 ## License

 This project is provided as-is; add a license file if you plan to release it publicly.

 ----

 Happy hacking!
