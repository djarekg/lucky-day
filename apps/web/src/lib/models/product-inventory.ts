import type { Size } from './enums';

export type ProductInventoryModel = {
  id: string;
  productId: string;
  size: Size;
  quantity: number;
  dateCreated: string;
  dateUpdated: string | null;
};

export type ProductInventoryCreateModel = Pick<
  ProductInventoryModel,
  'productId' | 'size' | 'quantity'
>;

export type ProductInventoryUpdateModel = Pick<
  ProductInventoryModel,
  'productId' | 'size' | 'quantity'
>;

export type ProductInventoryResponseModel = Pick<
  ProductInventoryModel,
  'id' | 'productId' | 'size' | 'quantity' | 'dateCreated' | 'dateUpdated'
>;
