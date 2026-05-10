import { SigninFormSchema, type SigninFormState } from '@/lib/models/auth';

export async function signin(_state: SigninFormState, formData: FormData) {
  const validatedFields = SigninFormSchema.safeParse({
    email: formData.get('email'),
    password: formData.get('password'),
  });

  if (!validatedFields.success) {
    return {
      errors: validatedFields.error.flatten().fieldErrors,
    };
  }
}
