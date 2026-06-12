import type { Color } from './enums';

export type ProductColorModel = {
  id: string;
  productId: string;
  color: Color;
  dateCreated: string;
  dateUpdated: string | null;
};

export type ProductColorCreateModel = Pick<ProductColorModel, 'productId' | 'color'>;

export type ProductColorUpdateModel = Pick<ProductColorModel, 'productId' | 'color'>;

export type ProductColorResponseModel = Pick<
  ProductColorModel,
  'id' | 'productId' | 'color' | 'dateCreated' | 'dateUpdated'
>;
