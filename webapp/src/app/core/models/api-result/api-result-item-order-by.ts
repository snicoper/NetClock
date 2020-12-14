import { OrderType } from './types/order-type';

export class ApiResultItemOrderBy {
  propertyName: string;
  order: OrderType;
  precedence: number;

  constructor(propertyName: string, order: OrderType, precedence: number) {
    this.propertyName = propertyName;
    this.order = order;
    this.precedence = precedence;
  }
}
