import 'server-only';

import { jwtVerify, SignJWT } from 'jose';
import { cookies } from 'next/headers';

import type { SessionPayload } from '@/lib/models/session';

const secretKey = process.env.PRIVATE_KEY as string;
const encodedKey = new TextEncoder().encode(secretKey);

/**
 * Encrypts a session payload into an HS256 JWT token.
 *
 * @param payload The session payload to be encrypted.
 * @returns A promise that resolves to the encrypted JWT token.
 */
export async function encrypt(payload: SessionPayload) {
  return new SignJWT(payload)
    .setProtectedHeader({ alg: 'HS256' })
    .setIssuedAt()
    .setExpirationTime('7d')
    .sign(encodedKey);
}

/**
 * Decrypts and verifies an HS256 JWT token, returning its payload when valid.
 *
 * @param session The JWT token to be decrypted and verified.
 * @returns A promise that resolves to the decoded session payload if the token is valid,
 * or undefined if invalid.
 */
export async function decrypt(session: string | undefined = '') {
  try {
    const { payload } = await jwtVerify(session, encodedKey, {
      algorithms: ['HS256'],
    });
    return payload;
  } catch (error) {
    console.error('Failed to verify session', error);
  }
}

/**
 * Creates a new user session and stores it in a cookie.
 *
 * @param userId The ID of the user for whom the session is being created.
 */
export const createSession = async (userId: string) => {
  const expiresAt = Math.floor(Date.now() / 1000) + 7 * 24 * 60 * 60; // Expires in 7 days
  const session = await encrypt({ userId, expiresAt });
  const cookieStore = await cookies();

  cookieStore.set('session', session, {
    httpOnly: true,
    secure: process.env.NODE_ENV === 'production',
    sameSite: 'lax',
    path: '/',
  });
};

/**
 * Refreshes the existing session cookie and returns the session payload when valid.
 */
export const updateSession = async () => {
  const session = (await cookies()).get('session')?.value;
  const payload = await decrypt(session);

  if (!session || !payload) {
    return null;
  }

  const expires = new Date(Date.now() + 7 * 24 * 60 * 60 * 1000);
  const cookieStore = await cookies();

  cookieStore.set('session', session, {
    httpOnly: true,
    secure: true,
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
  cookieStore.delete('session');
};
