/**
 * Formats a phone number string into the format (XXX) XXX-XXXX.
 * If the input does not match the expected format, it returns the original string.
 *
 * @param phone - The phone number string to format.
 * @returns The formatted phone number or the original string if formatting fails.
 */
export const formatPhoneNumber = (phone: string) => {
  const cleaned = phone.replace(/\D/g, '');
  const match = cleaned.match(/^(\d{3})(\d{3})(\d{4})$/);

  if (match) {
    return `(${match[1]}) ${match[2]}-${match[3]}`;
  }

  return phone;
};
