import { Component, EventEmitter, Input, Output } from '@angular/core';

import { HttpTransferData } from '../../../models';

@Component({
  selector: 'nc-table',
  templateUrl: './table.component.html'
})
export class TableComponent {
  @Input() tableResponsive = true;
  @Input() loading = false;
  @Input() transferData: HttpTransferData<any>;
}
