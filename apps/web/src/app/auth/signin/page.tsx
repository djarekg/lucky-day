'use client';

import Button from '@mui/material/Button';
import { useActionState } from 'react';

import { signin } from '@/lib/actions/auth';

const Signin = () => {
  const [state, action, pending] = useActionState(signin, undefined);

  return (
    <form action={action}>
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

      <Button
        disabled={pending}
        type="submit">
        Sign In
      </Button>
    </form>
  );
};

export default Signin;
