import { LogicalOperator } from './logical-operator';
import { RelationalOperator } from './relational-operator';

export class RequestItemFilter {
  propertyName: string;
  relationalOperator: RelationalOperator;
  value: string;
  logicalOperator: LogicalOperator;

  constructor(propertyName: string, operator: RelationalOperator, value: string, concat = LogicalOperator.none) {
    this.propertyName = propertyName;
    this.relationalOperator = operator;
    this.value = value;
    this.logicalOperator = concat;
  }
}
