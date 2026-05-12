import { NextRequest, NextResponse } from 'next/server';

import { authKeys } from '@/lib/data/keys';
import { protectedRoutes } from '@/lib/routes';
import { decrypt } from '@/lib/session';

/** Protects guarded routes by redirecting unauthenticated requests to sign-in. */
export default async function proxy(req: NextRequest) {
  // Check if the current route is protected or public.
  const path = req.nextUrl.pathname;
  const isProtectedRoute = protectedRoutes.includes(path);

  // Decrypt the session from the cookie in the request headers.
  const cookie = req.cookies.get('session')?.value;
  const session = await decrypt(cookie);

  // Redirect to /auth/signin if the user is not authenticated.
  if (isProtectedRoute && !session?.userId) {
    return NextResponse.redirect(new URL(authKeys.signin(), req.nextUrl));
  }

  return NextResponse.next();
}

/** Defines route patterns that should bypass the proxy middleware. */
export const config = {
  matcher: ['/((?!api|_next/static|_next/image|.*\\.png$).*)'],
};
