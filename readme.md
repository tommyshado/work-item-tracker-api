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

## Authentication (JWT)

The API uses JSON Web Tokens (JWT) for authentication. All endpoints except `/api/auth/login` require a valid Bearer token in the `Authorization` header.

### Configuration

A `JWT_SECRET_KEY` must be set. In development, it's provided via `appsettings.Development.json`. For production, set it as an environment variable.

### Login

**POST** `/api/auth/login`

Request body:

```json
{
  "username": "your-username",
  "password": "your-password"
}
```

Response body:

```json
{
  "token": "<jwt-token>",
  "username": "your-username",
  "expiresIn": 3600
}
```

### Using the token

Include the token in subsequent requests:

```
Authorization: Bearer <jwt-token>
```

Requests without a valid token receive a `401 Unauthorized` response.

### How it works

- `AuthService` generates and validates JWT tokens (HMAC-SHA256, 1-hour expiry).
- `TokenAuthMiddleware` intercepts all requests (except `/api/auth/login`) and validates the Bearer token.
- `AuthGuard` wraps validation logic for use in services.

## Set Environment to Development

Source the helper script to set the `ASPNETCORE_ENVIRONMENT` variable before running the API:

```bash
source ./set-env-to-dev.sh
```

This sets `ASPNETCORE_ENVIRONMENT=Development`, which enables:

- Developer exception pages
- Development-specific configuration from `appsettings.Development.json`
- Swagger UI at `/swagger`
