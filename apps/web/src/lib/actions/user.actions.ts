'use server';

import { getUsers } from '@/lib/api/user.api';
import type { UserModel } from '@/lib/models';

export const fetchUsers = async (): Promise<UserModel[]> => {
  return getUsers();
};
