import { LogicalOperator } from './types/logical-operator';
import { RelationalOperator } from './types/relational-operator';

export class ApiResultItemFilter {
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
