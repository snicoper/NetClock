import { RequestData } from '../../../models';
import { TableHeader } from './table-header.interface';
import { TableOrdering } from './table-ordering.enum';

export class TableHeaderConfig<T> {
  requestData: RequestData<T>;
  headers = Array<TableHeader>();

  constructor() {
    this.headers = [];
  }

  addField(field: string, text: string, sortable = false, ordering = TableOrdering.none): TableHeaderConfig<T> {
    this.headers.push({ field: field, text: text, sortable: sortable, ordering: ordering });

    return this;
  }
}
