'use client';

import PasswordRounded from '@mui/icons-material/PasswordRounded';
import PersonOutlineRounded from '@mui/icons-material/PersonOutlineRounded';
import Button from '@mui/material/Button';
import InputAdornment from '@mui/material/InputAdornment';
import TextField from '@mui/material/TextField';
import { useRouter } from 'next/navigation';
import { useState } from 'react';

import { HttpError } from '@/lib/data/fetcher';
import { useSignin } from '@/lib/data/hooks/use-signin';
import { signinFormSchema, type SigninFormState } from '@/lib/models/auth';

import styles from './page.module.css';

const Signin = () => {
  const router = useRouter();
  const { trigger, isMutating } = useSignin();
  const [state, setState] = useState<SigninFormState>(undefined);

  const onSubmit = async (formData: FormData) => {
    const validatedFields = signinFormSchema.safeParse({
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
    <div className={styles.container}>
      <form
        className={styles.form}
        action={onSubmit}>
        <header className={styles.header}>
          <h2>Welcome to Lucky Day</h2>
          <h4>Sign in to access your account</h4>
        </header>
        <TextField
          variant="standard"
          label="Email"
          name="email"
          type="email"
          slotProps={{
            input: {
              endAdornment: (
                <InputAdornment position="end">
                  <PersonOutlineRounded />
                </InputAdornment>
              ),
            },
          }}
        />
        {state?.errors?.email && <p>{state.errors.email}</p>}

        <TextField
          variant="standard"
          label="Password"
          name="password"
          type="password"
          required
          slotProps={{
            input: {
              endAdornment: (
                <InputAdornment position="end">
                  <PasswordRounded />
                </InputAdornment>
              ),
            },
          }}
        />
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
    </div>
  );
};

/** Renders the sign-in page form and handles submission state. */
export default Signin;
