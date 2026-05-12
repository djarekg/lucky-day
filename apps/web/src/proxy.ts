import { NextRequest, NextResponse } from 'next/server';

import { protectedRoutes } from '@/lib/routes';
import { decrypt } from '@/lib/session';

export default async function proxy(req: NextRequest) {
  // Check if the current route is protected or public.
  const path = req.nextUrl.pathname;
  const isProtectedRoute = protectedRoutes.includes(path);

  // Decrypt the session from the cookie in the request headers.
  const cookie = req.cookies.get('session')?.value;
  const session = await decrypt(cookie);

  // Redirect to /auth/signin if the user is not authenticated.
  if (isProtectedRoute && !session?.userId) {
    return NextResponse.redirect(new URL('/auth/signin', req.nextUrl));
  }

  return NextResponse.next();
}

// Routes Proxy should not run on
export const config = {
  matcher: ['/((?!api|_next/static|_next/image|.*\\.png$).*)'],
};
