# Work Item Tracker API

[![.NET](https://github.com/tommyshado/work-item-tracker-api/actions/workflows/dotnet.yml/badge.svg)](https://github.com/tommyshado/work-item-tracker-api/actions/workflows/dotnet.yml)

## Project Description

Work Item Tracker API is a lightweight ASP.NET Core 8 REST API for creating and managing work items.
It uses Entity Framework Core with SQLite for persistence, follows a layered architecture (controllers, services, repositories), and includes Swagger/OpenAPI for interactive API exploration.
The project includes integration tests that verify core work-item behaviors such as creating, fetching, and updating data.

## Prerequisites

- .NET SDK 8.0+
- Git (optional, for cloning)

Check your installed .NET version:

```bash
dotnet --version
```

## Setup

1. Clone and enter the project directory:

```bash
git clone <your-repo-url>
cd work-item-tracker-api
```

2. Restore dependencies:

```bash
dotnet restore
```

3. Apply database migrations to create/update the local SQLite database:

```bash
dotnet ef database update
```

If `dotnet ef` is not available, install it globally first:

```bash
dotnet tool install --global dotnet-ef
```

The API uses the SQLite connection string in `appsettings.json`:

- `DefaultConnection=Data Source=workitems.db`

## Run the API

### Option 1: Use the helper script

```bash
./dev.sh
```

This runs:

```bash
dotnet watch run
```

### Option 2: Use dotnet directly

```bash
dotnet run
```

Default local URLs:

- API: `http://localhost:5173`
- Swagger UI: `http://localhost:5173/swagger`

## Test Setup

No external database setup is required for tests.
Integration tests use in-memory SQLite (`Data Source=:memory:`), create schema at startup, and clean up automatically.

## Run Tests

Use the helper script:

```bash
./run-tests.sh
```

This runs:

```bash
dotnet test test/WorkItemService.IntegrationTests/WorkItemService.IntegrationTests.csproj
```

## Useful Commands

Build:

```bash
dotnet build
```

Run with hot reload:

```bash
dotnet watch run
```

## Project Structure

- `src/` - API source code (controllers, services, repositories, models, EF Core DbContext)
- `Migrations/` - EF Core migrations
- `test/WorkItemService.IntegrationTests/` - integration tests
