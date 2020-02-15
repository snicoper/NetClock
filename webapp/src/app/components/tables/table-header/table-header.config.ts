import { HttpTransferData, OrderType } from '../../../models';
import { TableHeader } from './table-header.interface';

export class TableHeaderConfig<T> {
  transferData: HttpTransferData<T>;
  headers: TableHeader[] = [];

  constructor() {
    this.headers = [];
  }

  addField(field: string, text: string, sortable = false, ordering = OrderType.none): TableHeaderConfig<T> {
    this.headers.push({ field, text, sortable, ordering });

    return this;
  }
}
