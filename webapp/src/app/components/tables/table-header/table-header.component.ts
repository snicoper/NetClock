import { Component, EventEmitter, Input, Output } from '@angular/core';

import { RequestData } from '../../../models';
import { OrderPrecedence } from './order-precedence.interface';
import { TableHeaderConfig } from './table-header.config';
import { TableHeader } from './table-header.interface';
import { TableOrdering } from './table-ordering.enum';

@Component({
  selector: 'nc-table-header',
  templateUrl: './table-header.component.html',
  styleUrls: ['./table-header.component.scss']
})
export class TableHeaderComponent<T> {
  @Input() headerConfig: TableHeaderConfig<T>;

  @Output() clickOrdering = new EventEmitter<RequestData<T>>();

  orderings = TableOrdering;
  orderPrecedence: OrderPrecedence[] = [];

  onClickSort(header: TableHeader): void {
    this.removeSortItemIfExists(header);

    switch (header.ordering) {
      case TableOrdering.none:
        header.ordering = TableOrdering.ascending;
        break;
      case TableOrdering.ascending:
        header.ordering = TableOrdering.descending;
        break;
      default:
        header.ordering = TableOrdering.none;
    }

    this.sortItem(header);
    this.updateOrderPrecedence();
    this.clickOrdering.emit(this.headerConfig.requestData);
  }

  getOrderPrecedence(field: TableHeader): number {
    const precedence = this.orderPrecedence.find((f) => f.fieldName === field.field);

    return precedence.position;
  }

  private removeSortItemIfExists(header: TableHeader): void {
    const sortString = this.getSortString(header);
    this.headerConfig.requestData.sorts = this.headerConfig.requestData.sorts.replace(sortString, '');
  }

  private sortItem(header: TableHeader): void {
    this.headerConfig.requestData.sorts = this.getSortString(header) + ',' + this.headerConfig.requestData.sorts;
    this.headerConfig.requestData.sorts = this.headerConfig.requestData.sorts.replace(',,', ',');
  }

  private getSortString(header: TableHeader): string {
    if (header.ordering === TableOrdering.none) {
      return '';
    }

    const order = header.ordering === TableOrdering.ascending ? 'ASC' : 'DESC';
    return `${header.field}:${order}`;
  }

  private updateOrderPrecedence(): void {
    this.orderPrecedence = [];
    let fields = this.headerConfig.requestData.sorts.split(',').filter((field) => field !== '');

    for (let i = 0; i < fields.length; i++) {
      const fieldName = fields[i].split(':')[0];
      this.orderPrecedence.push({ fieldName: fieldName, position: i + 1 });
    }
  }
}
