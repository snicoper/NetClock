import { HttpTransferData } from '../../../models';
import { TableHeader } from './table-header.interface';
import { TableOrdering } from './table-ordering.enum';

export class TableHeaderConfig<T> {
  transferData: HttpTransferData<T>;
  headers: TableHeader[] = [];

  constructor() {
    this.headers = [];
  }

  addField(field: string, text: string, sortable = false, ordering = TableOrdering.none): TableHeaderConfig<T> {
    this.headers.push({ field, text, sortable, ordering });

    return this;
  }
}
