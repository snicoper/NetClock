import { LogicalOperators } from './types/logical-operator';
import { RelationalOperators } from './types/relational-operator';

export class ApiResultItemFilter {
  propertyName: string;
  relationalOperator: RelationalOperators;
  value: string;
  logicalOperator: LogicalOperators;

  constructor(
    propertyName: string,
    operator: RelationalOperators,
    value: string,
    logicalOperator = LogicalOperators.none
  ) {
    this.propertyName = propertyName;
    this.relationalOperator = operator;
    this.value = value;
    this.logicalOperator = logicalOperator;
  }
}
