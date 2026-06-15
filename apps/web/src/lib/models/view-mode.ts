export const ViewMode = {
  Card: 0,
  Table: 1,
} as const;

export type ViewMode = (typeof ViewMode)[keyof typeof ViewMode];
