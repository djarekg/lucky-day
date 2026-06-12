export type CustomerContactModel = {
  id: string;
  customerId: string;
  firstName: string;
  lastName: string;
  email: string;
  streetAddress: string;
  streetAddress2: string | null;
  city: string;
  stateId: string;
  zip: string;
  phone: string;
  imageId: number;
  isActive: boolean;
  dateCreated: string;
  dateUpdated: string | null;
};

export type CustomerContactCreateModel = Pick<
  CustomerContactModel,
  | 'customerId'
  | 'firstName'
  | 'lastName'
  | 'email'
  | 'streetAddress'
  | 'streetAddress2'
  | 'city'
  | 'stateId'
  | 'zip'
  | 'phone'
  | 'imageId'
  | 'isActive'
>;

export type CustomerContactUpdateModel = Pick<
  CustomerContactModel,
  | 'customerId'
  | 'firstName'
  | 'lastName'
  | 'email'
  | 'streetAddress'
  | 'streetAddress2'
  | 'city'
  | 'stateId'
  | 'zip'
  | 'phone'
  | 'imageId'
  | 'isActive'
>;

export type CustomerContactResponseModel = Pick<
  CustomerContactModel,
  | 'id'
  | 'customerId'
  | 'firstName'
  | 'lastName'
  | 'email'
  | 'streetAddress'
  | 'streetAddress2'
  | 'city'
  | 'stateId'
  | 'zip'
  | 'phone'
  | 'imageId'
  | 'isActive'
  | 'dateCreated'
  | 'dateUpdated'
>;
