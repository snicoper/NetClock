import { Component, EventEmitter, Input, Output } from '@angular/core';

import { HttpTransferData, HttpTransferDataItemOrderBy, OrderType } from '../../../models';
import { TableHeaderConfig } from './table-header.config';
import { TableHeader } from './table-header.interface';

@Component({
  // tslint:disable-next-line:component-selector
  selector: '[ncTableHeader]',
  templateUrl: './table-header.component.html',
  styleUrls: ['./table-header.component.scss']
})
export class TableHeaderComponent<T> {
  @Input() headerConfig: TableHeaderConfig<T>;

  @Output() clickOrdering = new EventEmitter<HttpTransferData<T>>();

  orderings = OrderType;

  onClickOrder(header: TableHeader): void {
    this.removeOrderItemIfExists(header);

    switch (header.ordering) {
      case OrderType.none:
        header.ordering = OrderType.ascending;
        break;
      case OrderType.ascending:
        header.ordering = OrderType.descending;
        break;
      default:
        header.ordering = OrderType.none;
    }

    this.updateOrderItem(header);
    this.updateOrderPrecedence();
    this.clickOrdering.emit(this.headerConfig.transferData);
  }

  getOrderPrecedence(header: TableHeader): number {
    const item = this.getHttpTransferDataItemByHeader(header);

    return item ? item.precedence : undefined;
  }

  private updateOrderItem(header: TableHeader): void {
    this.headerConfig.transferData.addOrder(header.field, header.ordering, 1);
  }

  private removeOrderItemIfExists(header: TableHeader): void {
    const item = this.getHttpTransferDataItemByHeader(header);
    if (item) {
      this.headerConfig.transferData.removeOrder(item);
    }
  }

  private updateOrderPrecedence(): void {
    for (let i = 0; i < this.headerConfig.transferData.orders.length; i += 1) {
      this.headerConfig.transferData.orders[i].precedence = i + 1;
    }
  }

  private getHttpTransferDataItemByHeader(header: TableHeader): HttpTransferDataItemOrderBy {
    return this.headerConfig.transferData.orders.find((field) => field.propertyName === header.field);
  }
}
