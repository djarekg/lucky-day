export type DashboardWidgetCategory = 'ACCOUNTING' | 'INVENTORY' | 'SALES' | 'USERACTIVITY';

export type DashboardWidgetType = 'CHART' | 'TOTAL' | 'TOTALLIST';

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
