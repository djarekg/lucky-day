import 'server-only';

import { cookies } from 'next/headers';

import { API_BASE_URL } from '@/lib/config';
import type { SessionPayload } from '@/lib/models/session';

const SESSION_COOKIE_NAME = 'session';

/**
 * Session management for the web app.
 *
 * Note: The API (.NET backend) is the sole owner of session cookie issuance and lifetime.
 * This module provides helpers to read and clear the API-issued session cookie on the client side.
 * - decrypt: Verifies the access token via the API and reconstructs session data
 * - deleteSession: Clears the session cookie during sign-out (should be paired with an API logout)
 */

/** Verifies an access token against the API and returns a session payload when valid. */
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
 * Clears the session cookie during sign-out.
 * Note: The API is now the authority for session cookie issuance and lifetime.
 * This helper is used only for client-side sign-out cleanup.
 */
export const deleteSession = async () => {
  const cookieStore = await cookies();
  cookieStore.delete(SESSION_COOKIE_NAME);
};
