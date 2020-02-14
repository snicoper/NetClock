import { LogicalOperator } from './logical-operator';
import { RelationalOperator } from './relational-operator';
import { RequestItemFilter } from './request-item-filter';

/** Request data actual como request y response. */
export class HttpTransferData<T> {
  hasPreviousPage: boolean;
  hasNextPage: boolean;
  totalItems: number;
  pageNumber: number;
  totalPages: number;
  pageSize: number;
  ratio: number;
  items: T[];
  sorts: string;
  filters: RequestItemFilter[];

  constructor() {
    // @see: webapi/src/Application/Models/Http/transferData.cs default values.
    this.totalItems = 0;
    this.pageNumber = 1;
    this.totalPages = 1;
    this.pageSize = 10;
    this.ratio = 3;
    this.items = [];
    this.sorts = '';
    this.filters = [];
  }

  addFilter(propertyName: string, operator: RelationalOperator, value: string, concat = LogicalOperator.none): HttpTransferData<T> {
    const filter = new RequestItemFilter(propertyName, operator, value, concat);
    this.filters.push(filter);

    return this;
  }

  removeFilter(filter: RequestItemFilter): HttpTransferData<T> {
    const item = this.filters.indexOf(filter);
    this.filters.splice(item, 1);

    return this;
  }

  cleanFilters(): void {
    this.filters = [];
  }

  stringifyFilters(): string {
    return JSON.stringify(this.filters);
  }

  parseFilters(filters): void {
    this.filters = JSON.parse(filters);
  }
}
