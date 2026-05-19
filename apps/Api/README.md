# Api

ASP.NET Core Web API backend for authentication, user management, and secure REST endpoints.

## TOC

## Overview

`Api` is an ASP.NET Core Web API project for Lucky Day. It provides authentication and user management endpoints, JWT validation, CORS configuration, and session cookie issuance. It includes OpenAPI/Swagger support in development.

## Structure

- [`Api`](.): ASP.NET Core Web API project root.
  - [`Program.cs`](Program.cs): Application startup and service registration.
    - [`HttpPipelineConfigurationExtensions.cs`](Configuration/HttpPipelineConfigurationExtensions.cs): HTTP pipeline middleware.
  - [`Controllers`](Controllers): HTTP endpoint controllers.
    - [`AuthController.cs`](Controllers/AuthController.cs): Sign-in and auth status endpoints.
    - [`UsersController.cs`](Controllers/UsersController.cs): User CRUD endpoints.
  - [`Auth`](Auth): Authentication helpers.
    - [`JwtTokenFactory.cs`](Auth/JwtTokenFactory.cs): JWT generation.
    - [`AuthRoleResolver.cs`](Auth/AuthRoleResolver.cs): Role resolution helper.
  - [`Services`](Services): Business logic used by controllers.
  - [`Models`](Models): Request/response and domain-facing API models.

## APIs

- [`POST /auth/signin`](Controllers/AuthController.cs): Validates credentials, issues an access token, and sets an HttpOnly session cookie. Example:

```bash
curl -X POST http://localhost:5066/auth/signin \
  -H "Content-Type: application/json" \
  -d '{"email":"admin@fu.com","password":"password"}'
```

- [`GET /auth/is-authenticated`](Controllers/AuthController.cs): Verifies a bearer token and returns auth status/claims. Example:

```bash
curl http://localhost:5066/auth/is-authenticated \
  -H "Authorization: Bearer <access_token>"
```

- [`GET /users`](Controllers/UsersController.cs): Returns all users.
- [`GET /users/{id}`](Controllers/UsersController.cs): Returns one user by ID.
- [`POST /users`](Controllers/UsersController.cs): Creates a user.
- [`PUT /users/{id}`](Controllers/UsersController.cs): Updates a user.
- [`DELETE /users/{id}`](Controllers/UsersController.cs): Deletes a user.

## References

- [Workspace](../../README.md)
- [Db](../../packages/dotnet/Db/README.md)
