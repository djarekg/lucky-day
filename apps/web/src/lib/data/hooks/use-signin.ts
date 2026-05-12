'use client';

import useSWRMutation from 'swr/mutation';

import { jsonFetcher } from '@/lib/data/fetcher';
import { authKeys } from '@/lib/data/keys';
import type { SigninRequest, SigninResponse } from '@/lib/models/auth';

const signinFetcher = async (url: string, { arg }: { arg: SigninRequest }) =>
  jsonFetcher<SigninResponse>(url, {
    method: 'POST',
    body: JSON.stringify(arg),
    credentials: 'include',
  });

export const useSignin = () =>
  useSWRMutation(authKeys.signin(), signinFetcher, {
    throwOnError: true,
  });
