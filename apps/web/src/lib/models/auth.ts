import { object, string } from 'zod';

/** Defines validation rules for the sign-in form fields. */
export const signinFormSchema = object({
  email: string({ error: 'Email is required' }).min(1, 'Email is required').email('Invalid email'),
  password: string()
    .min(4, 'Password must be more than 4 characters')
    .max(32, 'Password must be less than 32 characters')
    .trim(),
});

/** Describes validation errors and messages shown by the sign-in form. */
export type SigninFormState =
  | {
      errors?: {
        email?: string[];
        password?: string[];
      };
      message?: string;
    }
  | undefined;

/** Represents the payload sent to the sign-in endpoint. */
export type SigninRequest = {
  email: string;
  password: string;
};

/** Represents the response returned by the sign-in endpoint. */
export type SigninResponse = {
  success: boolean;
};
