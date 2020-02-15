import { OrderType } from '../../../models';
import { HeaderField } from './header-field.interface';

export class TableHeaderConfig {
  headers: HeaderField[] = [];

  add(field: string, text: string, sortable = false, orderType = OrderType.none): TableHeaderConfig {
    this.headers.push({ field, text, sortable, orderType });

    return this;
  }

  addField(field: HeaderField): TableHeaderConfig {
    return this.add(field.field, field.text, field.sortable, field.orderType);
  }
}
