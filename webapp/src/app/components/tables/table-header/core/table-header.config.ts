import { ITableHeaderField } from './table-header-field.interface';

export class TableHeaderConfig {
  headers: ITableHeaderField[] = [];

  addField(field: ITableHeaderField): TableHeaderConfig {
    this.headers.push(field);

    return this;
  }

  addRange(fields: ITableHeaderField[]): TableHeaderConfig {
    fields.forEach((field) => this.headers.push(field));

    return this;
  }
}
