'use server';

import { redirect } from 'next/navigation';
import { treeifyError } from 'zod';

import { SigninFormSchema, type SigninFormState } from '@/lib/models/auth';
import { createSession, deleteSession } from '@/lib/session';

/**
 * Handles the sign-in process by validating the form data against the defined schema.
 * If the validation fails, it returns an object containing the validation errors.
 * If the validation succeeds, it can proceed with further authentication logic
 * (not implemented here).
 *
 * @param _state The current state of the sign-in form, which can include validation
 * errors and messages.
 * @param formData The form data submitted by the user, containing the email
 * and password fields.
 * @returns An object containing validation errors if the input is invalid, or
 * undefined if the input is valid.
 */
export const signin = async (_state: SigninFormState, formData: FormData) => {
  const validatedFields = SigninFormSchema.safeParse({
    email: formData.get('email'),
    password: formData.get('password'),
  });

  if (!validatedFields.success) {
    return {
      errors: treeifyError(validatedFields.error),
    };
  }

  const user = { id: '123', email: validatedFields.data.email }; // Placeholder user object

  await createSession(user.id);
};

/**
 * Handles the sign-out process by deleting the user's session and redirecting
 * them to the sign-in page.
 */
export const signout = async () => {
  await deleteSession();
  redirect('/auth/signin');
};
