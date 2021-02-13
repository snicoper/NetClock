import { ApiResultItemFilter } from './api-result-item-filter';
import { ApiResultItemOrderBy } from './api-result-item-order-by';
import { LogicalOperators } from './types/logical-operator';
import { OrderTypes } from './types/order-type';
import { RelationalOperators } from './types/relational-operator';

export class ApiResult<T> {
  hasPreviousPage: boolean;
  hasNextPage: boolean;
  totalItems = 0;
  pageNumber = 1;
  totalPages = 1;
  pageSize = 10;
  ratio = 2;
  items: T[] = [];
  orders: ApiResultItemOrderBy[] = [];
  filters: ApiResultItemFilter[] = [];

  constructor() {
    this.cleanFilters();
    this.cleanOrders();
  }

  addFilter(
    propertyName: string,
    operator: RelationalOperators,
    value: string,
    concat = LogicalOperators.none
  ): ApiResult<T> {
    const filter = new ApiResultItemFilter(propertyName, operator, value, concat);
    this.filters.push(filter);

    return this;
  }

  removeFilter(filter: ApiResultItemFilter): ApiResult<T> {
    const index = this.orders.findIndex((item) => item.propertyName === filter.propertyName);
    this.filters.splice(index, 1);

    return this;
  }

  cleanFilters(): void {
    this.filters = [];
  }

  addOrder(propertyName: string, orderType: OrderTypes, precedence: number): ApiResult<T> {
    if (orderType !== OrderTypes.none) {
      const order = new ApiResultItemOrderBy(propertyName, orderType, precedence);
      this.orders.unshift(order);
    }

    return this;
  }

  removeOrder(filter: ApiResultItemOrderBy): ApiResult<T> {
    const index = this.orders.findIndex((item) => item.propertyName === filter.propertyName);
    this.orders.splice(index, 1);

    return this;
  }

  cleanOrders(): void {
    this.orders = [];
  }
}
