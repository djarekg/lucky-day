export const SEARCH_RESULT_TYPE = {
  user: 1,
  customer: 2,
  customerContact: 3,
  product: 4,
} as const;

export type SearchResultType = (typeof SEARCH_RESULT_TYPE)[keyof typeof SEARCH_RESULT_TYPE];

export type SearchModel = {
  type: SearchResultType;
  rank: number;
  json: string;
};

export type SearchResultParamsModel = {
  query: string;
  highlightStartTag: string;
  highlightEndTag: string;
};

export type SearchResultModel = Pick<SearchModel, 'type' | 'rank' | 'json'>;
