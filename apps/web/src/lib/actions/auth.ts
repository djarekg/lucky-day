'use server';

import { redirect } from 'next/navigation';

import { deleteSession } from '@/lib/session';

/**
 * Handles the sign-out process by deleting the user's session and redirecting
 * them to the sign-in page.
 */
export const signout = async () => {
  await deleteSession();
  redirect('/auth/signin');
};
