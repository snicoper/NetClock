import { TableOrdering } from './table-ordering.enum';

export interface TableHeader {
  field: string;
  text: string;
  sortable: boolean;
  ordering: TableOrdering;
}
