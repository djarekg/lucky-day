export type ProductSaleModel = {
  id: string;
  productId: string;
  customerId: string;
  userId: string;
  quantity: number;
  price: number;
  dateCreated: string;
  dateUpdated: string | null;
};

export type ProductSaleCreateModel = Pick<
  ProductSaleModel,
  'productId' | 'customerId' | 'userId' | 'quantity' | 'price'
>;

export type ProductSaleUpdateModel = Pick<
  ProductSaleModel,
  'productId' | 'customerId' | 'userId' | 'quantity' | 'price'
>;

export type ProductSaleResponseModel = Pick<
  ProductSaleModel,
  | 'id'
  | 'productId'
  | 'customerId'
  | 'userId'
  | 'quantity'
  | 'price'
  | 'dateCreated'
  | 'dateUpdated'
>;
