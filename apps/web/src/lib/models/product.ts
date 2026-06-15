import type { Gender } from './gender';
import type { ProductType } from './product-type';

export type ProductModel = {
  id: string;
  name: string;
  description: string;
  price: string;
  gender: Gender;
  productType: ProductType;
  isActive: boolean;
  dateCreated: string;
  dateUpdated: string | null;
};

export type ProductCreateModel = Pick<
  ProductModel,
  'name' | 'description' | 'price' | 'gender' | 'productType' | 'isActive'
>;

export type ProductUpdateModel = Pick<
  ProductModel,
  'name' | 'description' | 'price' | 'gender' | 'productType' | 'isActive'
>;

export type ProductResponseModel = Pick<
  ProductModel,
  | 'id'
  | 'name'
  | 'description'
  | 'price'
  | 'gender'
  | 'productType'
  | 'isActive'
  | 'dateCreated'
  | 'dateUpdated'
>;
