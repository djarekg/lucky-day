/** Represents session data stored and validated for authenticated users. */
export type SessionPayload = {
  userId: string;
  accessToken: string;
  expiresAt: number;
};
