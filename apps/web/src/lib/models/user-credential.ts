export type Role = 'ADMIN' | 'USER' | 'SALES' | 'ACCOUNTING';

export type UserCredentialModel = {
  id: string;
  userId: string;
  password: string;
  role: Role;
};

export type UserCredentialCreateModel = Pick<UserCredentialModel, 'userId' | 'password' | 'role'>;

export type UserCredentialUpdateModel = Pick<UserCredentialModel, 'userId' | 'password' | 'role'>;

export type UserCredentialResponseModel = Pick<UserCredentialModel, 'id' | 'userId' | 'role'>;
