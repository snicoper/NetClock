import { OrderType } from '../../../../models';
import { TableHeaderFilterType } from './table-header-filter-type.enum';

export interface ITableHeaderField {
  field: string;
  text: string;
  sortable: boolean;
  orderType: OrderType;
  filterable: boolean;
  filterType: TableHeaderFilterType;
}
