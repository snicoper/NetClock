import { OrderType } from '../../../models';

export interface TableHeader {
  field: string;
  text: string;
  sortable: boolean;
  orderType: OrderType;
}
