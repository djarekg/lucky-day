# @lucky-day/web

Next.js frontend application for Lucky Day, handling authentication flow, sessions, and protected routes.

## TOC

## Overview

`@lucky-day/web` is a Next.js application that provides the browser UI for Lucky Day. It handles sign-in flow, session cookies, route protection, and communication with the API.

## Installation

From the workspace root:

```bash
bun run dev
```

## Structure

- [`src`](src): Application source root.
  - [`app`](src/app): App Router pages and route handlers.
    - [`api`](src/app/api): Server route handlers exposed by the web app.
      - [`auth`](src/app/api/auth): Auth-related routes.
        - [`signin/route.ts`](src/app/api/auth/signin/route.ts): Sign-in API endpoint.
    - [`auth/signin/page.tsx`](src/app/auth/signin/page.tsx): Sign-in page.
    - [`layout.tsx`](src/app/layout.tsx): Root app layout.
    - [`page.tsx`](src/app/page.tsx): Home page.
  - [`lib`](src/lib): Shared app logic.
    - [`actions`](src/lib/actions): Server actions.
      - [`auth.ts`](src/lib/actions/auth.ts): Sign-out action.
    - [`data`](src/lib/data): Data fetchers, keys, and hooks.
      - [`fetcher.ts`](src/lib/data/fetcher.ts): JSON fetch helper.
      - [`keys.ts`](src/lib/data/keys.ts): SWR key builders.
      - [`hooks/use-signin.ts`](src/lib/data/hooks/use-signin.ts): Sign-in mutation hook.
    - [`models`](src/lib/models): Shared DTO and validation types.
      - [`auth.ts`](src/lib/models/auth.ts): Auth request/response and schema.
      - [`session.ts`](src/lib/models/session.ts): Session payload type.
    - [`session.ts`](src/lib/session.ts): Session cookie creation, refresh, and verification.
    - [`config.ts`](src/lib/config.ts): Runtime config values.
    - [`routes.ts`](src/lib/routes.ts): Route access metadata.
  - [`styles`](src/styles): Theme and styling primitives.

## APIs

- [`POST /api/auth/signin`](src/app/api/auth/signin/route.ts): Validates credentials, calls the backend auth endpoint, verifies authentication, and creates the session cookie. Example:

```bash
curl -X POST http://localhost:3000/api/auth/signin \
	-H "Content-Type: application/json" \
	-d '{"email":"admin@fu.com","password":"password"}'
```

- [`useSignin`](src/lib/data/hooks/use-signin.ts): SWR mutation hook that performs sign-in requests. Example:

```ts
const { trigger, isMutating, error } = useSignin();
await trigger({ email: 'admin@fu.com', password: 'password' });
```

- [`jsonFetcher`](src/lib/data/fetcher.ts): Generic JSON fetch helper that throws `HttpError` for non-2xx responses.
- [`signout`](src/lib/actions/auth.ts): Server action that clears the session and redirects to `/auth/signin`.

## References

- [Workspace](../../README.md)
- [@lucky-day/core](../../packages/ui/core/README.md)
