import type { DashboardWidgetCategory, DashboardWidgetType } from './enums';

export type DashboardWidgetModel = {
  id: string;
  name: string;
  category: DashboardWidgetCategory;
  type: DashboardWidgetType;
};

export type DashboardWidgetResponseModel = Pick<
  DashboardWidgetModel,
  'id' | 'name' | 'category' | 'type'
>;
