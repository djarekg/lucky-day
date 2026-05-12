import { NextResponse } from 'next/server';

import { API_BASE_URL } from '@/lib/config';
import { SigninFormSchema } from '@/lib/models/auth';
import { createSession } from '@/lib/session';

type AuthStatusResult = {
  isAuthenticated: boolean;
  email: string | null;
  role: string | null;
};

const parseToken = async (response: Response) => {
  const raw = await response.text();
  if (!raw) {
    return null;
  }

  try {
    const parsed = JSON.parse(raw);
    if (typeof parsed === 'string') {
      return parsed;
    }

    if (typeof parsed?.token === 'string') {
      return parsed.token as string;
    }
  } catch {
    // Return raw text when body is not JSON.
    return raw;
  }

  return null;
};

/**
 * Handles sign-in by validating credentials, creating a session, and
 * returning a success response.
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

  const accessToken = await parseToken(signinResponse);
  if (!accessToken) {
    return NextResponse.json(
      {
        message: 'Authentication token was not returned by the API.',
      },
      {
        status: 502,
      },
    );
  }

  const authResponse = await fetch(`${API_BASE_URL}/auth/is-authenticated`, {
    method: 'GET',
    headers: {
      Authorization: `Bearer ${accessToken}`,
      Accept: 'application/json',
    },
    cache: 'no-store',
  });

  if (!authResponse.ok) {
    return NextResponse.json(
      {
        message: 'Authentication verification failed.',
      },
      {
        status: authResponse.status,
      },
    );
  }

  const auth = (await authResponse.json()) as AuthStatusResult;

  await createSession({
    userId: auth.email ?? validatedFields.data.email,
    accessToken,
  });

  return NextResponse.json({
    success: true,
  });
}
