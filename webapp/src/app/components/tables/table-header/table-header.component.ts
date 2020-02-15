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
  @Input() headerConfig: TableHeaderConfig;
  @Input() transferData: HttpTransferData<T>;

  @Output() clickOrdering = new EventEmitter<void>();

  orderings = OrderType;

  onClickOrder(header: TableHeader): void {
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

  getOrderPrecedence(header: TableHeader): number {
    const item = this.getHttpTransferDataItemByHeader(header);

    return item ? item.precedence : undefined;
  }

  private updateOrderItem(header: TableHeader): void {
    this.transferData = Object.assign(new HttpTransferData<T>(), this.transferData);
    this.transferData.addOrder(header.field, header.orderType, 1);
  }

  private removeOrderItemIfExists(header: TableHeader): void {
    this.transferData = Object.assign(new HttpTransferData<T>(), this.transferData);
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

  private getHttpTransferDataItemByHeader(header: TableHeader): HttpTransferDataItemOrderBy {
    return this.transferData.orders.find((field) => field.propertyName === header.field);
  }
}
