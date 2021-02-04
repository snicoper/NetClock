import { OrderTypes } from './types/order-type';

export class ApiResultItemOrderBy {
  propertyName: string;
  order: OrderTypes;
  precedence: number;

  constructor(propertyName: string, order: OrderTypes, precedence: number) {
    this.propertyName = propertyName;
    this.order = order;
    this.precedence = precedence;
  }
}
