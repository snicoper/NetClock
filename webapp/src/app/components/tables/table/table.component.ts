import { Component, EventEmitter, Input, Output } from '@angular/core';

import { HttpTransferData } from '../../../models';
import { TableHeaderConfig } from '../table-header/core';

@Component({
  selector: 'nc-table',
  templateUrl: './table.component.html'
})
export class TableComponent<T> {
  @Input() tableResponsive = true;
  @Input() loading = false;
  @Input() transferData: HttpTransferData<T>;
  @Input() tableHeaderConfig: TableHeaderConfig;

  @Output() clickOrdering = new EventEmitter<void>();

  onReloadData(): void {
    this.clickOrdering.emit();
  }
}
