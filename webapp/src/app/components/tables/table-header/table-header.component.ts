import { Component, EventEmitter, Input, Output } from '@angular/core';

import { ApiResult, ApiResultItemOrderBy, OrderType } from '../../../models';
import { ITableHeaderField, TableHeaderConfig } from './core';

@Component({
  // tslint:disable-next-line:component-selector
  selector: '[ncTableHeader]',
  templateUrl: './table-header.component.html',
  styleUrls: ['./table-header.component.scss']
})
export class TableHeaderComponent<T> {
  @Input() tableHeaderConfig: TableHeaderConfig;
  @Input() apiResult: ApiResult<T>;

  @Output() clickOrdering = new EventEmitter<void>();

  orderings = OrderType;

  onClickFilter(header: ITableHeaderField): void {
  }

  onClickOrder(header: ITableHeaderField): void {
    this.removeOrderItemIfExists(header);

    switch (header.orderType) {
      case OrderType.none:
        header.orderType = OrderType.ascending;
        break;
      case OrderType.ascending:
        header.orderType = OrderType.descending;
        break;
      default:
        header.orderType = OrderType.none;
    }

    this.updateOrderItem(header);
    this.updateOrderPrecedence();
    this.clickOrdering.emit();
  }

  getOrderPrecedence(header: ITableHeaderField): number {
    const item = this.getHttpApiResultItemByHeader(header);

    return item ? item.precedence : undefined;
  }

  private updateOrderItem(header: ITableHeaderField): void {
    this.apiResult = Object.assign(new ApiResult<T>(), this.apiResult);
    this.apiResult.addOrder(header.field, header.orderType, 1);
  }

  private removeOrderItemIfExists(header: ITableHeaderField): void {
    this.apiResult = Object.assign(new ApiResult<T>(), this.apiResult);
    const item = this.getHttpApiResultItemByHeader(header);
    if (item) {
      this.apiResult.removeOrder(item);
    }
  }

  private updateOrderPrecedence(): void {
    for (let i = 0; i < this.apiResult.orders.length; i += 1) {
      this.apiResult.orders[i].precedence = i + 1;
    }
  }

  private getHttpApiResultItemByHeader(header: ITableHeaderField): ApiResultItemOrderBy {
    this.apiResult = Object.assign(new ApiResult<T>(), this.apiResult);
    return this.apiResult.orders.find((field) => field.propertyName === header.field);
  }
}
