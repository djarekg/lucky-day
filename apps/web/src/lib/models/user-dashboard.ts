import type { DashboardWidgetResponseModel } from './dashboard-widget';

export type UserDashboardModel = {
  id: string;
  userId: string;
  dashboardWidgetId: string;
  position: number;
  widget: DashboardWidgetResponseModel;
};

export type UserDashboardResponseModel = Pick<
  UserDashboardModel,
  'id' | 'userId' | 'dashboardWidgetId' | 'position' | 'widget'
>;

export type UserDashboardCreateModel = Pick<
  UserDashboardModel,
  'userId' | 'dashboardWidgetId' | 'position'
>;

export type UserDashboardUpdateModel = Pick<UserDashboardModel, 'id' | 'position'>;
