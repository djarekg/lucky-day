'use client';

import useSWRMutation from 'swr/mutation';

import { API_BASE_URL } from '@/lib/config';
import { jsonFetcher } from '@/lib/data/fetcher';
import { authKeys } from '@/lib/data/keys';
import type { SigninRequest, SigninResponse } from '@/lib/models/auth';

const signinFetcher = async (url: string, { arg }: { arg: SigninRequest }) =>
  jsonFetcher<SigninResponse>(url, {
    method: 'POST',
    body: JSON.stringify(arg),
    credentials: 'include',
  });

/** Returns the SWR mutation hook used to call the sign-in endpoint. */
export const useSignin = () =>
  useSWRMutation(`${API_BASE_URL}${authKeys.signin()}`, signinFetcher, {
    throwOnError: true,
  });
