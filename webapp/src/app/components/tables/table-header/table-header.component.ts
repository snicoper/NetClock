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
  @Input() transferData: ApiResult<T>;

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
    const item = this.getHttpTransferDataItemByHeader(header);

    return item ? item.precedence : undefined;
  }

  private updateOrderItem(header: ITableHeaderField): void {
    this.transferData = Object.assign(new ApiResult<T>(), this.transferData);
    this.transferData.addOrder(header.field, header.orderType, 1);
  }

  private removeOrderItemIfExists(header: ITableHeaderField): void {
    this.transferData = Object.assign(new ApiResult<T>(), this.transferData);
    const item = this.getHttpTransferDataItemByHeader(header);
    if (item) {
      this.transferData.removeOrder(item);
    }
  }

  private updateOrderPrecedence(): void {
    for (let i = 0; i < this.transferData.orders.length; i += 1) {
      this.transferData.orders[i].precedence = i + 1;
    }
  }

  private getHttpTransferDataItemByHeader(header: ITableHeaderField): ApiResultItemOrderBy {
    this.transferData = Object.assign(new ApiResult<T>(), this.transferData);
    return this.transferData.orders.find((field) => field.propertyName === header.field);
  }
}
