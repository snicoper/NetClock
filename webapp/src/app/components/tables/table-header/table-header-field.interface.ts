import { OrderTypes } from '../../../core/models';

export interface ITableHeaderField {
  field: string;
  text: string;
  sortable: boolean;
  orderType: OrderTypes;
}
