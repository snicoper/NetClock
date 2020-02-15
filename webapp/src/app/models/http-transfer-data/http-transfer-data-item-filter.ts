import { LogicalOperator } from './logical-operator';
import { RelationalOperator } from './relational-operator';

export class HttpTransferDataItemFilter {
  propertyName: string;
  relationalOperator: RelationalOperator;
  value: string;
  logicalOperator: LogicalOperator;

  constructor(propertyName: string, operator: RelationalOperator, value: string, logicalOperator = LogicalOperator.none) {
    this.propertyName = propertyName;
    this.relationalOperator = operator;
    this.value = value;
    this.logicalOperator = logicalOperator;
  }
}
