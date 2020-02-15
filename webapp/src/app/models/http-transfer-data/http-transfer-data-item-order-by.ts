import { OrderType } from './order-type';

export class HttpTransferDataItemOrderBy {
  propertyName: string;
  order: OrderType;
  precedence: number;

  constructor(propertyName: string, order: OrderType, precedence: number) {
    this.propertyName = propertyName;
    this.order = order;
    this.precedence = precedence;
  }
}
