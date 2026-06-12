/** Represents session data stored and validated for authenticated users. */
export type SessionModel = {
  userId: string;
  accessToken: string;
  expiresAt: number;
};

export type SessionPayload = Pick<SessionModel, 'userId' | 'accessToken' | 'expiresAt'>;
