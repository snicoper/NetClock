import { OrderType } from '../../../models';

export interface HeaderField {
  field: string;
  text: string;
  sortable: boolean;
  orderType: OrderType;
}
