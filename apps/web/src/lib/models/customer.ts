export type CustomerModel = {
  id: string;
  name: string;
  streetAddress: string;
  streetAddress2: string | null;
  city: string;
  stateId: string;
  zip: string;
  phone: string;
  isActive: boolean;
  dateCreated: string;
  dateUpdated: string | null;
};

export type CustomerCreateModel = Pick<
  CustomerModel,
  'name' | 'streetAddress' | 'streetAddress2' | 'city' | 'stateId' | 'zip' | 'phone' | 'isActive'
>;

export type CustomerUpdateModel = Pick<
  CustomerModel,
  'name' | 'streetAddress' | 'streetAddress2' | 'city' | 'stateId' | 'zip' | 'phone' | 'isActive'
>;

export type CustomerResponseModel = Pick<
  CustomerModel,
  | 'id'
  | 'name'
  | 'streetAddress'
  | 'streetAddress2'
  | 'city'
  | 'stateId'
  | 'zip'
  | 'phone'
  | 'isActive'
  | 'dateCreated'
  | 'dateUpdated'
>;
