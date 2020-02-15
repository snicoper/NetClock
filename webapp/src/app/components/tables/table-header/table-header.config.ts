import { OrderType } from '../../../models';
import { TableHeader } from './table-header.interface';

export class TableHeaderConfig {
  headers: TableHeader[] = [];

  addField(field: string, text: string, sortable = false, ordering = OrderType.none): TableHeaderConfig {
    this.headers.push({ field, text, sortable, orderType: ordering });

    return this;
  }
}
