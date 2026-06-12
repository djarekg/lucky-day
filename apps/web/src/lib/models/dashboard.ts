export type DashboardModel = {
  userId: string;
  firstName: string;
  lastName: string;
  totalSales: number;
};

export type UserSalesTotalResponseModel = Pick<
  DashboardModel,
  'userId' | 'firstName' | 'lastName' | 'totalSales'
>;
