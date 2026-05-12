'use client';

import Button from '@mui/material/Button';
import { useRouter } from 'next/navigation';
import { useState } from 'react';

import { HttpError } from '@/lib/data/fetcher';
import { useSignin } from '@/lib/data/hooks/use-signin';
import { SigninFormSchema, type SigninFormState } from '@/lib/models/auth';

const Signin = () => {
  const router = useRouter();
  const { trigger, isMutating } = useSignin();
  const [state, setState] = useState<SigninFormState>(undefined);

  const onSubmit = async (formData: FormData) => {
    const validatedFields = SigninFormSchema.safeParse({
      email: formData.get('email'),
      password: formData.get('password'),
    });

    if (!validatedFields.success) {
      setState({
        errors: validatedFields.error.flatten().fieldErrors,
      });
      return;
    }

    setState(undefined);

    try {
      await trigger(validatedFields.data);
      router.push('/');
      router.refresh();
    } catch (error) {
      const message = error instanceof HttpError ? error.message : 'Sign-in failed.';
      setState({
        message,
      });
    }
  };

  return (
    <form action={onSubmit}>
      <div>
        <label htmlFor="email">Email</label>
        <input
          id="email"
          name="email"
          type="email"
          placeholder="Email"
        />
      </div>
      {state?.errors?.email && <p>{state.errors.email}</p>}

      <div>
        <label htmlFor="password">Password</label>
        <input
          id="password"
          name="password"
          type="password"
        />
      </div>
      {state?.errors?.password && (
        <div>
          <p>Password must:</p>
          <ul>
            {state.errors.password.map(error => (
              <li key={error}>- {error}</li>
            ))}
          </ul>
        </div>
      )}
      {state?.message && <p>{state.message}</p>}

      <Button
        disabled={isMutating}
        type="submit">
        Sign In
      </Button>
    </form>
  );
};

export default Signin;
