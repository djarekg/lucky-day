import { object, string } from 'zod';

/**
 * Defines the schema for the sign-in form, including validation rules for
 * email and password fields.
 */
export const SigninFormSchema = object({
  email: string({ error: 'Email is required' }).min(1, 'Email is required').email('Invalid email'),
  password: string()
    .min(4, 'Password must be more than 4 characters')
    .max(32, 'Password must be less than 32 characters')
    .trim(),
});

/**
 * Defines the type for the sign-in form state, which can include validation errors
 * for email and password fields, as well as a general message.
 */
export type SigninFormState =
  | {
      errors?: {
        email?: string[];
        password?: string[];
      };
      message?: string;
    }
  | undefined;

export type SigninRequest = {
  email: string;
  password: string;
};

export type SigninResponse = {
  success: boolean;
};
