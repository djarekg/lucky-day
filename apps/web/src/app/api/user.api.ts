import { cacheLife } from 'next/cache';

import { API_BASE_URL } from '@/lib/config';
import type { UserModel } from '@/lib/models';

export const getUsers = async (): Promise<UserModel[]> => {
  'use cache';
  cacheLife('minutes');

  const response = await fetch(`${API_BASE_URL}/users`);

  if (!response.ok) {
    throw new Error(`Failed to fetch users: ${response.status} ${response.statusText}`);
  }

  return (await response.json()) as UserModel[];
};
