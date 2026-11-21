# Furniture Exchange

[![CI](https://github.com/rogerscaleb78-art/Furniture-Exchange/actions/workflows/ci.yml/badge.svg)](https://github.com/rogerscaleb78-art/Furniture-Exchange/actions/workflows/ci.yml)

A web app for buying and selling used furniture. It has a C# REST API backed by MySQL, and a front end in plain HTML, CSS and JavaScript. Sellers list furniture with type, condition, city, price and a photo, buyers place pickup orders, and listings are marked sold once they're claimed.

This was my first full-stack project (April 2024, team project). I've since come back to it to add automated tests, a CI pipeline, and safer configuration.

## My role

I worked on the **C# backend**: the API logic and the data layer that talks to the MySQL database.

When I came back to the project later, I:

- moved the database credentials out of the source code and into an environment variable
- fixed a bug where looking up a single furniture item swapped its city and quality values, and added a regression test for it
- removed an order `DELETE` endpoint that actually deleted the *furniture* with that ID, plus some empty placeholder endpoints
- centralized row-to-model mapping, added xUnit tests and a GitHub Actions CI pipeline
- cleaned up template leftovers, unused imports, duplicate CORS settings and committed build output

## Skills demonstrated

C# · .NET · ASP.NET Core Web API · REST API design · MySQL · parameterized SQL · JavaScript (Fetch API) · HTML/CSS · xUnit · Git · GitHub Actions CI

## Highlights

- **REST API in C# / ASP.NET Core**: controllers for accounts, furniture and orders
- **Interface-based data layer**: each database operation (read, post, sell, delete) sits behind its own interface, such as `IGetFurniture` or `IPostOrder`
- **Parameterized SQL**: every query uses MySQL command parameters to protect against SQL injection
- **Shared row mappers**: one place turns database rows into model objects, so different queries can't map columns differently
- **Automated tests**: xUnit tests for the data mapping and configuration, with no live database needed
- **CI pipeline**: GitHub Actions builds and tests every push and pull request on Windows, macOS and Linux

## Tech stack

| Area | Tools |
|---|---|
| Backend | C#, ASP.NET Core Web API (.NET 7) |
| Database | MySQL (via `MySql.Data`) |
| API docs | Swagger / OpenAPI (in development mode) |
| Front end | HTML, CSS, JavaScript (Fetch API) |
| Testing | xUnit |
| CI | GitHub Actions |

## API endpoints

| Resource | Endpoints |
|---|---|
| Accounts | `GET /api/account`, `GET /api/account/{id}`, `POST /api/account` |
| Furniture | `GET /api/furniture`, `GET /api/furniture/{id}`, `POST /api/furniture`, `PUT /api/furniture/{id}` (mark sold), `DELETE /api/furniture/{id}` (also removes its orders) |
| Orders | `GET /api/order`, `GET /api/order/{id}`, `POST /api/order` |

When the API runs in development mode, interactive docs are available at `/swagger`.

## Project structure

```
├── API/                     # ASP.NET Core Web API
│   ├── Controllers/         # account, furniture and order endpoints
│   ├── Database/            # MySQL data access + RecordMappers
│   └── Models/              # Account, Furniture, OrderForm + interfaces
├── CLIENT/resources/        # HTML pages, CSS and JavaScript front end
├── tests/API.Tests/         # xUnit test project
└── .github/workflows/ci.yml # Build + test on Windows, macOS, Linux
```

## Running locally

**Prerequisites:** [.NET 7 SDK](https://dotnet.microsoft.com/download) or newer, and a MySQL database with `accounts`, `furniture` and `orderform` tables.

> The free hosted database originally used for this project no longer exists, so you'll need your own MySQL instance to run the full app.

1. Set the connection string as an environment variable (credentials are never stored in the code):

   ```bash
   # macOS / Linux
   export FURNITURE_DB_CONNECTION="server=localhost;port=3306;database=furniture;user=<user>;password=<password>"

   # Windows (PowerShell)
   $env:FURNITURE_DB_CONNECTION="server=localhost;port=3306;database=furniture;user=<user>;password=<password>"
   ```

2. Start the API:

   ```bash
   dotnet run --project API
   ```

3. Open `CLIENT/resources/index.html` in a browser. The front end expects the API at `http://localhost:5178`.

## Running the tests

```bash
dotnet test
```

The tests check that each database row is mapped to the right model fields, including a regression test for the city/quality mix-up described above. They also check that the connection string is read from the environment. Nothing in the suite touches a real database, so it runs anywhere, including in CI.

## Possible next steps

- Hash account passwords instead of storing them as plain text
- Return proper HTTP status codes (404, 400) instead of `null` when a lookup fails
- Add update and delete endpoints for accounts and orders
- Inject the data-access classes through dependency injection so the controllers can be unit tested
- Upgrade to .NET 8 (the current long-term support release)
