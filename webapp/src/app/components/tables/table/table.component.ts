import { Component, Input } from '@angular/core';

import { HttpTransferData } from '../../../models';

@Component({
  selector: 'nc-table',
  templateUrl: './table.component.html'
})
export class TableComponent<T> {
  @Input() transferData: HttpTransferData<T>;
  @Input() tableResponsive = true;
  @Input() loading = false;
  @Input() tableCss = 'table-hover';
}
