import type { Gender } from './gender';

export type UserModel = {
  id: string;
  firstName: string;
  lastName: string;
  gender: Gender;
  email: string;
  streetAddress: string;
  streetAddress2: string | null;
  city: string;
  stateId: string;
  zip: string;
  phone: string;
  jobTitle: string;
  imageId: number;
  isActive: boolean;
  dateCreated: string;
  dateUpdated: string | null;
};

export type UserCreateModel = Pick<
  UserModel,
  | 'firstName'
  | 'lastName'
  | 'gender'
  | 'email'
  | 'streetAddress'
  | 'streetAddress2'
  | 'city'
  | 'stateId'
  | 'zip'
  | 'phone'
  | 'jobTitle'
  | 'imageId'
  | 'isActive'
>;

export type UserUpdateModel = Pick<
  UserModel,
  | 'firstName'
  | 'lastName'
  | 'gender'
  | 'email'
  | 'streetAddress'
  | 'streetAddress2'
  | 'city'
  | 'stateId'
  | 'zip'
  | 'phone'
  | 'jobTitle'
  | 'imageId'
  | 'isActive'
>;

export type UserResponseModel = Pick<
  UserModel,
  | 'id'
  | 'firstName'
  | 'lastName'
  | 'gender'
  | 'email'
  | 'streetAddress'
  | 'streetAddress2'
  | 'city'
  | 'stateId'
  | 'zip'
  | 'phone'
  | 'jobTitle'
  | 'imageId'
  | 'isActive'
  | 'dateCreated'
  | 'dateUpdated'
>;
