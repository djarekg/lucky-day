import { NextResponse } from 'next/server';

import { API_BASE_URL } from '@/lib/config';
import { SigninFormSchema } from '@/lib/models/auth';

/**
 * Handles sign-in by forwarding credentials to the API.
 * The API validates credentials and issues a session cookie via Set-Cookie header,
 * which the browser automatically stores.
 */
export async function POST(request: Request) {
  const body = await request.json();

  const validatedFields = SigninFormSchema.safeParse(body);
  if (!validatedFields.success) {
    return NextResponse.json(
      {
        message: 'Email and password are required.',
      },
      {
        status: 400,
      },
    );
  }

  const signinResponse = await fetch(`${API_BASE_URL}/auth/signin`, {
    method: 'POST',
    headers: {
      'Content-Type': 'application/json',
      Accept: 'application/json',
    },
    body: JSON.stringify(validatedFields.data),
    cache: 'no-store',
  });

  if (!signinResponse.ok) {
    return NextResponse.json(
      {
        message: 'Invalid credentials.',
      },
      {
        status: signinResponse.status,
      },
    );
  }

  // The API has validated credentials and set the session cookie via Set-Cookie header.
  // The browser will automatically store and send it on subsequent requests.
  return NextResponse.json({
    success: true,
  });
}
