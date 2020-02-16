import { HttpTransferDataItemFilter } from './http-transfer-data-item-filter';
import { HttpTransferDataItemOrderBy } from './http-transfer-data-item-order-by';
import { LogicalOperator } from './logical-operator';
import { OrderType } from './order-type';
import { RelationalOperator } from './relational-operator';

export class HttpTransferData<T> {
  hasPreviousPage: boolean;
  hasNextPage: boolean;
  totalItems = 0;
  pageNumber = 1;
  totalPages = 1;
  pageSize = 10;
  ratio = 2;
  items: T[] = [];
  orders: HttpTransferDataItemOrderBy[] = [];
  filters: HttpTransferDataItemFilter[] = [];

  addFilter(propertyName: string, operator: RelationalOperator, value: string, concat = LogicalOperator.none): HttpTransferData<T> {
    const filter = new HttpTransferDataItemFilter(propertyName, operator, value, concat);
    this.filters.push(filter);

    return this;
  }

  removeFilter(filter: HttpTransferDataItemFilter): HttpTransferData<T> {
    const index = this.orders.findIndex((item) => item.propertyName === filter.propertyName);
    this.filters.splice(index, 1);

    return this;
  }

  cleanFilters(): void {
    this.filters = [];
  }

  addOrder(propertyName: string, orderType: OrderType, precedence: number): HttpTransferData<T> {
    if (orderType !== OrderType.none) {
      const order = new HttpTransferDataItemOrderBy(propertyName, orderType, precedence);
      this.orders.unshift(order);
    }

    return this;
  }

  removeOrder(filter: HttpTransferDataItemOrderBy): HttpTransferData<T> {
    const index = this.orders.findIndex((item) => item.propertyName === filter.propertyName);
    this.orders.splice(index, 1);

    return this;
  }

  cleanOrders(): void {
    this.orders = [];
  }
}
