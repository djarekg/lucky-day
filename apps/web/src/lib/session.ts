import 'server-only';

import { cookies } from 'next/headers';

import { API_BASE_URL } from '@/lib/config';
import type { SessionPayload } from '@/lib/models/session';

const SESSION_COOKIE_NAME = 'session';

/**
 * Verifies the current access token by calling the API authentication endpoint.
 *
 * @returns A minimal session payload when token is valid, or undefined when invalid.
 */
export async function decrypt(session: string | undefined = '') {
  if (!session) {
    return undefined;
  }

  try {
    const response = await fetch(`${API_BASE_URL}/auth/is-authenticated`, {
      method: 'GET',
      headers: {
        Authorization: `Bearer ${session}`,
        Accept: 'application/json',
      },
      cache: 'no-store',
    });

    if (!response.ok) {
      return undefined;
    }

    const auth = (await response.json()) as {
      isAuthenticated: boolean;
      email: string | null;
    };

    if (!auth.isAuthenticated) {
      return undefined;
    }

    return {
      userId: auth.email ?? 'authenticated-user',
      accessToken: session,
      expiresAt: Math.floor(Date.now() / 1000) + 7 * 24 * 60 * 60,
    } as SessionPayload;
  } catch (error) {
    console.error('Failed to verify session', error);
  }
}

/**
 * Creates a new user session and stores it in a cookie.
 *
 * @param payload The session payload used to persist the API access token.
 */
export const createSession = async (payload: Pick<SessionPayload, 'userId' | 'accessToken'>) => {
  const expiresAt = Math.floor(Date.now() / 1000) + 7 * 24 * 60 * 60;
  const cookieStore = await cookies();

  cookieStore.set(SESSION_COOKIE_NAME, payload.accessToken, {
    httpOnly: true,
    secure: process.env.NODE_ENV === 'production',
    sameSite: 'lax',
    path: '/',
    expires: new Date(expiresAt * 1000), // Convert expiresAt to milliseconds for Date constructor
  });
};

/**
 * Refreshes the existing session cookie and returns the session payload when valid.
 */
export const updateSession = async () => {
  const session = (await cookies()).get(SESSION_COOKIE_NAME)?.value;
  const payload = await decrypt(session);

  if (!session || !payload) {
    return null;
  }

  const expires = new Date(Date.now() + 7 * 24 * 60 * 60 * 1000); // Convert expires to milliseconds for Date constructor
  const cookieStore = await cookies();

  cookieStore.set(SESSION_COOKIE_NAME, session, {
    httpOnly: true,
    secure: process.env.NODE_ENV === 'production',
    expires,
    sameSite: 'lax',
    path: '/',
  });
};

/**
 * Deletes the session cookie, effectively logging the user out.
 */
export const deleteSession = async () => {
  const cookieStore = await cookies();
  cookieStore.delete(SESSION_COOKIE_NAME);
};
