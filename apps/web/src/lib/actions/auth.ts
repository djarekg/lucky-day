'use server';

import { redirect } from 'next/navigation';

import { deleteSession } from '@/lib/session';

/** Signs out the current user by clearing the session and redirecting to the sign-in page. */
export const signout = async () => {
  await deleteSession();
  redirect('/auth/signin');
};
