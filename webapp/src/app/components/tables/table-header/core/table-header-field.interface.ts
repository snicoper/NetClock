import { OrderType } from '../../../../models';

export interface ITableHeaderField {
  field: string;
  text: string;
  sortable: boolean;
  orderType: OrderType;
}
