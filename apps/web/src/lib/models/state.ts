export type StateModel = {
  id: string;
  name: string;
  code: string;
};

export type StateCreateModel = Pick<StateModel, 'name' | 'code'>;

export type StateUpdateModel = Pick<StateModel, 'name' | 'code'>;

export type StateResponseModel = Pick<StateModel, 'id' | 'name' | 'code'>;
