import { Component, Input } from '@angular/core';

import { ApiResult } from '../../../models';

@Component({
  selector: 'nc-table',
  templateUrl: './table.component.html'
})
export class TableComponent<T> {
  @Input() apiResult: ApiResult<T>;
  @Input() tableResponsive = true;
  @Input() loading = false;
  @Input() tableCss = 'table-hover';
}
