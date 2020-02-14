import { LogicalOperator } from './logical-operator';
import { RelationalOperator } from './relational-operator';

export class RequestItemFilter {
  propertyName: string;
  operator: RelationalOperator;
  value: string;
  concat: LogicalOperator;

  constructor(propertyName: string, operator: RelationalOperator, value: string, concat = LogicalOperator.none) {
    this.propertyName = propertyName;
    this.operator = operator;
    this.value = value;
    this.concat = concat;
  }
}
